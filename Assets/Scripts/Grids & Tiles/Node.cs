using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster
{
    [System.Serializable]
    public class Node
    {

        public int x;
        public int y;

        public float hCost;
        public float gCost;

        public float fCost { get { return gCost + hCost; } }

        public Node parentNode;
        public bool isWalkable = true;
        public bool hasOccupant = false;
        public float movementPenalty;

        public GameObject worldObject;

        public NodeType nodeType;
        public enum NodeType
        {
            ground,
            wall
        }

    }
}

