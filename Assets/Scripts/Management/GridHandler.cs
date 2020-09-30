using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster
{
    public class GridHandler : MonoBehaviour
    {

        public int mapsize;
        public Node[,] grid;
        public int gridSize;
        public int alignIntX;
        public int alignIntY;
        public int startNodeX;
        public int startNodeY;
        public int endNodeX;
        public int endNodeY;
        public GameObject[] tiles;
        public GameObject[] players;
        public GameObject tilePrefab;
        public bool isProcGen;
        public bool isGuideGen;
        public bool hasGenerated;
        public Vector2 initOffset;

        public void BeginGeneration()
        {
            alignIntX = 1;
            alignIntY = 1;
            if (isProcGen == false && isGuideGen == false) {
                CreateGrid();
            } else if (isProcGen == true && isGuideGen == false) {
                //Generate the map.
            } else if (isGuideGen == true) {
                GuidedGeneration(alignIntX, alignIntY);
            }
        }

        void CreateGrid()
        {
            tiles = GameObject.FindGameObjectsWithTag("Tile");
            players = GameObject.FindGameObjectsWithTag("Actor");
            grid = new Node[tiles.Length * 10 + Mathf.RoundToInt(initOffset.x * 0.145f), tiles.Length * 10 + Mathf.RoundToInt(initOffset.y * 0.17f)];

            foreach (GameObject g in tiles)
            {
                g.GetComponent<GridLayer>().sendNode();
                g.transform.SetParent(this.gameObject.transform);
            }
            foreach (GameObject h in players)
            {
                h.GetComponent<Controller>().Alignment();
            }

            hasGenerated = true;
            MagicHandler.instance.cycleCountX = 1;
            MagicHandler.instance.cycleCountY = 1;

            MagicHandler.instance.GeneratePixelGrids();
        }

        public Node GetNode(int x, int y)
        {
            Node retVal = null;

            retVal = grid[x, y];

            return retVal;
        }

        public void GuidedGeneration (int alX, int alY) {
            int helperX = 1 + ((alX - 1) * 10);
            int helperY = 1 + ((alY - 1) * 10);

            //This is going to build the grid based off of a pre-made map.
            //For all the points within the pre-defined map size:
            for (int i = helperX; i < helperX + 10; i++) {
                for (int e = helperY; e < helperY + 10; e++) {
                    float offX = initOffset.x;
                    float offY = initOffset.y;
                    if (i % 2 == 1) {
                        RaycastHit2D hit = Physics2D.Raycast(new Vector2 (i * 0.145f + offX, e * 0.17f + 0.085f + offY), -Vector3.forward);
                        if (hit.collider == null) {
                            GameObject freshNode = Instantiate(tilePrefab, new Vector2 (i * 0.145f + offX, e * 0.17f + 0.085f + offY), Quaternion.identity);
                        }
                    } else {
                        RaycastHit2D hit = Physics2D.Raycast(new Vector2 (i * 0.145f + offX, e * 0.17f + offY), -Vector3.forward);
                        if (hit.collider == null) {
                            GameObject freshNode = Instantiate(tilePrefab, new Vector2 (i * 0.145f + offX, e * 0.17f + offY), Quaternion.identity);
                        }
                    }
                }
            }
            
            Invoke("StepUp", 0.01f);
        }

        void StepUp () {
            if (alignIntY <= mapsize / 10) {
                if (alignIntX <= mapsize / 10) {
                    alignIntX++;
                } else if (alignIntY <= mapsize / 10) {
                    alignIntX = 1;
                    alignIntY++;
                }

                GuidedGeneration(alignIntX, alignIntY);
            } else if (alignIntY > mapsize / 10) {
                CreateGrid();
            }
        }



        public static GridHandler instance;
        public static GridHandler GetInstance()
        {
            return instance;
        }

        void Awake()
        {
            instance = this;
        }
    }
}

