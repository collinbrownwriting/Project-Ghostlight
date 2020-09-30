using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class NPCHandler : MonoBehaviour
    {
    
        public Material stand;
        public Material outline;
        public float interactDist;
        public bool isOver;
        public bool isInteracting;

        public bool interactable;
        public bool willTalk;
        public bool grantSpell;
        public Spell spellToGrant;

        public bool recepientSpell;
        public bool recepientAttack;

        void OnMouseOver () {
            isOver = true;
            if (Vector3.Distance(this.gameObject.transform.position, Controller.instance.gameObject.transform.position) <= interactDist && interactable == true  && this.gameObject.GetComponent<Controller>().hostile == false){
                foreach (Renderer rend in this.gameObject.GetComponentsInChildren<Renderer>()){
                    rend.material = outline;
                    this.gameObject.GetComponent<RenderLevel>().npcInt = true;
                }
            }
        }
        
        void OnMouseExit () {
            isOver = false;
            foreach (Renderer rend in this.gameObject.GetComponentsInChildren<Renderer>()){
                rend.material = stand;
            }
        }

        void Update () {
            if (isOver == true && Input.GetButtonDown("Fire1") && isInteracting == false && interactable == true && this.gameObject.GetComponent<Controller>().hostile == false) {
                isInteracting = true;

                if (willTalk == true) {
                    //Figure out how to handle dialogue here.
                    AnnouncerManager.instance.ReceiveText(this.gameObject.GetComponent<CharacterData>().charName + ": 'Hey there, pal.'", true);
                }
                if (grantSpell == true) {
                    AnnouncerManager.instance.ReceiveText("You received the " + spellToGrant.name, true);
                    
                    BookManager.instance.spells.Add(spellToGrant);
                    spellToGrant.isInInven = true;
                    SpellbookUI.instance.UpdateUI();
                    spellToGrant = null;
                    grantSpell = false;
                }
            } 
            if (recepientAttack == true) {
                foreach (Renderer rend in this.gameObject.GetComponentsInChildren<Renderer>()){
                    rend.material = outline;
                }
                if (this.gameObject.GetComponent<Controller>().willHostile == true && isOver == true && Input.GetButtonDown("Fire1")) {
                    foreach (GameObject g in Controller.instance.attackableEnemies) {
                        if (g == this.gameObject) {
                            Controller.instance.PlayerAttack(g);
                        }
                    }
                }
            }
            if (recepientSpell == true) {
                foreach (Renderer rend in this.gameObject.GetComponentsInChildren<Renderer>()){
                    rend.material = outline;
                }
                if (isOver == true && Input.GetButtonDown("Fire1")) {
                    recepientSpell = false;
                    SpellHandler.instance.TriggerSpell(this.gameObject);
                } else if (Input.GetButtonDown("Fire2")) {
                    SpellHandler.instance.CancelSpell();
                }
            }
        }

    }
}
