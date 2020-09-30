using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridMaster;

namespace Pathfinding
{

    public class Pathfinder
    {

        GridHandler gridBase;
        public Node startPosition;
        public Node endPosition;
        bool changed;
        GameObject[] tiles;
        public GameObject pather;
        int width;

        public List<Node> FindPath()
        {
            gridBase = GridHandler.GetInstance();

            if (startPosition.hasOccupant == true)
            {
                startPosition.hasOccupant = false;
            }

            if (pather.tag == "Actor") {
                width = pather.GetComponent<CharacterData>().width;
            } else {
                width = 2;
            }

            return FindPathActual(startPosition, endPosition);

        }

        private List<Node> FindPathActual(Node start, Node target)
        {
            List<Node> foundPath = new List<Node>();

            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();

            openSet.Add(start);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];

                if (currentNode.Equals(target))
                {
                    foundPath = RetracePath(start, currentNode);
                    break;
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                for (int i = 0; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost ||
                        (openSet[i].fCost == currentNode.fCost &&
                        openSet[i].hCost < currentNode.hCost))
                    {
                        if (!currentNode.Equals(openSet[i]))
                        {
                            currentNode = openSet[i];
                        }
                    }
                }

                //				openSet.Remove (currentNode);
                //				closedSet.Add (currentNode);
                //
                //				if (currentNode.Equals (target)) {
                //					foundPath = RetracePath (start, currentNode);
                //					break;
                //				}

