using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridMaster {
    public class LoadingHandler : MonoBehaviour
    {
        bool willChange;
        bool ascDes;
        int dotNum;
        public Text text;
        public static LoadingHandler instance;

        void Awake () {
            instance = this;
        }

        public void StartUpGame()
        {
            GridHandler.instance.mapsize = Manager.instance.hexGridDimensions;
            MagicHandler.instance.indivGridCoor = Manager.instance.pixelGridDimensions;
            MagicHandler.instance.gridSize = GridHandler.instance.mapsize / (MagicHandler.instance.indivGridCoor / 25);


            if (GridHandler.instance.initOffset.y % 2 == 1) {
                GridHandler.instance.initOffset = new Vector2 (GridHandler.instance.initOffset.x * 0.145f, GridHandler.instance.initOffset.y * 0.17f + 0.085f);
                MagicHandler.instance.anchorOffset = new Vector3 (GridHandler.instance.initOffset.x, GridHandler.instance.initOffset.y, 0f);
            } else {
                GridHandler.instance.initOffset = new Vector2 (GridHandler.instance.initOffset.x * 0.145f, GridHandler.instance.initOffset.y * 0.17f);
                MagicHandler.instance.anchorOffset = new Vector3 (GridHandler.instance.initOffset.x, GridHandler.instance.initOffset.y, 0f);
            }

            dotNum = 1;
            ascDes = true;
            GridHandler.instance.BeginGeneration();
        }

        void Update () {
            if (MagicHandler.instance.hasGeneratedGrid == true && GridHandler.instance.hasGenerated == true) {
                this.gameObject.SetActive(false);
            } else if (willChange == false) {
                willChange = true;
                Invoke("ChangeLoadScreen", 0.2f);
            }
        }

        void ChangeLoadScreen () {
            if (ascDes == true) {
                dotNum++;
                if (dotNum == 1) {
                    text.text = "Loading.";
                } else if (dotNum == 2) {
                    text.text = "Loading..";
                } else if (dotNum == 3) {
                    ascDes = false;
                    text.text = "Loading...";
                }
            } else if (ascDes == false) {
                dotNum--;
                if (dotNum == 3) {
                    text.text = "Loading...";
                } else if (dotNum == 2) {
                    text.text = "Loading..";
                } else if (dotNum == 1) {
                    ascDes = true;
                    text.text = "Loading.";
                }
            }
            willChange = false;
        }
    }
}
