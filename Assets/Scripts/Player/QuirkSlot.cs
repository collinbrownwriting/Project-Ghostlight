using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GridMaster {
    [ExecuteAlways]
    public class QuirkSlot : MonoBehaviour, IPointerClickHandler
    {
        
        public GameObject acquiredPanel;
        public GameObject possiblePanel;
        public Quirk heldQuirk;
        public bool isAcquired;

        void Update () {
            if ((int)heldQuirk.type == 0) {
                heldQuirk.weight = Mathf.RoundToInt(1f + heldQuirk.martialMod + heldQuirk.athleticsMod + heldQuirk.evasionMod + heldQuirk.stealthMod + heldQuirk.enduranceMod + heldQuirk.regenerationMod + heldQuirk.persuasionMod + heldQuirk.egoMod + heldQuirk.creativityMod + heldQuirk.logicMod + heldQuirk.attunementMod + heldQuirk.zealMod);
            }
        }
        
        public void OnPointerClick(PointerEventData eventData) {
            if (eventData.button == PointerEventData.InputButton.Left) {
                if (isAcquired == false && QuirkHandler.instance.quirkBurden + heldQuirk.weight <= QuirkHandler.instance.maxQuirks) {
                    QuirkHandler.instance.acquiredQuirks.Add(heldQuirk);
                    isAcquired = true;
                    QuirkHandler.instance.quirkBurden += heldQuirk.weight;
                    QuirkHandler.instance.possibleQuirks.Remove(heldQuirk);
                    this.transform.SetParent(acquiredPanel.transform, false);

                    CreationHandler.instance.allocatedMartial += heldQuirk.martialMod;
                    CreationHandler.instance.allocatedAthletics += heldQuirk.athleticsMod;
                    CreationHandler.instance.allocatedEvasion += heldQuirk.evasionMod;
                    CreationHandler.instance.allocatedStealth += heldQuirk.stealthMod;
                    CreationHandler.instance.allocatedEndurance += heldQuirk.enduranceMod;
                    CreationHandler.instance.allocatedRegeneration += heldQuirk.regenerationMod;
                    CreationHandler.instance.allocatedPersuasion += heldQuirk.persuasionMod;
                    CreationHandler.instance.allocatedEgo += heldQuirk.egoMod;
                    CreationHandler.instance.allocatedCreativity += heldQuirk.creativityMod;
                    CreationHandler.instance.allocatedLogic += heldQuirk.logicMod;
                    CreationHandler.instance.allocatedAttunement += heldQuirk.attunementMod;
                    CreationHandler.instance.allocatedZeal += heldQuirk.zealMod;

                } else if (isAcquired == true) {
                    QuirkHandler.instance.acquiredQuirks.Remove(heldQuirk);
                    isAcquired = false;
                    QuirkHandler.instance.quirkBurden -= heldQuirk.weight;
                    QuirkHandler.instance.possibleQuirks.Add(heldQuirk);
                    this.transform.SetParent(possiblePanel.transform, false);

                    CreationHandler.instance.allocatedMartial -= heldQuirk.martialMod;
                    CreationHandler.instance.allocatedAthletics -= heldQuirk.athleticsMod;
                    CreationHandler.instance.allocatedEvasion -= heldQuirk.evasionMod;
                    CreationHandler.instance.allocatedStealth -= heldQuirk.stealthMod;
                    CreationHandler.instance.allocatedEndurance -= heldQuirk.enduranceMod;
                    CreationHandler.instance.allocatedRegeneration -= heldQuirk.regenerationMod;
                    CreationHandler.instance.allocatedPersuasion -= heldQuirk.persuasionMod;
                    CreationHandler.instance.allocatedEgo -= heldQuirk.egoMod;
                    CreationHandler.instance.allocatedCreativity -= heldQuirk.creativityMod;
                    CreationHandler.instance.allocatedLogic -= heldQuirk.logicMod;
                    CreationHandler.instance.allocatedAttunement -= heldQuirk.attunementMod;
                    CreationHandler.instance.allocatedZeal -= heldQuirk.zealMod;
                } 
            } else if (eventData.button == PointerEventData.InputButton.Right) {
                QuirkHandler.instance.quirkName.text = heldQuirk.name;
                QuirkHandler.instance.quirkDesc.text = heldQuirk.description;
                QuirkHandler.instance.quirkCost.text = "Cost: " + heldQuirk.weight.ToString();
            }
        }
        
        [System.Serializable]
        public class Quirk {
            public string name;
            public string description;
            public QuirkType type;
            public int weight;
            public int quirkID;

            public float martialMod;
            public float athleticsMod;
            public float evasionMod;
            public float stealthMod;
            public float enduranceMod;
            public float regenerationMod;
            public float persuasionMod;
            public float egoMod;
            public float creativityMod;
            public float logicMod;
            public float attunementMod;
            public float zealMod;

            public enum QuirkType { stats, ability, other}
        }
    }
}
