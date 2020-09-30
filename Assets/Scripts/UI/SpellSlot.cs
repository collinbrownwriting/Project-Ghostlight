using UnityEngine;
using UnityEngine.UI;

namespace GridMaster{
public class SpellSlot : MonoBehaviour
{
    public Image icon;
    public Spell spell;
    public Button centerButton;
    public Button removeButton;
    public bool isUsing;

    public Sprite placeholder;

    public void AddSpell (Spell newSpell) {
        spell = newSpell;
        icon.sprite = placeholder;
        icon.enabled = true;
        centerButton.interactable = true;
        removeButton.interactable = true;
    }

    public void ClearSlot () {
        spell = null;
        icon.sprite = null;
        icon.enabled = false;
        centerButton.interactable = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton() {
        if (isUsing == false){
            spell.isInInven = false;
            BookManager.instance.spells.Remove(spell);
            SpellbookUI.instance.UpdateUI();
        }
    }

    public void DragSpell() {
        if (CameraController.instance.isDragging == false) {
            CameraController.instance.isDragging = true; 
            CameraController.instance.dragSpell = spell;
        }
    }
}
}