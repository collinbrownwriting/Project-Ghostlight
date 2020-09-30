using UnityEngine;
using UnityEngine.UI;

namespace GridMaster{
public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public GameObject item;
    public Button removeButton;
    public Inventory inventory;
    public GameObject usageMenu;
    public bool isUsing;
    public GameObject quantity;
    public Text quantText;


    public void AddItem (GameObject newItem) {
        item = newItem;
        newItem.GetComponent<SpriteRenderer>().enabled = false;

        icon.sprite = item.GetComponent<ItemData>().icon;
        icon.enabled = true;

        removeButton.interactable = true;
    }

    public void ClearSlot () {
        item = null;
        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;
    }

    public void OnRemoveButton() {
        if (isUsing == false){
            item.GetComponent<SpriteRenderer>().enabled = true;
            item.GetComponent<ItemData>().isInInven = false;
            item.transform.parent = null;
            Inventory.instance.items.Remove(item);
            InventoryUI.instance.UpdateUI();
        }
    }

    public void UseItem () {
        if (item != null && isUsing == false){
            isUsing = true;
            usageMenu.SetActive(true);
        } else if (item != null && isUsing == true){
            isUsing = false;
            usageMenu.SetActive(false);
        }
    }

    public void Update () {
        if (item != null) {
            if (item.GetComponent<ItemData>().compQuant > 1f) {
                quantity.SetActive(true);
                string s = item.GetComponent<ItemData>().compQuant.ToString();
                quantText.text = s;
            } else if (item.GetComponent<ItemData>().compQuant > 0f) {
                quantity.SetActive(false);
            } else {
                item.transform.parent = null;
                Inventory.instance.items.Remove(item);
                InventoryUI.instance.UpdateUI();
                Destroy(item);
            }
        } else {
            quantity.SetActive(false);
        }
    }
}
}