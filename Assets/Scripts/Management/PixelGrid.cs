using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class PixelGrid {
        public MagicPixel[,] pixelArray;
        public List<MagicPixel> pixelList = new List<MagicPixel>();
        public List<MagicPixel> activePixels = new List<MagicPixel>();
        public List<MagicPixel> pixelQueue = new List<MagicPixel>();
        public List<MagicPixel> pixelGrave = new List<MagicPixel>();
        
        
        
        public Vector3 centerVector;

        public float timeOffset;
        float lastRecordedTime;
        public int layerSize;
        public int alignIntX;
        public int alignIntY;
        public int neighAliX;
        public int neighAliY;
        public GameObject pixelPrefab;
        public bool hasLayedGrid;


        public void StartUp () {
            pixelArray = new MagicPixel[layerSize * 20, layerSize * 20];
            alignIntX = 1;
            alignIntY = 1;
            GenerateLayer(alignIntX, alignIntY);
        }


        public void GenerateLayer (int alX, int alY) {
            int helperX = 1 + ((alX - 1) * 20);
            int helperY = 1 + ((alY - 1) * 20);


            for (int x = helperX; x < helperX + 20; x++) {
                for (int y = helperY; y < helperY + 20; y++) {
                    MagicPixel pixel = new MagicPixel();

                    pixel.corX = x;
                    pixel.corY = y;
                    pixel.pType = MagicHandler.instance.naught;

                    pixel.worldObject = pixelPrefab;
                    
                    pixel.worldPosition = new Vector3 (((x * 0.01f) + MagicHandler.instance.anchorOffset.x) + (MagicHandler.instance.cycleCountX - 1) * 3.8f, ((y * 0.01f) + MagicHandler.instance.anchorOffset.y)  + (MagicHandler.instance.cycleCountY - 1) * 3.8f, 0f);
                    pixel.myGrid = this;

                    pixelArray[pixel.corX, pixel.corY] = pixel;
                    pixelList.Add(pixel);
                }
            }

            MagicHandler.instance.ObtuseWorkAround(1, this);
        }

        public void StepUp () {
            if (alignIntY < layerSize / 20) {
                if (alignIntX < layerSize / 20) {
                    alignIntX++;
                } else if (alignIntY < layerSize / 20) {
                    alignIntX = 1;
                    alignIntY++;
                }

                GenerateLayer(alignIntX, alignIntY);

            } else if (alignIntY >= layerSize / 20) {
                centerVector = new Vector3 ((Manager.instance.pixelGridDimensions * 0.005f) + MagicHandler.instance.anchorOffset.x + (MagicHandler.instance.cycleCountX - 1), (Manager.instance.pixelGridDimensions * 0.005f) + MagicHandler.instance.anchorOffset.y + (MagicHandler.instance.cycleCountY - 1), 0);

               neighAliX = 1;
               neighAliY = 1;
               MagicHandler.instance.CreateObjectAnchor(centerVector, MagicHandler.instance.cycleCountX + "/" + MagicHandler.instance.cycleCountY);
               MassGatherNeighbors(neighAliX, neighAliY);
            }
        }

        void MassGatherNeighbors (int alX, int alY) {
            int helperX = 1 + ((alX - 1) * 20);
            int helperY = 1 + ((alY - 1) * 20);


            for (int x = helperX; x < helperX + 20; x++) {
                for (int y = helperY; y < helperY + 20; y++) {
                    MagicPixel k = MagicHandler.instance.GetMagicPixelCoords(x,y, true);
                    if (k != null) {
                        for (int x2 = -1; x2 <= 1; x2++) {
                            for (int y2 = -1; y2 <= 1; y2++) {
                                MagicPixel g = MagicHandler.instance.GetMagicPixelCoords(k.corX + x2, k.corY + y2, true);
                                if (g != null) {
                                    k.neighbors.Add(g);
                                }
                            }
                        }
                    } else {
                        Debug.Log("This is bugging up.");
                    }
                }
            }
            MagicHandler.instance.ObtuseWorkAround(2, this);
        }

        public void StepUpNeighbors () {
            if (neighAliY < layerSize / 20) {
                if (neighAliX < layerSize / 20) {
                    neighAliX++;
                } else if (neighAliY < layerSize / 20) {
                    neighAliX = 1;
                    neighAliY++;
                }

                MassGatherNeighbors(neighAliX, neighAliY);

            } else if (neighAliY >= layerSize / 20) {
               hasLayedGrid = true;
                MagicHandler.instance.StepUpGeneration();
            }
        }

        public void UpdateRun () {
            if (MagicHandler.instance.hasGeneratedGrid == true && Manager.instance.renderPixels == true) {
                if (Time.fixedTime > lastRecordedTime + timeOffset) {
                    lastRecordedTime = Time.fixedTime + timeOffset;

                    foreach (MagicPixel m in pixelList) {
                        m.UpdatePixel();
                    }
                }
            }
        }
    }
}
