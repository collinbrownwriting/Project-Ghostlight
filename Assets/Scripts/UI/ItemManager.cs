using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class ItemManager : MonoBehaviour
    {
        public bool isRummaging;
        public static ItemManager instance;
        public GameObject potentialSlot;
        public GameObject overheadSlot;
        public GameObject overheadPortrait;
        public int activeNum;

        void Awake () {
            instance = this;
        }

        public void Update () {
            if (isRummaging == false && Input.GetButtonDown("Backpack") && EquipmentRenderer.instance.testInven == false && CharacterData.instance.isDead == false && Controller.instance.isMoving == false && BookManager.instance.hasStartedUp == false){
                isRummaging = true;
                potentialSlot = Inventory.instance.bagItems[activeNum];
                foreach (Animator animator in this.gameObject.GetComponentsInChildren<Animator>()){
                    animator.SetBool("Open Bag", true);
                }
                overheadSlot.GetComponent<SpriteRenderer>().enabled = true;
                overheadPortrait.GetComponent<SpriteRenderer>().enabled = true;
                overheadPortrait.GetComponent<SpriteRenderer>().sprite = potentialSlot.GetComponent<ItemData>().icon;

            } else if (isRummaging == true && Input.GetButtonDown("Backpack")){
                isRummaging = false;
                foreach (Animator animator in this.gameObject.GetComponentsInChildren<Animator>()){
                    animator.SetBool("Open Bag", false);
                }
                overheadSlot.GetComponent<SpriteRenderer>().enabled = false;
                overheadPortrait.GetComponent<SpriteRenderer>().enabled = false;
                activeNum = 0;
            }

            if (isRummaging == true && Input.GetButtonDown("Cycle Items")){
                if (activeNum >= Inventory.instance.bagItems.Count - 1){
                    activeNum = 0;
                } else {
                    activeNum += 1;
                }
                 
                potentialSlot = Inventory.instance.bagItems[activeNum];
                overheadPortrait.gameObject.GetComponent<SpriteRenderer>().sprite = potentialSlot.GetComponent<ItemData>().icon;
            }

            if (isRummaging == true && Input.GetButtonDown("Jump")){
                isRummaging = false;
                foreach (Animator animator in this.gameObject.GetComponentsInChildren<Animator>()){
                    animator.SetBool("Open Bag", false);
                }
                overheadSlot.GetComponent<SpriteRenderer>().enabled = false;
                overheadPortrait.GetComponent<SpriteRenderer>().enabled = false;

                potentialSlot.GetComponent<ItemData>().Usage();

                activeNum = 0;
            }
            if (isRummaging == true && Input.GetButtonDown("Drop Item")) {
                if (potentialSlot.GetComponent<ItemData>().isDroppable == true) {
                    isRummaging = false;
                    foreach (Animator animator in this.gameObject.GetComponentsInChildren<Animator>()){
                        animator.SetBool("Open Bag", false);
                    }
                    overheadSlot.GetComponent<SpriteRenderer>().enabled = false;
                    overheadPortrait.GetComponent<SpriteRenderer>().enabled = false;

                    Inventory.instance.bagItems.Remove(potentialSlot);

                    potentialSlot.transform.parent = null;
                    potentialSlot.GetComponent<SpriteRenderer>().enabled = true;


                    activeNum = 0;
                }
            }
        }
    }   
}
