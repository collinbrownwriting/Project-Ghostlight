using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    [System.Serializable]
    public class WorldData {
        public List<DummyFaction> factions;
        public List<DummyFaction> remainingFactions;
        public List<DummyFaction> livingSchools;
        public List<DummyFaction> deadSchools;
        public List<HistoricalFigure> livingFigures;
        public List<HistoricalFigure> deadFigures;
        public List<LoreHandler.HistoricalEvent> historicalEvents;
        public List<Spell> spellList;
        public int spellNumber;
        public int year;
        public int[,] wealthArray;

        [System.Serializable]
        public class DummyFaction {
            public string factionName;
            public int factionID;
            public FactionHandler.FactionType factionType;
            public FactionHandler.SchoolType schoolType;
            public float baseStrength;
            public float overallStrength;
            public float genHostility;
            public int startTileX;
            public int startTileY;
            public bool subservient;

            public int primaryElementAssociation;
            public int spellSystemEffect;
            public int secondaryElementAssociation;
            public List<Spell> knownSpells;
            public bool onOffensive;
            public bool willAct;
            public bool recentlyRessurected;
            public int resurgenceCounter;

            public int[,] domain = new int[20, 20];

            public List<FactionHandler.RelationshipArray> relationships;
        }

        public WorldData (FactionHandler factionHandler, MapGeneration mapGeneration, SpellGeneration spellGeneration, LoreHandler loreHandler) {
            factions = new List<DummyFaction>();

            foreach (FactionHandler.FactionArray f in factionHandler.factionArray) {
                DummyFaction d = new DummyFaction();
                d.factionName = f.factionName;
                d.factionID = f.factionID;
                d.factionType = f.factionType;
                d.schoolType = f.schoolType;
                d.baseStrength = f.baseStrength;
                d.overallStrength = f.overallStrength;
                d.genHostility = f.genHostility;
                d.startTileX = f.startTileX;
                d.startTileY = f.startTileY;
                d.subservient = f.subservient;
                d.spellSystemEffect = (int)f.spellSystemEffect;
                d.primaryElementAssociation = (int)f.primaryElementAssociation;
                d.secondaryElementAssociation = (int)f.secondaryElementAssociation;
                d.knownSpells = f.knownSpells;
                d.willAct = f.willAct;
                d.recentlyRessurected = f.recentlyRessurected;
                d.resurgenceCounter = f.resurgenceCounter;
                d.relationships = f.relationships;
                foreach (GameObject g in f.domain) {
                    d.domain[Mathf.RoundToInt(g.GetComponent<ChunkData>().corX), Mathf.RoundToInt(g.GetComponent<ChunkData>().corX)] = (int)g.GetComponent<ChunkData>().magicResource;
                }

                factions.Add(d);
            }

            remainingFactions = new List<DummyFaction>();
            foreach (FactionHandler.FactionArray f in mapGeneration.remainingFactions) {
                DummyFaction d = new DummyFaction();
                d.factionName = f.factionName;
                d.factionID = f.factionID;
                d.factionType = f.factionType;
                d.schoolType = f.schoolType;
                d.baseStrength = f.baseStrength;
                d.overallStrength = f.overallStrength;
                d.genHostility = f.genHostility;
                d.startTileX = f.startTileX;
                d.startTileY = f.startTileY;
                d.subservient = f.subservient;
                d.spellSystemEffect = (int)f.spellSystemEffect;
                d.primaryElementAssociation = (int)f.primaryElementAssociation;
                d.secondaryElementAssociation = (int)f.secondaryElementAssociation;
                d.knownSpells = f.knownSpells;
                d.willAct = f.willAct;
                d.recentlyRessurected = f.recentlyRessurected;
                d.resurgenceCounter = f.resurgenceCounter;
                d.relationships = f.relationships;
                foreach (GameObject g in f.domain) {
                    d.domain[Mathf.RoundToInt(g.GetComponent<ChunkData>().corX), Mathf.RoundToInt(g.GetComponent<ChunkData>().corX)] = (int)g.GetComponent<ChunkData>().magicResource;
                }

                remainingFactions.Add(d);
            }

            livingSchools = new List<DummyFaction>();
            foreach (FactionHandler.FactionArray f in spellGeneration.livingSchools) {
                DummyFaction d = new DummyFaction();
                d.factionName = f.factionName;
                d.factionID = f.factionID;
                d.factionType = f.factionType;
                d.schoolType = f.schoolType;
                d.baseStrength = f.baseStrength;
                d.overallStrength = f.overallStrength;
                d.genHostility = f.genHostility;
                d.startTileX = f.startTileX;
                d.startTileY = f.startTileY;
                d.subservient = f.subservient;
                d.spellSystemEffect = (int)f.spellSystemEffect;
                d.primaryElementAssociation = (int)f.primaryElementAssociation;
                d.secondaryElementAssociation = (int)f.secondaryElementAssociation;
                d.knownSpells = f.knownSpells;
                d.willAct = f.willAct;
                d.recentlyRessurected = f.recentlyRessurected;
                d.resurgenceCounter = f.resurgenceCounter;
                d.relationships = f.relationships;
                foreach (GameObject g in f.domain) {
                    d.domain[Mathf.RoundToInt(g.GetComponent<ChunkData>().corX), Mathf.RoundToInt(g.GetComponent<ChunkData>().corX)] = (int)g.GetComponent<ChunkData>().magicResource;
                }

                livingSchools.Add(d);
            }

            deadSchools = new List<DummyFaction>();
            foreach (FactionHandler.FactionArray f in spellGeneration.deadSchools) {
                DummyFaction d = new DummyFaction();
                d.factionName = f.factionName;
                d.factionID = f.factionID;
                d.factionType = f.factionType;
                d.schoolType = f.schoolType;
                d.baseStrength = f.baseStrength;
                d.overallStrength = f.overallStrength;
                d.genHostility = f.genHostility;
                d.startTileX = f.startTileX;
                d.startTileY = f.startTileY;
                d.subservient = f.subservient;
                d.spellSystemEffect = (int)f.spellSystemEffect;
                d.primaryElementAssociation = (int)f.primaryElementAssociation;
                d.secondaryElementAssociation = (int)f.secondaryElementAssociation;
                d.knownSpells = f.knownSpells;
                d.willAct = f.willAct;
                d.recentlyRessurected = f.recentlyRessurected;
                d.resurgenceCounter = f.resurgenceCounter;
                d.relationships = f.relationships;
                foreach (GameObject g in f.domain) {
                    d.domain[Mathf.RoundToInt(g.GetComponent<ChunkData>().corX), Mathf.RoundToInt(g.GetComponent<ChunkData>().corX)] = (int)g.GetComponent<ChunkData>().magicResource;
                }

                deadSchools.Add(d);
            }
            
            year = mapGeneration.year;
            wealthArray = mapGeneration.wealthArray;

            livingFigures = loreHandler.importantLivingFigures;
            deadFigures = loreHandler.importantDeadFigures;

            historicalEvents = loreHandler.importantEvents;

            spellList = spellGeneration.spellList;
            spellNumber = spellGeneration.spellNumber;
        }
    }
}
