using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class FactionHandler : MonoBehaviour
    {
        
        public List<FactionArray> factionArray = new List<FactionArray>();
        public List<FactionArray> livingSchools = new List<FactionArray>();
        public List<FactionArray> deadSchools = new List<FactionArray>();
        public List<FactionArray> remainingFactions = new List<FactionArray>();
        public bool hasSetUp;

        public FactionArray CallFactionArray(int factID) {
            FactionArray factArray = factionArray[factID];
            return factArray;
        }

        public void LoadWorldFactions (WorldData worldData) {
            foreach (WorldData.DummyFaction f in worldData.factions) {
                FactionArray newFaction = new FactionArray();

                newFaction.factionName = f.factionName;
                newFaction.factionID = f.factionID;
                newFaction.factionType = f.factionType;
                newFaction.schoolType = f.schoolType;
                newFaction.baseStrength = f.baseStrength;
                newFaction.overallStrength = f.overallStrength;
                newFaction.genHostility = f.genHostility;
                newFaction.startTileX = f.startTileX;
                newFaction.startTileY = f.startTileY;
                newFaction.subservient = f.subservient;
                newFaction.spellSystemEffect = (SpellEffect)f.spellSystemEffect;
                newFaction.primaryElementAssociation = (PixelType)f.primaryElementAssociation;
                newFaction.secondaryElementAssociation = (PixelType)f.secondaryElementAssociation;
                newFaction.knownSpells = f.knownSpells;
                newFaction.willAct = f.willAct;
                newFaction.recentlyRessurected = f.recentlyRessurected;
                newFaction.resurgenceCounter = f.resurgenceCounter;
                newFaction.relationships = f.relationships;

                factionArray.Add(newFaction);
            }

            foreach (WorldData.DummyFaction f in worldData.remainingFactions) {
                FactionArray newFaction = new FactionArray();

                newFaction.factionName = f.factionName;
                newFaction.factionID = f.factionID;
                newFaction.factionType = f.factionType;
                newFaction.schoolType = f.schoolType;
                newFaction.baseStrength = f.baseStrength;
                newFaction.overallStrength = f.overallStrength;
                newFaction.genHostility = f.genHostility;
                newFaction.startTileX = f.startTileX;
                newFaction.startTileY = f.startTileY;
                newFaction.subservient = f.subservient;
                newFaction.spellSystemEffect = (SpellEffect)f.spellSystemEffect;
                newFaction.primaryElementAssociation = (PixelType)f.primaryElementAssociation;
                newFaction.secondaryElementAssociation = (PixelType)f.secondaryElementAssociation;
                newFaction.knownSpells = f.knownSpells;
                newFaction.willAct = f.willAct;
                newFaction.recentlyRessurected = f.recentlyRessurected;
                newFaction.resurgenceCounter = f.resurgenceCounter;
                newFaction.relationships = f.relationships;

                remainingFactions.Add(newFaction);
            }

            foreach (WorldData.DummyFaction f in worldData.livingSchools) {
                FactionArray newFaction = new FactionArray();

                newFaction.factionName = f.factionName;
                newFaction.factionID = f.factionID;
                newFaction.factionType = f.factionType;
                newFaction.schoolType = f.schoolType;
                newFaction.baseStrength = f.baseStrength;
                newFaction.overallStrength = f.overallStrength;
                newFaction.genHostility = f.genHostility;
                newFaction.startTileX = f.startTileX;
                newFaction.startTileY = f.startTileY;
                newFaction.subservient = f.subservient;
                newFaction.spellSystemEffect = (SpellEffect)f.spellSystemEffect;
                newFaction.primaryElementAssociation = (PixelType)f.primaryElementAssociation;
                newFaction.secondaryElementAssociation = (PixelType)f.secondaryElementAssociation;
                newFaction.knownSpells = f.knownSpells;
                newFaction.willAct = f.willAct;
                newFaction.recentlyRessurected = f.recentlyRessurected;
                newFaction.resurgenceCounter = f.resurgenceCounter;
                newFaction.relationships = f.relationships;

                livingSchools.Add(newFaction);
            }

            foreach (WorldData.DummyFaction f in worldData.deadSchools) {
                FactionArray newFaction = new FactionArray();

                newFaction.factionName = f.factionName;
                newFaction.factionID = f.factionID;
                newFaction.factionType = f.factionType;
                newFaction.schoolType = f.schoolType;
                newFaction.baseStrength = f.baseStrength;
                newFaction.overallStrength = f.overallStrength;
                newFaction.genHostility = f.genHostility;
                newFaction.startTileX = f.startTileX;
                newFaction.startTileY = f.startTileY;
                newFaction.subservient = f.subservient;
                newFaction.spellSystemEffect = (SpellEffect)f.spellSystemEffect;
                newFaction.primaryElementAssociation = (PixelType)f.primaryElementAssociation;
                newFaction.secondaryElementAssociation = (PixelType)f.secondaryElementAssociation;
                newFaction.knownSpells = f.knownSpells;
                newFaction.willAct = f.willAct;
                newFaction.recentlyRessurected = f.recentlyRessurected;
                newFaction.resurgenceCounter = f.resurgenceCounter;
                newFaction.relationships = f.relationships;

                deadSchools.Add(newFaction);
            }

            GameObject[] actors = GameObject.FindGameObjectsWithTag("Actor");
            foreach (GameObject g in actors) {
                foreach (FactionArray f in factionArray) {
                    if (f.factionID == g.GetComponent<CharacterData>().factionID) {
                        g.GetComponent<CharacterData>().factionArray = f;
                    }
                }
            }

            hasSetUp = true;
            PlayerData playerData = SaveSystem.LoadPlayer(Manager.instance.seed);
            Manager.instance.SetUpPlayer(playerData);
        }

        public static FactionHandler instance;
        void Awake () {
            instance = this;
        }

        public void StartUpGame () {

            for (int i = 1; i <= System.Enum.GetNames(typeof(PixelType)).Length * 2 - 2; i++) {
                FactionArray newFaction = new FactionArray();
                if (i <= System.Enum.GetNames(typeof(PixelType)).Length - 1) {
                    newFaction.primaryElementAssociation = (PixelType)i;
                } else {
                    newFaction.primaryElementAssociation = (PixelType)i - System.Enum.GetNames(typeof(PixelType)).Length;
                }
                int secondaryTemp = Mathf.RoundToInt(Random.Range(1, System.Enum.GetNames(typeof(PixelType)).Length - 1));
                if (secondaryTemp != i) {
                    newFaction.secondaryElementAssociation = (PixelType)secondaryTemp;
                } else {
                    newFaction.secondaryElementAssociation = (PixelType)Mathf.RoundToInt(Random.Range(1, System.Enum.GetNames(typeof(PixelType)).Length - 1));
                }
                newFaction.factionType = (FactionType)1;
                newFaction.factionName = "School of " + newFaction.primaryElementAssociation + " and " + newFaction.secondaryElementAssociation;
                newFaction.factionID = factionArray.Count;
                
                newFaction.culture = 4;

                int schoolType = Random.Range(1, 100);
                if (schoolType <= 50) {
                    newFaction.schoolType = (SchoolType)0;
                    newFaction.genHostility = -25f;
                    newFaction.baseStrength = 150;
                } else {
                    newFaction.schoolType = (SchoolType)1;
                    newFaction.genHostility = 75f;
                }

                Vector2 newVector2 = GenerateStartCoords();

                newFaction.startTileX = Mathf.RoundToInt(newVector2.x);
                newFaction.startTileY = Mathf.RoundToInt(newVector2.y);

                factionArray.Add(newFaction);
            }
            
            foreach (FactionArray p in factionArray) {
                GameObject[] actors;
                actors = GameObject.FindGameObjectsWithTag("Actor");

                foreach(GameObject g in actors) {
                    if (g.GetComponent<CharacterData>().factionID == p.factionID) {
                        g.GetComponent<CharacterData>().factionArray = p;
                        p.members.Add(g);
                    }
                }

                foreach (FactionArray f in factionArray) {
                    if (f.factionID != p.factionID) {
                        RelationshipArray relArray = new RelationshipArray();
                        relArray.relationshipNum = 0 - f.genHostility - p.genHostility;
                        relArray.relName = f.factionName;
                        relArray.relID = f.factionID;
                        p.relationships.Add(relArray);
                    } else {
                        RelationshipArray relArray = new RelationshipArray();
                        relArray.relationshipNum = 100;
                        relArray.relName = f.factionName;
                        relArray.relID = f.factionID;
                        p.relationships.Add(relArray);
                    }
                }
                p.SortRelationShips();
            }

            hasSetUp = true;
            MapGeneration.instance.LayDownFactions();
        }

        Vector2 GenerateStartCoords () {
            int startX = Mathf.RoundToInt(Random.Range(2, 9));
            int startY = 1;
            if (startX % 2 == 1) {
                startY = Mathf.RoundToInt(Random.Range(Mathf.RoundToInt((startX + 1) / 2), 6 + Mathf.RoundToInt((startX + 1) / 2)));
            } else {
                startY = Mathf.RoundToInt(Random.Range(Mathf.RoundToInt(startX / 2), 6 + Mathf.RoundToInt(startX / 2)));
            }
            Vector2 returnCoords = new Vector2 (startX, startY);
            foreach (FactionArray f in factionArray) {
                if (f.startTileX == startX && f.startTileY == startY) {
                    returnCoords = GenerateStartCoords();
                }
            }

            return returnCoords;
        }



        [System.Serializable]
        public class FactionArray {
            public string factionName;
            public int factionID;
            public HistoricalFigure leader;
            public FactionType factionType;
            public SchoolType schoolType;
            public int culture;
            public float baseStrength;
            public float overallStrength;
            public float genHostility;
            public int startTileX;
            public int startTileY;
            public bool subservient;

            public PixelType primaryElementAssociation;
            public SpellEffect spellSystemEffect;
            public PixelType secondaryElementAssociation;
            public List<Spell> knownSpells = new List<Spell>();

            public List<FactionTarget> targetTiles = new List<FactionTarget>();
            public FactionTarget bestTarget;
            public bool onOffensive;
            public bool willAct;
            public bool recentlyRessurected;
            public int resurgenceCounter;

            public List<GameObject> members = new List<GameObject>();
            public List<HistoricalFigure> relatedFigures = new List<HistoricalFigure>();
            public List<HistoricalFigure> livingFigures = new List<HistoricalFigure>();
            public List<HistoricalFigure> deadFigures = new List<HistoricalFigure>();
            public List<GameObject> domain = new List<GameObject>();

            public List<RelationshipArray> relationships = new List<RelationshipArray>();
            //public RelationshipArray[] relationships;

            public RelationshipArray CallRelationshipArray(int factID) {
                RelationshipArray relArray = relationships[factID];
                return relArray;
            }

            static int SortByID(RelationshipArray rel1, RelationshipArray rel2) {
                return rel1.relID.CompareTo(rel2.relID);
            }

            public void SortRelationShips () {
                relationships.Sort(SortByID);
            }
        }

        [System.Serializable]
        public class RelationshipArray {
            public string relName;
            public float relID;
            public float relationshipNum;
            public bool subservientTo;
            public bool atWar;
        }

        public enum FactionType { nation, school, other }
        public enum SchoolType { scholar, assassin }
    }
}
