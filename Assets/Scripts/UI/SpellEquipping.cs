using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class SpellEquipping : MonoBehaviour
    {
        public SpellSlot spellSlot;
        public bool isOver;

        void OnMouseEnter () {
            isOver = true;
        }

        void OnMouseExit () {
            isOver = false;
        }

        void Update () {
            if (isOver = true && Input.GetButtonDown("Fire1")) {
                CameraController.instance.dragSpell = spellSlot.spell;
            }
        }

    }
}
