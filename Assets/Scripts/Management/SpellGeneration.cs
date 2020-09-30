using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class SpellGeneration : MonoBehaviour
    {
        public List<FactionHandler.FactionArray> livingSchools = new List<FactionHandler.FactionArray>();
        public List<FactionHandler.FactionArray> deadSchools = new List<FactionHandler.FactionArray>();
        public List<Spell> spellList = new List<Spell>();
        public bool hasGenerated;
        public int spellNumber;

        public void BeginGeneratingSpells () {
            foreach (FactionHandler.FactionArray f in MapGeneration.instance.remainingFactions) {
                if ((int)f.factionType == 1) {
                    livingSchools.Add(f);
                }
            }
            foreach (FactionHandler.FactionArray f in MapGeneration.instance.deadSchools) {
                deadSchools.Add(f);
            }

            spellNumber = 0;
            GenerateSpells(livingSchools[spellNumber]);
        }

        void GenerateSpells (FactionHandler.FactionArray spellFaction) {
            bool spellTendency = true;

            if ((int)spellFaction.schoolType == 0) {
                spellTendency = true;
            } else if ((int)spellFaction.schoolType == 1) {
                spellTendency = false;
            }

            SpellEffect systemEffect = (SpellEffect)Mathf.RoundToInt(Random.Range(0, System.Enum.GetNames(typeof(SpellEffect)).Length - 1));
            spellFaction.spellSystemEffect = systemEffect;
            SpellComponent systemComponent = (SpellComponent)Mathf.RoundToInt(Random.Range(0, System.Enum.GetNames(typeof(SpellComponent)).Length - 1));
            SpellQuirk systemQuirk = (SpellQuirk)Mathf.RoundToInt(Random.Range(0, System.Enum.GetNames(typeof(SpellQuirk)).Length - 1));

            for (int i = 1; i <= 12; i++) {
                Spell freshSpell = new Spell();
                if (spellTendency == false) {
                    if (i == 3) {
                        freshSpell.element = spellFaction.primaryElementAssociation;
                        freshSpell.spellEffect = systemEffect;
                        freshSpell.specificEffect = Mathf.RoundToInt(Random.Range(1, 12));
                        freshSpell.spellComponent = (SpellComponent)0;
                        freshSpell.spellQuirk = systemQuirk;
                        freshSpell.tier = 1;
                        freshSpell.statRequirement = 5;
                        freshSpell.cost = Mathf.RoundToInt(Random.Range(1, 5));
                        freshSpell.modifier = 1;
                    } else if (i <= 6) {
                        freshSpell.element = spellFaction.primaryElementAssociation;
                        freshSpell.spellEffect = systemEffect;
                        freshSpell.specificEffect = Mathf.RoundToInt(Random.Range(1, 12));
                        freshSpell.spellComponent = (SpellComponent)Mathf.RoundToInt(Random.Range(0, 1));
                        freshSpell.spellQuirk = systemQuirk;
                        freshSpell.tier = 1;
                        freshSpell.statRequirement = 5;
                        freshSpell.cost = Mathf.RoundToInt(Random.Range(1, 5));
                        freshSpell.modifier = Mathf.RoundToInt(Random.Range(1, 3));
                    } else {
                        float elementType = Random.Range(1, 10);
                        if (elementType < 5) {
                            freshSpell.element = spellFaction.primaryElementAssociation;
                        } else {
                            freshSpell.element = spellFaction.secondaryElementAssociation;
                        }
                        freshSpell.spellEffect = systemEffect;
                        freshSpell.specificEffect = Mathf.RoundToInt(Random.Range(1, 12));
                        freshSpell.spellComponent = systemComponent;
                        freshSpell.spellQuirk = systemQuirk;
                        freshSpell.tier = Mathf.RoundToInt(Random.Range(1, 5));
                        freshSpell.statRequirement = (4 + freshSpell.tier) * Random.Range(1, freshSpell.tier);
                        freshSpell.cost = Mathf.RoundToInt(i * Random.Range(i, i * 2));
                        freshSpell.modifier = freshSpell.tier + Mathf.RoundToInt(Random.Range(1, 5));
                        float isLost = Random.Range(1, 10);
                        if (isLost > 8) {
                            freshSpell.lost = true;
                        }
                    }
                }

                if (spellTendency == true) {
                    if (i < 5) {
                        freshSpell.element = spellFaction.primaryElementAssociation;
                        freshSpell.spellEffect = systemEffect;
                        freshSpell.specificEffect = Mathf.RoundToInt(Random.Range(1, 12));
                        freshSpell.spellComponent = systemComponent;
                        freshSpell.spellQuirk = systemQuirk;
                        freshSpell.tier = 1;
                        freshSpell.statRequirement = 5;
                        freshSpell.cost = Mathf.RoundToInt(Random.Range(1, 5));
                        freshSpell.modifier = 1;
                    } else {
                        float elementType = Random.Range(1, 10);
                        if (elementType < 5) {
                            freshSpell.element = spellFaction.primaryElementAssociation;
                        } else {
                            freshSpell.element = spellFaction.secondaryElementAssociation;
                        }
                        freshSpell.spellEffect = systemEffect;
                        freshSpell.specificEffect = Mathf.RoundToInt(Random.Range(1, 12));
                        freshSpell.spellComponent = systemComponent;
                        freshSpell.spellQuirk = systemQuirk;
                        freshSpell.tier = Mathf.RoundToInt(Random.Range(1, 5));
                        freshSpell.statRequirement = (4 + freshSpell.tier) * Random.Range(1, freshSpell.tier);
                        freshSpell.cost = Mathf.RoundToInt(i * Random.Range(i, i * 2));
                        freshSpell.modifier = freshSpell.tier + Mathf.RoundToInt(Random.Range(1, 5));
                        float isLost = Random.Range(1, 10);
                        if (isLost > 8) {
                            freshSpell.lost = true;
                        }
                    }
                }
                string prefix = "";
                string halfName = "Something.";
                string secondHalfName = "Something.";
                string suffix = "";

                if (freshSpell.modifier < 3) {
                    prefix = "Lesser ";
                } else if (freshSpell.modifier > 7) {
                    prefix = "Greater ";
                }

                if ((int)freshSpell.element == 1) {
                    halfName = "Ethereal ";
                } else if ((int)freshSpell.element == 2) {
                    halfName = "Infernal ";
                } else if ((int)freshSpell.element == 3) {
                    halfName = "Necrotic ";
                } else if ((int)freshSpell.element == 4) {
                    halfName = "Sanguine ";
                } else if ((int)freshSpell.element == 5) {
                    halfName = "Fabled ";
                } else if ((int)freshSpell.element == 6) {
                    halfName = "Frigid ";
                }

                if ((int)freshSpell.spellEffect == 0) {
                    if ((int)freshSpell.specificEffect == 1) {
                        secondHalfName = "Rejuvenation ";
                    } else if ((int)freshSpell.specificEffect == 2) {
                        secondHalfName = "Healing ";
                    } else if ((int)freshSpell.specificEffect == 3) {
                        secondHalfName = "Cleansing ";
                    } else if ((int)freshSpell.specificEffect == 4) {
                        secondHalfName = "Mending ";
                    } else if ((int)freshSpell.specificEffect == 5) {
                        secondHalfName = "Withering ";
                        freshSpell.offensive = true;
                    } else if ((int)freshSpell.specificEffect == 6) {
                        secondHalfName = "Piercing ";
                        freshSpell.offensive = true;
                    } else if ((int)freshSpell.specificEffect == 7) {
                        secondHalfName = "Blight ";
                        freshSpell.offensive = true;
                    } else if ((int)freshSpell.specificEffect == 8) {
                        secondHalfName = "Rending ";
                        freshSpell.offensive = true;
                    } else if ((int)freshSpell.specificEffect == 9) {
                        secondHalfName = "Self-Rejuvenation ";
                    } else if ((int)freshSpell.specificEffect == 10) {
                        secondHalfName = "Self-Healing ";
                    } else if ((int)freshSpell.specificEffect == 11) {
                        secondHalfName = "Self-Cleansing ";
                    } else if ((int)freshSpell.specificEffect == 12) {
                        secondHalfName = "Self-Mending ";
                    }
                } else if ((int)freshSpell.spellEffect == 1) {
                    if ((int)freshSpell.specificEffect == 1) {
                        secondHalfName = "Featherstep ";
                    } else if ((int)freshSpell.specificEffect == 2) {
                        secondHalfName = "Levitation ";
                    } else if ((int)freshSpell.specificEffect == 3) {
                        secondHalfName = "Burdening ";
                    } else if ((int)freshSpell.specificEffect == 4) {
                        secondHalfName = "Rooting ";
                    } else if ((int)freshSpell.specificEffect == 5) {
                        secondHalfName = "Force ";
                        freshSpell.offensive = true;
                    } else if ((int)freshSpell.specificEffect == 6) {
                        secondHalfName = "Drawing ";
                        freshSpell.offensive = true;
                    } else if ((int)freshSpell.specificEffect == 7) {
                        secondHalfName = "Swift Approach ";
                    } else if ((int)freshSpell.specificEffect == 8) {
                        secondHalfName = "Uncanny Flight ";
                    } else if ((int)freshSpell.specificEffect == 9) {
                        secondHalfName = "Self-Featherstep ";
                    } else if ((int)freshSpell.specificEffect == 10) {
                        secondHalfName = "Self-Levitation ";
                    } else if ((int)freshSpell.specificEffect == 11) {
                        secondHalfName = "Self-Burdening ";
                    } else if ((int)freshSpell.specificEffect == 12) {
                        secondHalfName = "Self-Rooting ";
                    }
                } else {
                    halfName = "Something Cool";
                }
                

                float wackySuffix = Random.Range(1, 100);
                if (wackySuffix > 50) {
                    if (wackySuffix < 55) {
                        suffix = " of the Deep";
                    } else if (wackySuffix < 60) {
                        suffix = " of the Old World";
                    } else if (wackySuffix < 65) {
                        suffix = " of the Otherworld";
                    } else if (wackySuffix < 70) {
                        suffix = " of Yore";
                    } else if (wackySuffix < 75) {
                        suffix = " of the Arcanum";
                    } else if (wackySuffix < 80) {
                        suffix = " of the Witchqueen";
                    } else if (wackySuffix < 85) {
                        suffix = " of the Dead King";
                    } else if (wackySuffix < 90) {
                        suffix = " of the Huntsman";
                    } else if (wackySuffix < 95) {
                        suffix = " of the Outrider";
                    } else if (wackySuffix < 99) {
                        suffix = " of the Fae";
                    } else if (wackySuffix == 100) {
                        suffix = " TM";
                    }
                }
                freshSpell.name = prefix + halfName + secondHalfName + suffix;
                
                spellList.Add(freshSpell);
                freshSpell.schoolOfOrigin = spellFaction.factionID;


                if (freshSpell.lost == false) {
                    spellFaction.knownSpells.Add(freshSpell);
                }
            }
            if (spellNumber < livingSchools.Count - 1) {
                Invoke("StepUpLivingGen", 0.01f);
            } else if (deadSchools.Count != 0){
                spellNumber = -1;
                Invoke("StepUpDeadGen", 0.01f);
            } else {
                hasGenerated = true;
                spellNumber = spellList.Count;
                Manager.instance.useWorldButton.interactable = true;
                Manager.instance.reloadButton.interactable = true;
            }
        }

        void GenerateDeadSpells (FactionHandler.FactionArray spellFaction) {
            bool spellTendency = true;

            if ((int)spellFaction.schoolType == 0) {
                spellTendency = true;
            } else if ((int)spellFaction.schoolType == 1) {
                spellTendency = false;
            }

            SpellEffect systemEffect = (SpellEffect)Mathf.RoundToInt(Random.Range(0, System.Enum.GetNames(typeof(SpellEffect)).Length - 1));
            spellFaction.spellSystemEffect = systemEffect;
            SpellComponent systemComponent = (SpellComponent)Mathf.RoundToInt(Random.Range(0, System.Enum.GetNames(typeof(SpellComponent)).Length - 1));
            SpellQuirk systemQuirk = (SpellQuirk)Mathf.RoundToInt(Random.Range(0, System.Enum.GetNames(typeof(SpellQuirk)).Length - 1));

            for (int i = 1; i <= 12; i++) {
                Spell freshSpell = new Spell();

                if (spellTendency == false) {
                    float elementType = Random.Range(1, 10);
                    if (elementType < 5) {
                        freshSpell.element = spellFaction.primaryElementAssociation;
                    } else {
                        freshSpell.element = spellFaction.secondaryElementAssociation;
                    }
                    freshSpell.spellEffect = systemEffect;
                    freshSpell.specificEffect = Mathf.RoundToInt(Random.Range(1, 12));
                    freshSpell.spellComponent = systemComponent;
                    freshSpell.spellQuirk = systemQuirk;
                    freshSpell.tier = Mathf.RoundToInt(Random.Range(1, 5));
                    freshSpell.statRequirement = (4 + freshSpell.tier) * Random.Range(1, freshSpell.tier);
                    freshSpell.cost = Mathf.RoundToInt(i * Random.Range(i, i * 2));
                    freshSpell.modifier = freshSpell.tier + Mathf.RoundToInt(Random.Range(3, 8));
                }

                if (spellTendency == true) {
                    float elementType = Random.Range(1, 10);
                    if (elementType < 5) {
                        freshSpell.element = spellFaction.primaryElementAssociation;
                    } else {
                        freshSpell.element = spellFaction.secondaryElementAssociation;
                    }
                    freshSpell.spellEffect = systemEffect;
                    freshSpell.specificEffect = Mathf.RoundToInt(Random.Range(1, 12));
                    freshSpell.spellComponent = systemComponent;
                    freshSpell.spellQuirk = systemQuirk;
                    freshSpell.tier = Mathf.RoundToInt(Random.Range(1, 5));
                    freshSpell.statRequirement = (4 + freshSpell.tier) * Random.Range(1, freshSpell.tier);
                    freshSpell.cost = Mathf.RoundToInt(i * Random.Range(i, i * 2));
                    freshSpell.modifier = freshSpell.tier + Mathf.RoundToInt(Random.Range(3, 8));
                }

                string prefix = "";
                string halfName = "";
                string secondHalfName = "";
                string suffix = "";

                if (freshSpell.modifier < 3) {
                    prefix = "Lesser ";
                } else if (freshSpell.modifier > 7) {
                    prefix = "Greater ";
                }

                if ((int)freshSpell.element == 1) {
                    halfName = "Ethereal ";
                } else if ((int)freshSpell.element == 2) {
                    halfName = "Infernal ";
                } else if ((int)freshSpell.element == 3) {
                    halfName = "Necrotic ";
                } else if ((int)freshSpell.element == 4) {
                    halfName = "Sanguine ";
                } else if ((int)freshSpell.element == 5) {
                    halfName = "Fabled ";
                } else if ((int)freshSpell.element == 6) {
                    halfName = "Frigid ";
                }

                if ((int)freshSpell.spellEffect == 0) {
                    if ((int)freshSpell.specificEffect == 1) {
                        secondHalfName = "Rejuvenation ";
                    } else if ((int)freshSpell.specificEffect == 2) {
                        secondHalfName = "Healing ";
                    } else if ((int)freshSpell.specificEffect == 3) {
                        secondHalfName = "Cleansing ";
                    } else if ((int)freshSpell.specificEffect == 4) {
                        secondHalfName = "Mending ";
                    } else if ((int)freshSpell.specificEffect == 5) {
                        secondHalfName = "Withering ";
                    } else if ((int)freshSpell.specificEffect == 6) {
                        secondHalfName = "Piercing ";
                    } else if ((int)freshSpell.specificEffect == 7) {
                        secondHalfName = "Blight ";
                    } else if ((int)freshSpell.specificEffect == 8) {
                        secondHalfName = "Rending ";
                    } else if ((int)freshSpell.specificEffect == 9) {
                        secondHalfName = "Self-Rejuvenation ";
                    } else if ((int)freshSpell.specificEffect == 10) {
                        secondHalfName = "Self-Healing ";
                    } else if ((int)freshSpell.specificEffect == 11) {
                        secondHalfName = "Self-Cleansing ";
                    } else if ((int)freshSpell.specificEffect == 12) {
                        secondHalfName = "Self-Mending ";
                    }
                } else if ((int)freshSpell.spellEffect == 1) {
                    if ((int)freshSpell.specificEffect == 1) {
                        secondHalfName = "Featherstep ";
                    } else if ((int)freshSpell.specificEffect == 2) {
                        secondHalfName = "Levitation ";
                    } else if ((int)freshSpell.specificEffect == 3) {
                        secondHalfName = "Burdening ";
                    } else if ((int)freshSpell.specificEffect == 4) {
                        secondHalfName = "Rooting ";
                    } else if ((int)freshSpell.specificEffect == 5) {
                        secondHalfName = "Force ";
                    } else if ((int)freshSpell.specificEffect == 6) {
                        secondHalfName = "Drawing ";
                    } else if ((int)freshSpell.specificEffect == 7) {
                        secondHalfName = "Swift Approach ";
                    } else if ((int)freshSpell.specificEffect == 8) {
                        secondHalfName = "Uncanny Flight ";
                    } else if ((int)freshSpell.specificEffect == 9) {
                        secondHalfName = "Self-Featherstep ";
                    } else if ((int)freshSpell.specificEffect == 10) {
                        secondHalfName = "Self-Levitation ";
                    } else if ((int)freshSpell.specificEffect == 11) {
                        secondHalfName = "Self-Burdening ";
                    } else if ((int)freshSpell.specificEffect == 12) {
                        secondHalfName = "Self-Rooting ";
                    }
                } else {
                    halfName = "Something Cool";
                }

                float wackySuffix = Random.Range(1, 100);
                if (wackySuffix > 50) {
                    if (wackySuffix < 55) {
                        suffix = " of the Deep";
                    } else if (wackySuffix < 60) {
                        suffix = " of the Old World";
                    } else if (wackySuffix < 65) {
                        suffix = " of the Otherworld";
                    } else if (wackySuffix < 70) {
                        suffix = " of Yore";
                    } else if (wackySuffix < 75) {
                        suffix = " of the Arcanum";
                    } else if (wackySuffix < 80) {
                        suffix = " of the Witchqueen";
                    } else if (wackySuffix < 85) {
                        suffix = " of the Dead King";
                    } else if (wackySuffix < 90) {
                        suffix = " of the Huntsman";
                    } else if (wackySuffix < 95) {
                        suffix = " of the Outrider";
                    } else if (wackySuffix < 99) {
                        suffix = " of the Fae";
                    } else if (wackySuffix == 100) {
                        suffix = " TM";
                    }
                }
                freshSpell.name = prefix + halfName + secondHalfName + suffix;
                
                freshSpell.lost = true;
                spellList.Add(freshSpell);
                float isTrulyLost = Random.Range(1, 10);
                if (isTrulyLost > 8) {
                    spellFaction.knownSpells.Add(freshSpell);
                }
                freshSpell.schoolOfOrigin = spellFaction.factionID;
            }
            if (spellNumber < deadSchools.Count - 1) {
                Invoke("StepUpDeadGen", 0.01f);
            } else {
                hasGenerated = true;
                spellNumber = spellList.Count;
                Manager.instance.useWorldButton.interactable = true;
                Manager.instance.reloadButton.interactable = true;
            }
        }

        void StepUpLivingGen () {
            spellNumber++;
            GenerateSpells(livingSchools[spellNumber]);
        }

        void StepUpDeadGen () {
            spellNumber++;
            GenerateDeadSpells(deadSchools[spellNumber]);
        }

        void ShareSpells () {
            foreach (FactionHandler.FactionArray s in livingSchools) {
                foreach (FactionHandler.FactionArray p in livingSchools) {
                    if (s.relationships[p.factionID].relationshipNum > 0) {
                        foreach (Spell spell in s.knownSpells) {
                            float shareNum = Random.Range(1, 10);
                            if (shareNum > 8) {
                                p.knownSpells.Add(spell);
                            }
                        }
                    }
                }
            }
        }
        
        public static SpellGeneration instance;
        void Awake () {
            instance = this;
        }
    }
}
