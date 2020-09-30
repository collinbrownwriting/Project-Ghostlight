using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridMaster {
    public class CreationHandler : MonoBehaviour
    {
        public string charName;
        public List<QuirkSlot.Quirk> acquiredQuirks = new List<QuirkSlot.Quirk>();
        public int allocatedStrength;
        public int allocatedDexterity;
        public int allocatedFortitude;
        public int allocatedCharm;
        public int allocatedIntelligence;
        public int allocatedEmpathy;

        public float allocatedMartial;
        public float allocatedAthletics;
        public float allocatedEvasion;
        public float allocatedStealth;
        public float allocatedEndurance;
        public float allocatedRegeneration;
        public float allocatedPersuasion;
        public float allocatedEgo;
        public float allocatedCreativity;
        public float allocatedLogic;
        public float allocatedAttunement;
        public float allocatedZeal;
        public int chosenElement;

    
        public GameObject[] pages;
        public int lastPage;
        public int whichPage;
        public int maxPages;
        public GameObject flippingAnim;
        public GameObject flippingAnimBack;

        void Update () {
            allocatedStrength = pages[0].GetComponent<CharacterCreation>().allocatedStrength;
            allocatedDexterity = pages[0].GetComponent<CharacterCreation>().allocatedDexterity;
            allocatedFortitude = pages[0].GetComponent<CharacterCreation>().allocatedFortitude;
            allocatedCharm = pages[0].GetComponent<CharacterCreation>().allocatedCharm;
            allocatedIntelligence = pages[0].GetComponent<CharacterCreation>().allocatedIntelligence;
            allocatedEmpathy = pages[0].GetComponent<CharacterCreation>().allocatedEmpathy;

            acquiredQuirks = pages[1].GetComponent<QuirkHandler>().acquiredQuirks;
            chosenElement = pages[2].GetComponent<RoleplayHandler>().elementChosen;
        }

        public void EnterGame () {
            //Do stuff.
        }

        public void FlipRight () {
            flippingAnim.SetActive(true);
            if (whichPage < maxPages) {
                lastPage = whichPage;
                whichPage++;
            } else {
                lastPage = whichPage;
                whichPage = 0;
            }
            pages[lastPage].SetActive(false);
            Invoke("TurnOffFlip", 0.5f);
        }

        public void FlipLeft () {
            flippingAnimBack.SetActive(true);
            if (whichPage > 0) {
                lastPage = whichPage;
                whichPage--;
            } else {
                lastPage = whichPage;
                whichPage = maxPages;
            }
            pages[lastPage].SetActive(false);
            Invoke("TurnOffFlip", 0.5f);
        }

        public void ActivatePages () {
            pages[whichPage].SetActive(true);
        }

        public void TurnOffFlip () {
            flippingAnimBack.SetActive(false);
            flippingAnim.SetActive(false);
            ActivatePages();
        }

        public void UseCharacterWorkAround () {
            Manager.instance.UsePlayer();
        }

        public static CreationHandler instance;
        void Awake () {
            instance = this;
        }

    }
}
