using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    List<GameObject> balas = new List<GameObject>();
    public GameObject balaPrefab;
    public int cantidadDeBalas;
    void Start()
    {
        RellenarPool(cantidadDeBalas);
    }

    private void RellenarPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bala = Instantiate(balaPrefab);
            bala.SetActive(false);
            balas.Add(bala);
            bala.transform.parent = transform;
        }
    }

    public GameObject RequerirBala()
    {
        for (int i = 0; i < balas.Count; i++)
        {
            if (!balas[i].activeSelf)
            {
                balas[i].SetActive(true);
                return balas[i];
            }
        }
        RellenarPool(1);
        balas[balas.Count - 1].SetActive(true);
        return balas[balas.Count - 1];
    }

}
