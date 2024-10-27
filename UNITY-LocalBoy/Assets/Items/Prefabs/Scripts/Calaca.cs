using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calaca : BaseOrbital
{
    [SerializeField]
    private float attackSpeed;

    private float attackTime;


    private ShootPlayer shoot;
    private BulletPool bp;

    protected override void Awake()
    {
        base.Awake();
        shoot = GetComponent<ShootPlayer>();
        bp = GetComponent<BulletPool>();
    }

    private void Update()
    {
        transform.RotateAround(padre.position, rotationVector, speed * Time.deltaTime);

        if (!rotateAroundItself)
        {
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }

        if (attackTime <= 0)
        {
            Shoot();
            attackTime = attackSpeed;
        }
        else
        {
            attackTime -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        shoot.OnShoot(shoot.AutoShootDirection(), bp.RequerirBala());
    }
}
