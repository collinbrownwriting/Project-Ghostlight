using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridMaster {
    public class CharacterCreation : MonoBehaviour
    {
        public int characterPoints;

        public int allocatedStrength;
        public int allocatedDexterity;
        public int allocatedFortitude;
        public int allocatedCharm;
        public int allocatedIntelligence;
        public int allocatedEmpathy;

        public Text strengthNum;
        public Text dexterityNum;
        public Text fortitudeNum;
        public Text charmNum;
        public Text intelligenceNum;
        public Text empathyNum;

        public Button moreStrengthButton;
        public Button lessStrengthButton;
        public Button moreDexterityButton;
        public Button lessDexterityButton;
        public Button moreFortitudeButton;
        public Button lessFortitudeButton;
        public Button moreCharmButton;
        public Button lessCharmButton;
        public Button moreIntelligenceButton;
        public Button lessIntelligenceButton;
        public Button moreEmpathyButton;
        public Button lessEmpathyButton;

        void Start () {
            allocatedStrength = 1;
            allocatedDexterity = 1;
            allocatedFortitude = 1;
            allocatedCharm = 1;
            allocatedIntelligence = 1;
            allocatedEmpathy = 1;
        }

        void Update () {
            if (allocatedStrength == 10) {
                moreStrengthButton.interactable = false;
            } else {
                moreStrengthButton.interactable = true;
            }
            if (allocatedDexterity == 10) {
                moreDexterityButton.interactable = false;
            } else {
                moreDexterityButton.interactable = true;
            }
            if (allocatedFortitude == 10) {
                moreFortitudeButton.interactable = false;
            } else {
                moreFortitudeButton.interactable = true;
            }
            if (allocatedCharm == 10) {
                moreCharmButton.interactable = false;
            } else {
                moreCharmButton.interactable = true;
            }
            if (allocatedIntelligence == 10) {
                moreIntelligenceButton.interactable = false;
            } else {
                moreIntelligenceButton.interactable = true;
            }
            if (allocatedEmpathy == 10) {
                moreEmpathyButton.interactable = false;
            } else {
                moreEmpathyButton.interactable = true;
            }

            if (allocatedStrength == 1) {
                lessStrengthButton.interactable = false;
            } else {
                lessStrengthButton.interactable = true;
            }
            if (allocatedDexterity == 1) {
                lessDexterityButton.interactable = false;
            } else {
                lessDexterityButton.interactable = true;
            }
            if (allocatedFortitude == 1) {
                lessFortitudeButton.interactable = false;
            } else {
                lessFortitudeButton.interactable = true;
            }
            if (allocatedCharm == 1) {
                lessCharmButton.interactable = false;
            } else {
                lessCharmButton.interactable = true;
            }
            if (allocatedIntelligence == 1) {
                lessIntelligenceButton.interactable = false;
            } else {
                lessIntelligenceButton.interactable = true;
            }
            if (allocatedEmpathy == 1) {
                lessEmpathyButton.interactable = false;
            } else {
                lessEmpathyButton.interactable = true;
            }

            if (characterPoints == 0) {
                moreStrengthButton.interactable = false;
                moreDexterityButton.interactable = false;
                moreFortitudeButton.interactable = false;
                moreCharmButton.interactable = false;
                moreIntelligenceButton.interactable = false;
                moreEmpathyButton.interactable = false;
            }
        }

        public void AddStrength () {
            characterPoints--;
            allocatedStrength++;
            CreationHandler.instance.allocatedMartial += 1;
            CreationHandler.instance.allocatedAthletics += 1;
            strengthNum.text = allocatedStrength.ToString();
        }
        public void SubtractStrength () {
            characterPoints++;
            allocatedStrength--;
            CreationHandler.instance.allocatedMartial -= 1;
            CreationHandler.instance.allocatedAthletics -= 1;
            strengthNum.text = allocatedStrength.ToString();
        }
        public void AddDexterity () {
            characterPoints--;
            allocatedDexterity++;
            CreationHandler.instance.allocatedEvasion += 1;
            CreationHandler.instance.allocatedStealth += 1;
            dexterityNum.text = allocatedDexterity.ToString();
        }
        public void SubtractDexterity () {
            characterPoints++;
            allocatedDexterity--;
            CreationHandler.instance.allocatedEvasion -= 1;
            CreationHandler.instance.allocatedStealth -= 1;
            dexterityNum.text = allocatedDexterity.ToString();
        }
        public void AddFortitude () {
            characterPoints--;
            allocatedFortitude++;
            CreationHandler.instance.allocatedEndurance += 1;
            CreationHandler.instance.allocatedRegeneration += 1;
            fortitudeNum.text = allocatedFortitude.ToString();
        }
        public void SubtractFortitude () {
            characterPoints++;
            allocatedFortitude--;
            CreationHandler.instance.allocatedEndurance -= 1;
            CreationHandler.instance.allocatedRegeneration -= 1;
            fortitudeNum.text = allocatedFortitude.ToString();
        }
        public void AddCharm () {
            characterPoints--;
            allocatedCharm++;
            CreationHandler.instance.allocatedEgo += 1;
            CreationHandler.instance.allocatedPersuasion += 1;
            charmNum.text = allocatedCharm.ToString();
        }
        public void SubtractCharm () {
            characterPoints++;
            allocatedCharm--;
            CreationHandler.instance.allocatedEgo -= 1;
            CreationHandler.instance.allocatedPersuasion -= 1;
            charmNum.text = allocatedCharm.ToString();
        }
        public void AddIntelligence () {
            characterPoints--;
            allocatedIntelligence++;
            CreationHandler.instance.allocatedCreativity += 1;
            CreationHandler.instance.allocatedLogic += 1;
            intelligenceNum.text = allocatedIntelligence.ToString();
        }
        public void SubtractIntelligence () {
            characterPoints++;
            allocatedIntelligence--;
            CreationHandler.instance.allocatedCreativity -= 1;
            CreationHandler.instance.allocatedLogic -= 1;
            intelligenceNum.text = allocatedIntelligence.ToString();
        }
        public void AddEmpathy () {
            characterPoints--;
            allocatedEmpathy++;
            CreationHandler.instance.allocatedAttunement += 1;
            CreationHandler.instance.allocatedZeal += 1;
            empathyNum.text = allocatedEmpathy.ToString();
        }
        public void SubtractEmpathy () {
            characterPoints++;
            allocatedEmpathy--;
            CreationHandler.instance.allocatedAttunement -= 1;
            CreationHandler.instance.allocatedZeal -= 1;
            empathyNum.text = allocatedEmpathy.ToString();
        }
    }
}
