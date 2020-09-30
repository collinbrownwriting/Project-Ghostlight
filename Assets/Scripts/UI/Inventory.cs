using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridMaster {

    public class Inventory : MonoBehaviour
    {
        public GameObject activeObject;
        
        public List<GameObject> items = new List<GameObject>();
        public List<GameObject> bagItems = new List<GameObject>();

        public GameObject inventoryBG;
        public bool isOpen;
        public int page;
        public int slot;
        public GameObject slotOne;
        public GameObject slotTwo;
        public GameObject slotThree;
        public GameObject slotFour;
        public GameObject slotFive;
        public GameObject slotSix;
        public GameObject slotSeven;
        public GameObject slotEight;
        public GameObject slotNine;
        public GameObject slotTen;
        
        public Text textOne;
        public Text textTwo;
        public Text textThree;
        public Text textFour;
        public Text textFive;
        public Text textSix;
        public Text textSeven;
        public Text textEight;
        public Text textNine;
        public Text textTen;

        public GameObject[] agents;
        public GameObject player;
        public Controller cont;
        public CharacterData data;
        public bool isOpenGrab;
        public GameObject usagePageCounter;
        public GameObject grabTitle;
        public CharacterData characterStats;
        public InventoryUI inventoryUI;
        public GameObject spellBook;

        public bool delay;

        public void Start (){
            inventoryUI = GameObject.Find("Canvas").GetComponent<InventoryUI>();

            agents = GameObject.FindGameObjectsWithTag("Actor");
            characterStats = this.gameObject.GetComponent<CharacterData>();

            inventoryBG = GameObject.Find("Pick Up");
            usagePageCounter = GameObject.Find("Inventory Page");
            grabTitle = GameObject.Find("Pick Up Title");

            foreach (GameObject g in agents){
                if (g.GetComponent<CharacterData>().isPlayer == true){
                    player = g;
                }
            }
            data = player.GetComponent<CharacterData>();
            cont = player.GetComponent<Controller>();
        }
        

        void Update(){
            if (isOpenGrab == true){
                if (Input.GetKey("1") && slotOne != null && items.Count < data.maxInventory){
                    if ((int)slotOne.GetComponent<ItemData>().itemType == 0 
                    && CharacterData.instance.weaponsHeld < CharacterData.instance.maxWeaponSlots){
                        CharacterData.instance.weaponsHeld++;
                        AddItem(1);
                    } else if ((int)slotOne.GetComponent<ItemData>().itemType == 1 
                    && CharacterData.instance.potionsHeld < CharacterData.instance.maxPotionSlots){
                        CharacterData.instance.potionsHeld++;
                        AddItem(1);
                    } else if ((int)slotOne.GetComponent<ItemData>().itemType == 2 
                    && CharacterData.instance.componentsHeld < CharacterData.instance.maxComponentSlots){
                        CharacterData.instance.componentsHeld++;
                        AddItem(1);
                    } else if ((int)slotOne.GetComponent<ItemData>().itemType == 3 
                    && CharacterData.instance.bulkyHeld < CharacterData.instance.maxBulkySlots){
                        CharacterData.instance.bulkyHeld++;
                        AddItem(1);
                    } else if ((int)slotOne.GetComponent<ItemData>().itemType == 4){
                        if ((int)slotOne.GetComponent<ItemData>().equipSlot == 0) {
                            EquipmentManager.instance.Equip(slotOne);
                        } else if ((int)slotOne.GetComponent<ItemData>().equipSlot == 1) {
                            EquipmentManager.instance.Equip(slotOne);
                        } else if ((int)slotOne.GetComponent<ItemData>().equipSlot == 2) {
                            EquipmentManager.instance.Equip(slotOne);
                        } else if ((int)slotOne.GetComponent<ItemData>().equipSlot == 3) {
                            EquipmentManager.instance.Equip(slotOne);
                        }
                        slotOne.GetComponent<ItemData>().isInInven = true;
                        slotOne = null;
                        cont.grabbingItems = false;
                        cont.pickableItems = null;
                    } else if (delay == false) {
                        delay = true;
                        AnnouncerManager.instance.ReceiveText("You don't have space for that.", true);
                        Invoke("ResetDelay", 1f);
                    }
                } else if (items.Count >= data.maxInventory){
                    AnnouncerManager.instance.ReceiveText("You're carrying too much.", true);
                }
                if (Input.GetKey("2") && slotTwo != null && items.Count < data.maxInventory){
                    if ((int)slotTwo.GetComponent<ItemData>().itemType == 0 
                    && CharacterData.instance.weaponsHeld < CharacterData.instance.maxWeaponSlots){
                        CharacterData.instance.weaponsHeld++;
                        AddItem(2);
                    } else if ((int)slotTwo.GetComponent<ItemData>().itemType == 1 
                    && CharacterData.instance.potionsHeld < CharacterData.instance.maxPotionSlots){
                        CharacterData.instance.potionsHeld++;
                        AddItem(2);
                    } else if ((int)slotTwo.GetComponent<ItemData>().itemType == 2 
                    && CharacterData.instance.componentsHeld < CharacterData.instance.maxComponentSlots){
                        CharacterData.instance.componentsHeld++;
                        AddItem(2);
                    } else if ((int)slotTwo.GetComponent<ItemData>().itemType == 3 
                    && CharacterData.instance.bulkyHeld < CharacterData.instance.maxBulkySlots){
                        CharacterData.instance.bulkyHeld++;
                        AddItem(2);
                    } else if ((int)slotTwo.GetComponent<ItemData>().itemType == 4){
                        if ((int)slotTwo.GetComponent<ItemData>().equipSlot == 0) {
                            EquipmentManager.instance.Equip(slotTwo);
                        } else if ((int)slotTwo.GetComponent<ItemData>().equipSlot == 1) {
                            EquipmentManager.instance.Equip(slotTwo);
                        } else if ((int)slotTwo.GetComponent<ItemData>().equipSlot == 2) {
                            EquipmentManager.instance.Equip(slotTwo);
                        } else if ((int)slotTwo.GetComponent<ItemData>().equipSlot == 3) {
                            EquipmentManager.instance.Equip(slotTwo);
                        }
                        slotTwo.GetComponent<ItemData>().isInInven = true;
                        slotTwo = null;
                        cont.grabbingItems = false;
                        cont.pickableItems = null;
                    } else if (delay == false) {
                        delay = true;
                        AnnouncerManager.instance.ReceiveText("You don't have space for that.", true);
                        Invoke("ResetDelay", 1f);
                    }
                } else if (items.Count >= data.maxInventory){
                    AnnouncerManager.instance.ReceiveText("You're carrying too much.", true);
                }
                if (Input.GetKey("3") && slotThree != null && items.Count < data.maxInventory){
                    if ((int)slotThree.GetComponent<ItemData>().itemType == 0 
                    && CharacterData.instance.weaponsHeld < CharacterData.instance.maxWeaponSlots){
                        CharacterData.instance.weaponsHeld++;
                        AddItem(3);
                    } else if ((int)slotThree.GetComponent<ItemData>().itemType == 1 
                    && CharacterData.instance.potionsHeld < CharacterData.instance.maxPotionSlots){
                        CharacterData.instance.potionsHeld++;
                        AddItem(3);
                    } else if ((int)slotThree.GetComponent<ItemData>().itemType == 2 
                    && CharacterData.instance.componentsHeld < CharacterData.instance.maxComponentSlots){
                        CharacterData.instance.componentsHeld++;
                        AddItem(3);
                    } else if ((int)slotThree.GetComponent<ItemData>().itemType == 3 
                    && CharacterData.instance.bulkyHeld < CharacterData.instance.maxBulkySlots){
                        CharacterData.instance.bulkyHeld++;
                        AddItem(3);
                    } else if ((int)slotThree.GetComponent<ItemData>().itemType == 4){
                        EquipmentManager.instance.Equip(slotThree);
                        slotThree.GetComponent<ItemData>().isInInven = true;
                        slotThree = null;
                        cont.grabbingItems = false;
                        cont.pickableItems = null;
                    } else if (delay == false) {
                        delay = true;
                        AnnouncerManager.instance.ReceiveText("You don't have space for that.", true);
                        Invoke("ResetDelay", 1f);
                    }
                } else if (items.Count >= data.maxInventory){
                    AnnouncerManager.instance.ReceiveText("You're carrying too much.", true);
                }
                if (Input.GetKey("4") && slotFour != null && items.Count < data.maxInventory){
                    if ((int)slotFour.GetComponent<ItemData>().itemType == 0 
                    && CharacterData.instance.weaponsHeld < CharacterData.instance.maxWeaponSlots){
                        CharacterData.instance.weaponsHeld++;
                        AddItem(4);
                    } else if ((int)slotFour.GetComponent<ItemData>().itemType == 1 
                    && CharacterData.instance.potionsHeld < CharacterData.instance.maxPotionSlots){
                        CharacterData.instance.potionsHeld++;
                        AddItem(4);
                    } else if ((int)slotFour.GetComponent<ItemData>().itemType == 2 
                    && CharacterData.instance.componentsHeld < CharacterData.instance.maxComponentSlots){
                        CharacterData.instance.componentsHeld++;
                        AddItem(4);
                    } else if ((int)slotFour.GetComponent<ItemData>().itemType == 3 
                    && CharacterData.instance.bulkyHeld < CharacterData.instance.maxBulkySlots){
                        CharacterData.instance.bulkyHeld++;
                        AddItem(4);
                    } else if ((int)slotFour.GetComponent<ItemData>().itemType == 4){
                        EquipmentManager.instance.Equip(slotFour);
                        slotFour.GetComponent<ItemData>().isInInven = true;
                        slotFour = null;
                        cont.grabbingItems = false;
                        cont.pickableItems = null;
                    } else if (delay == false) {
                        delay = true;
                        AnnouncerManager.instance.ReceiveText("You don't have space for that.", true);
                        Invoke("ResetDelay", 1f);
                    }
                } else if (items.Count >= data.maxInventory){
                    AnnouncerManager.instance.ReceiveText("You're carrying too much.", true);
                }
                if (Input.GetKey("5") && slotFive != null && items.Count < data.maxInventory){
                    if ((int)slotFive.GetComponent<ItemData>().itemType == 0 
                    && CharacterData.instance.weaponsHeld < CharacterData.instance.maxWeaponSlots){
                        CharacterData.instance.weaponsHeld++;
                        AddItem(5);
                    } else if ((int)slotFive.GetComponent<ItemData>().itemType == 1 
                    && CharacterData.instance.potionsHeld < CharacterData.instance.maxPotionSlots){
                        CharacterData.instance.potionsHeld++;
                        AddItem(5);
                    } else if ((int)slotFive.GetComponent<ItemData>().itemType == 2 
                    && CharacterData.instance.componentsHeld < CharacterData.instance.maxComponentSlots){
                        CharacterData.instance.componentsHeld++;
                        AddItem(5);
                    } else if ((int)slotFive.GetComponent<ItemData>().itemType == 3 
                    && CharacterData.instance.bulkyHeld < CharacterData.instance.maxBulkySlots){
                        CharacterData.instance.bulkyHeld++;
                        AddItem(5);
                    } else if ((int)slotFive.GetComponent<ItemData>().itemType == 4){
                        EquipmentManager.instance.Equip(slotFive);
                        slotFive.GetComponent<ItemData>().isInInven = true;
                        slotFive = null;
                        cont.grabbingItems = false;
                        cont.pickableItems = null;
                    } else if (delay == false) {
                        delay = true;
                        AnnouncerManager.instance.ReceiveText("You don't have space for that.", true);
                        Invoke("ResetDelay", 1f);
                    }
                } else if (items.Count >= data.maxInventory){
                    AnnouncerManager.instance.ReceiveText("You're carrying too much.", true);
                }
                if (Input.GetKey("6") && slotSix != null && items.Count < data.maxInventory){
                    if ((int)slotSix.GetComponent<ItemData>().itemType == 0 
                    && CharacterData.instance.weaponsHeld < CharacterData.instance.maxWeaponSlots){
                        CharacterData.instance.weaponsHeld++;
                        AddItem(6);
                    } else if ((int)slotSix.GetComponent<ItemData>().itemType == 1 
                    && CharacterData.instance.potionsHeld < CharacterData.instance.maxPotionSlots){
                        CharacterData.instance.potionsHeld++;
                        AddItem(6);
                    } else if ((int)slotSix.GetComponent<ItemData>().itemType == 2 
                    && CharacterData.instance.componentsHeld < CharacterData.instance.maxComponentSlots){
                        CharacterData.instance.componentsHeld++;
                        AddItem(6);
                    } else if ((int)slotSix.GetComponent<ItemData>().itemType == 3 
                    && CharacterData.instance.bulkyHeld < CharacterData.instance.maxBulkySlots){
                        CharacterData.instance.bulkyHeld++;
                        AddItem(6);
                    } else if ((int)slotSix.GetComponent<ItemData>().itemType == 4){
                        EquipmentManager.instance.Equip(slotSix);
                        slotSix.GetComponent<ItemData>().isInInven = true;
                        slotSix = null;
                        cont.grabbingItems = false;
                        cont.pickableItems = null;
                    } else if (delay == false) {
                        delay = true;
                        AnnouncerManager.instance.ReceiveText("You don't have space for that.", true);
                        Invoke("ResetDelay", 1f);
                    }
                } else if (items.Count >= data.maxInventory){
                    AnnouncerManager.instance.ReceiveText("You're carrying too much.", true);
                }
                if (Input.GetKey("7") && slotSeven != null && items.Count < data.maxInventory){
                if ((int)slotSeven.GetComponent<ItemData>().itemType == 0 
                    && CharacterData.instance.weaponsHeld < CharacterData.instance.maxWeaponSlots){
                        CharacterData.instance.weaponsHeld++;
                        AddItem(7);
                    } else if ((int)slotSeven.GetComponent<ItemData>().itemType == 1 
                    && CharacterData.instance.potionsHeld < CharacterData.instance.maxPotionSlots){
                        CharacterData.instance.potionsHeld++;
                        AddItem(7);
                    } else if ((int)slotSeven.GetComponent<ItemData>().itemType == 2 
                    && CharacterData.instance.componentsHeld < CharacterData.instance.maxComponentSlots){
                        CharacterData.instance.componentsHeld++;
                        AddItem(7);
                    } else if ((int)slotSeven.GetComponent<ItemData>().itemType == 3 
                    && CharacterData.instance.bulkyHeld < CharacterData.instance.maxBulkySlots){
                        CharacterData.instance.bulkyHeld++;
                        AddItem(7);
                    } else if ((int)slotSeven.GetComponent<ItemData>().itemType == 4){
                        EquipmentManager.instance.Equip(slotSeven);
                        slotSeven.GetComponent<ItemData>().isInInven = true;
                        slotSeven = null;
                        cont.grabbingItems = false;
                        cont.pickableItems = null;
                    } else if (delay == false) {
                        delay = true;
                        AnnouncerManager.instance.ReceiveText("You don't have space for that.", true);
                        Invoke("ResetDelay", 1f);
                    }                  
                } else if (items.Count >= data.maxInventory){
                    AnnouncerManager.instance.ReceiveText("You're carrying too much.", true);
                }
                if (Input.GetKey("8") && slotEight != null && items.Count < data.maxInventory){
                    if ((int)slotEight.GetComponent<ItemData>().itemType == 0 
                    && CharacterData.instance.weaponsHeld < CharacterData.instance.maxWeaponSlots){
                        CharacterData.instance.weaponsHeld++;
                        AddItem(8);
                    } else if ((int)slotEight.GetComponent<ItemData>().itemType == 1 
                    && CharacterData.instance.potionsHeld < CharacterData.instance.maxPotionSlots){
                        CharacterData.instance.potionsHeld++;
                        AddItem(8);
                    } else if ((int)slotEight.GetComponent<ItemData>().itemType == 2 
                    && CharacterData.instance.componentsHeld < CharacterData.instance.maxComponentSlots){
                        CharacterData.instance.componentsHeld++;
                        AddItem(8);
                    } else if ((int)slotEight.GetComponent<ItemData>().itemType == 3 
                    && CharacterData.instance.bulkyHeld < CharacterData.instance.maxBulkySlots){
                        CharacterData.instance.bulkyHeld++;
                        AddItem(8);
                    } else if ((int)slotEight.GetComponent<ItemData>().itemType == 4){
                        EquipmentManager.instance.Equip(slotEight);
                        slotEight.GetComponent<ItemData>().isInInven = true;
                        slotEight = null;
                        cont.grabbingItems = false;
                        cont.pickableItems = null;
                    } else if (delay == false) {
                        delay = true;
                        AnnouncerManager.instance.ReceiveText("You don't have space for that.", true);
                        Invoke("ResetDelay", 1f);
                    }
                } else if (items.Count >= data.maxInventory){
                    AnnouncerManager.instance.ReceiveText("You're carrying too much.", true);
                }
                if (Input.GetKey("9") && slotNine != null && items.Count < data.maxInventory){
                    if ((int)slotNine.GetComponent<ItemData>().itemType == 0 
                    && CharacterData.instance.weaponsHeld < CharacterData.instance.maxWeaponSlots){
                        CharacterData.instance.weaponsHeld++;
                        AddItem(9);
                    } else if ((int)slotNine.GetComponent<ItemData>().itemType == 1 
                    && CharacterData.instance.potionsHeld < CharacterData.instance.maxPotionSlots){
                        CharacterData.instance.potionsHeld++;
                        AddItem(9);
                    } else if ((int)slotNine.GetComponent<ItemData>().itemType == 2 
                    && CharacterData.instance.componentsHeld < CharacterData.instance.maxComponentSlots){
                        CharacterData.instance.componentsHeld++;
                        AddItem(9);
                    } else if ((int)slotNine.GetComponent<ItemData>().itemType == 3 
                    && CharacterData.instance.bulkyHeld < CharacterData.instance.maxBulkySlots){
                        CharacterData.instance.bulkyHeld++;
                        AddItem(9);
                    } else if ((int)slotNine.GetComponent<ItemData>().itemType == 4){
                        EquipmentManager.instance.Equip(slotNine);
                        slotNine.GetComponent<ItemData>().isInInven = true;
                        slotNine = null;
                        cont.grabbingItems = false;
                        cont.pickableItems = null;
                    } else if (delay == false) {
                        delay = true;
                        AnnouncerManager.instance.ReceiveText("You don't have space for that.", true);
                        Invoke("ResetDelay", 1f);
                    }
                } else if (items.Count >= data.maxInventory){
                    AnnouncerManager.instance.ReceiveText("You're carrying too much.", true);
                }
                if (Input.GetKey("0") && slotTen != null && items.Count < data.maxInventory){
                    if ((int)slotTen.GetComponent<ItemData>().itemType == 0 
                    && CharacterData.instance.weaponsHeld < CharacterData.instance.maxWeaponSlots){
                        CharacterData.instance.weaponsHeld++;
                        AddItem(10);
                    } else if ((int)slotTen.GetComponent<ItemData>().itemType == 1 
                    && CharacterData.instance.potionsHeld < CharacterData.instance.maxPotionSlots){
                        CharacterData.instance.potionsHeld++;
                        AddItem(10);
                    } else if ((int)slotTen.GetComponent<ItemData>().itemType == 2 
                    && CharacterData.instance.componentsHeld < CharacterData.instance.maxComponentSlots){
                        CharacterData.instance.componentsHeld++;
                        AddItem(10);
                    } else if ((int)slotTen.GetComponent<ItemData>().itemType == 3 
                    && CharacterData.instance.bulkyHeld < CharacterData.instance.maxBulkySlots){
                        CharacterData.instance.bulkyHeld++;
                        AddItem(10);
                    } else if ((int)slotTen.GetComponent<ItemData>().itemType == 4){
                        EquipmentManager.instance.Equip(slotTen);
                        slotTen.GetComponent<ItemData>().isInInven = true;
                        slotTen = null;
                        cont.grabbingItems = false;
                        cont.pickableItems = null;
                    } else if (delay == false) {
                        delay = true;
                        AnnouncerManager.instance.ReceiveText("You don't have space for that.", true);
                        Invoke("ResetDelay", 1f);
                    }
                } else if (items.Count >= data.maxInventory){
                    AnnouncerManager.instance.ReceiveText("You're carrying too much.", true);
                }
            }

            if (cont.grabbingItems == true && isOpenGrab == false){
                isOpenGrab = true;
                usagePageCounter.GetComponent<Text>().enabled = true;
                grabTitle.GetComponent<Text>().enabled = true;
                inventoryBG.GetComponent<Image>().enabled = true;
                slot = 1;
                page = 0;
                if (cont.pickableItems != null){
                    CyclePageUnheld();
                }
            } else if (cont.grabbingItems == false && isOpenGrab == true) {
                isOpenGrab = false;
                inventoryBG.GetComponent<Image>().enabled = false;
                usagePageCounter.GetComponent<Text>().enabled = false;
                grabTitle.GetComponent<Text>().enabled = false;
                slot = 1;
                page = 0;
                cont.items = null;
                cont.pickableItems = null;
                textOne.enabled = false;
                textTwo.enabled = false;
                textThree.enabled = false;
                textFour.enabled = false;
                textFive.enabled = false;
                textSix.enabled = false;
                textSeven.enabled = false;
                textEight.enabled = false;
                textNine.enabled = false;
                textTen.enabled = false;
            }

            if (isOpen == true || isOpenGrab == true){
                if (Input.GetButtonDown("Down")){
                    page += 1;
                    slot = 1;
                    CyclePageUnheld();
                } else if (Input.GetButtonDown("Up")){
                    page -= 1;
                    slot = 1;
                    CyclePageUnheld();
                }
            }
        }

        public void CyclePageUnheld()
        {
            if (slot == 1 && cont.pickableItems.Count > ((page * 10) + slot) - 1){
                slotOne = cont.pickableItems[(page * 10) + slot - 1];
                textOne.GetComponent<Text>().enabled = true;
                textOne.text = "1) " + slotOne.GetComponent<ItemData>().invenName;
                slot++;
            } else {
                textOne.text = null;
            }
            if (slot == 2 && cont.pickableItems.Count > ((page * 10) + slot) - 1){
                slotTwo = cont.pickableItems[(page * 10) + slot - 1];
                textTwo.GetComponent<Text>().enabled = true;
                textTwo.text = "2) " + slotTwo.GetComponent<ItemData>().invenName;
                slot++;
            } else {
                textTwo.text = null;
            }
            if (slot == 3 && cont.pickableItems.Count > ((page * 10) + slot) - 1){
                slotThree = cont.pickableItems[(page * 10) + slot - 1];
                textThree.GetComponent<Text>().enabled = true;
                textThree.text = "3) " + slotThree.GetComponent<ItemData>().invenName;
                slot++;
            } else {
                textThree.text = null;
            }
            if (slot == 4 && cont.pickableItems.Count > ((page * 10) + slot) - 1){
                slotFour = cont.pickableItems[(page * 10) + slot - 1];
                textFour.GetComponent<Text>().enabled = true;
                textFour.text = "4) " + slotFour.GetComponent<ItemData>().invenName;
                slot++;
            } else {
                textFour.text = null;
            }
            if (slot == 5 && cont.pickableItems.Count > ((page * 10) + slot) - 1){
                slotFive = cont.pickableItems[(page * 10) + slot - 1];
                textFive.GetComponent<Text>().enabled = true;
                textFive.text = "5) " + slotFive.GetComponent<ItemData>().invenName;
                slot++;
            } else {
                textFive.text = null;
            }
            if (slot == 6 && cont.pickableItems.Count > ((page * 10) + slot) - 1){
                slotSix = cont.pickableItems[(page * 10) + slot - 1];
                textSix.GetComponent<Text>().enabled = true;
                textSix.text = "6) " + slotSix.GetComponent<ItemData>().invenName;
                slot++;
            } else {
                textSix.text = null;
            }
            if (slot == 7 && cont.pickableItems.Count > ((page * 10) + slot) - 1){
                slotSeven = cont.pickableItems[(page * 10) + slot - 1];
                textSeven.GetComponent<Text>().enabled = true;
                textSeven.text = "7) " + slotSeven.GetComponent<ItemData>().invenName;
                slot++;
            } else {
                textSeven.text = null;
            }
            if (slot == 8 && cont.pickableItems.Count > ((page * 10) + slot) - 1){
                slotEight = cont.pickableItems[(page * 10) + slot - 1];
                textEight.GetComponent<Text>().enabled = true;
                textEight.text = "8) " + slotEight.GetComponent<ItemData>().invenName;
                slot++;
            } else {
                textEight.text = null;
            }
            if (slot == 9 && cont.pickableItems.Count > ((page * 10) + slot) - 1){
                slotNine = cont.pickableItems[(page * 10) + slot - 1];
                textNine.GetComponent<Text>().enabled = true;
                textNine.text = "9) " + slotNine.GetComponent<ItemData>().invenName;
                slot++;
            } else {
                textNine.text = null;
            }
            if (slot == 10 && cont.pickableItems.Count > ((page * 10) + slot) - 1){
                slotTen = cont.pickableItems[(page * 10) + slot - 1];
                textTen.GetComponent<Text>().enabled = true;
                textTen.text = "0) " + slotTen.GetComponent<ItemData>().invenName;
                slot++;
            } else {
                textTen.text = null;
            }

            usagePageCounter.GetComponent<Text>().text = "P. " + (page + 1) + "/ " + (Mathf.RoundToInt(cont.pickableItems.Count / 10) + 1);
        }

        public void AddItem (int i) {
            if (i == 1){
                slotOne.transform.parent = this.transform;
                if ((int)slotOne.GetComponent<ItemData>().itemType == 2){
                    items.Add(slotOne);
                } else {
                    bagItems.Add(slotOne);
                }
                //Adding bulk items needs to happen here.
                slotOne.GetComponent<ItemData>().isInInven = true;
                slotOne.GetComponent<SpriteRenderer>().enabled = false;
                AnnouncerManager.instance.ReceiveText("You picked up " + slotOne.GetComponent<ItemData>().invenName, true);
                inventoryUI.UpdateUI();
                slotOne = null;
                cont.grabbingItems = false;
                cont.pickableItems = null;
            }
            if (i == 2) {
                slotTwo.transform.parent = this.transform;
                if ((int)slotTwo.GetComponent<ItemData>().itemType == 2){
                    items.Add(slotTwo);
                } else {
                    bagItems.Add(slotTwo);
                }
                //Adding bulk items needs to happen here.
                slotTwo.GetComponent<ItemData>().isInInven = true;
                slotTwo.GetComponent<SpriteRenderer>().enabled = false;
                AnnouncerManager.instance.ReceiveText("You picked up " + slotTwo.GetComponent<ItemData>().invenName, true);
                inventoryUI.UpdateUI();
                slotTwo = null;
                cont.grabbingItems = false;
                cont.pickableItems = null;
            }
            if (i == 3) {
                slotThree.transform.parent = this.transform;
                if ((int)slotThree.GetComponent<ItemData>().itemType == 2){
                    items.Add(slotThree);
                } else {
                    bagItems.Add(slotThree);
                }
                //Adding bulk items needs to happen here.
                slotThree.GetComponent<ItemData>().isInInven = true;
                slotThree.GetComponent<SpriteRenderer>().enabled = false;
                AnnouncerManager.instance.ReceiveText("You picked up " + slotThree.GetComponent<ItemData>().invenName, true);
                inventoryUI.UpdateUI();
                slotThree = null;
                cont.grabbingItems = false;
                cont.pickableItems = null;
            }
            if (i == 4) {
                slotFour.transform.parent = this.transform;
                if ((int)slotFour.GetComponent<ItemData>().itemType == 2){
                    items.Add(slotFour);
                } else {
                    bagItems.Add(slotFour);
                }
                //Adding bulk items needs to happen here.
                slotFour.GetComponent<ItemData>().isInInven = true;
                slotFour.GetComponent<SpriteRenderer>().enabled = false;
                AnnouncerManager.instance.ReceiveText("You picked up " + slotFour.GetComponent<ItemData>().invenName, true);
                inventoryUI.UpdateUI();
                slotFour = null;
                cont.grabbingItems = false;
                cont.pickableItems = null;
            }
            if (i == 5) {
                slotFive.transform.parent = this.transform;
                if ((int)slotFive.GetComponent<ItemData>().itemType == 2){
                    items.Add(slotFive);
                } else {
                    bagItems.Add(slotFive);
                }
                //Adding bulk items needs to happen here.
                slotFive.GetComponent<ItemData>().isInInven = true;
                slotFive.GetComponent<SpriteRenderer>().enabled = false;
                AnnouncerManager.instance.ReceiveText("You picked up " + slotFive.GetComponent<ItemData>().invenName, true);
                inventoryUI.UpdateUI();
                slotFive = null;
                cont.grabbingItems = false;
                cont.pickableItems = null;
            }
            if (i == 6) {
                slotSix.transform.parent = this.transform;
                if ((int)slotSix.GetComponent<ItemData>().itemType == 2){
                    items.Add(slotSix);
                } else {
                    bagItems.Add(slotSix);
                }
                //Adding bulk items needs to happen here.
                slotSix.GetComponent<ItemData>().isInInven = true;
                slotSix.GetComponent<SpriteRenderer>().enabled = false;
                AnnouncerManager.instance.ReceiveText("You picked up " + slotSix.GetComponent<ItemData>().invenName, true);
                inventoryUI.UpdateUI();
                slotSix = null;
                cont.grabbingItems = false;
                cont.pickableItems = null;
            }
            if (i == 7) {
                slotSeven.transform.parent = this.transform;
                if ((int)slotSeven.GetComponent<ItemData>().itemType == 2){
                    items.Add(slotSeven);
                } else {
                    bagItems.Add(slotSeven);
                }
                //Adding bulk items needs to happen here.
                slotSeven.GetComponent<ItemData>().isInInven = true;
                slotSeven.GetComponent<SpriteRenderer>().enabled = false;
                AnnouncerManager.instance.ReceiveText("You picked up " + slotSeven.GetComponent<ItemData>().invenName, true);
                inventoryUI.UpdateUI();
                slotSeven = null;
                cont.grabbingItems = false;
                cont.pickableItems = null;
            }
            if (i == 8) {
                slotEight.transform.parent = this.transform;
                if ((int)slotEight.GetComponent<ItemData>().itemType == 2){
                    items.Add(slotEight);
                } else {
                    bagItems.Add(slotEight);
                }
                //Adding bulk items needs to happen here.
                slotEight.GetComponent<ItemData>().isInInven = true;
                slotEight.GetComponent<SpriteRenderer>().enabled = false;
                AnnouncerManager.instance.ReceiveText("You picked up " + slotEight.GetComponent<ItemData>().invenName, true);
                inventoryUI.UpdateUI();
                slotEight = null;
                cont.grabbingItems = false;
                cont.pickableItems = null;
            }
            if (i == 9) {
                slotNine.transform.parent = this.transform;
                if ((int)slotNine.GetComponent<ItemData>().itemType == 2){
                    items.Add(slotNine);
                } else {
                    bagItems.Add(slotNine);
                }
                //Adding bulk items needs to happen here.
                slotNine.GetComponent<ItemData>().isInInven = true;
                slotNine.GetComponent<SpriteRenderer>().enabled = false;
                AnnouncerManager.instance.ReceiveText("You picked up " + slotNine.GetComponent<ItemData>().invenName, true);
                inventoryUI.UpdateUI();
                slotNine = null;
                cont.grabbingItems = false;
                cont.pickableItems = null;
            }
            if (i == 10) {
                slotTen.transform.parent = this.transform;
                if ((int)slotTen.GetComponent<ItemData>().itemType == 2){
                    items.Add(slotTen);
                } else {
                    bagItems.Add(slotTen);
                }
                //Adding bulk items needs to happen here.
                slotTen.GetComponent<ItemData>().isInInven = true;
                slotTen.GetComponent<SpriteRenderer>().enabled = false;
                AnnouncerManager.instance.ReceiveText("You picked up " + slotTen.GetComponent<ItemData>().invenName, true);
                inventoryUI.UpdateUI();
                slotTen = null;
                cont.grabbingItems = false;
                cont.pickableItems = null;
            }
        }


        void ResetDelay () {
            delay = false;
        }


        public static Inventory instance;
        public static Inventory GetInstance()
        {
            return instance;
        }

        void Awake()
        {
            instance = this;

            bagItems.Add(spellBook);
        }
    }
}
