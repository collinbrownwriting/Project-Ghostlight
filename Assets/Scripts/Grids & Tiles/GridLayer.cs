using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster
{

    public class GridLayer : MonoBehaviour
    {

        GridHandler gridBase;
        int x;
        int y;
        float z;

        public void sendNode()
        {
            gridBase = GridHandler.GetInstance();
            Node node = new Node();

            x = Mathf.RoundToInt((this.gameObject.transform.position.x - GridHandler.instance.initOffset.x) / 0.145f);
            if (x % 2 == 1)
            {
                z = x;
                y = Mathf.RoundToInt((this.gameObject.transform.position.y - GridHandler.instance.initOffset.y + 0.085f) / 0.17f + ((z / 2) - 0.5f));
            }
            else
            {
                z = x;
                y = Mathf.RoundToInt((this.gameObject.transform.position.y - GridHandler.instance.initOffset.y) / 0.17f + (z / 2));
            }

            this.GetComponent<CoordinateHolder>().corX = x;
            this.GetComponent<CoordinateHolder>().corY = y;

            node.x = x;
            node.y = y;
            node.worldObject = this.gameObject;
            gridBase.grid[node.x, node.y] = node;
            this.gameObject.name = "Tile" + node.x + "/" + node.y;

        }

    }
}

