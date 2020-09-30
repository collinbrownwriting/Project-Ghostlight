using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class SpellHandler : MonoBehaviour
    {
        public GameObject aetherRune;
        public GameObject fireRune;
        public GameObject bloodRune;
        public GameObject necroticRune;
        public GameObject fableRune;
        public GameObject frostRune;

        public Material stand;
        public Material outline;
        GameObject[] actors;
        GameObject[] nearbyObjects;
        public List<GameObject> targettableObjects = new List<GameObject>();
        public HeldSpell heldSpell;

        public class HeldSpell {
            public SpellEffect heldEffect;
            public int heldSpecific;
            public PixelType heldElement;
            public SpellQuirk heldQuirk;
            public int modifier;
            public GameObject caster;
        }

        public void CastSpell (SpellEffect spellEffect, int specificEffect, SpellComponent spellComponent, SpellQuirk spellQuirk, PixelType element, int modifier, int statReq, int cost, GameObject caster, GameObject target) {
            actors = GameObject.FindGameObjectsWithTag("Actor");
            bool willCast = false;

            if ((int)spellComponent == 0) {
                willCast = true;

                if ((int)element == 3) {
                    willCast = true;
                    CharacterData.SpellOverload n = null;
                    foreach (CharacterData.SpellOverload s in caster.GetComponent<CharacterData>().spellOverloads) {
                        if ((int)s.overloadType == 3 && n == null) {
                            s.overloadCounter++;
                            n = s;
                        }
                    }
                    if (n == null) {
                        n = new CharacterData.SpellOverload();
                        n.overloadCounter++;
                        n.overloadType = (PixelType)3;
                    }
                }

                if ((int)element == 4) {
                    caster.GetComponent<CharacterData>().bleedCounter += cost;
                    willCast = true;
                }

                if ((int)element == 10) {
                    willCast = true;
                    CharacterData.Curse c = null;
                    foreach (CharacterData.Curse s in caster.GetComponent<CharacterData>().curses) {
                        if ((int)s.curseType == 2 && c == null) {
                            s.curseCounter++;
                            c = s;
                        }
                    }
                    if (c == null) {
                        c = new CharacterData.Curse();
                        c.curseCounter++;
                        c.curseType = (CharacterData.Curse.CurseType)3;
                    }
                }
            }

            if ((int)spellComponent == 1) {
                if ((int)element == 1) {
                    if (caster.GetComponent<CharacterData>().ghostlightHeld > cost) {
                        caster.GetComponent<CharacterData>().ghostlightHeld -= cost;
                        willCast = true;
                    }
                } else if ((int)element == 2) {
                    if (caster.GetComponent<CharacterData>().oilHeld > cost) {
                        caster.GetComponent<CharacterData>().oilHeld -= cost;
                        willCast = true;
                    }
                } else if ((int)element == 3) {
                    willCast = true;
                    CharacterData.SpellOverload n = null;
                    foreach (CharacterData.SpellOverload s in caster.GetComponent<CharacterData>().spellOverloads) {
                        if ((int)s.overloadType == 3 && n == null) {
                            s.overloadCounter++;
                            n = s;
                        }
                    }
                    if (n == null) {
                        n = new CharacterData.SpellOverload();
                        n.overloadCounter++;
                        n.overloadType = (PixelType)3;
                    }
                } else if ((int)element == 4) {
                    if (caster.GetComponent<CharacterData>().bloodHeld > cost) {
                        caster.GetComponent<CharacterData>().bloodHeld -= cost;
                        willCast = true;
                    } else {
                        caster.GetComponent<CharacterData>().bleedCounter += cost;
                        willCast = true;
                    }
                } else if ((int)element == 5) {
                if (caster.GetComponent<CharacterData>().inkHeld > cost) {
                    caster.GetComponent<CharacterData>().inkHeld -= cost;
                    willCast = true;
                }
            } else if ((int)element == 6) {
                if (caster.GetComponent<CharacterData>().iceHeld > cost) {
                    caster.GetComponent<CharacterData>().iceHeld -= cost;
                    willCast = true;
                }
            }
        }

        if ((int)spellComponent == 2) {
            caster.GetComponent<CharacterData>().bleedCounter += cost;
            willCast = true;
        }
        if ((int)spellComponent == 3) {
            caster.GetComponent<CharacterData>().toxinCounter += cost;
            willCast = true;
        }
        if ((int)spellComponent == 4) {
            caster.GetComponent<PhysicalProperties>().animate.temperature += cost;
            willCast = true;
        }
        if ((int)spellComponent == 5) {
            caster.GetComponent<PhysicalProperties>().animate.temperature -= cost;
            willCast = true;
        }
        if ((int)spellComponent == 5) {
            CharacterData.SpellOverload n = null;
            foreach (CharacterData.SpellOverload s in caster.GetComponent<CharacterData>().spellOverloads) {
                if (s.overloadType == element && n == null) {
                    s.overloadCounter++;
                    n = s;
                }
            }
            if (n == null) {
                n = new CharacterData.SpellOverload();
                n.overloadCounter++;
                n.overloadType = element;
            }
            willCast = true;
        }

        if ((int)spellComponent == 6) {
            //I intend to change this later.
            int randomCurse = Mathf.RoundToInt(Random.Range(0, 2));

            CharacterData.Curse c = null;
            foreach (CharacterData.Curse s in caster.GetComponent<CharacterData>().curses) {
                if (s.curseType == (CharacterData.Curse.CurseType)randomCurse && c == null) {
                    s.curseCounter++;
                    c = s;
                }
            }
            if (c == null) {
                c = new CharacterData.Curse();
                c.curseCounter++;
                c.curseType = (CharacterData.Curse.CurseType)randomCurse;
            }
            willCast = true;
        }

        if ((int)spellComponent == 7) {
            //Change the player's relationships.
            //This will be fleshed out when individual characters have relationships with factions and others.
            willCast = true;
        }

        if ((int)spellComponent == 8) {
            if (caster.GetComponent<CharacterData>().hasLostLimb == true) {
                willCast = true;
            } else {
                int whichLimb = Mathf.RoundToInt(Random.Range(1, 4));
                if (whichLimb < 2) {
                    caster.GetComponent<CharacterData>().RemoveLimb(25);
                    willCast = true;
                }
                if (whichLimb >= 2) {
                    caster.GetComponent<CharacterData>().RemoveLimb(75);
                    willCast = true;
                }
            }
        }

        if ((int)spellComponent == 9) {
            //Kill the caster unless they kill another character in X turns.
            willCast = true;
        }

        if (willCast == true) {
            float maxDist = caster.GetComponent<CharacterData>().creativity * 0.25f;

            if ((int)spellEffect == 0) {
                if (specificEffect <= 8) {
                    if (caster.gameObject.GetComponent<CharacterData>().isPlayer == true) {
                        int possibleTargets = 0;
                        foreach (GameObject g in actors) {
                            if (Vector3.Distance(g.transform.position, caster.transform.position) <= maxDist && g.gameObject.GetComponent<CharacterData>().isPlayer == false) {
                                g.GetComponent<NPCHandler>().recepientSpell = true;
                                possibleTargets++;
                            }
                        }
                        if (possibleTargets == 0) {
                            AnnouncerManager.instance.ReceiveText("No targets are in range.", true);
                            caster.GetComponent<Controller>().isCasting = false;    
                        } else {
                            heldSpell = new HeldSpell();
                            heldSpell.heldEffect = spellEffect;
                            heldSpell.heldSpecific = specificEffect;
                            heldSpell.modifier = modifier;
                            heldSpell.heldElement = element;
                            heldSpell.caster = caster;
                            heldSpell.heldQuirk = spellQuirk;
                        }
                        
                    } else {
                        if (specificEffect == 1) {
                            foreach (PhysicalProperties.OrganOrLimb l in target.GetComponent<PhysicalProperties>().animate.limbsAndOrgans) {
                                l.health += modifier;
                            }
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to rejuvenate " + target.GetComponent<CharacterData>().charName, false);
                            caster.GetComponent<Controller>().isCasting = false;
                        } else if (specificEffect == 2) {
                            PhysicalProperties.OrganOrLimb mostInjuredLimb = null;
                            float tempHealth = 100000;
                            foreach (PhysicalProperties.OrganOrLimb l in target.GetComponent<PhysicalProperties>().animate.limbsAndOrgans) {
                                if (l.health < tempHealth) {
                                    tempHealth = l.health;
                                    mostInjuredLimb = l;
                                }
                            }
                            mostInjuredLimb.health += modifier * target.GetComponent<PhysicalProperties>().animate.limbsAndOrgans.Count;
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to heal the " + mostInjuredLimb.name + " of " + target.GetComponent<CharacterData>().charName, false);
                            if (mostInjuredLimb.health > mostInjuredLimb.maxHealth) {
                                mostInjuredLimb.health = mostInjuredLimb.maxHealth;
                            }
                            caster.GetComponent<Controller>().isCasting = false;
                        } else if (specificEffect == 3) {
                            target.GetComponent<CharacterData>().toxinCounter -= modifier;
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to purge poisons from " + target.GetComponent<CharacterData>().charName + "'s body.", false);
                            caster.GetComponent<Controller>().isCasting = false;
                        } else if (specificEffect == 4) {
                            target.GetComponent<CharacterData>().bleedCounter -= modifier;
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to stop " + target.GetComponent<CharacterData>().charName + "'s bleeding.", false);
                            caster.GetComponent<Controller>().isCasting = false;
                        } else if (specificEffect == 5) {
                            foreach (PhysicalProperties.OrganOrLimb l in target.GetComponent<PhysicalProperties>().animate.limbsAndOrgans) {
                                l.health -= modifier;
                            }
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to wither " + target.GetComponent<CharacterData>().charName + "'s flesh.", false);
                            caster.GetComponent<Controller>().isCasting = false;
                        } else if (specificEffect == 6) {
                            //I think I want to change these so that you pick a specific body part.
                            PhysicalProperties.OrganOrLimb leastInjuredLimb = null;
                            float tempHealth = -100000;
                            foreach (PhysicalProperties.OrganOrLimb l in target.GetComponent<PhysicalProperties>().animate.limbsAndOrgans) {
                                if (l.health > tempHealth) {
                                    tempHealth = l.health;
                                    leastInjuredLimb = l;
                                }
                            }
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to pierce the " + leastInjuredLimb.name + " of " + target.GetComponent<CharacterData>().charName, false);
                            leastInjuredLimb.health -= modifier * target.GetComponent<PhysicalProperties>().animate.limbsAndOrgans.Count;
                            caster.GetComponent<Controller>().isCasting = false;
                        } else if (specificEffect == 7) {
                            target.GetComponent<CharacterData>().toxinCounter += modifier;
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to infuse " + target.GetComponent<CharacterData>().charName + "'s blood with poison.", false);
                            caster.GetComponent<Controller>().isCasting = false;
                        } else if (specificEffect == 8) {
                            target.GetComponent<CharacterData>().bleedCounter += modifier;
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to tear open " + target.GetComponent<CharacterData>().charName + "'s skin.", false);
                            caster.GetComponent<Controller>().isCasting = false;
                        }
                        MagicPixel m = MagicHandler.instance.FindNearestPixel(target.gameObject.transform.position);
                        MagicHandler.instance.SummonPixel(m, 3, element);
                        
                        this.gameObject.GetComponent<Controller>().actionCount++;
                        if ((int)spellQuirk == 0) {
                            if ((int)element == 1) {
                                GameObject rune = Instantiate(aetherRune);
                                rune.transform.position = target.transform.position;
                                rune.GetComponent<RoundWearOff>().length = 2;
                                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            } else if ((int)element == 2) {
                                GameObject rune = Instantiate(fireRune);
                                rune.transform.position = target.transform.position;
                                rune.GetComponent<RoundWearOff>().length = 2;
                                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            } else if ((int)element == 3) {
                                GameObject rune = Instantiate(necroticRune);
                                rune.transform.position = target.transform.position;
                                rune.GetComponent<RoundWearOff>().length = 2;
                                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            } else if ((int)element == 4) {
                                GameObject rune = Instantiate(bloodRune);
                                rune.transform.position = target.transform.position;
                                rune.GetComponent<RoundWearOff>().length = 2;
                                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            } else if ((int)element == 5) {
                                GameObject rune = Instantiate(fableRune);
                                rune.transform.position = target.transform.position;
                                rune.GetComponent<RoundWearOff>().length = 2;
                                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            } else if ((int)element == 6) {
                                GameObject rune = Instantiate(frostRune);
                                rune.transform.position = target.transform.position;
                                rune.GetComponent<RoundWearOff>().length = 2;
                                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            }
                        }
                    }
                } else {
                    if (specificEffect == 9) {
                        foreach (PhysicalProperties.OrganOrLimb l in caster.GetComponent<PhysicalProperties>().animate.limbsAndOrgans) {
                            l.health += modifier / caster.GetComponent<PhysicalProperties>().animate.limbsAndOrgans.Count;
                        }
                        AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to rejuvenate their body.", false);
                    } else if (specificEffect == 10) {
                        PhysicalProperties.OrganOrLimb mostInjuredLimb = null;
                        float tempHealth = 100000;
                        foreach (PhysicalProperties.OrganOrLimb l in caster.GetComponent<PhysicalProperties>().animate.limbsAndOrgans) {
                            if (l.health < tempHealth) {
                                tempHealth = l.health;
                                mostInjuredLimb = l;
                            }
                        }
                        mostInjuredLimb.health += modifier;
                        AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to heal their " + mostInjuredLimb.name, false);
                        if (mostInjuredLimb.health > mostInjuredLimb.maxHealth) {
                            mostInjuredLimb.health = mostInjuredLimb.maxHealth;
                        }
                    } else if (specificEffect == 11) {
                        caster.GetComponent<CharacterData>().toxinCounter -= modifier;
                        AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to purge poisons from their body.", false);
                    } else if (specificEffect == 12) {
                            caster.GetComponent<CharacterData>().bleedCounter -= modifier;
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to stop their bleeding.", false);
                    }
                    MagicPixel m = MagicHandler.instance.FindNearestPixel(caster.gameObject.transform.position);
                    MagicHandler.instance.SummonPixel(m, 3, element);
                    Controller.instance.isCasting = false;
                    
                    caster.GetComponent<Controller>().actionCount++;

                    if ((int)spellQuirk == 0) {
                        if ((int)element == 1) {
                            GameObject rune = Instantiate(aetherRune);
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                        } else if ((int)element == 2) {
                            GameObject rune = Instantiate(fireRune);
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                        } else if ((int)element == 3) {
                            GameObject rune = Instantiate(necroticRune);
                            rune.transform.position = target.transform.position;
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                        } else if ((int)element == 4) {
                            GameObject rune = Instantiate(bloodRune);
                            rune.transform.position = target.transform.position;
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                        } else if ((int)element == 5) {
                            GameObject rune = Instantiate(fableRune);
                            rune.transform.position = target.transform.position;
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                        } else if ((int)element == 6) {
                            GameObject rune = Instantiate(frostRune);
                            rune.transform.position = target.transform.position;
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                        }
                    }

                    if (caster.gameObject == CharacterData.instance.gameObject) {
                        Controller.instance.actionBar.GetComponent<ActionBar>().CutAction();
                        if (Controller.instance.actionCount >= Controller.instance.maxActionCount) {
                            Controller.instance.actionCount = 0;
                            Controller.instance.hasTakenTurn = true;
                        }
                    }
                }
            }

            if ((int)spellEffect == 1) {
                if (specificEffect <= 6) {
                    if (caster.gameObject.GetComponent<CharacterData>().isPlayer == true) {
                        int possibleTargets = 0;
                        foreach (GameObject g in actors) {
                            if (Vector3.Distance(g.transform.position, caster.transform.position) <= maxDist && g.gameObject.GetComponent<CharacterData>().isPlayer == false) {
                                g.GetComponent<NPCHandler>().recepientSpell = true;
                                possibleTargets++;
                            }
                        }
                        if (possibleTargets == 0) {
                            AnnouncerManager.instance.ReceiveText("No targets are in range.", true);
                            caster.GetComponent<Controller>().isCasting = false;    
                        } else {
                            heldSpell = new HeldSpell();
                            heldSpell.heldEffect = spellEffect;
                            heldSpell.heldSpecific = specificEffect;
                            heldSpell.modifier = modifier;
                            heldSpell.heldElement = element;
                            heldSpell.caster = caster;
                            heldSpell.heldQuirk = spellQuirk;
                        }
                    } else {
                        if (specificEffect == 1) {
                            target.GetComponent<PhysicalProperties>().modifiedWeight = Mathf.RoundToInt(target.GetComponent<PhysicalProperties>().animate.weight * 0.75f);
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to lighten " + target.GetComponent<CharacterData>().charName, false);
                            MagicPixel m = MagicHandler.instance.FindNearestPixel(target.gameObject.transform.position);
                            MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
                            caster.GetComponent<Controller>().isCasting = false;
                        } else if (specificEffect == 2) {
                            target.GetComponent<PhysicalProperties>().modifiedWeight = Mathf.RoundToInt(target.GetComponent<PhysicalProperties>().animate.weight * 0.25f);
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to make " + target.GetComponent<CharacterData>().charName + " weightless.", false);
                            MagicPixel m = MagicHandler.instance.FindNearestPixel(target.gameObject.transform.position);
                            MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
                            caster.GetComponent<Controller>().isCasting = false;
                        } else if (specificEffect == 3) {
                            target.GetComponent<PhysicalProperties>().modifiedWeight = Mathf.RoundToInt(target.GetComponent<PhysicalProperties>().animate.weight * 1.25f);
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to weigh " + target.GetComponent<CharacterData>().charName + " down.", false);
                            MagicPixel m = MagicHandler.instance.FindNearestPixel(target.gameObject.transform.position);
                            MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
                            caster.GetComponent<Controller>().isCasting = false;
                        } else if (specificEffect == 4) {
                            target.GetComponent<PhysicalProperties>().modifiedWeight = Mathf.RoundToInt(target.GetComponent<PhysicalProperties>().animate.weight * 2.25f);
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to make " + target.GetComponent<CharacterData>().charName + " too heavy to move.", false);
                            MagicPixel m = MagicHandler.instance.FindNearestPixel(target.gameObject.transform.position);
                            MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
                            caster.GetComponent<Controller>().isCasting = false;
                        } else if (specificEffect == 5) {
                            int tempX = 0;
                            int tempY = 0;
                            if (caster.transform.position.x < target.transform.position.y) {
                                tempX = 1;
                            } else {
                                tempX = -1;
                            }
                            if (caster.transform.position.y < target.transform.position.y) {
                                tempY = 1;
                            } else {
                                tempY = -1;
                            }
                            target.GetComponent<PhysicalProperties>().xMomentum = tempX * modifier - target.GetComponent<PhysicalProperties>().animate.weight;
                            target.GetComponent<PhysicalProperties>().yMomentum = tempY * modifier - target.GetComponent<PhysicalProperties>().animate.weight;
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to propel " + target.GetComponent<CharacterData>().charName + " away.", false);
                            MagicPixel m = MagicHandler.instance.FindNearestPixel(target.gameObject.transform.position);
                            MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
                            caster.GetComponent<Controller>().isCasting = false;
                        } else if (specificEffect == 6) {
                            int tempX = 0;
                            int tempY = 0;
                            if (caster.transform.position.x < target.transform.position.y) {
                                tempX = -1;
                            } else {
                                tempX = 1;
                            }
                            if (caster.transform.position.y < target.transform.position.y) {
                                tempY = -1;
                            } else {
                                tempY = 1;
                            }
                            tempX *= modifier;
                            tempY *= modifier;

                            target.GetComponent<PhysicalProperties>().xMomentum = tempX - target.GetComponent<PhysicalProperties>().animate.weight;
                            target.GetComponent<PhysicalProperties>().yMomentum = tempY - target.GetComponent<PhysicalProperties>().animate.weight;
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to launch " + target.GetComponent<CharacterData>().charName + " back towards them.", false);
                            MagicPixel m = MagicHandler.instance.FindNearestPixel(target.gameObject.transform.position);
                            MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
                            caster.GetComponent<Controller>().isCasting = false;
                        } else if (specificEffect == 7) {
                            int tempX = 0;
                            int tempY = 0;
                            if (caster.transform.position.x < target.transform.position.y) {
                                tempX = 1;
                            } else {
                                tempX = -1;
                            }
                            if (caster.transform.position.y < target.transform.position.y) {
                                tempY = 1;
                            } else {
                                tempY = -1;
                            }
                            caster.GetComponent<PhysicalProperties>().xMomentum = tempX * modifier - caster.GetComponent<PhysicalProperties>().animate.weight;
                            caster.GetComponent<PhysicalProperties>().yMomentum = tempY * modifier - caster.GetComponent<PhysicalProperties>().animate.weight;
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to propel theirself towards " + target.GetComponent<CharacterData>().charName, false);
                            MagicPixel m = MagicHandler.instance.FindNearestPixel(caster.gameObject.transform.position);
                            MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
                            caster.GetComponent<Controller>().isCasting = false;
                        } else if (specificEffect == 8) {
                            int tempX = 0;
                            int tempY = 0;
                            if (caster.transform.position.x < target.transform.position.y) {
                                tempX = -1;
                            } else {
                                tempX = 1;
                            }
                            if (caster.transform.position.y < target.transform.position.y) {
                                tempY = -1;
                            } else {
                                tempY = 1;
                            }
                            tempX *= modifier;
                            tempY *= modifier;

                            caster.GetComponent<PhysicalProperties>().xMomentum = tempX - caster.GetComponent<PhysicalProperties>().animate.weight;
                            caster.GetComponent<PhysicalProperties>().yMomentum = tempY - caster.GetComponent<PhysicalProperties>().animate.weight;
                            AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to launch away from " + target.GetComponent<CharacterData>().charName, false);
                            MagicPixel m = MagicHandler.instance.FindNearestPixel(caster.gameObject.transform.position);
                            MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
                            caster.GetComponent<Controller>().isCasting = false;
                        }
                        this.gameObject.GetComponent<Controller>().actionCount++;
                        target.GetComponent<PhysicalProperties>().CarryMomentum();
                        target.GetComponent<PhysicalProperties>().EvaluateWeight();
                        if ((int)spellQuirk == 0) {
                            if ((int)element == 1) {
                                GameObject rune = Instantiate(aetherRune);
                                rune.transform.position = target.transform.position;
                                rune.GetComponent<RoundWearOff>().length = 2;
                                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            } else if ((int)element == 2) {
                                GameObject rune = Instantiate(fireRune);
                                rune.transform.position = target.transform.position;
                                rune.GetComponent<RoundWearOff>().length = 2;
                                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            } else if ((int)element == 3) {
                                GameObject rune = Instantiate(necroticRune);
                                rune.transform.position = target.transform.position;
                                rune.GetComponent<RoundWearOff>().length = 2;
                                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            } else if ((int)element == 4) {
                                GameObject rune = Instantiate(bloodRune);
                                rune.transform.position = target.transform.position;
                                rune.GetComponent<RoundWearOff>().length = 2;
                                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            } else if ((int)element == 5) {
                                GameObject rune = Instantiate(fableRune);
                                rune.transform.position = target.transform.position;
                                rune.GetComponent<RoundWearOff>().length = 2;
                                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            } else if ((int)element == 6) {
                                GameObject rune = Instantiate(frostRune);
                                rune.transform.position = target.transform.position;
                                rune.GetComponent<RoundWearOff>().length = 2;
                                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            }
                        }
                    }
                } else {
                    if (specificEffect == 9) {
                        caster.GetComponent<PhysicalProperties>().modifiedWeight = Mathf.RoundToInt(target.GetComponent<PhysicalProperties>().animate.weight * 0.75f);
                        AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to decrease their own weight.", false);
                        caster.GetComponent<Controller>().isCasting = false;
                    } else if (specificEffect == 10) {
                        caster.GetComponent<PhysicalProperties>().modifiedWeight = Mathf.RoundToInt(caster.GetComponent<PhysicalProperties>().animate.weight * 0.25f);
                        AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to turn theirself weightless.", false);
                        caster.GetComponent<Controller>().isCasting = false;
                    } else if (specificEffect == 11) {
                        caster.GetComponent<PhysicalProperties>().modifiedWeight = Mathf.RoundToInt(caster.GetComponent<PhysicalProperties>().animate.weight * 1.25f);
                        AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to weigh theirself down.", false);
                        caster.GetComponent<Controller>().isCasting = false;
                    } else if (specificEffect == 12) {
                        caster.GetComponent<PhysicalProperties>().modifiedWeight = Mathf.RoundToInt(caster.GetComponent<PhysicalProperties>().animate.weight * 2.25f);
                        AnnouncerManager.instance.ReceiveText(caster.GetComponent<CharacterData>().charName + " cast a spell to cement theirself in place.", false);
                        caster.GetComponent<Controller>().isCasting = false;
                    }
                    MagicPixel m = MagicHandler.instance.FindNearestPixel(caster.gameObject.transform.position);
                    MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);

                    this.gameObject.GetComponent<Controller>().actionCount++;
                    target.GetComponent<PhysicalProperties>().CarryMomentum();
                    target.GetComponent<PhysicalProperties>().EvaluateWeight();

                    if (caster.gameObject == CharacterData.instance.gameObject) {
                        Controller.instance.actionBar.GetComponent<ActionBar>().CutAction();
                        if (Controller.instance.actionCount >= Controller.instance.maxActionCount) {
                            Controller.instance.actionCount = 0;
                            Controller.instance.hasTakenTurn = true;
                        }
                    }
                    if ((int)spellQuirk == 0 && specificEffect > 4) {
                        if ((int)element == 1) {
                            GameObject rune = Instantiate(aetherRune);
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().length = modifier;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            rune.GetComponent<RoundWearOff>().target = target;
                            rune.GetComponent<RoundWearOff>().spellEffect = spellEffect;
                            rune.GetComponent<RoundWearOff>().specificEffect = specificEffect;
                        } else if ((int)element == 2) {
                            GameObject rune = Instantiate(fireRune);
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().length = modifier;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            rune.GetComponent<RoundWearOff>().target = target;
                            rune.GetComponent<RoundWearOff>().spellEffect = spellEffect;
                            rune.GetComponent<RoundWearOff>().specificEffect = specificEffect;
                        } else if ((int)element == 3) {
                            GameObject rune = Instantiate(necroticRune);
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().length = modifier;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            rune.GetComponent<RoundWearOff>().target = target;
                            rune.GetComponent<RoundWearOff>().spellEffect = spellEffect;
                            rune.GetComponent<RoundWearOff>().specificEffect = specificEffect;
                        } else if ((int)element == 4) {
                            GameObject rune = Instantiate(bloodRune);
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().length = modifier;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            rune.GetComponent<RoundWearOff>().target = target;
                            rune.GetComponent<RoundWearOff>().spellEffect = spellEffect;
                            rune.GetComponent<RoundWearOff>().specificEffect = specificEffect;
                        } else if ((int)element == 5) {
                            GameObject rune = Instantiate(fableRune);
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().length = modifier;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            rune.GetComponent<RoundWearOff>().target = target;
                            rune.GetComponent<RoundWearOff>().spellEffect = spellEffect;
                            rune.GetComponent<RoundWearOff>().specificEffect = specificEffect;
                        } else if ((int)element == 6) {
                            GameObject rune = Instantiate(frostRune);
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().length = modifier;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                            rune.GetComponent<RoundWearOff>().target = target;
                            rune.GetComponent<RoundWearOff>().spellEffect = spellEffect;
                            rune.GetComponent<RoundWearOff>().specificEffect = specificEffect;
                        }
                    } else if ((int)spellQuirk == 0 && specificEffect <= 4) {
                        if ((int)element == 1) {
                            GameObject rune = Instantiate(aetherRune);
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                        } else if ((int)element == 2) {
                            GameObject rune = Instantiate(fireRune);
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                        } else if ((int)element == 3) {
                            GameObject rune = Instantiate(necroticRune);
                            rune.transform.position = target.transform.position;
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                        } else if ((int)element == 4) {
                            GameObject rune = Instantiate(bloodRune);
                            rune.transform.position = target.transform.position;
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                        } else if ((int)element == 5) {
                            GameObject rune = Instantiate(fableRune);
                            rune.transform.position = target.transform.position;
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                        } else if ((int)element == 6) {
                            GameObject rune = Instantiate(frostRune);
                            rune.transform.position = target.transform.position;
                            rune.transform.position = target.transform.position;
                            rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                            rune.GetComponent<RoundWearOff>().destroysWhen = true;
                        }
                    }
                }
            }

        } else {
            AnnouncerManager.instance.ReceiveText("You lack components.", true);
            caster.GetComponent<Controller>().isCasting = false;
        }
    }

    public void CancelSpell () {
        Controller.instance.isCasting = false;
        heldSpell = null;
    }

    public void TriggerSpell (GameObject target) {
        //Spells relating to healing.
        if ((int)heldSpell.heldEffect == 0) {
            if (heldSpell.heldSpecific == 1) {
                foreach (PhysicalProperties.OrganOrLimb l in target.GetComponent<PhysicalProperties>().animate.limbsAndOrgans) {
                    l.health += heldSpell.modifier;
                }
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to rejuvenate " + target.GetComponent<CharacterData>().charName, false);
            } else if (heldSpell.heldSpecific == 2) {
                PhysicalProperties.OrganOrLimb mostInjuredLimb = null;
                float tempHealth = 100000;
                foreach (PhysicalProperties.OrganOrLimb l in target.GetComponent<PhysicalProperties>().animate.limbsAndOrgans) {
                    if (l.health < tempHealth) {
                        tempHealth = l.health;
                        mostInjuredLimb = l;
                    }
                }
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to heal the " + mostInjuredLimb.name + " of " + target.GetComponent<CharacterData>().charName, false);
                mostInjuredLimb.health += heldSpell.modifier * target.GetComponent<PhysicalProperties>().animate.limbsAndOrgans.Count;
                if (mostInjuredLimb.health > mostInjuredLimb.maxHealth) {
                    mostInjuredLimb.health = mostInjuredLimb.maxHealth;
                }
            } else if (heldSpell.heldSpecific == 3) {
                target.GetComponent<CharacterData>().toxinCounter -= heldSpell.modifier;
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to purge poisons from " + target.GetComponent<CharacterData>().charName + "'s body.", false);
            } else if (heldSpell.heldSpecific == 4) {
                target.GetComponent<CharacterData>().bleedCounter -= heldSpell.modifier;
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to stop " + target.GetComponent<CharacterData>().charName + "'s bleeding.", false);
            } else if (heldSpell.heldSpecific == 5) {
                foreach (PhysicalProperties.OrganOrLimb l in target.GetComponent<PhysicalProperties>().animate.limbsAndOrgans) {
                    l.health -= heldSpell.modifier;
                }
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to wither " + target.GetComponent<CharacterData>().charName + "'s flesh.", false);
            } else if (heldSpell.heldSpecific == 6) {
                //I think I want to change these so that you pick a specific body part.
                PhysicalProperties.OrganOrLimb leastInjuredLimb = null;
                float tempHealth = -100000;
                foreach (PhysicalProperties.OrganOrLimb l in target.GetComponent<PhysicalProperties>().animate.limbsAndOrgans) {
                    if (l.health > tempHealth) {
                        tempHealth = l.health;
                        leastInjuredLimb = l;
                    }
                }
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to pierce the " + leastInjuredLimb.name + " of " + target.GetComponent<CharacterData>().charName, false);
                leastInjuredLimb.health -= heldSpell.modifier * target.GetComponent<PhysicalProperties>().animate.limbsAndOrgans.Count;
            } else if (heldSpell.heldSpecific == 7) {
                target.GetComponent<CharacterData>().toxinCounter += heldSpell.modifier;
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to infuse " + target.GetComponent<CharacterData>().charName + "'s blood with poison.", false);
            } else if (heldSpell.heldSpecific == 8) {
                target.GetComponent<CharacterData>().bleedCounter += heldSpell.modifier;
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to tear open " + target.GetComponent<CharacterData>().charName + "'s skin.", false);
            }
            MagicPixel m = MagicHandler.instance.FindNearestPixel(target.gameObject.transform.position);
            MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
            Controller.instance.isCasting = false;
            if ((int)heldSpell.heldQuirk == 0) {
                if ((int)heldSpell.heldElement == 1) {
                    GameObject rune = Instantiate(aetherRune);
                    rune.transform.position = target.transform.position;
                    rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                    rune.GetComponent<RoundWearOff>().destroysWhen = true;
                } else if ((int)heldSpell.heldElement == 2) {
                    GameObject rune = Instantiate(fireRune);
                    rune.transform.position = target.transform.position;
                    rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                    rune.GetComponent<RoundWearOff>().destroysWhen = true;
                } else if ((int)heldSpell.heldElement == 3) {
                    GameObject rune = Instantiate(necroticRune);
                    rune.transform.position = target.transform.position;
                    rune.transform.position = target.transform.position;
                    rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                    rune.GetComponent<RoundWearOff>().destroysWhen = true;
                } else if ((int)heldSpell.heldElement == 4) {
                    GameObject rune = Instantiate(bloodRune);
                    rune.transform.position = target.transform.position;
                    rune.transform.position = target.transform.position;
                    rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                    rune.GetComponent<RoundWearOff>().destroysWhen = true;
                } else if ((int)heldSpell.heldElement == 5) {
                    GameObject rune = Instantiate(fableRune);
                    rune.transform.position = target.transform.position;
                    rune.transform.position = target.transform.position;
                    rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                    rune.GetComponent<RoundWearOff>().destroysWhen = true;
                } else if ((int)heldSpell.heldElement == 6) {
                    GameObject rune = Instantiate(frostRune);
                    rune.transform.position = target.transform.position;
                    rune.transform.position = target.transform.position;
                    rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                    rune.GetComponent<RoundWearOff>().destroysWhen = true;
                }
            }
        }

        //Spells relating to physical space.
        if ((int)heldSpell.heldEffect == 1) {
            if (heldSpell.heldSpecific == 1) {
                target.GetComponent<PhysicalProperties>().modifiedWeight = Mathf.RoundToInt(target.GetComponent<PhysicalProperties>().animate.weight * 0.75f);
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to lighten " + target.GetComponent<CharacterData>().charName, false);
                MagicPixel m = MagicHandler.instance.FindNearestPixel(target.gameObject.transform.position);
                MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
            } else if (heldSpell.heldSpecific == 2) {
                target.GetComponent<PhysicalProperties>().modifiedWeight = Mathf.RoundToInt(target.GetComponent<PhysicalProperties>().animate.weight * 0.25f);
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to make " + target.GetComponent<CharacterData>().charName + " weightless.", false);
                MagicPixel m = MagicHandler.instance.FindNearestPixel(target.gameObject.transform.position);
                MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
            } else if (heldSpell.heldSpecific == 3) {
                target.GetComponent<PhysicalProperties>().modifiedWeight = Mathf.RoundToInt(target.GetComponent<PhysicalProperties>().animate.weight * 1.25f);
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to weigh " + target.GetComponent<CharacterData>().charName + " down.", false);
                MagicPixel m = MagicHandler.instance.FindNearestPixel(target.gameObject.transform.position);
                MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
            } else if (heldSpell.heldSpecific == 4) {
                target.GetComponent<PhysicalProperties>().modifiedWeight = Mathf.RoundToInt(target.GetComponent<PhysicalProperties>().animate.weight * 2.25f);
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to make " + target.GetComponent<CharacterData>().charName + " too heavy to move.", false);
                MagicPixel m = MagicHandler.instance.FindNearestPixel(target.gameObject.transform.position);
                MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
            } else if (heldSpell.heldSpecific == 5) {
                int tempX = 0;
                int tempY = 0;
                if (heldSpell.caster.transform.position.x < target.transform.position.y) {
                    tempX = 1;
                } else {
                    tempX = -1;
                }
                if (heldSpell.caster.transform.position.y < target.transform.position.y) {
                    tempY = 1;
                } else {
                    tempY = -1;
                }
                target.GetComponent<PhysicalProperties>().xMomentum = tempX * heldSpell.modifier - target.GetComponent<PhysicalProperties>().animate.weight;
                target.GetComponent<PhysicalProperties>().yMomentum = tempY * heldSpell.modifier - target.GetComponent<PhysicalProperties>().animate.weight;
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to propel " + target.GetComponent<CharacterData>().charName + " away.", false);
                MagicPixel m = MagicHandler.instance.FindNearestPixel(target.gameObject.transform.position);
                MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
            } else if (heldSpell.heldSpecific == 6) {
                int tempX = 0;
                int tempY = 0;
                if (heldSpell.caster.transform.position.x < target.transform.position.y) {
                    tempX = -1;
                } else {
                    tempX = 1;
                }
                if (heldSpell.caster.transform.position.y < target.transform.position.y) {
                    tempY = -1;
                } else {
                    tempY = 1;
                }
                tempX *= heldSpell.modifier;
                tempY *= heldSpell.modifier;

                target.GetComponent<PhysicalProperties>().xMomentum = tempX - target.GetComponent<PhysicalProperties>().animate.weight;
                target.GetComponent<PhysicalProperties>().yMomentum = tempY - target.GetComponent<PhysicalProperties>().animate.weight;
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to launch " + target.GetComponent<CharacterData>().charName + " back towards them.", false);
                MagicPixel m = MagicHandler.instance.FindNearestPixel(target.gameObject.transform.position);
                MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
            } else if (heldSpell.heldSpecific == 7) {
                int tempX = 0;
                int tempY = 0;
                if (heldSpell.caster.transform.position.x < target.transform.position.y) {
                    tempX = 1;
                } else {
                    tempX = -1;
                }
                if (heldSpell.caster.transform.position.y < target.transform.position.y) {
                    tempY = 1;
                } else {
                    tempY = -1;
                }
                heldSpell.caster.GetComponent<PhysicalProperties>().xMomentum = tempX * heldSpell.modifier - heldSpell.caster.GetComponent<PhysicalProperties>().animate.weight;
                heldSpell.caster.GetComponent<PhysicalProperties>().yMomentum = tempY * heldSpell.modifier - heldSpell.caster.GetComponent<PhysicalProperties>().animate.weight;
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to propel theirself towards " + target.GetComponent<CharacterData>().charName, false);
                MagicPixel m = MagicHandler.instance.FindNearestPixel(heldSpell.caster.gameObject.transform.position);
                MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
            } else if (heldSpell.heldSpecific == 8) {
                int tempX = 0;
                int tempY = 0;
                if (heldSpell.caster.transform.position.x < target.transform.position.y) {
                    tempX = -1;
                } else {
                    tempX = 1;
                }
                if (heldSpell.caster.transform.position.y < target.transform.position.y) {
                    tempY = -1;
                } else {
                    tempY = 1;
                }
                tempX *= heldSpell.modifier;
                tempY *= heldSpell.modifier;

                heldSpell.caster.GetComponent<PhysicalProperties>().xMomentum = tempX - heldSpell.caster.GetComponent<PhysicalProperties>().animate.weight;
                heldSpell.caster.GetComponent<PhysicalProperties>().yMomentum = tempY - heldSpell.caster.GetComponent<PhysicalProperties>().animate.weight;
                AnnouncerManager.instance.ReceiveText(CharacterData.instance.charName + " cast a spell to launch away from " + target.GetComponent<CharacterData>().charName, false);
                MagicPixel m = MagicHandler.instance.FindNearestPixel(heldSpell.caster.gameObject.transform.position);
                MagicHandler.instance.SummonPixel(m, 3, heldSpell.heldElement);
            }
            Controller.instance.isCasting = false;
            target.GetComponent<PhysicalProperties>().CarryMomentum();
            target.GetComponent<PhysicalProperties>().EvaluateWeight();
        }
        Controller.instance.actionCount++;
        Controller.instance.actionBar.GetComponent<ActionBar>().CutAction();
        if (Controller.instance.actionCount >= Controller.instance.maxActionCount) {
            Controller.instance.actionCount = 0;
            Controller.instance.hasTakenTurn = true;
        }
        if ((int)heldSpell.heldQuirk == 0 && heldSpell.heldSpecific <= 4) {
            if ((int)heldSpell.heldElement == 1) {
                GameObject rune = Instantiate(aetherRune);
                rune.transform.position = target.transform.position;
                rune.GetComponent<RoundWearOff>().length = heldSpell.modifier;
                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                rune.GetComponent<RoundWearOff>().target = target;
                rune.GetComponent<RoundWearOff>().spellEffect = heldSpell.heldEffect;
                rune.GetComponent<RoundWearOff>().specificEffect = heldSpell.heldSpecific;
            } else if ((int)heldSpell.heldElement == 2) {
                GameObject rune = Instantiate(fireRune);
                rune.transform.position = target.transform.position;
                rune.GetComponent<RoundWearOff>().length = heldSpell.modifier;
                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                rune.GetComponent<RoundWearOff>().target = target;
                rune.GetComponent<RoundWearOff>().spellEffect = heldSpell.heldEffect;
                rune.GetComponent<RoundWearOff>().specificEffect = heldSpell.heldSpecific;
            } else if ((int)heldSpell.heldElement == 3) {
                GameObject rune = Instantiate(necroticRune);
                rune.transform.position = target.transform.position;
                rune.GetComponent<RoundWearOff>().length = heldSpell.modifier;
                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                rune.GetComponent<RoundWearOff>().target = target;
                rune.GetComponent<RoundWearOff>().spellEffect = heldSpell.heldEffect;
                rune.GetComponent<RoundWearOff>().specificEffect = heldSpell.heldSpecific;
            } else if ((int)heldSpell.heldElement == 4) {
                GameObject rune = Instantiate(bloodRune);
                rune.transform.position = target.transform.position;
                rune.GetComponent<RoundWearOff>().length = heldSpell.modifier;
                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                rune.GetComponent<RoundWearOff>().target = target;
                rune.GetComponent<RoundWearOff>().spellEffect = heldSpell.heldEffect;
                rune.GetComponent<RoundWearOff>().specificEffect = heldSpell.heldSpecific;
            } else if ((int)heldSpell.heldElement == 5) {
                GameObject rune = Instantiate(fableRune);
                rune.transform.position = target.transform.position;
                rune.GetComponent<RoundWearOff>().length = heldSpell.modifier;
                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                rune.GetComponent<RoundWearOff>().target = target;
                rune.GetComponent<RoundWearOff>().spellEffect = heldSpell.heldEffect;
                rune.GetComponent<RoundWearOff>().specificEffect = heldSpell.heldSpecific;
            } else if ((int)heldSpell.heldElement == 6) {
                GameObject rune = Instantiate(frostRune);
                rune.transform.position = target.transform.position;
                rune.GetComponent<RoundWearOff>().length = heldSpell.modifier;
                rune.GetComponent<RoundWearOff>().destroysWhen = true;
                rune.GetComponent<RoundWearOff>().target = target;
                rune.GetComponent<RoundWearOff>().spellEffect = heldSpell.heldEffect;
                rune.GetComponent<RoundWearOff>().specificEffect = heldSpell.heldSpecific;
            }
        } else if ((int)heldSpell.heldQuirk == 0 && heldSpell.heldSpecific > 4) {
            if ((int)heldSpell.heldElement == 1) {
                GameObject rune = Instantiate(aetherRune);
                rune.transform.position = target.transform.position;
                rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                rune.GetComponent<RoundWearOff>().destroysWhen = true;
            } else if ((int)heldSpell.heldElement == 2) {
                GameObject rune = Instantiate(fireRune);
                rune.transform.position = target.transform.position;
                rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                rune.GetComponent<RoundWearOff>().destroysWhen = true;
            } else if ((int)heldSpell.heldElement == 3) {
                GameObject rune = Instantiate(necroticRune);
                rune.transform.position = target.transform.position;
                rune.transform.position = target.transform.position;
                rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                rune.GetComponent<RoundWearOff>().destroysWhen = true;
            } else if ((int)heldSpell.heldElement == 4) {
                GameObject rune = Instantiate(bloodRune);
                rune.transform.position = target.transform.position;
                rune.transform.position = target.transform.position;
                rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                rune.GetComponent<RoundWearOff>().destroysWhen = true;
            } else if ((int)heldSpell.heldElement == 5) {
                GameObject rune = Instantiate(fableRune);
                rune.transform.position = target.transform.position;
                rune.transform.position = target.transform.position;
                rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                rune.GetComponent<RoundWearOff>().destroysWhen = true;
            } else if ((int)heldSpell.heldElement == 6) {
                GameObject rune = Instantiate(frostRune);
                rune.transform.position = target.transform.position;
                rune.transform.position = target.transform.position;
                rune.GetComponent<RoundWearOff>().lastSeconds = 5;
                rune.GetComponent<RoundWearOff>().destroysWhen = true;
            }
        }
    }

    public static SpellHandler instance;
    void Awake () {
        instance = this;
    }
}
}
