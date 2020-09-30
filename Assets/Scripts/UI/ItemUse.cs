using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class ItemUse : MonoBehaviour
    {
        InventorySlot inSlot;
        void Start () {
            inSlot = this.gameObject.GetComponentInParent<InventorySlot>();
        }

        public void UseItem (){
            if (inSlot.item.gameObject.GetComponent<ItemData>().hasUse == true) {
                inSlot.item.gameObject.GetComponent<ItemData>().Usage();
            } else {
                AnnouncerManager.instance.ReceiveText("You cannot use " + inSlot.item.gameObject.GetComponent<ItemData>().invenName, true);
            }
        }

        public void EatItem (){
            if (inSlot.item.gameObject.GetComponent<ItemData>().canEat == true) {
                inSlot.item.gameObject.GetComponent<ItemData>().Usage();
            } else {
                AnnouncerManager.instance.ReceiveText("You cannot eat " + inSlot.item.gameObject.GetComponent<ItemData>().invenName, true);
            }
        }
    }
}
