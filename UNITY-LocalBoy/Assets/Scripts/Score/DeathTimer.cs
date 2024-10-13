using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    
    public static DeathTimer instance;

    private bool isTiming;

    [SerializeField] private float maxTimer;

    private bool isEpic;

    private float counter;

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

   

    public void MuerteEnem()
    {
        Debug.Log("AAAA");
        if (!isTiming)
        {
            StartCoroutine(DeathCounter());
        }

        else
        {
            isEpic = true;
            counter = maxTimer;

        }
    }

    private IEnumerator DeathCounter()
    {
        counter = maxTimer;
        isTiming = true;
        while (counter > 0)
        {
            counter -= Time.deltaTime;

            yield return null;
        }

        isTiming = false;
        
        if(isEpic)
        {
            Counter.instance.AddScore(15,true);
        }

        else
        {
            Counter.instance.AddScore(10, false);
        }
        isEpic=false;

    }


}
