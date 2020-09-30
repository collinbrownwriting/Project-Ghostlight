using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class RoundController : MonoBehaviour
    {
        public GameObject[] actors;
        public GameObject[] cycledActors;
        //public GameObject[] factions;
        int initiative;
        public bool allAligned;
        public bool firstRound;
        public GameObject activeActor;
        public float topInitiative;
        public bool inCombat;
        public GameObject player;
        public GameObject actionBar;
        public int freeMoves;

        public void Start () {
            actionBar = GameObject.FindGameObjectWithTag("Action Bar");
            actors = GameObject.FindGameObjectsWithTag("Actor");
            foreach (GameObject g in actors){
                if (g.GetComponent<CharacterData>().isPlayer == true){
                    player = g;
                }
            }

            //factions = GameObject.FindGameObjectsWithTag("Faction");
            //foreach (GameObject g in factions){
            //    g.GetComponent<Faction>().StartUpGame();
            //}
        }

        public void Startuptime()
        {
            inCombat = true;
            firstRound = true;
            AlignCheck();
        }

        public void RoundHandler()
        {
            if (activeActor != null) {
                activeActor.GetComponent<PhysicalProperties>().EvaluateBloodStream();
                activeActor.GetComponent<PhysicalProperties>().EvaluateHealth();
            }
            actors = GameObject.FindGameObjectsWithTag("Actor");
            if (player.GetComponent<Controller>().isRoaming == false){

                foreach (GameObject g in actors)
                {
                    if (g != null) {
                        initiative = (Random.Range(0, actors.Length) + g.GetComponent<CharacterData>().initiative);
                        if (topInitiative < initiative && g.GetComponent<Controller>().hasTakenTurn == false){
                            activeActor = g.gameObject;
                        }
                        else {
                            //Do nothing.
                        }
                    }
                }

                if (activeActor != null && activeActor.GetComponent<Controller>().hasTakenTurn == false){
                    activeActor.GetComponent<Controller>().isTurn = true;
                    activeActor.GetComponent<PhysicalProperties>().CarryMomentum();
                    activeActor.GetComponent<PhysicalProperties>().EvaluateWeight();
                    activeActor.GetComponent<PhysicalProperties>().EvaluateTemperature();
                    activeActor.GetComponent<PhysicalProperties>().EvaluatePsychology();
                } else {
                    RoundReset();
                }

                if (activeActor == null){
                    RoundReset();
                }
            } else if (player.GetComponent<Controller>().isRoaming == true) {
                Invoke("RoundHandler", 1.0f);
            }
        }

        public void RoundReset()
        {
            actors = null;
            activeActor = null;

            actors = GameObject.FindGameObjectsWithTag("Actor");
            cycledActors = new GameObject[actors.Length * 4];

            actionBar.GetComponent<ActionBar>().AddAction();

            foreach (GameObject g in actors)
            {
                g.GetComponent<Controller>().hasTakenTurn = false;
            }

            GameObject[] spellEffects = GameObject.FindGameObjectsWithTag("Spell Effect");
            foreach (GameObject g in spellEffects) {
                g.gameObject.GetComponent<RoundWearOff>().RoundCount();
            }

            int f = 0;
            foreach (GameObject g in actors)
            {
                if (g.GetComponent<Controller>().hostile == true){
                    f += 1;
                }
            }
            if (f == 0){
                EndCombat();
            } else if (f > 0){
                RoundHandler();
            }
        }

        public void ArtificialTickUp () {
            freeMoves++;
            if (freeMoves >= CharacterData.instance.moveDistance) {
                ArtificialRound();
                freeMoves = 0;
            }
        }

        public void ArtificialRound () {
            if (inCombat == false && CharacterData.instance.isDead == false) {
                GameObject[] spellEffects = GameObject.FindGameObjectsWithTag("Spell Effect");
                foreach (GameObject g in spellEffects) {
                    g.gameObject.GetComponent<RoundWearOff>().RoundCount();
                }
                foreach (GameObject g in actors) {
                    g.gameObject.GetComponent<PhysicalProperties>().CarryMomentum();
                    g.gameObject.GetComponent<Controller>().hasTakenTurn = false;
                    g.gameObject.GetComponent<Controller>().actionCount = 0;
                    if (g.gameObject.GetComponent<CharacterData>().isPlayer == true) {
                        actionBar.GetComponent<ActionBar>().AddAction();
                    }
                    if (g.GetComponent<Controller>().stunCounter > 0) {
                        g.GetComponent<Controller>().stunCounter--;
                        AnnouncerManager.instance.ReceiveText(g.GetComponent<CharacterData>().charName + " is stunned.", false);
                    }
                    if (g.GetComponent<CharacterData>().bleedCounter > 0) {
                        g.GetComponent<CharacterData>().HoldDamage(1f, (PhysicalProperties.DamageType)4);
                        g.GetComponent<CharacterData>().bleedCounter--;
                    }
                }
            } else if (Controller.instance.isTurn == true) {
                Controller.instance.actionCount++;
            }

            //Anything else that needs to happen when a round is passed outside of combat.
        }

        public void NextRound()
        {
            RoundHandler();
        }
        public void EndCombat()
        {
            inCombat = false;
            Controller.instance.cutRoam = false;
            Controller.instance.freeRoam = true;
        }

        public void AlignCheck ()
        {
            int nopes = 0;
            foreach (GameObject g in actors)
            {
                if (g.GetComponent<CharacterData>().isPlayer == true){
                    g.GetComponent<Controller>().freeRoam = false;
                }
                if (g.GetComponent<Controller>().hasAligned == true)
                {
                    //Do nothing
                }
                else
                {
                    nopes++;
                }
            }

            if (nopes != 0)
            {
                AlignCheck();
            } else
            {
                firstRound = false;
                RoundHandler();
            }
        }

        public static RoundController instance;

        void Awake () {
            instance = this;
        }
    }
}