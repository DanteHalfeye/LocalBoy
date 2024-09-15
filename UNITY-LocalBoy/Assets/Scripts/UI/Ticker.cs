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

    // Lista temporal para manejar los mensajes
    List<string> remainingMessages = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        pixelsPerSeconds = width / itemDuration;

        // Inicializar la lista de mensajes con todos los items de fillerItems
        ResetRemainingMessages();

        AddTickerItem(GetNextMessage());
    }

    // Update is called once per frame
    void Update()
    {
        if (currentItem.GetXPosition <= -currentItem.GetWidth)
        {
            AddTickerItem(GetNextMessage());
        }
    }

    // Método para agregar un nuevo item al ticker
    void AddTickerItem(string message)
    {
        currentItem = Instantiate(tickerItemPrefab, transform);
        currentItem.Initialized(width, pixelsPerSeconds, message);
    }

    // Método para obtener el siguiente mensaje a mostrar
    string GetNextMessage()
    {
        // Si ya mostramos todos los mensajes, reiniciamos la lista
        if (remainingMessages.Count == 0)
        {
            ResetRemainingMessages();
        }

        // Seleccionar un mensaje aleatorio de los mensajes restantes
        int randomIndex = Random.Range(0, remainingMessages.Count);
        string nextMessage = remainingMessages[randomIndex];

        // Eliminar el mensaje de la lista para no repetirlo
        remainingMessages.RemoveAt(randomIndex);

        return nextMessage;
    }

    // Método para reiniciar la lista de mensajes no mostrados
    void ResetRemainingMessages()
    {
        remainingMessages = new List<string>(fillerItems);
    }
}
