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


    [SerializeField] bool showAim;
    [SerializeField] float startShowing, stopShowing;
    [SerializeField] float fuerzaSniper = 30;
    [SerializeField] int cantidadBalasEscopeta = 30;
    GameObject[] balasEscopeta;

    string type;

    // Define las opciones como un enum
    public enum FeatureOption
    {
        disparoNormal,
        disparoTriple,
        sniper,
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
            case FeatureOption.sniper:
                type = "sniper";
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
        balasEscopeta = new GameObject[cantidadBalasEscopeta];
    }


    private void FixedUpdate()
    {
        if (disparando)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
                if(showAim)
                {
                    if (timer * 100 / shootCooldown > stopShowing && timer * 100 / shootCooldown < startShowing) // intervalo para mostrar el aim
                    {
                        shoot.mostrandoRayo = true;
                    }
                    else
                    {
                        shoot.mostrandoRayo = false;
                    }
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
        else if(type == "sniper")
        {
            shoot.OnShoot(shoot.AutoShootDirection(), bp.RequerirBala(),fuerzaSniper);
        }
        else if(type == "escopeta")
        {
            for (int i = 0; i < balasEscopeta.Length; i++)
            {
                balasEscopeta[i] = bp.RequerirBala();
            }
            shoot.OnShotGunShot(shoot.AutoShootDirection(), balasEscopeta);
        }
        
    }

}
