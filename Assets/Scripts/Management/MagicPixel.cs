using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class MagicPixel {
        public bool active;
        public int corX;
        public int corY;
        public PixelType pType;
        public GameObject worldObject;
        public bool hasPixel;
        public GameObject tempObj;
        public bool hasFoundNeighbors;
        public List<MagicPixel> neighbors = new List<MagicPixel>();
        public Vector3 worldPosition;
        public PixelGrid myGrid;

        public MagicPixel FindNeighbor (int x, int y) {
            MagicPixel retVal = null;

            foreach (MagicPixel neighBoy in neighbors) {
                if (neighBoy.corX == corX + x && neighBoy.corY == corY + y) {
                    retVal = neighBoy;
                }
            }

            return retVal;
        }

        public void UpdatePixel () {
            if (MagicHandler.instance.hasGeneratedGrid == true) {
                if (active == true && hasPixel == true) {
                    //if (tempObj.GetComponent<PixelHandler>().canBeSeen == true) {

                        int dirX = 0;
                        int dirY = 0;
                        bool dissipate = false;

                        if ((int)pType == 1) {
                            int i = Random.Range(1, 100);
                            if (i > 5) {
                                dissipate = false;
                                dirX = Mathf.RoundToInt(Random.Range(-1, 1));
                                dirY = 1;
                            } else if (i <= 5) {
                                dissipate = true;
                            }
                        }

                        if ((int)pType == 2) {
                            int i = Random.Range(1, 100);
                            if (i > 10) {
                                dissipate = false;
                                dirX = 1;
                                dirY = 1;
                            } else if (i <= 10) {
                                dissipate = true;
                            }
                        }

                        if ((int)pType == 3) {
                            int i = Random.Range(1, 100);
                            if (i > 5) {
                                dissipate = false;
                                float ranY = Random.Range(1, 100);
                                if (ranY > 25) {
                                    dirY = 1;
                                } else if (ranY > 10) {
                                    dirY = 0;
                                } else {
                                    dirY = -1;
                                }
                                float ranX = Random.Range(1, 100);
                                if (ranX > 66) {
                                    dirX = 1;
                                } else if (ranX > 33) {
                                    dirX = 0;
                                } else {
                                    dirX = -1;
                                }
                            } else if (i <= 5) {
                                dissipate = true;
                            }
                        }

                        if ((int)pType == 4) {
                            int i = Random.Range(1, 100);
                            if (i > 5) {
                                dissipate = false;
                                float ranY = Random.Range(1, 100);
                                if (ranY < 5) {
                                    dirY = 1;
                                } else if (ranY < 15) {
                                    dirY = 0;
                                } else {
                                    dirY = -1;
                                }
                                float ranX = Random.Range(1, 100);
                                if (ranX > 66) {
                                    dirX = 1;
                                } else if (ranX > 33) {
                                    dirX = 0;
                                } else {
                                    dirX = -1;
                                }
                            } else if (i <= 5) {
                                dissipate = true;
                            }
                        }

                        //Temporary behavior.

                        if ((int)pType >= 5) {
                            int i = Random.Range(1, 100);
                            if (i > 5) {
                                dissipate = false;
                                float ranY = Random.Range(1, 100);
                                if (ranY > 25) {
                                    dirY = 1;
                                } else if (ranY > 10) {
                                    dirY = 0;
                                } else {
                                    dirY = -1;
                                }
                                float ranX = Random.Range(1, 100);
                                if (ranX > 66) {
                                    dirX = 1;
                                } else if (ranX > 33) {
                                    dirX = 0;
                                } else {
                                    dirX = -1;
                                }
                            } else if (i <= 5) {
                                dissipate = true;
                            }
                        }

                        //Add the behavior of various particles here.

                        if (dissipate == false) {

                            MagicPixel actNeigh = FindNeighbor(dirX, dirY);
                            if (actNeigh != null) {
                                if (actNeigh.active == false) {
                                    actNeigh.active = true;
                                    actNeigh.pType = pType;

                                    active = false;
                                }
                            }
                        } else {
                            active = false;
                        }
                }
                
                if (active == true && hasPixel == false) {
                    hasPixel = true;
                    GameObject g = MagicHandler.instance.CreatePhysicalPixel(worldObject);
                    g.GetComponent<PixelHandler>().SetType((int)pType);
                    g.transform.position = worldPosition;
                    tempObj = g;
                }
                
                if (active == false && hasPixel == true) {
                    MagicHandler.instance.DestroyPhysicalPixel(tempObj);
                    hasPixel = false;
                }
            }
        }
            
    }
    public enum PixelType { Nothing, Aether, Fire, Necrosis, Blood, Fable, Frost }
    //I'll add some more later.
}
