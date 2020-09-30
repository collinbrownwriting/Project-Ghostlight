using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridMaster{
    public class ItemData : MonoBehaviour
    {
        //General variables.
        public string invenName;
        public Sprite icon;
        public bool isInInven;
        public bool isWearable;
        public bool isWorn;
        public bool hasUse;
        public bool canEat;
        public float compQuant;
        public PhysicalProperties.InanimateObject objectProperties;
        public EquipmentSlot equipSlot;
        public ItemType itemType;
        public AnimatorOverrideController animOverrider;
        public bool isDroppable;
        public GameObject spellUI;
        public GameObject ghostlight;

        public GameObject armPrefab;
        public AnimatorOverrideController missingLeftArm;
        public AnimatorOverrideController missingRightArm;

        //Various modifiers.
        public float defenseMod;
        public float evasionMod;
        public float attackMod;
        public float attackRange;
        public int renderMod;
        public int healCount;
        public bool startsEquipped;
        public GameObject characterToStartIn;

        void Start () {
            if (startsEquipped == true && characterToStartIn == null) {
                EquipmentManager.instance.Equip(this.gameObject);
            } else if (startsEquipped == true && characterToStartIn != null) {
                characterToStartIn.GetComponent<NPCEquipment>().Equip(this.gameObject);
            }
        }
    
        public void Usage () {
            AnnouncerManager.instance.ReceiveText("You used the " + this.invenName, false);

            if ((int)this.gameObject.GetComponent<ItemData>().itemType == 0 && EquipmentRenderer.instance.equippedWeapon != this.gameObject && CharacterData.instance.canAttack == true) {
                EquipmentManager.instance.Equip(this.gameObject);
            } else if ((int)this.gameObject.GetComponent<ItemData>().itemType == 0 && EquipmentRenderer.instance.equippedWeapon == this.gameObject && CharacterData.instance.canAttack == true) {
                EquipmentRenderer.instance.Unequip(4, this.gameObject);
            }

            if ((int)this.gameObject.GetComponent<ItemData>().itemType == 1){
                Inventory.instance.bagItems.Remove(this.gameObject);
                CharacterData.instance.health += healCount;
                if (ghostlight != null && CharacterData.instance.ghostlight == null) {
                    MagicPixel nearestPixel = MagicHandler.instance.FindNearestPixel(CharacterData.instance.gameObject.transform.position + new Vector3 (0f, 0.05f, 0f));
                    MagicHandler.instance.SummonAether(nearestPixel, 5);
                    GameObject g = Instantiate(ghostlight);
                    g.GetComponent<ItemData>().compQuant = 20f;
                    CharacterData.instance.componentsHeld++;
                    CharacterData.instance.ghostlightHeld += 20f;
                    CharacterData.instance.ghostlight = g;
                    
                    Inventory.instance.items.Add(g);
                    g.GetComponent<ItemData>().isInInven = true;
                    g.GetComponent<SpriteRenderer>().enabled = false;
                    g.transform.parent = Inventory.instance.gameObject.transform;
                    Inventory.instance.inventoryUI.UpdateUI();
                } else if (ghostlight != null && CharacterData.instance.ghostlight != null) {
                    MagicPixel nearestPixel = MagicHandler.instance.FindNearestPixel(CharacterData.instance.gameObject.transform.position + new Vector3 (0f, 0.05f, 0f));
                    MagicHandler.instance.SummonAether(nearestPixel, 5);
                    CharacterData.instance.ghostlight.GetComponent<ItemData>().compQuant += 20f;
                }

                //We'll add other types of potions later.
                Destroy(this.gameObject);
                CharacterData.instance.potionsHeld -= 1;
            }

            if ((int)this.gameObject.GetComponent<ItemData>().itemType == 2) {
                if (hasUse == true) {
                    AnnouncerManager.instance.ReceiveText("You used " + invenName, true);
                    //Use it.
                } else if (canEat == true) {
                    AnnouncerManager.instance.ReceiveText("You ate " + invenName, true);
                    //Eat it.
                }
            }

            if ((int)this.gameObject.GetComponent<ItemData>().itemType == 5) {
                BookManager.instance.StartingUp();
            }
        }
    }

    public enum EquipmentSlot { Head, Body, Arms, Legs, Weapon }
    public enum ItemType { Weapon, Potion, Component, Bulky, Armor, Spellbook }
}
