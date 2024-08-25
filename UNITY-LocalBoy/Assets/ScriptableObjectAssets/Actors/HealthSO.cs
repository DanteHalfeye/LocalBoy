using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health_", menuName = "Scriptable Objects/Actor/Health")]
public class HealthSO : ScriptableObject
{
    [SerializeField] float healthMaxAmount;
    [SerializeField] float healthCurrentAmount;


    void onHealthChanged(float amount)
    {
        healthCurrentAmount += amount;
    }
}
