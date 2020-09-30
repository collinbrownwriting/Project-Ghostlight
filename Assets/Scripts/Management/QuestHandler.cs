using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


namespace GridMaster {
    public class QuestHandler : MonoBehaviour
    {
        public Text textHist;
        public Text sideText;
        public InputField writtenNote;

        public List<string> announcements = new List<string>();

        public static QuestHandler instance;

        void Start () {
            if (Manager.instance.inGameMap == true) {
                textHist.text = textHist.text + "This is your quest log." + Environment.NewLine + "Here you'll see past deeds," + Environment.NewLine + "as well as future options.";
                sideText.text = sideText.text + "Here are your personal notes." + Environment.NewLine + "You can write notes to yourself in here.";
            }
        }

        public void ReceiveText (string freshmessage, bool personalNote) {
            announcements.Add(freshmessage);
            if (personalNote == false) {
                textHist.text = textHist.text + Environment.NewLine + Environment.NewLine + freshmessage;
            } else {
                sideText.text = sideText.text + Environment.NewLine + Environment.NewLine + freshmessage;
            }
        }

        public void AddNote () {
            ReceiveText(writtenNote.text, true);
            writtenNote.text = " ";
        }

        void Awake () {
            instance = this;
        }
    }
}
