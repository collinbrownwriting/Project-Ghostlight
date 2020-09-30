using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class NPCEquipment : MonoBehaviour
    {
        GameObject oldItem;
        public GameObject headEquip;
        public GameObject bodyEquip;
        public GameObject armEquip;
        public GameObject legEquip;
        public GameObject weaponEquip;
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
        public bool isPuttingOn;

        void Start () {
            foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                anim.SetBool("NPC", true);
            }
        }

        public void Update () {
            if (this.gameObject.GetComponent<Controller>().isFlipped == true){
                foreach (SpriteRenderer s in this.gameObject.GetComponentsInChildren<SpriteRenderer>()){
                    s.flipX = true;
                }
            } else if (this.gameObject.GetComponent<Controller>().isFlipped == false){
                foreach (SpriteRenderer s in this.gameObject.GetComponentsInChildren<SpriteRenderer>()){
                    s.flipX = false;
                }
            }
            if (this.gameObject.GetComponent<Controller>().isMoving == true){
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Moving", true);
                }
            } else if (this.gameObject.GetComponent<Controller>().isMoving == false) {
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Moving", false);
                }
            }
            if (this.gameObject.GetComponent<Controller>().isLaunching == true){
                armEquip.GetComponent<Animator>().SetBool("Flailing", true);
                legEquip.GetComponent<Animator>().SetBool("Flailing", true);
            } else if (this.gameObject.GetComponent<Controller>().isWeightless == true) {
                armEquip.GetComponent<Animator>().SetBool("Flailing", true);
                legEquip.GetComponent<Animator>().SetBool("Flailing", true);
            } else if (this.gameObject.GetComponent<Controller>().isLaunching == false && this.gameObject.GetComponent<Controller>().isWeightless == false) {
                armEquip.GetComponent<Animator>().SetBool("Flailing", false);
                legEquip.GetComponent<Animator>().SetBool("Flailing", false);
            }
            if (this.gameObject.GetComponent<CharacterData>().isDead == true){
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Dead", true);
                }
            }

            if (this.gameObject.GetComponent<Controller>().isAttacking == true){
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Attacking", true);
                }
            } else if (this.gameObject.GetComponent<Controller>().isAttacking == false) {
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Attacking", false);
                }
            }
            // if (this.gameObject.GetComponent<Controller>().attackAnim == true){
            //     foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
            //         anim.SetBool("Attacking", true);
            //     }
            // } else if (this.gameObject.GetComponent<Controller>().attackAnim == false) {
            //     foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
            //         anim.SetBool("Attacking", false);
            //     }
            // }
            if (this.gameObject.GetComponent<Controller>().isCasting == true) {
                if (this.gameObject.GetComponent<Controller>().isCursing == true) {
                    armEquip.GetComponent<Animator>().SetBool("Cursing", true);
                }
                armEquip.GetComponent<Animator>().SetBool("Casting", true);
            } else {
                armEquip.GetComponent<Animator>().SetBool("Cursing", false);
                armEquip.GetComponent<Animator>().SetBool("Casting", false);
            }

            if (this.gameObject.GetComponent<CharacterData>().tookDamage == true) {
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Damaged", true);
                }
            } else {
                foreach (Animator anim in this.gameObject.GetComponentsInChildren<Animator>()){
                    anim.SetBool("Damaged", false);
                }
            }
        }

        public void Equip (GameObject newItem) {
            int eqSlot = (int)newItem.GetComponent<ItemData>().equipSlot;
            this.gameObject.GetComponent<PhysicalProperties>().clothing.Add(newItem.GetComponent<ItemData>().objectProperties);

            if (eqSlot == 0) {
                if (equippedHead != null) {
                    oldItem = equippedHead;
                    this.gameObject.GetComponent<CharacterData>().endurance -= oldItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion -= oldItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack -= oldItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range -= oldItem.GetComponent<ItemData>().attackRange;
                    this.gameObject.GetComponent<PhysicalProperties>().clothing.Remove(oldItem.GetComponent<ItemData>().objectProperties);
                    this.gameObject.GetComponent<CharacterData>().endurance += newItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion += newItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack += newItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range += newItem.GetComponent<ItemData>().attackRange;
                    UpdateEquipment(newItem);
                } else {
                    this.gameObject.GetComponent<CharacterData>().endurance += newItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion += newItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack += newItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range += newItem.GetComponent<ItemData>().attackRange;
                    UpdateEquipment(newItem);
                }
            }
            if (eqSlot == 1) {
                if (equippedChest != null) {
                    oldItem = equippedChest;
                    this.gameObject.GetComponent<CharacterData>().endurance -= oldItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion -= oldItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack -= oldItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range -= oldItem.GetComponent<ItemData>().attackRange;
                    this.gameObject.GetComponent<PhysicalProperties>().clothing.Remove(oldItem.GetComponent<ItemData>().objectProperties);
                    this.gameObject.GetComponent<CharacterData>().endurance += newItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion += newItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack += newItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range += newItem.GetComponent<ItemData>().attackRange;
                    UpdateEquipment(newItem);
                } else {
                    this.gameObject.GetComponent<CharacterData>().endurance += newItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion += newItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack += newItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range += newItem.GetComponent<ItemData>().attackRange;
                    UpdateEquipment(newItem);
                }
            }
            if (eqSlot == 2) {
                if (equippedArms != null) {
                    oldItem = equippedArms;
                    this.gameObject.GetComponent<CharacterData>().endurance -= oldItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion -= oldItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack -= oldItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range -= oldItem.GetComponent<ItemData>().attackRange;
                    this.gameObject.GetComponent<PhysicalProperties>().clothing.Remove(oldItem.GetComponent<ItemData>().objectProperties);
                    this.gameObject.GetComponent<CharacterData>().endurance += newItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion += newItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack += newItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range += newItem.GetComponent<ItemData>().attackRange;
                    UpdateEquipment(newItem);
                } else {
                    this.gameObject.GetComponent<CharacterData>().endurance += newItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion += newItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack += newItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range += newItem.GetComponent<ItemData>().attackRange;
                    UpdateEquipment(newItem);
                }
            }
            if (eqSlot == 3) {
                if (equippedLegs != null) {
                    oldItem = equippedLegs;
                    this.gameObject.GetComponent<CharacterData>().endurance -= oldItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion -= oldItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack -= oldItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range -= oldItem.GetComponent<ItemData>().attackRange;
                    this.gameObject.GetComponent<PhysicalProperties>().clothing.Remove(oldItem.GetComponent<ItemData>().objectProperties);
                    this.gameObject.GetComponent<CharacterData>().endurance += newItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion += newItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack += newItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range += newItem.GetComponent<ItemData>().attackRange;
                    UpdateEquipment(newItem);
                } else {
                    this.gameObject.GetComponent<CharacterData>().endurance += newItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion += newItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack += newItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range += newItem.GetComponent<ItemData>().attackRange;
                    UpdateEquipment(newItem);
                }
            }
            if (eqSlot == 4) {
                if (equippedWeapon != null) {
                    oldItem = equippedWeapon;
                    this.gameObject.GetComponent<CharacterData>().endurance -= oldItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion -= oldItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack -= oldItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range -= oldItem.GetComponent<ItemData>().attackRange;
                    this.gameObject.GetComponent<PhysicalProperties>().clothing.Remove(oldItem.GetComponent<ItemData>().objectProperties);
                    this.gameObject.GetComponent<CharacterData>().endurance += newItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion += newItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack += newItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range += newItem.GetComponent<ItemData>().attackRange;
                    UpdateEquipment(newItem);
                } else {
                    this.gameObject.GetComponent<CharacterData>().endurance += newItem.GetComponent<ItemData>().defenseMod;
                    this.gameObject.GetComponent<CharacterData>().evasion += newItem.GetComponent<ItemData>().evasionMod;
                    this.gameObject.GetComponent<CharacterData>().attack += newItem.GetComponent<ItemData>().attackMod;
                    this.gameObject.GetComponent<CharacterData>().range += newItem.GetComponent<ItemData>().attackRange;
                    UpdateEquipment(newItem);
                }
            }
        }

        public void UpdateEquipment (GameObject g) {
            receivedItem = g;
            if ((int)g.GetComponent<ItemData>().equipSlot == 0){
                equippedHead = g;
                headEquip.GetComponent<Animator>().runtimeAnimatorController = g.GetComponent<ItemData>().animOverrider;
                headEquip.GetComponent<RenderLevel>().offset = g.GetComponent<ItemData>().renderMod;
                g.transform.parent = headEquip.transform;
                g.GetComponent<SpriteRenderer>().enabled = false;
                isPuttingOn = true;
            } else if ((int)g.GetComponent<ItemData>().equipSlot == 1){
                equippedChest = g;
                bodyEquip.GetComponent<Animator>().runtimeAnimatorController = g.GetComponent<ItemData>().animOverrider;
                bodyEquip.GetComponent<RenderLevel>().offset = g.GetComponent<ItemData>().renderMod;
                g.transform.parent = bodyEquip.transform;
                g.GetComponent<SpriteRenderer>().enabled = false;
                isPuttingOn = true;
            } else if ((int)g.GetComponent<ItemData>().equipSlot == 2){
                equippedArms = g;
                armEquip.GetComponent<Animator>().runtimeAnimatorController = g.GetComponent<ItemData>().animOverrider;
                this.gameObject.GetComponent<CharacterData>().missingLeftArm = g.GetComponent<ItemData>().missingLeftArm;
                this.gameObject.GetComponent<CharacterData>().missingRightArm = g.GetComponent<ItemData>().missingRightArm;
                this.gameObject.GetComponent<CharacterData>().armPrefab = g.GetComponent<ItemData>().armPrefab;
                armEquip.GetComponent<RenderLevel>().offset = g.GetComponent<ItemData>().renderMod;
                g.transform.parent = armEquip.transform;
                g.GetComponent<SpriteRenderer>().enabled = false;
                isPuttingOn = true;
            } else if ((int)g.GetComponent<ItemData>().equipSlot == 3){
                equippedLegs = g;
                legEquip.GetComponent<Animator>().runtimeAnimatorController = g.GetComponent<ItemData>().animOverrider;
                legEquip.GetComponent<RenderLevel>().offset = g.GetComponent<ItemData>().renderMod;
                g.transform.parent = legEquip.transform;
                g.GetComponent<SpriteRenderer>().enabled = false;
                isPuttingOn = true;
            } else if ((int)g.GetComponent<ItemData>().equipSlot == 4){
                equippedWeapon = g;
                weaponEquip.GetComponent<Animator>().enabled = true;
                weaponEquip.GetComponent<SpriteRenderer>().enabled = true;
                weaponEquip.GetComponent<Animator>().runtimeAnimatorController = g.GetComponent<ItemData>().animOverrider;
                weaponEquip.GetComponent<RenderLevel>().offset = g.GetComponent<ItemData>().renderMod;
                g.transform.parent = weaponEquip.transform;
                g.GetComponent<SpriteRenderer>().enabled = false;
                isPuttingOn = true;
            } else {
                Debug.Log("It is not a type we recognize.");
            }
        }

        public void Unequip (int i, GameObject g) {
            if (i == 0){
                headEquip.GetComponent<Animator>().runtimeAnimatorController = baseHeadOver;
                headEquip.GetComponent<RenderLevel>().offset = 0;
                isPuttingOn = true;
                
                g.transform.parent = null;
                g.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (i == 1){
                bodyEquip.GetComponent<Animator>().runtimeAnimatorController = baseBodyOver;
                bodyEquip.GetComponent<RenderLevel>().offset = 0;
                isPuttingOn = true;
                
                g.transform.parent = null;
                g.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (i == 2){
                armEquip.GetComponent<Animator>().runtimeAnimatorController = baseArmsOver;
                armEquip.GetComponent<RenderLevel>().offset = 0;
                isPuttingOn = true;
                
                g.transform.parent = null;
                g.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (i == 3){
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
