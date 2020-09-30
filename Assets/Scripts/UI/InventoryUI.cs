using UnityEngine;

namespace GridMaster {
    public class InventoryUI : MonoBehaviour
    {
    
        public Transform topPanel;
        public GameObject inventoryUI;
        InventorySlot[] slots;

        public void Start () {
            slots = topPanel.GetComponentsInChildren<InventorySlot>();
        }

        public void Update () {
            if (Input.GetButtonDown("Open Inventory") && CharacterData.instance.isDead == false && Controller.instance.isMoving == false && Controller.instance.isAttacking == false && BookManager.instance.hasStartedUp == false){
                inventoryUI.SetActive(!inventoryUI.activeSelf);
            }
        }

        public void UpdateUI () {
            for (int i = 0; i < slots.Length; i++) {
                if (i < Inventory.instance.items.Count){
                    slots[i].AddItem(Inventory.instance.items[i]);
                } else {
                    slots[i].ClearSlot();
                }
            }
        }

        public static InventoryUI instance;
        public static InventoryUI GetInstance()
        {
            return instance;
        }

        void Awake()
        {
            instance = this;
        }
    
    }
}