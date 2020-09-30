using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridMaster {
    public class CharacterPage : MonoBehaviour
    {
        public Text listedName;
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

        public void Update()
        {
            listedName.text = CharacterData.instance.charName;

            strengthNum.text = CharacterData.instance.strength.ToString();
            dexterityNum.text = CharacterData.instance.dexterity.ToString();
            fortitudeNum.text = CharacterData.instance.fortitude.ToString();
            charmNum.text = CharacterData.instance.charm.ToString();
            intelligenceNum.text = CharacterData.instance.intelligence.ToString();
            empathyNum.text = CharacterData.instance.empathy.ToString();

            martialNum.text = CharacterData.instance.martial.ToString();
            athleticsNum.text = CharacterData.instance.athletics.ToString();
            evasionNum.text = CharacterData.instance.evasion.ToString();
            stealthNum.text = CharacterData.instance.stealth.ToString();
            enduranceNum.text = CharacterData.instance.endurance.ToString();
            regenerationNum.text = CharacterData.instance.regeneration.ToString();
            persuasionNum.text = CharacterData.instance.persuasion.ToString();
            egoNum.text = CharacterData.instance.ego.ToString();
            creativityNum.text = CharacterData.instance.creativity.ToString();
            logicNum.text = CharacterData.instance.logic.ToString();
            attunementNum.text = CharacterData.instance.attunement.ToString();
            zealNum.text = CharacterData.instance.zeal.ToString();
        }
        

    }
}
