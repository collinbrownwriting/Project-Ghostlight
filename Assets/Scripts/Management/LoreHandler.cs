using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class LoreHandler : MonoBehaviour
    {
        public List<HistoricalFigure> importantLivingFigures = new List<HistoricalFigure>();
        public List<HistoricalFigure> importantDeadFigures = new List<HistoricalFigure>();
        public List<HistoricalEvent> importantEvents = new List<HistoricalEvent>();

        public void GenerateEvent (int year, HistoricalEvent.EventType type, List<HistoricalFigure> relatedFigs, string desc) {
            HistoricalEvent histEvent = new HistoricalEvent();

            histEvent.year = year;
            histEvent.type = type;
            histEvent.relatedFigures = relatedFigs;
            histEvent.description = desc;

            importantEvents.Add(histEvent);
        }

        public void HandleInheritance (HistoricalFigure deadFigure) {
            int eldestAge = 0;
            HistoricalFigure possibleInheritor = null;
            foreach (HistoricalFigure f in FactionHandler.instance.factionArray[deadFigure.factionID].livingFigures) {
                if (f.age > eldestAge && f.familyID == deadFigure.familyID) {
                    possibleInheritor = f;
                    eldestAge = f.age;
                }
            }
            if (possibleInheritor == null) {
                HistoricalFigure possibleFig = null;
                int possImp = 10000;
                int lowestDomainSize = 10000;
                foreach (HistoricalFigure f in FactionHandler.instance.factionArray[deadFigure.factionID].livingFigures) {
                    if (f.importance < possImp && f.domainSize < lowestDomainSize) {
                        lowestDomainSize = f.domainSize;
                        possImp = f.importance;
                        possibleFig = f;
                    }
                }

                possibleInheritor = possibleFig;
            }

            foreach (GameObject g in MapGeneration.instance.chunks) {
                if (g.GetComponent<ChunkData>().histOwner == deadFigure) {
                    if (possibleInheritor != null && (int)FactionHandler.instance.factionArray[deadFigure.factionID].factionType == 0) {
                        g.GetComponent<ChunkData>().histOwner = possibleInheritor;
                        possibleInheritor.domainSize++;
                    } else if (possibleInheritor != null && (int)FactionHandler.instance.factionArray[deadFigure.factionID].factionType == 1) {
                        g.GetComponent<ChunkData>().magicOwner = possibleInheritor;
                        possibleInheritor.domainSize++;
                    } else if ((int)FactionHandler.instance.factionArray[deadFigure.factionID].factionType == 0) {
                        g.GetComponent<ChunkData>().faction.domain.Remove(g);
                        g.GetComponent<ChunkData>().faction = FactionHandler.instance.factionArray[0];
                        g.GetComponent<ChunkData>().histOwner = null;
                    } else if ((int)FactionHandler.instance.factionArray[deadFigure.factionID].factionType == 1) {
                        g.GetComponent<ChunkData>().school.domain.Remove(g);
                        g.GetComponent<ChunkData>().school = FactionHandler.instance.factionArray[0];
                        g.GetComponent<ChunkData>().magicOwner = null;
                    }
                }
            }

            foreach (FactionHandler.FactionArray f in FactionHandler.instance.factionArray) {
                if (f.leader == deadFigure && possibleInheritor != null) {
                    f.leader = possibleInheritor;
                } else if (f.leader == deadFigure) {
                    HistoricalFigure newFig = FigureHandler.instance.GenerateChild(FactionHandler.instance.factionArray[deadFigure.factionID], 0);
                    f.leader = newFig;
                }
            }
        }

        [System.Serializable]
        public class HistoricalEvent {
            public int year;
            public EventType type;
            public List<HistoricalFigure> relatedFigures = new List<HistoricalFigure>();
            public string description;

            public enum EventType { war, battle, capture, peace, kill }
        }


        public static LoreHandler instance;
        void Awake () {
            instance = this;
        }
    }
}
