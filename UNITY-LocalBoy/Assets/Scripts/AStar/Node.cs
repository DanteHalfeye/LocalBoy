using System;
using UnityEngine;

public class Node : IComparable<Node>
{
    public Vector2Int gridPosition;
    public int gCost = 0; //distancia desde nodo inicio
    public int hCost = 0; //distancia desde nodo final
    public Node parentNode;


    public Node(Vector2Int gridPosition)
    {
        this.gridPosition = gridPosition;
        
        parentNode = null;
    }
    public int FCost
    {
        get
        {
            return gCost + hCost;
        }
    }





    public int CompareTo(Node nodeToCompare)
    {
        //comparar será <0 si la instancia Fcost es menor que nodeToCompare.Fcost
        //comparar será >0 si la instancia Fcost es mayor que nodeToCompare.Fcost
        //comparar será ==0 si los valores son los mismos

        int compare = FCost.CompareTo(nodeToCompare.FCost);

        if (compare != 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }

        return compare;
    }

}
