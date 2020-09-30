using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster
{

    public class SelectData : MonoBehaviour
    {
        public bool canMove;
        public GameObject selectedTile;
        public GameObject moveButton;
        public GameObject[] actors;
        public GameObject player;

        public void Start()
        {
            actors = GameObject.FindGameObjectsWithTag("Actor");
            foreach (GameObject g in actors)
            {
                if (g.GetComponent<CharacterData>().isPlayer == true)
                {
                    player = g;
                }
            }
        }
        public void SelectTile(GameObject g)
        {
            if (player.GetComponent<Controller>().isTurn == true){

                if (selectedTile != null)
                {
                    selectedTile.GetComponent<GridHighlight>().Unselect();

                    selectedTile = null;
                }
                selectedTile = g;

                selectedTile.GetComponent<GridHighlight>().Select();
            } else if (player.GetComponent<Controller>().freeRoam == true){
                if (selectedTile != null)
                {
                    selectedTile.GetComponent<GridHighlight>().Unselect();

                    selectedTile = null;
                }
                selectedTile = g;

                selectedTile.GetComponent<GridHighlight>().Select();
            }
        }
    }
}