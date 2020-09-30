using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridMaster
{
    public class CharacterData : MonoBehaviour
    {
        public static CharacterData instance;

        public string charName;
        public List<QuirkSlot.Quirk> acquiredQuirks;
        public int factionID;
        public FactionHandler.FactionArray factionArray;
        public List<Spell> knownSpells;
        public Vector3 alignMod;
        public bool isPlayer;
        public float sightDist;
        public float baseSight;
        public float sightMod;
        public bool canSee;
        public float speed;
        public int baseMoveDist;
        public int moveDistMod;
        public int moveDistance;
        public float health;
        public float maxHealth;

        public int bleedCounter;
        public float toxinCounter;

        public List<SpellOverload> spellOverloads = new List<SpellOverload>();

        [System.Serializable]
        public class SpellOverload {
            public int overloadCounter;
            public PixelType overloadType;
        }

        public List<Curse> curses = new List<Curse>();

        [System.Serializable]
        public class Curse {
            public int curseCounter;
            public CurseType curseType;

            //Death will be nullified by killing another character, but for now it doesn't do anything.

            public enum CurseType { damage, slowness, death };
        }


        public float attack;
        public PhysicalProperties.DamageType activeDamageType;
        public float range;
        public int width;
        public int maxActionCount;
        public int maxInventory;
        public int maxWeaponSlots;
        public int weaponsHeld;
        public int maxPotionSlots;
        public int potionsHeld;
        public int maxComponentSlots;
        public int componentsHeld;
        public int maxBulkySlots;
        public int bulkyHeld;

        //Component Counts
        public float ghostlightHeld;
        public float oilHeld;
        public float bloodHeld;
        public float inkHeld;
        public float iceHeld;
        public GameObject ghostlight;


        public int initiative;
        public bool canAttack;
        public bool canCast;
        public float hostileDist;
        public float damageTook;
        public PhysicalProperties.DamageType damageType;
        public bool tookDamage;
        public bool isDead;
        public bool isUnconscious;
        public float damageToTake;

        
        [HideInInspector] public bool isDisplaying;

        //Stats for character creation & abilities.
        public float strength;
        public float dexterity;
        public float fortitude;
        public float charm;
        public float intelligence;
        public float empathy;
        public float baseStrength;
        public float baseDexterity;
        public float baseFortitude;
        public float baseCharm;
        public float baseIntelligence;
        public float baseEmpathy;
        public float strengthMod;
        public float dexterityMod;
        public float fortitudeMod;
        public float charmMod;
        public float intelligenceMod;
        public float empathyMod;

        //Abilities & Skills
        public float baseMartial;
        public float baseAthletics;
        public float baseEvasion;
        public float baseStealth;
        public float baseEndurance;
        public float baseRegeneration;
        public float basePersuasion;
        public float baseEgo;
        public float baseCreativity;
        public float baseLogic;
        public float baseAttunement;
        public float baseZeal;
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
        public float martialMod;
        public float athleticsMod;
        public float evasionMod;
        public float stealthMod;
        public float enduranceMod;
        public float regenerationMod;
        public float persuasionMod;
        public float egoMod;
        public float creativityMod;
        public float logicMod;
        public float attunementMod;
        public float zealMod;

        //Mental Stats
        public float sanity;

        
        public GameObject spriteMask;
        public Material stand;
        public Material outline;

        //Missing Limbs
        public bool canLoseLimbs;
        public bool hasLostLimb;
        public GameObject armPrefab;
        public GameObject legPrefab;
        public bool missLeftArm;
        public AnimatorOverrideController missingLeftArm;
        public bool missRightArm;
        public AnimatorOverrideController missingRightArm;
        // public bool missLeftLeg;
        // public AnimatorOverrideController missingLeftLeg;
        // public bool missRightLeg;
        // public AnimatorOverrideController missingRightLeg;

        void Awake () {
            if (isPlayer == true){
                instance = this;
            }
            baseMoveDist = moveDistance;
        }

        public void HoldDamage (float i, PhysicalProperties.DamageType dType) {
            damageToTake = i;
            damageType = dType;
            Invoke("TakeDamage", 0.25f);
        }

        public void TakeDamage(){
            damageTook = (damageToTake - (endurance * 0.2f));
            if (damageTook > 0) {
                tookDamage = true;
    
                this.gameObject.GetComponent<PhysicalProperties>().TakeBioDamage(damageTook, damageType);
                Invoke("TurnOffDamaged", 0.1f);
            } else {
                AnnouncerManager.instance.ReceiveText("The attack does not harm " + charName, false);
            }

            if (health <= 0){
                if (isPlayer == true) {
                    AnnouncerManager.instance.ReceiveText("You are dead.", true);
                } else {
                    AnnouncerManager.instance.ReceiveText(charName + " is dead.", true);
                    //Spawn the corpse here.
                    isDead = true;
                    Invoke("DestroyCharacter", 5f);
                }
                isDead = true;
            }
        }

        void DestroyCharacter () {
            Destroy(this.gameObject);
        }

        public void TurnOffDamaged () {
            tookDamage = false;
        }
        public void RemoveLimb(int limb) {
            if (limb >= 1 && limb <= 49) {
                hasLostLimb = true;
                missRightArm = true;
                if (isPlayer == true) {
                    EquipmentRenderer.instance.armEquip.GetComponent<Animator>().runtimeAnimatorController = missingLeftArm;
                } else {
                    this.gameObject.GetComponent<NPCEquipment>().armEquip.GetComponent<Animator>().runtimeAnimatorController = missingLeftArm;
                }
                GameObject offArm = Instantiate(armPrefab);
                offArm.transform.position = this.transform.position;
                offArm.GetComponent<Rigidbody2D>().AddForce(new Vector3 (Random.Range(50,100f), Random.Range(50,100f), 0f));
                canCast = false;
            } else if (limb > 50 && limb <= 100) {
                missRightArm = true;
                hasLostLimb = true;
                if (isPlayer == true) {
                    if (EquipmentRenderer.instance.equippedWeapon != null) {
                        GameObject equippedWeapon = EquipmentRenderer.instance.equippedWeapon;
                        EquipmentRenderer.instance.Unequip(4, equippedWeapon);

                        Inventory.instance.bagItems.Remove(equippedWeapon);
                        equippedWeapon.transform.parent = null;
                        equippedWeapon.GetComponent<SpriteRenderer>().enabled = true;
                    }


                    EquipmentRenderer.instance.armEquip.GetComponent<Animator>().runtimeAnimatorController = missingRightArm;
                } else {
                    if (this.gameObject.GetComponent<NPCEquipment>().equippedWeapon != null) {
                        GameObject equippedWeapon = this.gameObject.GetComponent<NPCEquipment>().equippedWeapon;
                        this.gameObject.GetComponent<NPCEquipment>().Unequip(4, equippedWeapon);
                        equippedWeapon.transform.parent = null;
                        equippedWeapon.GetComponent<SpriteRenderer>().enabled = true;
                        this.gameObject.GetComponent<NPCEquipment>().armEquip.GetComponent<Animator>().runtimeAnimatorController = missingRightArm;
                    }
                }
                GameObject offArm = Instantiate(armPrefab);
                offArm.transform.position = this.transform.position;
                offArm.GetComponent<Rigidbody2D>().AddForce(new Vector3 (Random.Range(50,100f), Random.Range(50,100f), 0f));
                canAttack = false;
            }
            //if (limb == 3) {
            //    missLeftLeg = true;
            //    this.gameObject.GetComponent<Animator>().runtimeAnimatorController = missingLeftLeg;
            //}
            //if (limb == 4) {
            //    missRightLeg = true;
            //    this.gameObject.GetComponent<Animator>().runtimeAnimatorController = missingRightLeg;
            //}
        }

        void Update ()
        {
            if (isPlayer == true && Manager.instance.inGameMap == true) {
                spriteMask.gameObject.transform.localScale = new Vector3(sightDist * 0.1f, sightDist * 0.1f, 1f);
            }

            moveDistance = baseMoveDist + moveDistMod;

            strength = strengthMod + baseStrength;
            dexterity = dexterityMod + baseDexterity;
            fortitude = fortitudeMod + baseFortitude;
            charm = charmMod + baseCharm;
            intelligence = intelligenceMod + baseIntelligence;
            empathy = empathyMod + baseEmpathy;

            martial = martialMod + baseMartial;
            athletics = athleticsMod + baseAthletics;
            evasion = evasionMod + baseEvasion;
            stealth = stealthMod + baseStealth;
            endurance = enduranceMod + baseEndurance;
            regeneration = regenerationMod + baseRegeneration;
            persuasion = persuasionMod + basePersuasion;
            ego = egoMod + baseEgo;
            creativity = creativityMod + baseCreativity;
            logic = logicMod + baseLogic;
            attunement = attunementMod + baseAttunement;
            zeal = zealMod + baseZeal;

            sightDist = baseSight + sightMod;
            if (this.gameObject.GetComponent<PhysicalProperties>() != null) {
                health = this.gameObject.GetComponent<PhysicalProperties>().animate.overallHealth;
            }
        }
    }
}