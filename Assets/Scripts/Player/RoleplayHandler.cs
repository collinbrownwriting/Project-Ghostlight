using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridMaster {
    public class RoleplayHandler : MonoBehaviour
    {
        public Text enteredName;
        public Text listedName;

        public int elementChosen;

        public Text strengthNum;
        public Text dexterityNum;
        public Text fortitudeNum;
        public Text charmNum;
        public Text intelligenceNum;
        public Text empathyNum;

        public Text martialNum;
        public Text athleticsNum;
        public Text evasionNum;
        public Text stealthNum;
        public Text enduranceNum;
        public Text regenerationNum;
        public Text persuasionNum;
        public Text egoNum;
        public Text creativityNum;
        public Text logicNum;
        public Text attunementNum;
        public Text zealNum;

        public Image aetherFrame;
        public Image fireFrame;
        public Image necroFrame;
        public Image bloodFrame;
        public Image fableFrame;

        public void Update () {
            listedName.text = enteredName.text;

            strengthNum.text = CreationHandler.instance.allocatedStrength.ToString();
            dexterityNum.text = CreationHandler.instance.allocatedDexterity.ToString();
            fortitudeNum.text = CreationHandler.instance.allocatedFortitude.ToString();
            charmNum.text = CreationHandler.instance.allocatedCharm.ToString();
            intelligenceNum.text = CreationHandler.instance.allocatedIntelligence.ToString();
            empathyNum.text = CreationHandler.instance.allocatedEmpathy.ToString();

            martialNum.text = CreationHandler.instance.allocatedMartial.ToString();
            athleticsNum.text = CreationHandler.instance.allocatedAthletics.ToString();
            evasionNum.text = CreationHandler.instance.allocatedEvasion.ToString();
            stealthNum.text = CreationHandler.instance.allocatedStealth.ToString();
            enduranceNum.text = CreationHandler.instance.allocatedEndurance.ToString();
            regenerationNum.text = CreationHandler.instance.allocatedRegeneration.ToString();
            persuasionNum.text = CreationHandler.instance.allocatedPersuasion.ToString();
            egoNum.text = CreationHandler.instance.allocatedEgo.ToString();
            creativityNum.text = CreationHandler.instance.allocatedCreativity.ToString();
            logicNum.text = CreationHandler.instance.allocatedLogic.ToString();
            attunementNum.text = CreationHandler.instance.allocatedAttunement.ToString();
            zealNum.text = CreationHandler.instance.allocatedZeal.ToString();

            CreationHandler.instance.charName = listedName.text;
        }

        public void ChooseBiology () {
            elementChosen = 0;

            aetherFrame.enabled = true;
            fireFrame.enabled = false;
            necroFrame.enabled = false;
            bloodFrame.enabled = false;
            fableFrame.enabled = false;
        }
        public void ChooseSpace () {
            elementChosen = 1;

            aetherFrame.enabled = false;
            fireFrame.enabled = true;
            necroFrame.enabled = false;
            bloodFrame.enabled = false;
            fableFrame.enabled = false;
        }
        public void ChooseNecro () {
            elementChosen = 2;

            aetherFrame.enabled = false;
            fireFrame.enabled = false;
            necroFrame.enabled = true;
            bloodFrame.enabled = false;
            fableFrame.enabled = false;
        }
        public void ChooseBlood () {
            elementChosen = 3;

            aetherFrame.enabled = false;
            fireFrame.enabled = false;
            necroFrame.enabled = false;
            bloodFrame.enabled = true;
            fableFrame.enabled = false;
        }
        public void ChooseFable () {
            elementChosen = 4;

            aetherFrame.enabled = false;
            fireFrame.enabled = false;
            necroFrame.enabled = false;
            bloodFrame.enabled = false;
            fableFrame.enabled = true;
        }
    }
}
