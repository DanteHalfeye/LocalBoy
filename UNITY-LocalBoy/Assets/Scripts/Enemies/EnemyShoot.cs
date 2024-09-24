using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
     public float shootCooldown;
     float timer;

    bool disparando = true;

    BulletPool bp;
    Shoot shoot;

    private void Awake()
    {
        bp = GetComponent<BulletPool>();
        shoot = GetComponent<Shoot>();
    }


    private void FixedUpdate()
    {
        if (disparando)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = shootCooldown;
                Shoot();
            }
        }
    }

    public void Shoot()
    {
        shoot.OnShoot(shoot.AutoShootDirection(), bp.RequerirBala());
    }

}
