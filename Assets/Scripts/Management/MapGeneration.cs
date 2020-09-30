using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class MapGeneration : MonoBehaviour
    {
        public bool hasGenerated;
        public int year;
        public int seed;
        public GameObject[] chunks;
        float averageWealth;
        public int[,] wealthArray;
        public Node[,] grid;
        FactionHandler.FactionArray activeFaction;
        public List<FactionHandler.FactionArray> remainingFactions = new List<FactionHandler.FactionArray>();
        public List<FactionHandler.FactionArray> deadSchools = new List<FactionHandler.FactionArray>();
        int factionNumber;

        public void StartUpGame () {
            year = 0;
            Random.InitState(Manager.instance.seed);

            chunks = GameObject.FindGameObjectsWithTag("Chunk");
            grid = new Node[chunks.Length * 10, chunks.Length * 10];

            foreach (GameObject c in chunks)
            {
                c.GetComponent<ChunkData>().SendNode();
                c.transform.SetParent(this.gameObject.transform);
                wealthArray = new int[20, 20];
                wealthArray[Mathf.RoundToInt(c.GetComponent<ChunkData>().corX), Mathf.RoundToInt(c.GetComponent<ChunkData>().corY)] = c.GetComponent<ChunkData>().naturalWealth;
            }
            foreach (FactionHandler.FactionArray f in FactionHandler.instance.factionArray) {
                if (f.leader.age != 0) {
                    f.livingFigures.Add(f.leader);
                } else if (f.factionID != 0) {
                    f.leader = FigureHandler.instance.GenerateHistoricalFigure(f, 0, 0, 0, 0);
                    f.livingFigures.Add(f.leader);
                }

                for (int i = 1; i <= 5; i++) {
                    FigureHandler.instance.GenerateHistoricalFigure(f, Mathf.RoundToInt(Random.Range(20, 50)), 0, 0, -1);
                }
            }

            FactionHandler.instance.StartUpGame();
        }

        public void LayDownFactions () {
            foreach (FactionHandler.FactionArray f in FactionHandler.instance.factionArray) {
                if (f.startTileX != 0) {
                    if ((int)f.factionType == 0) {
                        grid[f.startTileX, f.startTileY].worldObject.GetComponent<ChunkData>().faction = f;
                        f.domain.Add(grid[f.startTileX, f.startTileY].worldObject);
                    } else if ((int)f.factionType == 1) {
                        grid[f.startTileX, f.startTileY].worldObject.GetComponent<ChunkData>().school = f;
                        f.domain.Add(grid[f.startTileX, f.startTileY].worldObject);
                    }
                }
            }

            //hasFinishedFactions = true;
            //Start ticking by.
            TickByYear();
        }

        public void TickByYear () {
            year++;

            foreach (FactionHandler.FactionArray f in FactionHandler.instance.factionArray) {
                f.domain = new List<GameObject>();
                List<HistoricalFigure> reaperQueue = new List<HistoricalFigure>();
                List<HistoricalFigure> storkQueue = new List<HistoricalFigure>();
                foreach (HistoricalFigure h in f.livingFigures) {
                    h.age++;
                    if (h.age > Mathf.RoundToInt(Random.Range(50, 80))) {
                        h.isDead = true;
                        reaperQueue.Add(h);
                    }
                    //I get that this bit isn't accurate, but I need some constraints on how often kids are born.
                    if (h.age > 16 && h.age < 30) {
                        float giveBirth = Random.Range(1, 100);
                        if (giveBirth > 95) {
                            HistoricalFigure newChild = FigureHandler.instance.GenerateChild(f, h.familyID);
                            h.children++;
                            h.figChildren.Add(newChild.name);
                            storkQueue.Add(newChild);
                        }
                    }
                }
                foreach (HistoricalFigure d in reaperQueue) {
                    FactionHandler.instance.factionArray[d.factionID].livingFigures.Remove(d);
                    FactionHandler.instance.factionArray[d.factionID].deadFigures.Add(d);
                    LoreHandler.instance.HandleInheritance(d);
                }
                foreach (HistoricalFigure d in storkQueue) {
                    FactionHandler.instance.factionArray[d.factionID].livingFigures.Add(d);
                    FigureHandler.instance.historicalFigures.Add(d);
                    FactionHandler.instance.factionArray[d.factionID].relatedFigures.Add(d);
                }

                foreach (GameObject g in chunks) {
                    if ((int)f.factionType == 0 && g.GetComponent<ChunkData>().faction == f) {
                        f.domain.Add(g);
                    } else if ((int)f.factionType == 1 && g.GetComponent<ChunkData>().school == f) {
                        f.domain.Add(g);
                    }
                }
            }

            int totalWealth = 0;
            int totalChunks = 0;
            foreach (GameObject g in chunks) {
                g.GetComponent<ChunkData>().claimants = 0;
                g.GetComponent<ChunkData>().desiredBy = 0;
                g.GetComponent<ChunkData>().defenseRating = 0;
                g.GetComponent<ChunkData>().launchNode = false;

                totalWealth += g.GetComponent<ChunkData>().naturalWealth;
                totalChunks++;
            }

            remainingFactions = new List<FactionHandler.FactionArray>();
            deadSchools = new List<FactionHandler.FactionArray>();

            foreach (FactionHandler.FactionArray f in FactionHandler.instance.factionArray) {
                
                if (f.recentlyRessurected == true && f.resurgenceCounter > 15) {
                    f.recentlyRessurected = false;
                    f.resurgenceCounter = 0;
                } else if (f.recentlyRessurected == true && f.resurgenceCounter <= 15) {
                    f.resurgenceCounter++;
                }
                
                if (f.domain.Count > 0) {
                    remainingFactions.Add(f);
                }

                if (f.domain.Count <= 0) {
                    float resurrection = Random.Range(1, 100);
                    if (resurrection > 95 && (int)f.factionType == 0) {
                        if ((int)f.factionType == 0) {
                            grid[f.startTileX, f.startTileY].worldObject.GetComponent<ChunkData>().faction = f;
                            f.domain.Add(grid[f.startTileX, f.startTileY].worldObject);
                        } else if ((int)f.factionType == 1) {
                            grid[f.startTileX, f.startTileY].worldObject.GetComponent<ChunkData>().school = f;
                            f.domain.Add(grid[f.startTileX, f.startTileY].worldObject);
                        }
                        remainingFactions.Add(f);
                        f.recentlyRessurected = true;
                        if ((int)f.factionType == 1) {
                            deadSchools.Remove(f);
                        }
                        foreach (FactionHandler.RelationshipArray r in f.relationships) {
                            if (r.subservientTo == true) {
                                r.subservientTo = false;
                                r.relationshipNum -= 25f;
                                FactionHandler.instance.factionArray[Mathf.RoundToInt(r.relID)].relationships[f.factionID].relationshipNum -= 25f;
                            }
                        }
                    }
                }

                if (f.domain.Count <= 0) {
                    if ((int)f.factionType == 1) {
                        deadSchools.Add(f);
                    }
                }

                foreach (FactionHandler.RelationshipArray r in f.relationships) {
                    if (FactionHandler.instance.factionArray[Mathf.RoundToInt(r.relID)].overallStrength > f.overallStrength) {
                        r.relationshipNum -= 5f;
                    }
                    if (r.atWar == true && r.relationshipNum > 100f) {
                        r.atWar = false;
                        //AnnouncerManager.instance.ReceiveText(f.factionName + " ended their war with " + FactionHandler.instance.factionArray[Mathf.RoundToInt(r.relID)].factionName);
                    } else if (r.atWar == false && r.relationshipNum < -50f) {
                        r.atWar = true;
                    }
                }
            }
            averageWealth = totalWealth / totalChunks;

            foreach (FactionHandler.FactionArray f in remainingFactions) {
                int naturalStrength = 0;
                foreach (GameObject d in f.domain) {
                    naturalStrength += d.GetComponent<ChunkData>().naturalWealth;
                }
                float resurgence = 0;
                float luck = Random.Range(-500, 500);

                if (f.recentlyRessurected == true) {
                    resurgence = 100000f;
                }

                float magicalPower = 0;
                if ((int)f.factionType == 1) {
                    foreach (GameObject g in f.domain) {
                        if (g.GetComponent<ChunkData>().magicResource == f.primaryElementAssociation) {
                            magicalPower += 50f;
                        } else if (g.GetComponent<ChunkData>().magicResource == f.secondaryElementAssociation) {
                            magicalPower += 25f;
                        }
                    }
                }

                f.overallStrength = naturalStrength + f.baseStrength * 0.25f + luck + resurgence + magicalPower;

                if (f.overallStrength < 1) {
                    f.overallStrength = 1;
                }
            }
            
            factionNumber = 1;
            InitialTargetGeneration();
        }

        public void InitialTargetGeneration () {
            if (factionNumber < FactionHandler.instance.factionArray.Count) {
                foreach (FactionHandler.FactionArray f in remainingFactions) {
                    if (f.factionID == factionNumber) {
                        activeFaction = f;
                    }
                }
                activeFaction.targetTiles = new List<FactionTarget>();
                activeFaction.targetTiles = GenerateTargets();
                Invoke("TickUpTargets", 0.01f);
            } else if (factionNumber >= FactionHandler.instance.factionArray.Count) {
                //We've genereated all the targets, but now we need to reanalyze them and determine the best one.
                factionNumber = 1;
                TargetReanalysis();    
            }
        }

        public void TargetReanalysis () {
            if (factionNumber < FactionHandler.instance.factionArray.Count) {
                foreach (FactionHandler.FactionArray f in remainingFactions) {
                    if (f.factionID == factionNumber) {
                        activeFaction = f;
                    }
                }

                FactionTarget bestTarget = null;
                int tempWorth = -10000;

                foreach (FactionTarget t in activeFaction.targetTiles) {
                    t.worth -= Mathf.RoundToInt(t.node.worldObject.GetComponent<ChunkData>().claimants * 10);
                    if (t.worth > tempWorth) {
                        tempWorth = t.worth;
                        bestTarget = t;
                    }
                }
                
                if (activeFaction != null) {
                    if ((int)activeFaction.factionType == 0 && activeFaction.bestTarget != null && bestTarget.node != null) {
                        if (bestTarget.node.worldObject.GetComponent<ChunkData>().faction != null && bestTarget.node.worldObject.GetComponent<ChunkData>().faction.factionID != 0) {
                            if (activeFaction.relationships[bestTarget.node.worldObject.GetComponent<ChunkData>().faction.factionID].atWar == false) {
                                activeFaction.relationships[bestTarget.node.worldObject.GetComponent<ChunkData>().faction.factionID].atWar = true;
                                activeFaction.relationships[bestTarget.node.worldObject.GetComponent<ChunkData>().faction.factionID].relationshipNum -= 30f;
                                bestTarget.node.worldObject.GetComponent<ChunkData>().faction.relationships[activeFaction.factionID].atWar = true;
                                bestTarget.node.worldObject.GetComponent<ChunkData>().faction.relationships[activeFaction.factionID].relationshipNum -= 30f;

                                //AnnouncerManager.instance.ReceiveText(activeFaction.factionName + " went to war with the " + bestTarget.node.worldObject.GetComponent<ChunkData>().faction.factionName);
                        
                                foreach (FactionHandler.FactionArray f in FactionHandler.instance.factionArray) {
                                    if (f.relationships[bestTarget.node.worldObject.GetComponent<ChunkData>().faction.factionID].atWar == true) {
                                        f.relationships[activeFaction.factionID].relationshipNum += 5f;
                                        activeFaction.relationships[f.factionID].relationshipNum += 5f;
                                    }
                                }
                        
                            } else {
                                activeFaction.relationships[bestTarget.node.worldObject.GetComponent<ChunkData>().faction.factionID].relationshipNum -= 1f;
                                bestTarget.node.worldObject.GetComponent<ChunkData>().faction.relationships[activeFaction.factionID].relationshipNum -= 1f;
                            }
                        }
                    } else if (activeFaction.bestTarget != null && bestTarget.node != null) {
                        if (bestTarget.node.worldObject.GetComponent<ChunkData>().school.factionID != 0 && bestTarget.node.worldObject.GetComponent<ChunkData>().school != null) {
                            if (activeFaction.relationships[bestTarget.node.worldObject.GetComponent<ChunkData>().school.factionID].atWar == false) {
                                activeFaction.relationships[bestTarget.node.worldObject.GetComponent<ChunkData>().school.factionID].atWar = true;
                                activeFaction.relationships[bestTarget.node.worldObject.GetComponent<ChunkData>().school.factionID].relationshipNum -= 30f;
                                bestTarget.node.worldObject.GetComponent<ChunkData>().school.relationships[activeFaction.factionID].atWar = true;
                                bestTarget.node.worldObject.GetComponent<ChunkData>().school.relationships[activeFaction.factionID].relationshipNum -= 30f;

                                //AnnouncerManager.instance.ReceiveText(activeFaction.factionName + " went to war with the " + bestTarget.node.worldObject.GetComponent<ChunkData>().school.factionName);
                        
                                foreach (FactionHandler.FactionArray f in FactionHandler.instance.factionArray) {
                                    if (f.relationships[bestTarget.node.worldObject.GetComponent<ChunkData>().school.factionID].atWar == true) {
                                        f.relationships[activeFaction.factionID].relationshipNum += 5f;
                                        activeFaction.relationships[f.factionID].relationshipNum += 5f;
                                    }
                                }
                        
                            } else {
                                activeFaction.relationships[bestTarget.node.worldObject.GetComponent<ChunkData>().school.factionID].relationshipNum -= 1f;
                                bestTarget.node.worldObject.GetComponent<ChunkData>().school.relationships[activeFaction.factionID].relationshipNum -= 1f;
                            }
                        }
                    }

                    if (bestTarget != null) {
                        Pathfinding.Pathfinder path = new Pathfinding.Pathfinder();

                        Node startNode = bestTarget.closestNodeInDomain;
                        Node end = bestTarget.node;

                        path.startPosition = startNode;
                        path.endPosition = end;
                        path.pather = this.gameObject;

                        bestTarget.pathToTarget = path.FindPath();


                        activeFaction.bestTarget = bestTarget;
                        bestTarget.node.worldObject.GetComponent<ChunkData>().claimants++;
                        activeFaction.willAct = true;
                    } else {
                        activeFaction.willAct = false;
                    }
                }

                Invoke("TickUpReanalysis", 0.01f);
            } else if (factionNumber >= FactionHandler.instance.factionArray.Count) {
                factionNumber = 1;
                AllocateResources();
            }
        }

        void AllocateResources () {
            if (factionNumber < FactionHandler.instance.factionArray.Count) {
                foreach (FactionHandler.FactionArray f in remainingFactions) {
                    if (f.factionID == factionNumber) {
                        activeFaction = f;
                    }
                }

                List<GameObject> contestedTiles = new List<GameObject>();
                foreach (GameObject d in activeFaction.domain) {
                    if (d.GetComponent<ChunkData>().claimants > 0) {
                        contestedTiles.Add(d);
                    }
                }

                int sliceDomain = Mathf.RoundToInt(activeFaction.domain.Count * 0.5f);
                if (sliceDomain < 1) {
                    sliceDomain = 1;
                }

                if (contestedTiles.Count > sliceDomain && activeFaction.recentlyRessurected == false) {
                    activeFaction.onOffensive = false;
                } else if (activeFaction.willAct == false) {
                    activeFaction.onOffensive = false;
                } else {
                    activeFaction.onOffensive = true;
                }

                if (activeFaction.onOffensive == false) {
                    foreach (GameObject c in contestedTiles) {
                        c.GetComponent<ChunkData>().defenseRating += Mathf.RoundToInt(activeFaction.overallStrength / contestedTiles.Count);
                        if ((int)activeFaction.factionType == 0) {
                            c.GetComponent<ChunkData>().defenseRating += c.GetComponent<ChunkData>().histOwner.prowess;
                        } else if ((int)activeFaction.factionType == 1) {
                            c.GetComponent<ChunkData>().defenseRating += c.GetComponent<ChunkData>().magicOwner.prowess;
                        }
                        
                        foreach (GameObject d in activeFaction.domain) {
                            int terrainDefense = 0;
                            if ((int)d.GetComponent<ChunkData>().type == 1) {
                                terrainDefense = 50;
                            } else if ((int)d.GetComponent<ChunkData>().type == 2) {
                                terrainDefense = 50;
                            } else if ((int)d.GetComponent<ChunkData>().type == 3) {
                                terrainDefense = 25;
                            } else if ((int)d.GetComponent<ChunkData>().type == 4) {
                                terrainDefense = 100;
                            } else if ((int)d.GetComponent<ChunkData>().type == 5) {
                                terrainDefense = 100;
                            }
                            d.GetComponent<ChunkData>().defenseRating += Mathf.RoundToInt(activeFaction.overallStrength * 0.5f) + terrainDefense;
                        }
                    }
                } else if (activeFaction.onOffensive == true && activeFaction.bestTarget != null) {
                    foreach (GameObject d in activeFaction.domain) {
                        int terrainDefense = 0;
                        if ((int)d.GetComponent<ChunkData>().type == 1) {
                            terrainDefense = 50;
                        } else if ((int)d.GetComponent<ChunkData>().type == 2) {
                            terrainDefense = 50;
                        } else if ((int)d.GetComponent<ChunkData>().type == 3) {
                            terrainDefense = 25;
                        } else if ((int)d.GetComponent<ChunkData>().type == 4) {
                            terrainDefense = 100;
                        } else if ((int)d.GetComponent<ChunkData>().type == 5) {
                            terrainDefense = 100;
                        }
                        d.GetComponent<ChunkData>().defenseRating += Mathf.RoundToInt(activeFaction.overallStrength * 0.5f) + terrainDefense;
                    }
                    activeFaction.bestTarget.closestNodeInDomain.worldObject.GetComponent<ChunkData>().launchNode = true;
                    activeFaction.bestTarget.closestNodeInDomain.worldObject.GetComponent<ChunkData>().defenseRating += Mathf.RoundToInt(activeFaction.overallStrength * 0.5f);
                    if ((int)activeFaction.factionType == 0 && activeFaction.bestTarget.closestNodeInDomain.worldObject.GetComponent<ChunkData>().histOwner != null) {
                        activeFaction.bestTarget.closestNodeInDomain.worldObject.GetComponent<ChunkData>().defenseRating += activeFaction.bestTarget.closestNodeInDomain.worldObject.GetComponent<ChunkData>().histOwner.prowess;
                    } else if ((int)activeFaction.factionType == 1 && activeFaction.bestTarget.closestNodeInDomain.worldObject.GetComponent<ChunkData>().magicOwner != null) {
                        activeFaction.bestTarget.closestNodeInDomain.worldObject.GetComponent<ChunkData>().defenseRating += activeFaction.bestTarget.closestNodeInDomain.worldObject.GetComponent<ChunkData>().magicOwner.prowess;
                    }
                }

                Invoke("TickUpAllocation", 0.01f);
            } else if (factionNumber >= FactionHandler.instance.factionArray.Count) {
                factionNumber = 1;
                MakeAction();
            }
        }

        void MakeAction () {
            if (factionNumber < FactionHandler.instance.factionArray.Count) {
               foreach (FactionHandler.FactionArray f in remainingFactions) {
                    if (f.factionID == factionNumber) {
                        activeFaction = f;
                    }
                }
                if (activeFaction.bestTarget != null) {
                    if (activeFaction.bestTarget.pathToTarget != null) {
                        if (activeFaction.bestTarget.pathToTarget.Count > 1) {
                            activeFaction.bestTarget.node = activeFaction.bestTarget.pathToTarget[1];
                        }
                    }
                }
                

                if (activeFaction.onOffensive == true && (int)activeFaction.factionType == 0 && activeFaction.bestTarget != null && activeFaction.bestTarget.pathToTarget != null && activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().histOwner != null) {
                    if (activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().histOwner.factionID != 0) {
                        if (activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().faction == activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().faction) {
                            if (activeFaction.bestTarget.closestNodeInDomain.worldObject.GetComponent<ChunkData>().defenseRating > activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().defenseRating) {
                                activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().securityDamage += 1;
                                //Handle the generation of this battle event.
                                List<HistoricalFigure> relBattleFigs = new List<HistoricalFigure>();
                                HistoricalFigure battleLeader = activeFaction.bestTarget.closestNodeInDomain.worldObject.GetComponent<ChunkData>().histOwner;
                                HistoricalFigure battleDefender = activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().histOwner;
                                relBattleFigs.Add(battleLeader);
                                relBattleFigs.Add(battleDefender);
                                string description = activeFaction.factionName + " attacked " + FactionHandler.instance.factionArray[battleDefender.factionID].factionName + "; the attackers were led by " + battleLeader.name + ", and the defenders by " + battleDefender.name + ".";
                                LoreHandler.instance.GenerateEvent(year, (LoreHandler.HistoricalEvent.EventType)1, relBattleFigs, description);
                                battleLeader.prowess += 50;
                                battleLeader.importance += 50;
                                battleDefender.prowess += 50;
                                battleLeader.importance += 50;


                                float doesDie = Random.Range(1, 100);
                                if (doesDie > 80) {
                                    battleDefender.isDead = true;
                                    FactionHandler.instance.factionArray[battleDefender.factionID].livingFigures.Remove(battleDefender);
                                    FactionHandler.instance.factionArray[battleDefender.factionID].deadFigures.Add(battleDefender);

                                    battleLeader.prowess += 50;
                                    battleLeader.importance += 50;
                                    string deathDesc = battleDefender.name + " was killed in battle.";
                                    battleLeader.kills++;
                                    battleLeader.figKills.Add(battleDefender.name);
                                    float atEnemHands = Random.Range(1, 100);
                                    if (atEnemHands > 75) {
                                        deathDesc = deathDesc + " Slain by " + battleLeader.name + ".";
                                    }

                                    LoreHandler.instance.GenerateEvent(year, (LoreHandler.HistoricalEvent.EventType)4, relBattleFigs, deathDesc);
                                    LoreHandler.instance.HandleInheritance(battleDefender);
                                }
                                float doesDieTwo = Random.Range(1, 100);
                                if (doesDieTwo > 80) {
                                    battleLeader.isDead = true;
                                    FactionHandler.instance.factionArray[battleLeader.factionID].livingFigures.Remove(battleLeader);
                                    FactionHandler.instance.factionArray[battleLeader.factionID].deadFigures.Add(battleLeader);

                                    battleDefender.prowess += 50;
                                    battleDefender.prowess += 50;
                                    battleDefender.kills++;
                                    battleDefender.figKills.Add(battleLeader.name);
                                    string deathDesc = battleLeader.name + " was killed in battle.";
                                    float atEnemHands = Random.Range(1, 100);
                                    if (atEnemHands > 75) {
                                        deathDesc = deathDesc + " Slain by " + battleDefender.name + ".";
                                    }

                                    LoreHandler.instance.GenerateEvent(year, (LoreHandler.HistoricalEvent.EventType)4, relBattleFigs, deathDesc);
                                    LoreHandler.instance.HandleInheritance(battleLeader);
                                }
                                //End of battle lore.


                                if (activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().securityDamage >= 3) {
                                    if (Mathf.RoundToInt(activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().corX) == Mathf.RoundToInt(activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().faction.startTileX)
                                    && Mathf.RoundToInt(activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().corY) == Mathf.RoundToInt(activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().faction.startTileY)) {
                                        activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().faction.relationships[activeFaction.factionID].subservientTo = true;
                                    }
                                    if (Mathf.RoundToInt(activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().corX) == Mathf.RoundToInt(activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().school.startTileX)
                                    && Mathf.RoundToInt(activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().corY) == Mathf.RoundToInt(activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().school.startTileY)) {
                                        activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().school.relationships[activeFaction.factionID].subservientTo = true;
                                        activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().school.culture = activeFaction.culture;
                                    }

                                    //AnnouncerManager.instance.ReceiveText(activeFaction.factionName + " took land from " + activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().faction.factionName);
                                    
                                    //Handle the generation of this capture event.
                                    HistoricalFigure possibleFig = null;
                                    int possImp = 10000;
                                    int lowestDomainSize = 10000;
                                    foreach (HistoricalFigure f in activeFaction.livingFigures) {
                                        if (f.importance < possImp && f.domainSize < lowestDomainSize && f.domainSize < 3) {
                                            lowestDomainSize = f.domainSize;
                                            possImp = f.importance;
                                            possibleFig = f;
                                        }
                                    }
                                    if (possibleFig == null) {
                                        int a = 0;
                                        int p = 0;
                                        if (activeFaction.factionID == 1) {
                                            a = 1;
                                            p = 1;
                                        }
                                        HistoricalFigure newFig = FigureHandler.instance.GenerateHistoricalFigure(activeFaction, 0, a, p, 0);
                                        possibleFig = newFig;
                                    }
                                    possibleFig.importance += 100;
                                    List<HistoricalFigure> histFigs = new List<HistoricalFigure>();
                                    HistoricalFigure oldOwner = activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().histOwner;
                                    possibleFig.domainSize += 1;
                                    if (oldOwner != null) {
                                        oldOwner.domainSize -= 1;
                                        histFigs.Add(oldOwner);
                                    }
                                    histFigs.Add(possibleFig); 
                                    histFigs.Add(activeFaction.leader);
                                    string newDesc = activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().regionName + " was captured by " + activeFaction.factionName + ".";
                                    LoreHandler.instance.GenerateEvent(year, (LoreHandler.HistoricalEvent.EventType)2, histFigs, newDesc);
                                    //Finish the generation of this capture event.

                                    //See if the old ruler dies.
                                    float randomChance = Random.Range(1, 10);
                                    //If so, then generate that event as well.
                                    if (randomChance > 7) {
                                        List<HistoricalFigure> relFigs = new List<HistoricalFigure>();
                                        relFigs.Add(oldOwner);
                                        relFigs.Add(possibleFig);

                                        oldOwner.isDead = true;
                                        FactionHandler.instance.factionArray[oldOwner.factionID].livingFigures.Remove(oldOwner);
                                        FactionHandler.instance.factionArray[oldOwner.factionID].deadFigures.Add(oldOwner);

                                        possibleFig.importance += 50;

                                        possibleFig.kills++;
                                        possibleFig.figKills.Add(oldOwner.name);

                                        string descriptionTwo = oldOwner.name + " was executed by " + possibleFig;
                                        LoreHandler.instance.GenerateEvent(year, (LoreHandler.HistoricalEvent.EventType)4, relFigs, descriptionTwo);
                                        LoreHandler.instance.HandleInheritance(oldOwner);
                                    }

                                    activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().histOwner = possibleFig;

                                    activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().faction = activeFaction;
                                    activeFaction.domain.Add(activeFaction.bestTarget.node.worldObject);
                                    activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().securityDamage = 0;
                                }
                            }
                        }
                    } else if (activeFaction.bestTarget != null) {
                        activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().securityDamage += 1;

                        if (activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().securityDamage >= 3) {
                            //Handle the seizing of unclaimed land.
                            int a = 0;
                            int p = 0;
                            if (activeFaction.factionID == 1) {
                                a = 1;
                                p = 1;
                            }
                            HistoricalFigure newFig = FigureHandler.instance.GenerateHistoricalFigure(activeFaction, 0, a, p, -1);
                            newFig.importance += 100;
                            newFig.domainSize += 1;
                            //Finish the seizing of unclaimed land.

                            activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().faction = activeFaction;
                            activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().histOwner = newFig;
                            activeFaction.domain.Add(activeFaction.bestTarget.node.worldObject);
                            activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().securityDamage = 0;
                        }
                    }
                } else if (activeFaction.onOffensive == true && (int)activeFaction.factionType == 1 && activeFaction.bestTarget != null && activeFaction.bestTarget.pathToTarget != null && activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().magicOwner != null)  {
                    if (activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().magicOwner.factionID != 0) {
                        if (activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().school == activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().school) {
                            if (activeFaction.bestTarget.closestNodeInDomain.worldObject.GetComponent<ChunkData>().defenseRating > activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().defenseRating) {
                                activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().magicalDamage += 1;
                                //Handle the generation of this battle event.
                                List<HistoricalFigure> relBattleFigs = new List<HistoricalFigure>();
                                HistoricalFigure battleLeader = activeFaction.bestTarget.closestNodeInDomain.worldObject.GetComponent<ChunkData>().magicOwner;
                                HistoricalFigure battleDefender = activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().magicOwner;
                                relBattleFigs.Add(battleLeader);
                                relBattleFigs.Add(battleDefender);
                                string description = activeFaction.factionName + " attacked " + FactionHandler.instance.factionArray[battleDefender.factionID].factionName + "; the attackers were led by " + battleLeader.name + ", and the defenders by " + battleDefender.name + ".";
                                LoreHandler.instance.GenerateEvent(year, (LoreHandler.HistoricalEvent.EventType)1, relBattleFigs, description);
                                battleLeader.prowess += 50;
                                battleDefender.prowess += 50;


                                float doesDie = Random.Range(1, 100);
                                if (doesDie > 90) {
                                    battleDefender.isDead = true;
                                    FactionHandler.instance.factionArray[battleDefender.factionID].livingFigures.Remove(battleDefender);
                                    FactionHandler.instance.factionArray[battleDefender.factionID].deadFigures.Add(battleDefender);

                                    battleLeader.prowess += 50;
                                    battleLeader.kills++;
                                    battleLeader.figKills.Add(battleDefender.name);

                                    string deathDesc = battleDefender.name + " was killed in battle.";
                                    float atEnemHands = Random.Range(1, 100);
                                    if (atEnemHands > 75) {
                                        deathDesc = deathDesc + " Slain by " + battleLeader.name + ".";
                                    }
                                    LoreHandler.instance.GenerateEvent(year, (LoreHandler.HistoricalEvent.EventType)4, relBattleFigs, deathDesc);
                                    LoreHandler.instance.HandleInheritance(battleDefender);
                                }
                                float doesDieTwo = Random.Range(1, 100);
                                if (doesDieTwo > 90) {
                                    battleLeader.isDead = true;
                                    FactionHandler.instance.factionArray[battleLeader.factionID].livingFigures.Remove(battleLeader);
                                    FactionHandler.instance.factionArray[battleLeader.factionID].deadFigures.Add(battleLeader);

                                    battleDefender.prowess += 50;
                                    battleDefender.kills++;
                                    battleDefender.figKills.Add(battleLeader.name);

                                    string deathDesc = battleLeader.name + " was killed in battle.";
                                    float atEnemHands = Random.Range(1, 100);
                                    if (atEnemHands > 75) {
                                        deathDesc = deathDesc + " Slain by " + battleDefender.name + ".";
                                    }

                                    LoreHandler.instance.GenerateEvent(year, (LoreHandler.HistoricalEvent.EventType)4, relBattleFigs, deathDesc);
                                    LoreHandler.instance.HandleInheritance(battleLeader);
                                }
                                //End of battle lore.

                                if (activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().magicalDamage >= 3) {
                                    //Handle the generation of this capture event.
                                    HistoricalFigure possibleFig = null;
                                    int possImp = 10000;
                                    int lowestDomainSize = 10000;
                                    foreach (HistoricalFigure f in activeFaction.livingFigures) {
                                        if (f.importance < possImp && f.domainSize < lowestDomainSize && f.domainSize < 3) {
                                            lowestDomainSize = f.domainSize;
                                            possImp = f.importance;
                                            possibleFig = f;
                                        }
                                    }
                                    if (possibleFig == null) {
                                        int a = 0;
                                        int p = 0;
                                        if (activeFaction.factionID == 1) {
                                            a = 1;
                                            p = 1;
                                        }
                                        HistoricalFigure newFig = FigureHandler.instance.GenerateHistoricalFigure(activeFaction, 0, a, p, -1);
                                        newFig.importance += 100;
                                        possibleFig = newFig;
                                    }
                                    List<HistoricalFigure> histFigs = new List<HistoricalFigure>();
                                    HistoricalFigure oldOwner = activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().magicOwner;
                                    possibleFig.domainSize += 1;
                                    oldOwner.domainSize -= 1;
                                    histFigs.Add(possibleFig);
                                    histFigs.Add(oldOwner);
                                    histFigs.Add(activeFaction.leader);
                                    string newDesc = activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().regionName + " was captured by " + activeFaction.factionName + ".";
                                    LoreHandler.instance.GenerateEvent(year, (LoreHandler.HistoricalEvent.EventType)2, histFigs, newDesc);
                                    //Finish the generation of this capture event.

                                    //See if the old ruler dies.
                                    float randomChance = Random.Range(1, 10);
                                    //If so, then generate that event as well.
                                    if (randomChance > 7) {
                                        List<HistoricalFigure> relFigs = new List<HistoricalFigure>();
                                        relFigs.Add(oldOwner);
                                        relFigs.Add(possibleFig);

                                        oldOwner.isDead = true;
                                        FactionHandler.instance.factionArray[oldOwner.factionID].livingFigures.Remove(oldOwner);
                                        FactionHandler.instance.factionArray[oldOwner.factionID].deadFigures.Add(oldOwner);

                                        possibleFig.importance += 50;
                                        possibleFig.kills++;
                                        possibleFig.figKills.Add(oldOwner.name);

                                        string descriptionTwo = oldOwner.name + " was executed by " + possibleFig;
                                        LoreHandler.instance.GenerateEvent(year, (LoreHandler.HistoricalEvent.EventType)4, relFigs, descriptionTwo);
                                        LoreHandler.instance.HandleInheritance(oldOwner);
                                    }

                                    activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().magicOwner = possibleFig;
                                    activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().school = activeFaction;
                                    activeFaction.domain.Add(activeFaction.bestTarget.node.worldObject);
                                    activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().magicalDamage = 0;
                                }
                            }
                        }
                    } else if (activeFaction.bestTarget != null) {
                        activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().magicalDamage += 1;

                        if (activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().magicalDamage >= 3) {
                            //Handle the seizing of unclaimed land.
                            int a = 0;
                            int p = 0;
                            if (activeFaction.factionID == 1) {
                                a = 1;
                                p = 1;
                            }
                            HistoricalFigure newFig = FigureHandler.instance.GenerateHistoricalFigure(activeFaction, 0, a, p, -1);
                            newFig.importance += 100;
                            newFig.domainSize += 1;
                            //Finish the seizing of unclaimed land.
                            activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().school = activeFaction;
                            activeFaction.domain.Add(activeFaction.bestTarget.node.worldObject);
                            activeFaction.bestTarget.node.worldObject.GetComponent<ChunkData>().magicalDamage = 0;
                        }
                    }
                }
                else {
                    //Do nothing.
                }
                activeFaction.bestTarget = null;

                Invoke("TickUpAction", 0.01f);
            } else if (factionNumber >= FactionHandler.instance.factionArray.Count) {
                factionNumber = 1;
                if (year < Manager.instance.maxYearGen) {
                    Invoke("TickByYear", 0.01f);
                } else {
                    foreach (HistoricalFigure f in FigureHandler.instance.historicalFigures) {
                        if (f.isDead == false) {
                            LoreHandler.instance.importantLivingFigures.Add(f);
                        } else if (f.isDead == true) {
                            LoreHandler.instance.importantDeadFigures.Add(f);
                        }
                    }
                    hasGenerated = true;
                    SpellGeneration.instance.BeginGeneratingSpells();
                }
            }
        }

        void TickUpTargets () {
            factionNumber++;
            InitialTargetGeneration();
        }
        void TickUpReanalysis () {
            factionNumber++;
            TargetReanalysis();
        }
        void TickUpAllocation () {
            factionNumber++;
            AllocateResources();
        }
        void TickUpAction () {
            factionNumber++;
            MakeAction();
        }

        GameObject FindClosestNode (GameObject target) {
            GameObject closestTile = null;
            float tempDist = 10000f;

            foreach (GameObject d in activeFaction.domain) {

                if (Vector2.Distance(new Vector2(d.GetComponent<ChunkData>().corX, d.GetComponent<ChunkData>().corY), new Vector2(target.GetComponent<ChunkData>().corX, target.GetComponent<ChunkData>().corY)) < tempDist) {
                    tempDist = Vector2.Distance(new Vector2(d.GetComponent<ChunkData>().corX, d.GetComponent<ChunkData>().corY), new Vector2(target.GetComponent<ChunkData>().corX, target.GetComponent<ChunkData>().corY));
                    closestTile = d;
                }
            }
            
            return closestTile;
        }


        public List<FactionTarget> GenerateTargets () {
            List<FactionTarget> possibleTargets = new List<FactionTarget>();
            if ((int)activeFaction.factionType == 0 && activeFaction.domain.Count < chunks.Length) {
                foreach (GameObject g in chunks) {
                    if (g.GetComponent<ChunkData>().faction.factionID != activeFaction.factionID) {
                        int overValue = 0;
                        if (g.GetComponent<ChunkData>().naturalWealth > averageWealth) {
                            overValue = 5;
                        }
                            
                        GameObject closestTileD = FindClosestNode(g);

                        int isOwned = 0;
                        if (g.GetComponent<ChunkData>().faction.factionID != 0 && g.GetComponent<ChunkData>().faction.factionID != activeFaction.factionID) {
                            if (activeFaction.relationships[g.GetComponent<ChunkData>().faction.factionID].atWar == false) {
                                isOwned = Mathf.RoundToInt(activeFaction.relationships[g.GetComponent<ChunkData>().faction.factionID].relationshipNum) + Mathf.RoundToInt(FactionHandler.instance.factionArray[g.GetComponent<ChunkData>().faction.factionID].overallStrength * 0.5f);
                            } else {
                                isOwned = -50 + Mathf.RoundToInt(FactionHandler.instance.factionArray[g.GetComponent<ChunkData>().faction.factionID].overallStrength * 0.5f);
                            }
                        }

                        FactionTarget newTarget = new FactionTarget();
                        newTarget.node = g.GetComponent<ChunkData>().thisNode;
                        newTarget.worth += 10 + Mathf.RoundToInt(g.GetComponent<ChunkData>().naturalWealth) + overValue - Mathf.RoundToInt(Vector2.Distance(new Vector2(activeFaction.startTileX, activeFaction.startTileY), new Vector2(g.GetComponent<ChunkData>().corX, g.GetComponent<ChunkData>().corY))) * 20 - isOwned;
                        newTarget.closestNodeInDomain = closestTileD.GetComponent<ChunkData>().thisNode;
                        g.GetComponent<ChunkData>().desiredBy++;
                        possibleTargets.Add(newTarget);
                    }
                }
            } else if ((int)activeFaction.factionType == 1 && activeFaction.domain.Count < chunks.Length) {
                foreach (GameObject g in chunks) {
                    if (g.GetComponent<ChunkData>().school.factionID != activeFaction.factionID) {
                        if ((int)g.GetComponent<ChunkData>().magicResource == (int)activeFaction.primaryElementAssociation) {
                            
                            GameObject closestTileD = FindClosestNode(g);
                            
                            int isOwned = 0;
                            if (g.GetComponent<ChunkData>().faction.factionID != 0 && g.GetComponent<ChunkData>().faction.factionID != activeFaction.factionID) {
                                if (activeFaction.relationships[g.GetComponent<ChunkData>().faction.factionID].atWar == false) {
                                    isOwned = Mathf.RoundToInt(activeFaction.relationships[g.GetComponent<ChunkData>().faction.factionID].relationshipNum) + Mathf.RoundToInt(FactionHandler.instance.factionArray[g.GetComponent<ChunkData>().faction.factionID].overallStrength * 0.5f);
                                } else {
                                    isOwned = -50 + Mathf.RoundToInt(FactionHandler.instance.factionArray[g.GetComponent<ChunkData>().faction.factionID].overallStrength * 0.5f);
                                }
                            }

                            FactionTarget newTarget = new FactionTarget();
                            newTarget.node = g.GetComponent<ChunkData>().thisNode;
                            newTarget.worth += 20 - Mathf.RoundToInt(Vector2.Distance(new Vector2(activeFaction.startTileX, activeFaction.startTileY), new Vector2(g.GetComponent<ChunkData>().corX, g.GetComponent<ChunkData>().corY))) * 20 - isOwned;
                            newTarget.closestNodeInDomain = closestTileD.GetComponent<ChunkData>().thisNode;
                            g.GetComponent<ChunkData>().desiredBy++;
                            possibleTargets.Add(newTarget);
                        } else if ((int)g.GetComponent<ChunkData>().magicResource == (int)activeFaction.secondaryElementAssociation) {
                            
                            GameObject closestTileD = FindClosestNode(g);

                            int isOwned = 0;
                            if (g.GetComponent<ChunkData>().faction.factionID != 0 && g.GetComponent<ChunkData>().faction.factionID != activeFaction.factionID) {
                                if (activeFaction.relationships[g.GetComponent<ChunkData>().faction.factionID].atWar == false) {
                                    isOwned = Mathf.RoundToInt(activeFaction.relationships[g.GetComponent<ChunkData>().faction.factionID].relationshipNum) + Mathf.RoundToInt(FactionHandler.instance.factionArray[g.GetComponent<ChunkData>().faction.factionID].overallStrength * 0.5f);
                                } else {
                                    isOwned = -50 + Mathf.RoundToInt(FactionHandler.instance.factionArray[g.GetComponent<ChunkData>().faction.factionID].overallStrength * 0.5f);
                                }
                            }

                            FactionTarget newTarget = new FactionTarget();
                            newTarget.node = g.GetComponent<ChunkData>().thisNode;
                            newTarget.worth += 10 - Mathf.RoundToInt(Vector2.Distance(new Vector2(activeFaction.startTileX, activeFaction.startTileY), new Vector2(g.GetComponent<ChunkData>().corX, g.GetComponent<ChunkData>().corY))) * 20 - isOwned;
                            newTarget.closestNodeInDomain = closestTileD.GetComponent<ChunkData>().thisNode;
                            g.GetComponent<ChunkData>().desiredBy++;
                            possibleTargets.Add(newTarget);
                        } else if (g.GetComponent<ChunkData>().naturalWealth > averageWealth) {
                            GameObject closestTileD = FindClosestNode(g);
                            
                            int isOwned = 0;
                            if (g.GetComponent<ChunkData>().faction.factionID != 0 && g.GetComponent<ChunkData>().faction.factionID != activeFaction.factionID) {
                                if (activeFaction.relationships[g.GetComponent<ChunkData>().faction.factionID].atWar == false) {
                                    isOwned = Mathf.RoundToInt(activeFaction.relationships[g.GetComponent<ChunkData>().faction.factionID].relationshipNum) + Mathf.RoundToInt(FactionHandler.instance.factionArray[g.GetComponent<ChunkData>().faction.factionID].overallStrength * 0.5f);
                                } else {
                                    isOwned = -50 + Mathf.RoundToInt(FactionHandler.instance.factionArray[g.GetComponent<ChunkData>().faction.factionID].overallStrength * 0.5f);
                                }
                            }

                            FactionTarget newTarget = new FactionTarget();
                            newTarget.node = g.GetComponent<ChunkData>().thisNode;
                            newTarget.worth += 10 + Mathf.RoundToInt(g.GetComponent<ChunkData>().naturalWealth / 2) - Mathf.RoundToInt(Vector2.Distance(new Vector2(activeFaction.startTileX, activeFaction.startTileY), new Vector2(g.GetComponent<ChunkData>().corX, g.GetComponent<ChunkData>().corY))) * 20 - isOwned;
                            newTarget.closestNodeInDomain = closestTileD.GetComponent<ChunkData>().thisNode;
                            g.GetComponent<ChunkData>().desiredBy++;
                            possibleTargets.Add(newTarget);
                        }
                    }
                }
            }

            return possibleTargets;
        }


        public Node GetNode(int x, int y)
        {
            Node retVal = null;

            retVal = grid[x, y];

            return retVal;
        }

        public static MapGeneration instance;
        void Awake () {
            instance = this;
        }
    }

    [System.Serializable]
    public class FactionTarget {
        public Node node;
        public int worth;
        public Node closestNodeInDomain;
        public List<Node> pathToTarget;
    }
}
