using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    [System.Serializable]
    public class PlayerData
    {
        public string name;
        public int factionID;

        public List<QuirkSlot.Quirk> acquiredQuirks;
        public List<Spell> knownSpells;
        public int strength;
        public int dexterity;
        public int fortitude;
        public int charm;
        public int intelligence;
        public int empathy;

        public float martial;
        public float athletics;
        public float evasion;
        public float stealth;
        public float endurance;
        public float regeneration;
        public float persuasion;
        public float ego;
        public float creativity;
        public float logic;
        public float attunement;
        public float zeal;

        public float[] alignMod;
        public float health;

        public float ghostlightHeld;
        public bool isDead;
        public bool hasLostLimb;
        public bool missLeftArm;
        public bool missRightArm;

        public PlayerData (GameObject player) {
            name = player.GetComponent<CharacterData>().charName;
            factionID = player.GetComponent<CharacterData>().factionID;

            acquiredQuirks = new List<QuirkSlot.Quirk>();
            knownSpells = new List<Spell>();
            acquiredQuirks = player.GetComponent<CharacterData>().acquiredQuirks;
            knownSpells = player.GetComponent<CharacterData>().knownSpells;

            strength = Mathf.RoundToInt(player.GetComponent<CharacterData>().strength);
            dexterity = Mathf.RoundToInt(player.GetComponent<CharacterData>().dexterity);
            fortitude = Mathf.RoundToInt(player.GetComponent<CharacterData>().fortitude);
            charm = Mathf.RoundToInt(player.GetComponent<CharacterData>().charm);
            intelligence = Mathf.RoundToInt(player.GetComponent<CharacterData>().intelligence);
            empathy = Mathf.RoundToInt(player.GetComponent<CharacterData>().empathy);

            martial = Mathf.RoundToInt(player.GetComponent<CharacterData>().martial);
            athletics = Mathf.RoundToInt(player.GetComponent<CharacterData>().athletics);
            evasion = Mathf.RoundToInt(player.GetComponent<CharacterData>().evasion);
            stealth = Mathf.RoundToInt(player.GetComponent<CharacterData>().stealth);
            endurance = Mathf.RoundToInt(player.GetComponent<CharacterData>().endurance);
            regeneration = Mathf.RoundToInt(player.GetComponent<CharacterData>().regeneration);
            persuasion = Mathf.RoundToInt(player.GetComponent<CharacterData>().persuasion);
            ego = Mathf.RoundToInt(player.GetComponent<CharacterData>().ego);
            creativity = Mathf.RoundToInt(player.GetComponent<CharacterData>().creativity);
            logic = Mathf.RoundToInt(player.GetComponent<CharacterData>().logic);
            attunement = Mathf.RoundToInt(player.GetComponent<CharacterData>().attunement);
            zeal = Mathf.RoundToInt(player.GetComponent<CharacterData>().zeal);

            alignMod = new float[2];

            alignMod[0] = player.GetComponent<CharacterData>().alignMod.x;
            alignMod[1] = player.GetComponent<CharacterData>().alignMod.y;
            
            health = player.GetComponent<CharacterData>().health;
            ghostlightHeld = player.GetComponent<CharacterData>().ghostlightHeld;
            isDead = player.GetComponent<CharacterData>().isDead;

            hasLostLimb = player.GetComponent<CharacterData>().hasLostLimb;
            missLeftArm = player.GetComponent<CharacterData>().missLeftArm;
            missRightArm = player.GetComponent<CharacterData>().missRightArm;
        }
    }
}
