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

    GameObject[] balas = new GameObject[3];

    string type;

    // Define las opciones como un enum
    public enum FeatureOption
    {
        disparoNormal,
        disparoTriple,
        escopeta
    }

    // Muestra el enum como un dropdown en el inspector
    [SerializeField] private FeatureOption selectedOption;

    void Update()
    {
        // Cambia el comportamiento según la opción seleccionada
        switch (selectedOption)
        {
            case FeatureOption.disparoNormal:
                type = "disparoNormal";
                break;
            case FeatureOption.disparoTriple:
                type = "disparoTriple";
                break;
            case FeatureOption.escopeta:
                type = "escopeta";
                break;
        }

    }

    private void Awake()
    {
        bp = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<BulletPool>();
        shoot = GetComponent<Shoot>();
    }


    private void FixedUpdate()
    {
        if (disparando)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
                if(timer * 100 /shootCooldown > 15 && timer * 100 / shootCooldown < 50) // entre el 50% y el 15% del tiempo de cooldown se muestra
                {
                    shoot.mostrandoRayo = true;
                }
                else
                {
                    shoot.mostrandoRayo = false;
                }
            }
            else
            {
                timer = shootCooldown;
                Shoot(type);
            }
        }
    }

    public void Shoot(string shootType)
    {
        if (type == "disparoNormal")
        {
            shoot.OnShoot(shoot.AutoShootDirection(), bp.RequerirBala());
        }
        else if(type == "disparoTriple")
        {
            for (int i = 0; i < balas.Length; i++)
            {
                balas[i] = bp.RequerirBala();
            }
            shoot.OnTripleShoot(shoot.AutoShootDirection(), balas);
        }
        
    }

}
