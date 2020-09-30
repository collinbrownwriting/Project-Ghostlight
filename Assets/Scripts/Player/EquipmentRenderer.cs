using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class EquipmentRenderer : MonoBehaviour
    {
        //Important Variables
        public GameObject headEquip;
        public GameObject bodyEquip;
        public GameObject armEquip;
        public GameObject legEquip;
        public GameObject backpackEquip;
        public GameObject weaponEquip;
        public static EquipmentRenderer instance;
        public bool isPuttingOn;
        public Sprite baseHead;
        public Sprite baseBody;
        public Sprite baseArms;
        public Sprite baseLegs;
        public AnimatorOverrideController baseHeadOver;
        public AnimatorOverrideController baseBodyOver;
        public AnimatorOverrideController baseArmsOver;
        public AnimatorOverrideController baseLegsOver;

        public GameObject equippedHead;
        public GameObject equippedChest;
        public GameObject equippedArms;
        public GameObject equippedLegs;
        public GameObject equippedWeapon;
        public GameObject receivedItem;
        

        
        public bool testInven;

        void Awake () {
            instance = this;
        }

        public void UpdateEquipment (GameObject g) {
            receivedItem = g;
            if ((int)g.GetComponent<ItemData>().equipSlot == 0){
                equippedHead = g;
                //headEquip.GetComponent<SpriteRenderer>().sprite = g.GetComponent<SpriteRenderer>().sprite;
                headEquip.GetComponent<Animator>().runtimeAnimatorController = g.GetComponent<ItemData>().animOverrider;
                headEquip.GetComponent<RenderLevel>().offset = g.GetComponent<ItemData>().renderMod;
                g.transform.parent = headEquip.transform;
                g.GetComponent<SpriteRenderer>().enabled = false;
                isPuttingOn = true;
            } else if ((int)g.GetComponent<ItemData>().equipSlot == 1){
                equippedChest = g;
                //bodyEquip.GetComponent<SpriteRenderer>().sprite = g.GetComponent<SpriteRenderer>().sprite;
                bodyEquip.GetComponent<Animator>().runtimeAnimatorController = g.GetComponent<ItemData>().animOverrider;
                bodyEquip.GetComponent<RenderLevel>().offset = g.GetComponent<ItemData>().renderMod;
                g.transform.parent = bodyEquip.transform;
                g.GetComponent<SpriteRenderer>().enabled = false;
                isPuttingOn = true;
            } else if ((int)g.GetComponent<ItemData>().equipSlot == 2){
                equippedArms = g;
                //armEquip.GetComponent<SpriteRenderer>().sprite = g.GetComponent<SpriteRenderer>().sprite;
                armEquip.GetComponent<Animator>().runtimeAnimatorController = g.GetComponent<ItemData>().animOverrider;
                CharacterData.instance.missingLeftArm = g.GetComponent<ItemData>().missingLeftArm;
                CharacterData.instance.missingRightArm = g.GetComponent<ItemData>().missingRightArm;
                CharacterData.instance.armPrefab = g.GetComponent<ItemData>().armPrefab;
                armEquip.GetComponent<RenderLevel>().offset = g.GetComponent<ItemData>().renderMod;
                g.transform.parent = armEquip.transform;
                g.GetComponent<SpriteRenderer>().enabled = false;
                isPuttingOn = true;
            } else if ((int)g.GetComponent<ItemData>().equipSlot == 3){
                equippedLegs = g;
                //legEquip.GetComponent<SpriteRenderer>().sprite = g.GetComponent<SpriteRenderer>().sprite;
                legEquip.GetComponent<Animator>().runtimeAnimatorController = g.GetComponent<ItemData>().animOverrider;
                legEquip.GetComponent<RenderLevel>().offset = g.GetComponent<ItemData>().renderMod;
                g.transform.parent = legEquip.transform;
                g.GetComponent<SpriteRenderer>().enabled = false;
                isPuttingOn = true;
            } else if ((int)g.GetComponent<ItemData>().equipSlot == 4){
                equippedWeapon = g;
                weaponEquip.GetComponent<Animator>().enabled = true;
                weaponEquip.GetComponent<SpriteRenderer>().enabled = true;
                //weaponEquip.GetComponent<SpriteRenderer>().sprite = g.GetComponent<SpriteRenderer>().sprite;
                weaponEquip.GetComponent<Animator>().runtimeAnimatorController = g.GetComponent<ItemData>().animOverrider;
                weaponEquip.GetComponent<RenderLevel>().offset = g.GetComponent<ItemData>().renderMod;
                g.transform.parent = weaponEquip.transform;
                g.GetComponent<SpriteRenderer>().enabled = false;
                isPuttingOn = true;
            } else {
                Debug.Log("It is not a type we recognize.");
            }
        }

        public void Update () {
            //Begin test.
            if (testInven == false && Input.GetButtonDown("Open Inventory") && ItemManager.instance.isRummaging == false && CharacterData.instance.isDead == false){
                testInven = true;
                foreach (Animator animator in this.gameObject.GetComponentsInChildren<Animator>()){
                    animator.SetBool("Open Bag", true);
                }
            } else if (testInven == true && Input.GetButtonDown("Open Inventory")){
                testInven = false;
                foreach (Animator animator in this.gameObject.GetComponentsInChildren<Animator>()){
                    animator.SetBool("Open Bag", false);
                }
            }
            //End test.
            if (Controller.instance.isLaunching == true){
                armEquip.GetComponent<Animator>().SetBool("Flailing", true);
                legEquip.GetComponent<Animator>().SetBool("Flailing", true);
            } else if (Controller.instance.isWeightless == true) {
                armEquip.GetComponent<Animator>().SetBool("Flailing", true);
                legEquip.GetComponent<Animator>().SetBool("Flailing", true);
            } else if (Controller.instance.isLaunching == false && Controller.instance.isWeightless == false) {
                armEquip.GetComponent<Animator>().SetBool("Flailing", false);
                legEquip.GetComponent<Animator>().SetBool("Flailing", false);
            }
            
            if (isPuttingOn == true) {
                isPuttingOn = false;
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Equipping", true);
                }

                //headEquip.GetComponent<Animator>().SetBool("Equipping", true);
                //bodyEquip.GetComponent<Animator>().SetBool("Equipping", true);
                //armEquip.GetComponent<Animator>().SetBool("Equipping", true);
                //legEquip.GetComponent<Animator>().SetBool("Equipping", true);

                Invoke("IsDoneEquipping", 0.5f);
            }
            
            if (Controller.instance.isFlipped == true){
                foreach (SpriteRenderer s in this.gameObject.GetComponentsInChildren<SpriteRenderer>()){
                    s.flipX = true;
                }
                
                //headEquip.GetComponent<SpriteRenderer>().flipX = true;
                //bodyEquip.GetComponent<SpriteRenderer>().flipX = true;
                //armEquip.GetComponent<SpriteRenderer>().flipX = true;
                //legEquip.GetComponent<SpriteRenderer>().flipX = true;
            } else if (Controller.instance.isFlipped == false){
                foreach (SpriteRenderer s in this.gameObject.GetComponentsInChildren<SpriteRenderer>()){
                    s.flipX = false;
                }

                //headEquip.GetComponent<SpriteRenderer>().flipX = false;
                //bodyEquip.GetComponent<SpriteRenderer>().flipX = false;
                //armEquip.GetComponent<SpriteRenderer>().flipX = false;
                //legEquip.GetComponent<SpriteRenderer>().flipX = false;
            }
            
            if (Controller.instance.isMoving == true){
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Moving", true);
                }

                //headEquip.GetComponent<Animator>().SetBool("Moving", true);
                //bodyEquip.GetComponent<Animator>().SetBool("Moving", true);
                //armEquip.GetComponent<Animator>().SetBool("Moving", true);
                //legEquip.GetComponent<Animator>().SetBool("Moving", true);
            } else if (Controller.instance.isMoving == false) {
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Moving", false);
                }

                //headEquip.GetComponent<Animator>().SetBool("Moving", false);
                //bodyEquip.GetComponent<Animator>().SetBool("Moving", false);
                //armEquip.GetComponent<Animator>().SetBool("Moving", false);
                //legEquip.GetComponent<Animator>().SetBool("Moving", false);
            }

            if (CharacterData.instance.isDead == true){
                backpackEquip.GetComponent<SpriteRenderer>().enabled = false;
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Dead", true);
                }

                //headEquip.GetComponent<Animator>().SetBool("Dead", true);
                //bodyEquip.GetComponent<Animator>().SetBool("Dead", true);
                //armEquip.GetComponent<Animator>().SetBool("Dead", true);
                //legEquip.GetComponent<Animator>().SetBool("Dead", true);
            }

            if (Controller.instance.isAttacking == true){
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Ready", true);
                }

                //headEquip.GetComponent<Animator>().SetBool("Ready", true);
                //bodyEquip.GetComponent<Animator>().SetBool("Ready", true);
                //armEquip.GetComponent<Animator>().SetBool("Ready", true);
                //legEquip.GetComponent<Animator>().SetBool("Ready", true);
            } else if (Controller.instance.isAttacking == false) {
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Ready", false);
                }
            }

            if (Controller.instance.attackAnim == true){
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Attacking", true);
                }
            } else if (Controller.instance.attackAnim == false) {
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Attacking", false);
                }
            }

            if (Controller.instance.isCasting == true) {
                if (Controller.instance.isCursing == true) {
                    armEquip.GetComponent<Animator>().SetBool("Cursing", true);
                }
                armEquip.GetComponent<Animator>().SetBool("Casting", true);
                backpackEquip.GetComponent<Animator>().SetBool("Casting", true);
            } else {
                armEquip.GetComponent<Animator>().SetBool("Cursing", false);
                armEquip.GetComponent<Animator>().SetBool("Casting", false);
                backpackEquip.GetComponent<Animator>().SetBool("Casting", false);
            }

            if (CharacterData.instance.tookDamage == true) {
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Damaged", true);
                }
            } else {
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Damaged", false);
                }
            }
        }

        public void IsDoneEquipping () {
            foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Equipping", false);
            }
            

            //headEquip.GetComponent<Animator>().SetBool("Equipping", false);
            //bodyEquip.GetComponent<Animator>().SetBool("Equipping", false);
            //armEquip.GetComponent<Animator>().SetBool("Equipping", false);
            //legEquip.GetComponent<Animator>().SetBool("Equipping", false);
        }

        public void Unequip (int i, GameObject g) {
            if (i == 0){
                headEquip.GetComponent<SpriteRenderer>().sprite = baseHead;
                headEquip.GetComponent<Animator>().runtimeAnimatorController = baseHeadOver;
                headEquip.GetComponent<RenderLevel>().offset = 0;
                isPuttingOn = true;
                
                g.transform.parent = null;
                g.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (i == 1){
                bodyEquip.GetComponent<SpriteRenderer>().sprite = baseBody;
                bodyEquip.GetComponent<Animator>().runtimeAnimatorController = baseBodyOver;
                bodyEquip.GetComponent<RenderLevel>().offset = 0;
                isPuttingOn = true;
                
                g.transform.parent = null;
                g.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (i == 2){
                armEquip.GetComponent<SpriteRenderer>().sprite = baseArms;
                armEquip.GetComponent<Animator>().runtimeAnimatorController = baseArmsOver;
                armEquip.GetComponent<RenderLevel>().offset = 0;
                isPuttingOn = true;
                
                g.transform.parent = null;
                g.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (i == 3){
                legEquip.GetComponent<SpriteRenderer>().sprite = baseLegs;
                legEquip.GetComponent<Animator>().runtimeAnimatorController = baseLegsOver;
                legEquip.GetComponent<RenderLevel>().offset = 0;
                isPuttingOn = true;

                g.transform.parent = null;
                g.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (i == 4){
                if (equippedWeapon == g) {
                    equippedWeapon = null;
                }
                weaponEquip.GetComponent<SpriteRenderer>().enabled = false;
                weaponEquip.GetComponent<Animator>().enabled = false;
                weaponEquip.GetComponent<RenderLevel>().offset = 0;
                isPuttingOn = true;
            }
        }

    }
}
