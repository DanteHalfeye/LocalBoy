using FMOD;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public static class AStar
{
    //basicamente acá vamos a hacer un camino para la habitacion, empezando por el nodo de inicio y finalizando pues en el nodo final
    //devuelve null si no se encuentra ningun camino

    public static Stack<Vector3> BuildPath(Room room, Vector3Int startGridPosition, Vector3Int endGridPosition)
    {
        //ajustamos posiciones en lower bounds
        startGridPosition -= (Vector3Int)room.templateLowerBounds;
        endGridPosition -= (Vector3Int)room.templateLowerBounds;

        //crear la open list y closed hashset

        List<Node> openNodeList = new List<Node>();
        HashSet<Node> closedNodeHashSet = new HashSet<Node>();

        //crea las gridnodes para el path finding

        GridNodes gridNodes = new GridNodes(room.templateUpperBounds.x - room.templateLowerBounds.x + 1, room.templateUpperBounds.y - room.templateLowerBounds.y + 1);

        Node startNode = gridNodes.GetGridNode(startGridPosition.x, startGridPosition.y);
        Node targetNode = gridNodes.GetGridNode(endGridPosition.x, endGridPosition.y);

        Node endPathNode = FindShortestPath(startNode, targetNode, gridNodes, openNodeList, closedNodeHashSet, room.instantiatedRoom);

        if (endPathNode != null)
        {
            return CreatePathStack(endPathNode, room);
        }

        return null;
    }

    //encontrar el camino mas corto - devuelve el nodo final si se encontró un camino, de resto, null.

    private static Node FindShortestPath(Node startNode, Node targetNode, GridNodes gridNodes, List<Node> openNodeList, HashSet<Node> closedNodeHashset,
        InstantiatedRoom instantiatedRoom)
    {
        //añadir nodo para abrir lista
        openNodeList.Add(startNode);

        //loop a traves de la open node list hasta que esté vacía
        while (openNodeList.Count > 0)
        {

            //clasificando
            openNodeList.Sort();

            //current node = el nodo en la open list que tenga en menor costo
            Node currentNode = openNodeList[0];
            openNodeList.RemoveAt(0);

            //añador current node a la lista cerrada
            closedNodeHashset.Add(currentNode);

            EvaluateCurrentNodeNeighbours(currentNode, targetNode, gridNodes, openNodeList, closedNodeHashset, instantiatedRoom);
        }

        return null;
    }


    //creamos un Stack<Vector3> para conetener el camino de movimiento
    private static Stack<Vector3> CreatePathStack(Node tagetNode, Room room)
    {
        Stack<Vector3> movementPathStack = new Stack<Vector3>();

        Node nextNode = tagetNode;

        //agarrar punto medio de una celda
        Vector3 cellMidPoint = room.instantiatedRoom.grid.cellSize * 0.5f;
        cellMidPoint.z = 0f;

        while(nextNode != null)
        {
            //convertir la posicion del grid a posicion de mundo
            Vector3 worldPosition = room.instantiatedRoom.grid.CellToWorld(new Vector3Int(nextNode.gridPosition.x + room.templateLowerBounds.x,
                nextNode.gridPosition.y + room.templateLowerBounds.y, 0));

            //poner la posicion del mundoen lamitad de la celda
            worldPosition += cellMidPoint;

            movementPathStack.Push(worldPosition);

            nextNode = nextNode.parentNode;
        }

        return movementPathStack;

    }

    //evaluamos los nodos vecinos

    private static void EvaluateCurrentNodeNeighbours(Node currentNode, Node targetNode, GridNodes gridNodes, List<Node> openNodeList, HashSet<Node>
        closedNodeHashSet, InstantiatedRoom instantiatedRoom)
    {
        Vector2Int currentNodeGridPosition = currentNode.gridPosition;

        Node validNeighbourNode;

        //loop en todas las direcciones

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                if (i == 0 && j == 0)
                    continue;

                validNeighbourNode = GetValidNodeNeighbour(currentNodeGridPosition.x + i, currentNodeGridPosition.y + j, gridNodes, closedNodeHashSet, instantiatedRoom);

                if (validNeighbourNode != null)
                {
                    //calcula  nuvo gCost para vecino
                    int newCostToNeighbour;

                    newCostToNeighbour = currentNode.gCost + GetDistance(currentNode, validNeighbourNode);

                    bool isValidNeighbourNodeInOpenList = openNodeList.Contains(validNeighbourNode);

                    if (newCostToNeighbour < validNeighbourNode.gCost || !isValidNeighbourNodeInOpenList)
                    {
                        validNeighbourNode.gCost = newCostToNeighbour;
                        validNeighbourNode.hCost = GetDistance(validNeighbourNode, targetNode);
                        validNeighbourNode.parentNode = currentNode;

                        if (!isValidNeighbourNodeInOpenList)
                        {
                            openNodeList.Add(validNeighbourNode);
                        }
                    }
                }

            }
        }
    }


    //  devuelve la distancia int entre nodeA y nodeB

    private static int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridPosition.x - nodeB.gridPosition.x);
        int dstY = Mathf.Abs(nodeA.gridPosition.y - nodeB.gridPosition.y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY); //se usa 10 en vez de 1, el 14 es por un pitagorazo SQRT(10*10 + 10*10) pa no usar floats
        return 14 * dstX + 10 * (dstY - dstX);
    }

    //evalua el nodo vecino en neighbourNodeXPosition, neighbourNodeYPosition, usando 
    //los gridNodes especificos, closedNodeHashSet, y instantiated room. devuelvve null si el nodo no es valido

    private static Node GetValidNodeNeighbour(int neighbourNodeXPosition, int neighbourNodeYPosition, GridNodes gridNodes, HashSet<Node> closedNodeHashSet,
        InstantiatedRoom instantiatedRoom)
    {
        //si la posicion del nodo vecnino esta mas alla del grid, devuelve null

        if( neighbourNodeXPosition >= instantiatedRoom.room.templateUpperBounds.x - instantiatedRoom.room.templateLowerBounds.x || neighbourNodeXPosition < 0
            || neighbourNodeYPosition >= instantiatedRoom.room.templateUpperBounds.y - instantiatedRoom.room.templateLowerBounds.y || neighbourNodeYPosition <0)
        {
            return null;
        }
        //obtener nodo vecino

        Node neighbourNode = gridNodes.GetGridNode(neighbourNodeXPosition, neighbourNodeYPosition);

        //si el nodo está en la closed list, lo salta
        if (closedNodeHashSet.Contains(neighbourNode))
        {
            return null;
        }
        else
        {
            return neighbourNode;
        }
    }


}

