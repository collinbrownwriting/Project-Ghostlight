using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class ActionBar : MonoBehaviour
    {
        public GameObject[] actors;
        public GameObject player;
        public GameObject[] runes;

        // Start is called before the first frame update
        public void StartUpGame()
        {
            actors = GameObject.FindGameObjectsWithTag("Actor");
            runes = GameObject.FindGameObjectsWithTag("Rune");


            foreach (GameObject g in actors)
            {
                if (g.GetComponent<CharacterData>().isPlayer == true)
                {
                    player = g;
                }
            }

            foreach (GameObject g in runes)
            {
                if (g.GetComponent<Attributes>().actionNumber > player.GetComponent<CharacterData>().maxActionCount)
                {
                    g.SetActive(false);
                }
            }
        }

        public void CutAction()
        {
            foreach (GameObject g in runes)
            {
                if ( g.GetComponent<Attributes>().actionNumber > (player.GetComponent<CharacterData>().maxActionCount - player.GetComponent<Controller>().actionCount))
                {
                    g.SetActive(false);
                }
            }
        }

        public void AddAction()
        {
            foreach (GameObject g in runes)
            {
                if ( g.GetComponent<Attributes>().actionNumber <= (player.GetComponent<CharacterData>().maxActionCount - player.GetComponent<Controller>().actionCount))
                {
                    g.SetActive(true);
                }
            }
        }

        public static ActionBar instance;
        public static ActionBar GetInstance()
        {
            return instance;
        }
        void Awake () {
            instance = this;
        }
    }
}
