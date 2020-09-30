using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridMaster {
    public class BookManager : MonoBehaviour
    {
        public GameObject[] pages;
        public bool hasStartedUp;
        public bool hasAligned;
        public bool unAlign;
        Vector3 target;
        public int lastPage;

        public int whichPage;
        public int maxPages;
        public GameObject flippingAnim;
        public GameObject flippingAnimBack;

        public List<Spell> spells = new List<Spell>();
        
        public void StartingUp () {
            spells = CharacterData.instance.knownSpells;
            SpellbookUI.instance.UpdateUI();

            whichPage = 0;
            hasAligned = false;
            this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3 (250f, -1000f, 0f);
            target = new Vector3 (250f, -100f, 0f);

            Controller.instance.bookOpen = true;
            hasStartedUp = true;
        }

        void Update () {
            if (hasStartedUp == true) {
                if (hasAligned == false) {
                    this.gameObject.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(this.gameObject.GetComponent<RectTransform>().anchoredPosition, target, Time.deltaTime * 2);

                    if (Vector3.Distance(this.gameObject.GetComponent<RectTransform>().anchoredPosition, target) <= 10f) {
                        this.gameObject.GetComponent<RectTransform>().anchoredPosition = target;
                        hasAligned = true;
                    }
                }

                if (Input.GetButtonDown("Cancel") && hasAligned == true) {
                    target = new Vector3 (250f, -1000f, 0f);
                    hasAligned = false;

                    Invoke ("CloseBook", 2f);
                }
            }
        }

        public void CloseBook () {
            foreach (GameObject g in pages) {
                if (g != pages[0]) {
                    g.SetActive(false);
                } else {
                    g.SetActive(true);
                }
            }
            whichPage = 0;
            Controller.instance.bookOpen = false;
            hasAligned = true;
            hasStartedUp = false;
        }

        public void FlipRight () {
            this.gameObject.GetComponent<Image>().enabled = false;
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
            this.gameObject.GetComponent<Image>().enabled = false;
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
            this.gameObject.GetComponent<Image>().enabled = true;
            pages[whichPage].SetActive(true);
        }

        public void TurnOffFlip () {
            flippingAnimBack.SetActive(false);
            flippingAnim.SetActive(false);
            ActivatePages();
        }

        public static BookManager instance;
        public static BookManager GetInstance()
        {
            return instance;
        }

        void Awake()
        {
            instance = this;
        }

    }
}
