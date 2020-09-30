using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
public class EquipmentManager : MonoBehaviour
{ 
        public static EquipmentManager instance;
        GameObject oldItem;

        void Awake () {
            instance = this;
        }

        
        
        public void Equip (GameObject newItem) {
            int eqSlot = (int)newItem.GetComponent<ItemData>().equipSlot;
            CharacterData.instance.gameObject.GetComponent<PhysicalProperties>().clothing.Add(newItem.GetComponent<ItemData>().objectProperties);

            if (eqSlot == 0) {
                if (EquipmentRenderer.instance.equippedHead != null) {
                    oldItem = EquipmentRenderer.instance.equippedHead;
                    CharacterData.instance.endurance -= oldItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion -= oldItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack -= oldItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range -= oldItem.GetComponent<ItemData>().attackRange;
                    CharacterData.instance.gameObject.GetComponent<PhysicalProperties>().clothing.Remove(oldItem.GetComponent<ItemData>().objectProperties);
                    CharacterData.instance.endurance += newItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion += newItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack += newItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range += newItem.GetComponent<ItemData>().attackRange;
                    EquipmentRenderer.instance.UpdateEquipment(newItem);
                } else {
                    CharacterData.instance.endurance += newItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion += newItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack += newItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range += newItem.GetComponent<ItemData>().attackRange;
                    EquipmentRenderer.instance.UpdateEquipment(newItem);
                }
            }
            if (eqSlot == 1) {
                if (EquipmentRenderer.instance.equippedChest != null) {
                    oldItem = EquipmentRenderer.instance.equippedChest;
                    CharacterData.instance.endurance -= oldItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion -= oldItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack -= oldItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range -= oldItem.GetComponent<ItemData>().attackRange;
                    CharacterData.instance.gameObject.GetComponent<PhysicalProperties>().clothing.Remove(oldItem.GetComponent<ItemData>().objectProperties);
                    CharacterData.instance.endurance += newItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion += newItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack += newItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range += newItem.GetComponent<ItemData>().attackRange;
                    EquipmentRenderer.instance.UpdateEquipment(newItem);
                } else {
                    CharacterData.instance.endurance += newItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion += newItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack += newItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range += newItem.GetComponent<ItemData>().attackRange;
                    EquipmentRenderer.instance.UpdateEquipment(newItem);
                }
            }
            if (eqSlot == 2) {
                if (EquipmentRenderer.instance.equippedArms != null) {
                    oldItem = EquipmentRenderer.instance.equippedArms;
                    CharacterData.instance.endurance -= oldItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion -= oldItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack -= oldItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range -= oldItem.GetComponent<ItemData>().attackRange;
                    CharacterData.instance.gameObject.GetComponent<PhysicalProperties>().clothing.Remove(oldItem.GetComponent<ItemData>().objectProperties);
                    CharacterData.instance.endurance += newItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion += newItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack += newItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range += newItem.GetComponent<ItemData>().attackRange;
                    EquipmentRenderer.instance.UpdateEquipment(newItem);
                } else {
                    CharacterData.instance.endurance += newItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion += newItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack += newItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range += newItem.GetComponent<ItemData>().attackRange;
                    EquipmentRenderer.instance.UpdateEquipment(newItem);
                }
            }
            if (eqSlot == 3) {
                if (EquipmentRenderer.instance.equippedLegs != null) {
                    oldItem = EquipmentRenderer.instance.equippedLegs;
                    CharacterData.instance.endurance -= oldItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion -= oldItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack -= oldItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range -= oldItem.GetComponent<ItemData>().attackRange;
                    CharacterData.instance.gameObject.GetComponent<PhysicalProperties>().clothing.Remove(oldItem.GetComponent<ItemData>().objectProperties);
                    CharacterData.instance.endurance += newItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion += newItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack += newItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range += newItem.GetComponent<ItemData>().attackRange;
                    EquipmentRenderer.instance.UpdateEquipment(newItem);
                } else {
                    CharacterData.instance.endurance += newItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion += newItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack += newItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range += newItem.GetComponent<ItemData>().attackRange;
                    EquipmentRenderer.instance.UpdateEquipment(newItem);
                }
            }
            if (eqSlot == 4) {
                if (EquipmentRenderer.instance.equippedWeapon != null) {
                    oldItem = EquipmentRenderer.instance.equippedWeapon;
                    CharacterData.instance.endurance -= oldItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion -= oldItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack -= oldItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range -= oldItem.GetComponent<ItemData>().attackRange;
                    CharacterData.instance.gameObject.GetComponent<PhysicalProperties>().clothing.Remove(oldItem.GetComponent<ItemData>().objectProperties);
                    CharacterData.instance.endurance += newItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion += newItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack += newItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range += newItem.GetComponent<ItemData>().attackRange;
                    EquipmentRenderer.instance.UpdateEquipment(newItem);
                } else {
                    CharacterData.instance.endurance += newItem.GetComponent<ItemData>().defenseMod;
                    CharacterData.instance.evasion += newItem.GetComponent<ItemData>().evasionMod;
                    CharacterData.instance.attack += newItem.GetComponent<ItemData>().attackMod;
                    CharacterData.instance.range += newItem.GetComponent<ItemData>().attackRange;
                    EquipmentRenderer.instance.UpdateEquipment(newItem);
                }
            }
        }
    }
}
