using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace GridMaster {
    public class AnnouncerManager : MonoBehaviour
    {
        public bool isActive;
        public Text textHist;
        public GameObject textBox;
        public GameObject scrollView;
        public GameObject content;

        public List<string> announcements = new List<string>();

        public static AnnouncerManager instance;
        public float fadeOutOffset;
        public float fadeOutTime;
        float lastTime;
        public bool fadingOut;
        bool testBool;
        Color originalColor;

        void Start () {
            if (Manager.instance.inGameMap == false) {
                textBox.GetComponent<RectTransform>().sizeDelta = new Vector2 (300f, 500f);
                scrollView.GetComponent<RectTransform>().sizeDelta = new Vector2 (275f, 450f);
                textHist.gameObject.GetComponent<RectTransform>().localScale = new Vector3 (0.5f, 0.5f, 1f);
                content.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3 (5f, 20f, 0f);
            } else {
                //Don't change anything.
            }

            originalColor = textHist.color;
            StartCoroutine(TextCrawl());
        }

        private IEnumerator TextCrawl () {
            if (Manager.instance.inGameMap == true) {
                ReceiveText("Welcome to Project Ghostlight." + Environment.NewLine + "This is a work in progress." + Environment.NewLine + "I hope you enjoy it.", true);
            }
            yield return null;
        }

        public void ReceiveText (string freshmessage, bool isImportant) {
            if (isImportant == true) {
                textBox.SetActive(true);
                lastTime = Time.fixedTime;
                fadingOut = false;
                textHist.color = originalColor;
            }
            announcements.Add(freshmessage);
            textHist.text = textHist.text + Environment.NewLine + Environment.NewLine + freshmessage;
        }

        void Awake () {
            instance = this;
        }

        void Update () {
            if (Time.fixedTime >= lastTime + fadeOutOffset && fadingOut == false && Manager.instance.initialGeneration == false) {
                lastTime = Time.fixedTime;
                fadingOut = true;
                StartCoroutine(FadeOutRoutine());
            }

            if (Input.GetButtonDown("Hide Announcements")  && BookManager.instance.hasStartedUp == false && Manager.instance.initialGeneration == false) {
                if (fadingOut == true) {
                    textBox.SetActive(true);
                    lastTime = Time.fixedTime;
                    fadingOut = false;
                    textHist.color = originalColor;
                } else {
                    lastTime = Time.fixedTime;
                    fadingOut = true;
                    textHist.color = Color.clear;
                    textBox.SetActive(false);
                }
            }
        }

        private IEnumerator FadeOutRoutine () {
            Color originalColor = textHist.color;
            if (fadingOut == true && textHist != null) {
                for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime) {
                    textHist.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t/fadeOutTime));
                    yield return null;
                }
                textBox.SetActive(false);
            }
        }
    }
}
