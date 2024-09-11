using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticker : MonoBehaviour
{
    [SerializeField] TickerItem tickerItemPrefab;
    [Range(1f, 10f)]
    float pixelsPerSeconds, width;
    public float itemDuration = 3.0f;
    public string[] fillerItems;
    TickerItem currentItem;
    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        pixelsPerSeconds = width / itemDuration;
        AddTickerItem(fillerItems[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentItem.GetXPosition <= -currentItem.GetWidth)
        {
            AddTickerItem(fillerItems[Random.Range(0, fillerItems.Length)]);
        }
    }

    void AddTickerItem(string message)
    {
        currentItem = Instantiate(tickerItemPrefab, transform);
        currentItem.Initialized(width, pixelsPerSeconds, message);
    }
}
