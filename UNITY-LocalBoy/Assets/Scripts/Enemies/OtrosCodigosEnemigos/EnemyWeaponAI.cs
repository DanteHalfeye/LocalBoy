using UnityEngine;

[RequireComponent(typeof(Enemy))]
[DisallowMultipleComponent]

public class EnemyWeaponAI : MonoBehaviour
{
    #region Tooltip
    [Tooltip("Select the layers that the enemy bullets will hit")]
    #endregion
    [SerializeField] private LayerMask layerMask;

    #region Tooltip
    [Tooltip("Populate this with the WeaponShootPosition child gameobject transform")]
    #endregion Tooltip
    [SerializeField] public Transform weaponShootPosition;
    private Enemy enemy;
    private EnemyDetailsSO enemyDetails;
    private float firingIntervalTimer;
    private float firingDurationTimer;

    private void Awake()
    {
        //load components
        enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        enemyDetails = enemy.enemyDetails;

        firingIntervalTimer = WeaponShootInterval();
        firingDurationTimer = WeaponShootDuration();
    }

    private void Update()
    {
       
         //update timers
         firingIntervalTimer -= Time.deltaTime;

        //interval timer
        if(firingIntervalTimer < 0f)
        {
            if(firingDurationTimer >=0)
            {
                firingDurationTimer -= Time.deltaTime;

                FireWeapon();
            }

            else
            {
                //reset timers
                firingIntervalTimer = WeaponShootInterval();
                firingDurationTimer = WeaponShootDuration();
            }
        }
    }
    // calculate a random weapon shoot duration between the min and max values
    private float WeaponShootDuration()
    {
        //calculate a random weapon shoot duration
        return Random.Range(enemyDetails.firingDurationMin, enemyDetails.firingDurationMax);
    }

    //calculate a random weapon shoot interval btween the min and max values
    private float WeaponShootInterval()
    {
        //calculate a random weapon shoot interval
        return Random.Range(enemyDetails.firingIntervalMin, enemyDetails .firingIntervalMax);
    }


    //fire the weapon
    private void FireWeapon()
    {
        //Player distance
        Vector3 playerDirectionVector = GameManager.Instance.GetPlayer().GetPlayerPosition() - transform.position;

        //Calculate ddirection vctor of player from weapon shoot position
        Vector3 weaponDirection = (GameManager.Instance.GetPlayer().GetPlayerPosition() - weaponShootPosition.position);

        //Get weapon to player angle
        float weaponAngleDegrees = HelperUtilities.GetAngleFromVector(weaponDirection);

        //get enemy to player angle
        float enemyAngleDegrees = HelperUtilities.GetAngleFromVector(playerDirectionVector);

        //set enemy aim direction
        AimDirection enemyAimDirection = HelperUtilities.GetAimDirection(enemyAngleDegrees);

        //trigger weapon aim event
        enemy.aimWeaponEvent.CallAimWeaponEvent(enemyAimDirection, enemyAngleDegrees, weaponAngleDegrees, weaponDirection);

        //only fire if enemy has a weapon
        if( enemyDetails.enemyWeapon != null)
        {
            //get ammo range
            float enemyAmmoRange = enemyDetails.enemyWeapon.weaponCurrentAmmo.ammoRange; 

            //is the player in range
            if(playerDirectionVector.magnitude <= enemyAmmoRange)
            {
                //does this enemy require line of sight to the player before firing?
                if (enemyDetails.firingLineOfSightRequired && !IsPlayerInLineOfSight(weaponDirection, enemyAmmoRange)) return;

                //trigger fire weapon event
                enemy.fireWeaponEvent.CallFireWeaponEvent(true, true, enemyAimDirection, enemyAngleDegrees, weaponAngleDegrees, weaponDirection);
            }
        }
    }

    private bool IsPlayerInLineOfSight(Vector3 weaponDirection, float enemyAmmoRange)
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(weaponShootPosition.position, (Vector2)weaponDirection, enemyAmmoRange, layerMask);

        if(raycastHit2D && raycastHit2D.transform.CompareTag(Settings.playerTag))
        {
            return true;
        }
        return false;
    }


    #region Validation
#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(weaponShootPosition), weaponShootPosition);

    }

#endif
    #endregion
}
