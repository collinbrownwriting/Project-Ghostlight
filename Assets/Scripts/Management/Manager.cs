using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GridMaster
{
    public class Manager : MonoBehaviour
    {
        public int seed;
        public int gameState;
        public bool devMode;
        public bool initialGeneration;
        public bool randomSeed;
        public int maxYearGen;
        public bool inGameMap;
        public bool renderPixels;
        public int walkType;
        public int hexGridDimensions;
        public int pixelGridDimensions;
        public float sightThreshold;
        public float tileSightThreshold;

        bool menuIsOpen;
        public GameObject menu;
        public GameObject pixelRenderButton;
        public GameObject walkStyleButton;

        public int worldMapViewType;
        public Button reloadButton;
        public Button useWorldButton;
        public GameObject player;
        public bool hasSetUpPlayer;

        public List<FactionHandler.FactionArray> factions = new List<FactionHandler.FactionArray>();
        public List<FactionHandler.FactionArray> livingSchools = new List<FactionHandler.FactionArray>();

        public static Manager instance;
        void Awake () {
            instance = this;
        }

        public void Start () {
            if (devMode != true) {
                seed = PlayerPrefs.GetInt("LastSeed");
            }
            
            if (initialGeneration != true) {
                gameState = PlayerPrefs.GetInt("GameState");
            }
            player = CharacterData.instance.gameObject;

            if (devMode == false) {
                if (gameState == 0) {
                    if (randomSeed == true) {
                        seed = Mathf.RoundToInt(Random.Range(10000, 99999));
                    }
                    MapGeneration.instance.StartUpGame();
                    player.SetActive(false);
                } else if (gameState == 1) {
                    WorldData worldData = SaveSystem.LoadWorld(seed);
                    PrepareFactions(worldData);
                } else if (gameState == 2) {
                    WorldData worldData = SaveSystem.LoadWorld(seed);
                    FactionHandler.instance.LoadWorldFactions(worldData);
                }
            } else {
                if (gameState == 0) {
                    MapGeneration.instance.StartUpGame();
                    player.SetActive(false);
                } else if (gameState == 1) {
                    WorldData worldData = SaveSystem.LoadWorld(seed);
                    PrepareFactions(worldData);
                } else if (gameState == 2) {
                    WorldData worldData = SaveSystem.LoadWorld(seed);
                    FactionHandler.instance.LoadWorldFactions(worldData);
                }
            }
        }

        void Update () {
            if (Input.GetButtonDown("Cancel") && menuIsOpen == false && Controller.instance.grabbingItems == false && Controller.instance.isCasting == false && BookManager.instance.hasStartedUp == false) {
                menuIsOpen = true;
                menu.SetActive(true);
            } else if (Input.GetButtonDown("Cancel") && menuIsOpen == false) {
                menuIsOpen = false;
                menu.SetActive(false);
            }
        }

        public void SwitchRenderingPixels () {
            if (renderPixels == true) {
                renderPixels = false;
                pixelRenderButton.GetComponent<Image>().color = new Color (1.0f, 0.7717767f, 0.7490196f);
            } else if (renderPixels == false) {
                renderPixels = true;
                pixelRenderButton.GetComponent<Image>().color = new Color (0.8181033f, 1f, 0.7490196f);
            }
        }

        public void SwitchWalkingStyles () {
            if (walkType == 1) {
                walkType = 2;
                walkStyleButton.GetComponent<Image>().color = new Color (1.0f, 0.9256178f, 0.7490196f);
            } else if (walkType == 2) {
                walkType = 1;
                walkStyleButton.GetComponent<Image>().color = new Color (0.7512632f, 0.7490196f, 1f);
            }
        }

        public void SwitchToNaturalView () {
            worldMapViewType = 1;
        }
        public void SwitchToNationView () {
            worldMapViewType = 2;
        }
        public void SwitchToMagicView () {
            worldMapViewType = 3;
        }

        public void ResumeGame () {
            menuIsOpen = false;
            menu.SetActive(false);
        }

        public void StartGameOver () {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            //We'll add "new game" type stuff here later.
        }

        public void QuitGame () {
            Debug.Log("The game is ending.");
            //We'll add saving and all that later.
            Application.Quit();
        }

        public void UseWorld () {
            SaveSystem.SaveWorld(seed, MapGeneration.instance, FactionHandler.instance, SpellGeneration.instance, LoreHandler.instance);
            PlayerPrefs.SetInt("LastSeed", seed);
            PlayerPrefs.SetInt("GameState", 1);
            SceneManager.LoadScene("CharacterCreation");
        }

        public void ReloadScene () {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void PrepareFactions (WorldData worldData) {
            foreach (WorldData.DummyFaction f in worldData.livingSchools) {
                if ((int)f.schoolType == 1) {
                    FactionHandler.FactionArray tempSchool = new FactionHandler.FactionArray();
                    tempSchool.schoolType = (FactionHandler.SchoolType)1;
                    tempSchool.factionType = (FactionHandler.FactionType)1;
                    tempSchool.factionID = f.factionID;
                    tempSchool.overallStrength = f.overallStrength;
                    tempSchool.knownSpells = f.knownSpells;
                    tempSchool.spellSystemEffect = (SpellEffect)f.spellSystemEffect;
                    tempSchool.primaryElementAssociation = (PixelType)f.primaryElementAssociation;
                    tempSchool.secondaryElementAssociation = (PixelType)f.secondaryElementAssociation;
                    livingSchools.Add(tempSchool);
                }
            }
        }

        public void UsePlayer () {
            float tempStrength = -10000;
            FactionHandler.FactionArray startingFaction = null;
            foreach (FactionHandler.FactionArray f in livingSchools) {
                if ((int)f.spellSystemEffect == CreationHandler.instance.chosenElement && f.overallStrength > tempStrength && (int)f.schoolType == 1) {
                    tempStrength = f.overallStrength;
                    startingFaction = f;
                }
            }
            if (startingFaction != null) {
                player.GetComponent<CharacterData>().factionID = startingFaction.factionID;
            } else {
                tempStrength = -10000;
                foreach (FactionHandler.FactionArray f in livingSchools) {
                    if (f.overallStrength > tempStrength && (int)f.factionType == 1) {
                        tempStrength = f.overallStrength;
                        startingFaction = f;
                    }
                }
            }
            if (startingFaction != null) {
                player.GetComponent<CharacterData>().factionID = startingFaction.factionID;
            } else {
                player.GetComponent<CharacterData>().factionID = 8;
            }

            player.GetComponent<CharacterData>().factionID = startingFaction.factionID;
            for (int i = 1; i <= 5; i++) {
                player.GetComponent<CharacterData>().knownSpells.Add(startingFaction.knownSpells[i]);
            }

            player.GetComponent<CharacterData>().charName = CreationHandler.instance.charName;
            player.GetComponent<CharacterData>().alignMod = new Vector3 (0.005f, 0.1f, 0f);

            player.GetComponent<CharacterData>().strength = CreationHandler.instance.allocatedStrength;
            player.GetComponent<CharacterData>().dexterity = CreationHandler.instance.allocatedDexterity;
            player.GetComponent<CharacterData>().fortitude = CreationHandler.instance.allocatedFortitude;
            player.GetComponent<CharacterData>().charm = CreationHandler.instance.allocatedCharm;
            player.GetComponent<CharacterData>().intelligence = CreationHandler.instance.allocatedIntelligence;
            player.GetComponent<CharacterData>().empathy = CreationHandler.instance.allocatedEmpathy;

            player.GetComponent<CharacterData>().martial = CreationHandler.instance.allocatedMartial;
            player.GetComponent<CharacterData>().athletics = CreationHandler.instance.allocatedAthletics;
            player.GetComponent<CharacterData>().evasion = CreationHandler.instance.allocatedEvasion;
            player.GetComponent<CharacterData>().stealth = CreationHandler.instance.allocatedStealth;
            player.GetComponent<CharacterData>().endurance = CreationHandler.instance.allocatedEndurance;
            player.GetComponent<CharacterData>().regeneration = CreationHandler.instance.allocatedRegeneration;

            player.GetComponent<CharacterData>().persuasion = CreationHandler.instance.allocatedPersuasion;
            player.GetComponent<CharacterData>().ego = CreationHandler.instance.allocatedEgo;
            player.GetComponent<CharacterData>().creativity = CreationHandler.instance.allocatedCreativity;
            player.GetComponent<CharacterData>().logic = CreationHandler.instance.allocatedLogic;
            player.GetComponent<CharacterData>().attunement = CreationHandler.instance.allocatedAttunement;
            player.GetComponent<CharacterData>().zeal = CreationHandler.instance.allocatedZeal;

            player.GetComponent<CharacterData>().maxHealth = player.GetComponent<CharacterData>().endurance * 2;
            player.GetComponent<CharacterData>().health = player.GetComponent<CharacterData>().endurance * 2;

            player.GetComponent<CharacterData>().acquiredQuirks = CreationHandler.instance.acquiredQuirks;

            foreach (QuirkSlot.Quirk q in CreationHandler.instance.acquiredQuirks) {
                if (q.quirkID == 2) {
                    player.GetComponent<CharacterData>().missRightArm = true;
                }
            }

            SaveSystem.SavePlayer(seed, player);
            PlayerPrefs.SetInt("GameState", 2);
            SceneManager.LoadScene("SampleScene");
        }

        public void SetUpPlayer (PlayerData playerData) {
            player.GetComponent<CharacterData>().charName = playerData.name;
            player.GetComponent<CharacterData>().factionID = playerData.factionID;
            foreach (FactionHandler.FactionArray f in FactionHandler.instance.factionArray) {
                if (player.GetComponent<CharacterData>().factionID == f.factionID) {
                    player.GetComponent<CharacterData>().factionArray = f;
                }
            }
            if (player.GetComponent<CharacterData>().factionArray.factionID == 0) {
                Debug.LogError("The player faction returned null.");
            }

            player.GetComponent<CharacterData>().knownSpells = playerData.knownSpells;

            BookManager.instance.spells = playerData.knownSpells;

            player.GetComponent<CharacterData>().alignMod = new Vector3 (playerData.alignMod[0], playerData.alignMod[1], 0f);

            player.GetComponent<CharacterData>().baseStrength = playerData.strength;
            player.GetComponent<CharacterData>().baseDexterity = playerData.dexterity;
            player.GetComponent<CharacterData>().baseFortitude = playerData.fortitude;
            player.GetComponent<CharacterData>().baseCharm = playerData.charm;
            player.GetComponent<CharacterData>().baseIntelligence = playerData.intelligence;
            player.GetComponent<CharacterData>().baseEmpathy = playerData.empathy;

            player.GetComponent<CharacterData>().baseMartial = playerData.martial;
            player.GetComponent<CharacterData>().baseAthletics = playerData.athletics;
            player.GetComponent<CharacterData>().baseEvasion = playerData.evasion;
            player.GetComponent<CharacterData>().baseStealth = playerData.stealth;
            player.GetComponent<CharacterData>().baseEndurance = playerData.endurance;
            player.GetComponent<CharacterData>().baseRegeneration = playerData.regeneration;

            player.GetComponent<CharacterData>().basePersuasion = playerData.persuasion;
            player.GetComponent<CharacterData>().baseEgo = playerData.ego;
            player.GetComponent<CharacterData>().baseCreativity = playerData.creativity;
            player.GetComponent<CharacterData>().baseLogic = playerData.logic;
            player.GetComponent<CharacterData>().baseAttunement = playerData.attunement;
            player.GetComponent<CharacterData>().baseZeal = playerData.zeal;

            player.GetComponent<CharacterData>().acquiredQuirks = playerData.acquiredQuirks;
            player.GetComponent<CharacterData>().ghostlightHeld = playerData.ghostlightHeld;
            player.GetComponent<CharacterData>().isDead = playerData.isDead;

            player.GetComponent<CharacterData>().hasLostLimb = playerData.hasLostLimb;
            player.GetComponent<CharacterData>().missLeftArm = playerData.missLeftArm;
            player.GetComponent<CharacterData>().missRightArm = playerData.missRightArm;

            //These are temporary ways to calculate these variables with what is currently in the game.
            //These will change once the systems are more robust.
            player.GetComponent<CharacterData>().width = 1;
            player.GetComponent<CharacterData>().maxActionCount = Mathf.RoundToInt(player.GetComponent<CharacterData>().athletics / 2);
            if (player.GetComponent<CharacterData>().maxActionCount < 1) {
                player.GetComponent<CharacterData>().maxActionCount = 1;
            }

            player.GetComponent<Controller>().maxActionCount = player.GetComponent<CharacterData>().maxActionCount;
            player.GetComponent<CharacterData>().maxHealth = player.GetComponent<CharacterData>().endurance * 2;
            player.GetComponent<CharacterData>().health = playerData.health;

            player.GetComponent<CharacterData>().maxInventory = 30;
            player.GetComponent<CharacterData>().maxWeaponSlots = 1;
            player.GetComponent<CharacterData>().maxPotionSlots = 3;
            player.GetComponent<CharacterData>().maxComponentSlots = 20;
            player.GetComponent<CharacterData>().maxBulkySlots = 1;
            
            foreach (QuirkSlot.Quirk q in player.GetComponent<CharacterData>().acquiredQuirks) {
                if (q.quirkID == 5) {
                    player.GetComponent<CharacterData>().maxWeaponSlots += 1;
                    player.GetComponent<CharacterData>().maxPotionSlots += 1;
                    player.GetComponent<CharacterData>().maxComponentSlots += 10;
                    player.GetComponent<CharacterData>().maxBulkySlots += 1;
                }
            }

            if (player.GetComponent<CharacterData>().missRightArm != true) {
                player.GetComponent<CharacterData>().canAttack = true;
            }
            if (player.GetComponent<CharacterData>().missLeftArm != true) {
                player.GetComponent<CharacterData>().canCast = true;
            }
            LoadingHandler.instance.StartUpGame();
            ActionBar.instance.StartUpGame();

            hasSetUpPlayer = true;
            GameObject[] actors = GameObject.FindGameObjectsWithTag("Actor");
            foreach (GameObject g in actors) {
                if (g.GetComponent<PhysicalProperties>() != null) {
                    g.GetComponent<PhysicalProperties>().InitiateRelationships();
                }
            }
            HealthBar.instance.CheckHealth();
        }
    }
}
