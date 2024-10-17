using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetPrice : MonoBehaviour
{
    private ApplyItem_noRNG_shop item;
    [SerializeField]
    private TextMeshPro tmp;

    public void ShowPrice(int price)
    {
        tmp = GetComponent<TextMeshPro>();
        tmp.text = "$" + price.ToString();
    }
}
