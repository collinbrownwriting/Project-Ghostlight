using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster
{

    public class GridHighlight : MonoBehaviour
    {

        //		GridHandler gridBase;
        public Renderer rend;
        public bool selectedTile;
        public bool isMousing;
        public Node thisNode;
        public GameObject currentTile;
        public bool isPathing;

        public bool canBeSeen;


        void Start()
        {
            rend = GetComponent<Renderer>();
            rend.enabled = false;
            selectedTile = false;
            isPathing = false;
            //gridBase = GridHandler.GetInstance ();
        }

        void Update()
        {
            if (selectedTile == false && isMousing == false)
            {
                rend.enabled = false;
            }

            if (Vector2.Distance(this.transform.position, CharacterData.instance.gameObject.transform.position) <= CharacterData.instance.sightDist * Manager.instance.tileSightThreshold) {
                canBeSeen = true;
            } else {
                canBeSeen = false;
            }
        }


        void OnMouseOver()
        {
            if (canBeSeen == true) {
            CameraController.instance.tileOver = this.gameObject;
            rend.enabled = true;
            isMousing = true;

            if (Input.GetButtonDown("Fire1") && selectedTile == true && this.gameObject.GetComponent<CoordinateHolder>().isWall == false && this.gameObject.transform.childCount == 0 && ItemManager.instance.isRummaging == false && EquipmentRenderer.instance.testInven == false && Controller.instance.bookOpen == false)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SelectData>().player.GetComponent<Controller>().MoveAndStuff();
            }
            if (Input.GetButtonDown("Fire1") && selectedTile == false && this.gameObject.GetComponent<CoordinateHolder>().isWall == false && this.gameObject.transform.childCount == 0 && ItemManager.instance.isRummaging == false && EquipmentRenderer.instance.testInven == false && Controller.instance.bookOpen == false)
            {
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SelectData>().SelectTile(this.gameObject);
            }

            if (isMousing == true && Input.GetButtonDown("Fire1") && this.gameObject.transform.childCount > 0 && this.gameObject.GetComponent<CoordinateHolder>().isWall == false && Controller.instance.isAttacking == true) {
                foreach (Node n in Controller.instance.attackableNodes) {
                    if (n.worldObject == this.gameObject && isMousing == true){
                        Controller.instance.PlayerAttack(this.gameObject.transform.GetChild(0).gameObject);
                    }
                }
            }
            }
        }

        void OnMouseExit()
        {
            if (selectedTile == false)
            {
                rend.enabled = false;
            }

            isMousing = false;
        }

        public void Unselect()
        {
            selectedTile = false;
        }
        public void Select()
        {
            selectedTile = true;
        }

    }
}