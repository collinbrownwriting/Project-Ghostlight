using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridMaster {
    public class EquippedSpellHandler : MonoBehaviour
    {
        public ActiveSpell activeSpell;
        public Spell heldSpell;
        public Image icon;
        public Button removeButton;
        public Sprite baseSprite;
        public Sprite placeholder;

        void Update () {
            if (heldSpell != null) {
                if ((int)activeSpell == 0 && Input.GetButtonDown("SpellHot1") && heldSpell != null) {
                    heldSpell.Activate(CharacterData.instance.gameObject);
                }
                if ((int)activeSpell == 1 && Input.GetButtonDown("SpellHot2") && heldSpell != null) {
                    heldSpell.Activate(CharacterData.instance.gameObject);
                }
                if ((int)activeSpell == 2 && Input.GetButtonDown("SpellHot3") && heldSpell != null) {
                    heldSpell.Activate(CharacterData.instance.gameObject);
                }
                if ((int)activeSpell == 3 && Input.GetButtonDown("SpellHot4") && heldSpell != null) {
                    heldSpell.Activate(CharacterData.instance.gameObject);
                }
                if ((int)activeSpell == 4 && Input.GetButtonDown("SpellHot5") && heldSpell != null) {
                    heldSpell.Activate(CharacterData.instance.gameObject);
                }
            }
        }

        public void AcceptSpell () {
            if (CameraController.instance.dragSpell != null) {
                heldSpell = CameraController.instance.dragSpell;
                icon.sprite = placeholder;
                removeButton.interactable = true;
            }
        }

        public void OnRemoveButton() {
            heldSpell = null;
            icon.sprite = baseSprite;

            removeButton.interactable = false;
        }

    }
    public enum ActiveSpell { A, B, C, D, E }
}
