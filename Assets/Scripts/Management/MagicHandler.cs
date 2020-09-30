using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class MagicHandler : MonoBehaviour
    {
        public List<PixelGrid> pixelGridArray = new List<PixelGrid>();
        public List<Spell> generatedSpells = new List<Spell>();

        public int gridSize;
        PixelGrid activeGrid;
        public bool doubleAct;
        PixelGrid secondaryGrid;
        public bool hasGeneratedGrid;
        public int cycleCountX;
        public int cycleCountY;

        public int indivGridCoor;

        public PixelType aether;
        public PixelType fire;
        public PixelType naught;
        public PixelType necro;
        public PixelType blood;
        public PixelType fable;
        public PixelType frost;

        public GameObject anchorObject;

        public Vector3 anchorOffset;
        public PixelGrid tempGrid;
        public GameObject pixelPrefab;

        public void PrepareWorldSpells (WorldData worldData) {
            generatedSpells = worldData.spellList;
        }

        public void CreateObjectAnchor (Vector3 u, string newName) {
            GameObject g = Instantiate(anchorObject);
            g.transform.position = u;
            g.transform.parent = this.gameObject.transform;
            g.name = newName;
        }

        public void GeneratePixelGrids () {
            if (cycleCountX <= gridSize && cycleCountY <= gridSize) {
                PixelGrid pixelGrid = new PixelGrid();
                pixelGrid.pixelPrefab = pixelPrefab;
                pixelGrid.layerSize = indivGridCoor;
                pixelGridArray.Add(pixelGrid);
                //pixelGrid.centerVector = new Vector3 (cycleCountX * (Manager.instance.pixelGridDimensions * 0.05f), cycleCountY * (Manager.instance.pixelGridDimensions * 0.05f));
                activeGrid = pixelGrid;
                pixelGrid.StartUp();
            }
        }

        public void StepUpGeneration () {
            if (cycleCountY <= gridSize) {
                if (cycleCountX < gridSize) {
                    cycleCountX++;
                } else if (cycleCountY <= gridSize) {
                    cycleCountX = 1;
                    cycleCountY++;
                }
                
                if (cycleCountY <= gridSize) {
                    GeneratePixelGrids();
                } else {
                    hasGeneratedGrid = true;
                }
            }
        }

        public void ActivateSecondaryGrid (GameObject g) {
            if (hasGeneratedGrid == true) {
                PixelGrid closestGrid = null;
                float tempDistance = Vector3.Distance(activeGrid.centerVector, g.transform.position);
                foreach (PixelGrid p in pixelGridArray) {
                    if (Vector3.Distance(p.centerVector, g.transform.position) < tempDistance) {
                        tempDistance = Vector3.Distance(p.centerVector, g.transform.position);
                        closestGrid = p;
                    }
                }

                if (closestGrid != activeGrid && closestGrid != null) {
                    doubleAct = true;
                    secondaryGrid = closestGrid;
                }
            }
        }


        public void Update () {
            if (hasGeneratedGrid == true) {
                PixelGrid closestGrid = null;
                float tempDistance = 10000;
                foreach (PixelGrid p in pixelGridArray) {
                    if (Vector3.Distance(p.centerVector, CharacterData.instance.gameObject.transform.position) < tempDistance) {
                        tempDistance = Vector3.Distance(p.centerVector, CharacterData.instance.gameObject.transform.position);
                        closestGrid = p;
                    }
                }

                if (activeGrid != closestGrid) {
                    activeGrid = closestGrid;
                }

                activeGrid.UpdateRun();

                if (doubleAct == true) {
                    secondaryGrid.UpdateRun();
                }
            }
        }

        public static MagicHandler instance;
        void Awake () {
            instance = this;
        }

        public GameObject CreatePhysicalPixel (GameObject g) {
            GameObject w = Instantiate(g);
            w.transform.parent = this.gameObject.transform;
            return w;
        }

        public void DestroyPhysicalPixel (GameObject w) {
            Destroy(w);
        }

        public MagicPixel FindNearestPixel (Vector3 vector) {
            MagicPixel retVal = null;
            float tempDist = 10000f;

            if (doubleAct == false) {
                foreach (MagicPixel m in activeGrid.pixelList) {
                    if (Vector3.Distance(vector, m.worldPosition) < tempDist) {
                        tempDist = Vector3.Distance(vector, m.worldPosition);
                        retVal = m;
                    }
                }
            } else {
                if (Vector3.Distance(vector, activeGrid.centerVector) > Vector3.Distance(vector, secondaryGrid.centerVector)) {
                    foreach (MagicPixel m in secondaryGrid.pixelList) {
                        if (Vector3.Distance(vector, m.worldPosition) < tempDist) {
                            tempDist = Vector3.Distance(vector, m.worldPosition);
                            retVal = m;
                        }
                    }
                } else {
                    foreach (MagicPixel m in activeGrid.pixelList) {
                        if (Vector3.Distance(vector, m.worldPosition) < tempDist) {
                            tempDist = Vector3.Distance(vector, m.worldPosition);
                            retVal = m;
                        }
                    }
                }
            }
            return retVal;
        }

        public MagicPixel GetMagicPixelCoords (int x, int y, bool primary) {
            MagicPixel retVal = null;
            if (primary == true) {
                retVal = activeGrid.pixelArray[x,y];
            } else {
                retVal = secondaryGrid.pixelArray[x,y];
            }
            return retVal;
        }

        public MagicPixel GetMagicPixelCoList (Vector3 vector) {
            MagicPixel retVal = null;

            if (doubleAct == false) {
                foreach (MagicPixel m in activeGrid.pixelList) {
                    if (vector == m.worldPosition) {
                        retVal = m;
                    }
                }
            } else {
                if (Vector3.Distance(vector, activeGrid.centerVector) > Vector3.Distance(vector, secondaryGrid.centerVector)) {
                    foreach (MagicPixel m in secondaryGrid.pixelList) {
                        if (vector == m.worldPosition) {
                            retVal = m;
                        }
                    }
                } else {
                    foreach (MagicPixel m in activeGrid.pixelList) {
                        if (vector == m.worldPosition) {
                            retVal = m;
                        }
                    }
                }
            }
            return retVal;
        }

        public void ObtuseWorkAround (int i, PixelGrid p) {
            tempGrid = p;
            if (i == 1) {
                Invoke("ActiveGridStepUp", 0.0001f);
            } else {
                Invoke("ActiveGridStepUpNeighbors", 0.0001f);
            }
        }

        public void ActiveGridStepUp() {
            tempGrid.StepUp();
        }
        public void ActiveGridStepUpNeighbors() {
            tempGrid.StepUpNeighbors();
        }

        public bool GridType (MagicPixel tempPix) {
            if (tempPix.myGrid == activeGrid) {
                return true;
            } else if (tempPix.myGrid == secondaryGrid) {
                return false;
            } else {
                return true;
            }
        }


        public void SummonAether (MagicPixel tempPix, int mass) {
            if (tempPix != null) {
                bool whichGrid = GridType(tempPix);
                for (int i = mass; i >= -mass; i--) {
                    for (int s = mass; s >= -mass; s--) {
                        MagicPixel o = GetMagicPixelCoords(tempPix.corX + i, tempPix.corY + s, whichGrid);
                        if (o != null) {
                            o.active = true;
                            o.pType = aether;
                        }
                    }
                }
            }
        }

        public void SummonNecro (MagicPixel tempPix, int mass) {
            if (tempPix != null) {
                bool whichGrid = GridType(tempPix);
                for (int i = mass; i >= -mass; i--) {
                    for (int s = mass; s >= -mass; s--) {
                        MagicPixel o = GetMagicPixelCoords(tempPix.corX + i, tempPix.corY + s, whichGrid);
                        if (o != null) {
                            o.active = true;
                            o.pType = necro;
                        }
                    }
                }
            }
        }

        public void SummonBlood (MagicPixel tempPix, int mass) {
            if (tempPix != null) {
                bool whichGrid = GridType(tempPix);
                for (int i = mass; i >= -mass; i--) {
                    for (int s = mass; s >= -mass; s--) {
                        MagicPixel o = GetMagicPixelCoords(tempPix.corX + i, tempPix.corY + s, whichGrid);
                        if (o != null) {
                            o.active = true;
                            o.pType = blood;
                        }
                    }
                }
            }
        }

        public void SummonPixel (MagicPixel tempPix, int mass, PixelType pixelType) {
            if (tempPix != null) {
                bool whichGrid = GridType(tempPix);
                for (int i = mass; i >= -mass; i--) {
                    for (int s = mass; s >= -mass; s--) {
                        MagicPixel o = GetMagicPixelCoords(tempPix.corX + i, tempPix.corY + s, whichGrid);
                        if (o != null) {
                            o.active = true;
                            o.pType = pixelType;
                        }
                    }
                }
            }
        }
    }
}