                foreach (Node neighbor in GetNeighbors(currentNode))
                {
                    if (!closedSet.Contains(neighbor))
                    {
                        float newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor) + neighbor.movementPenalty;

                        if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                        {
                            neighbor.gCost = newMovementCostToNeighbor;
                            neighbor.hCost = GetDistance(neighbor, target);
                            neighbor.parentNode = currentNode;

                            if (!openSet.Contains(neighbor))
                            {
                                openSet.Add(neighbor);
                            }
                        }
                    }
                }
            }

            return foundPath;
        }

        private List<Node> RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parentNode;
            }
            if (currentNode == startNode && Manager.instance.inGameMap == false) {
                path.Add(startNode);
            }
            if (endNode == startNode) {
                return null;
            } else {
                path.Reverse();

                return path;
            }
        }

        private List<Node> GetNeighbors(Node node)
        {
            List<Node> retList = new List<Node>();
            if (Manager.instance.inGameMap == true) {
                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0)
                        {
                            //Nothing
                        }
                        else if (x == -1 && y == 1)
                        {
                            //Nothing at all.
                        }
                        else if (x == 1 && y == -1)
                        {
                            //Absolutely nothing.
                        }
                        else
                        {
                            Node searchPos = gridBase.GetNode(node.x + x, node.y + y);

                            Node newNode = GetNeighborNode(searchPos, node);

                            if (newNode != null && newNode.worldObject.transform.childCount == 0 && newNode.worldObject.GetComponent<CoordinateHolder>().isWall == false)
                            {
                                if (width - 1 <= 0) {
                                    retList.Add(newNode);
                                } else if (width - 1 == 1) {
                                    int cannotPathInt = 0;

                                    for (int c = -1; c <= 1; c++) {
                                        for (int v = -1; v <= 1; v++) {
                                            if (c == 0 && v == 0) {
                                                //Nothing
                                            } else if (c == -1 && v == 1) {
                                                //Nothing at all.
                                            } else if (c == 1 && v == -1) {
                                                //Absolutely nothing.
                                            } else {
                                                Node newSearchPos = gridBase.GetNode(newNode.x + c, newNode.y + v);
                                                Node doubleNewNode = GetNeighborNode(newSearchPos, newNode);

                                                if (doubleNewNode != null) {
                                                    if (doubleNewNode.worldObject.transform.childCount == 0 && doubleNewNode.worldObject.GetComponent<CoordinateHolder>().isWall == false) {
                                                        //Nothing
                                                    } else if (doubleNewNode.worldObject.transform.childCount != 0) {
                                                        if (doubleNewNode.worldObject.GetComponentInChildren<CharacterData>().isPlayer == true) {
                                                            //Nothing at all.
                                                        } else if (doubleNewNode == startPosition) {
                                                            //Absolutely nothing.
                                                        } else {
                                                            cannotPathInt++;
                                                        }
                                                    } else if (doubleNewNode.worldObject.GetComponent<CoordinateHolder>().isWall == true) {
                                                        cannotPathInt++;
                                                    }
                                                } else {
                                                    cannotPathInt++;
                                                }
                                            }
                                        }
                                    }
                                    if (cannotPathInt <= 3) {
                                        retList.Add(newNode);
                                    }

                                } else if (width - 1 > 1) {
                                    bool cannotPath = false;

                                    for (int c = -1 * (width - 2); c <= width - 2; c++) {
                                        for (int v = -1 * (width - 2); v <= width - 2; v++) {
                                            if (c == 0 && v == 0) {
                                                //Nothing
                                            } else if (c == -1 && v == 1) {
                                                //Nothing at all.
                                            } else if (c == 1 && v == -1) {
                                                //Absolutely nothing.
                                            } else {
                                                Node newSearchPos = gridBase.GetNode(newNode.x + c, newNode.y + v);
                                                Node doubleNewNode = GetNeighborNode(newSearchPos, newNode);

                                                if (doubleNewNode != null) {
                                                    if (doubleNewNode.worldObject.transform.childCount == 0 && doubleNewNode.worldObject.GetComponent<CoordinateHolder>().isWall == false) {
                                                        //Nothing
                                                    } else if (doubleNewNode.worldObject.transform.childCount != 0) {
                                                        if (doubleNewNode.worldObject.GetComponentInChildren<CharacterData>().isPlayer == true) {
                                                            //Nothing at all.
                                                        } else if (doubleNewNode == startPosition) {
                                                            //Absolutely nothing.
                                                        } else {
                                                            cannotPath = true;
                                                        }
                                                    } else if (doubleNewNode.worldObject.GetComponent<CoordinateHolder>().isWall == true) {
                                                        cannotPath = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (cannotPath == false) {
                                        retList.Add(newNode);
                                    }
                                }
                            } else if (newNode != null && startPosition.worldObject.GetComponentInChildren<CharacterData>().isPlayer == false && newNode.worldObject.GetComponent<CoordinateHolder>().isWall == false){
                                retList.Add(newNode);
                            }
                        }
                    }
                }
            } else {

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        if (x == 0 && y == 0)
                        {
                            //Nothing
                        }
                        else if (x == -1 && y == 1)
                        {
                            //Nothing at all.
                        }
                        else if (x == 1 && y == -1)
                        {
                            //Absolutely nothing.
                        }
                        else
                        {
                            Node searchPos = MapGeneration.instance.GetNode(node.x + x, node.y + y);

                            Node newNode = GetNeighborNode(searchPos, node);

                            if (newNode != null)
                            {
                                retList.Add(newNode);
                            }
                        }
                    }
                }
            }

            return retList;
        }

        private Node GetNeighborNode(Node adjPos, Node currentNodePos)
        {
            Node retVal = null;

            if (adjPos != null && adjPos.isWalkable)
            {
                retVal = adjPos;
            }


            return retVal;
        }


        private float GetDistance(Node posA, Node posB)
        {
            if (posA != null && posB != null)
            {
                float xa = posA.x;
                float ya = posA.y;
                float xb = posB.x;
                float yb = posB.y;
                //float xa = posA.worldObject.GetComponent<CoordinateHolder>().corX;
                //float ya = posA.worldObject.GetComponent<CoordinateHolder>().corY;
                //float xb = posB.worldObject.GetComponent<CoordinateHolder>().corX;
                //float yb = posB.worldObject.GetComponent<CoordinateHolder>().corY;
                //			float xa = posA.worldObject.transform.position.x;
                //			float ya = posA.worldObject.transform.position.y;
                //			float xb = posB.worldObject.transform.position.x;
                //			float yb = posB.worldObject.transform.position.y;

                //			float dist = Mathf.Max (Mathf.Abs (yb - ya), Mathf.Abs (Mathf.Ceil (yb / -1) + xb - Mathf.Ceil (ya / -2) - xa), Mathf.Abs (-yb - Mathf.Ceil (yb / -2) - xb + ya + Mathf.Ceil (ya / -2) + xa));
                //
                //			return dist;


                float distX = xb - xa;
                float distY = yb - ya;

                return Mathf.Sqrt(distX * distX + distY * distY);
            } else
            {
                float inf = 10000f;
                return inf;
            }

            //			float dist = Vector2.Distance (posA.worldObject.transform.position, posB.worldObject.transform.position);
            //			return dist;
        }
    }
}

