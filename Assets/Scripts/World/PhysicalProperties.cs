using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster{
    public class PhysicalProperties : MonoBehaviour
    {
        public bool isAnimate;
        public bool isHumanoid;
        public bool isClothed;
        public bool canBeCastOn;
        public InanimateObject inanimate;
        public AnimateObject animate;
        public List<InanimateObject> clothing = new List<InanimateObject>();

        //Momentum related.
        public int xMomentum;
        public int yMomentum;
        public List<Node> flightNodes = new List<Node>();

        //Mass related.
        public int modifiedWeight;
        public Vector3 floatPosition;


        [System.Serializable]
        public class InanimateObject {
            public ObjectMaterial objectMaterial;
            public ObjectSize size; 
            public int weight;
            public float temperature;
        }

        [System.Serializable]
        public class AnimateObject {
            public ObjectMaterial objectMaterial;
            public ObjectSize size;
            public int weight;
            public float overallHealth;
            public float temperature;
            public bool hasThought;
            public Mind mind;
            
            public List<OrganOrLimb> limbsAndOrgans = new List<OrganOrLimb>();
        }

        void Start () {
            if (isAnimate == true) {
                if (isHumanoid == true) {
                    OrganOrLimb lA = new OrganOrLimb();
                    lA.partType = (OrgansAndLimbs)0;
                    lA.isLeftOfPair = true;
                    lA.name = "left arm";
                    animate.limbsAndOrgans.Add(lA);

                    OrganOrLimb rA = new OrganOrLimb();
                    rA.partType = (OrgansAndLimbs)0;
                    rA.name = "right arm";
                    animate.limbsAndOrgans.Add(rA);

                    OrganOrLimb lL = new OrganOrLimb();
                    lL.partType = (OrgansAndLimbs)1;
                    lL.isLeftOfPair = true;
                    lL.name = "left leg";
                    animate.limbsAndOrgans.Add(lL);

                    OrganOrLimb rL = new OrganOrLimb();
                    rL.partType = (OrgansAndLimbs)1;
                    rL.name = "right leg";
                    animate.limbsAndOrgans.Add(rL);

                    OrganOrLimb h = new OrganOrLimb();
                    h.partType = (OrgansAndLimbs)2;
                    h.name = "head";
                    h.centerOfThought = true;
                    animate.limbsAndOrgans.Add(h);

                    //Sanguine, Choleric, Melancholic, Phlegmatic
                    if ((int)animate.mind.personality == 0) {
                        animate.mind.sanity += -5;
                        animate.mind.stress += 10;
                        animate.mind.anger += 5;
                        animate.mind.sadness += 0;
                    } else if ((int)animate.mind.personality == 1) {
                        animate.mind.sanity += -5;
                        animate.mind.stress += 5;
                        animate.mind.anger += 10;
                        animate.mind.sadness += 0;
                    } else if ((int)animate.mind.personality == 2) {
                        animate.mind.sanity += 5;
                        animate.mind.stress += 0;
                        animate.mind.anger += -5;
                        animate.mind.sadness += 10;
                    } else if ((int)animate.mind.personality == 3) {
                        animate.mind.sanity += 10;
                        animate.mind.stress += 0;
                        animate.mind.anger += -5;
                        animate.mind.sadness += 5;
                    }

                    OrganOrLimb t = new OrganOrLimb();
                    t.partType = (OrgansAndLimbs)3;
                    t.name = "torso";
                    animate.limbsAndOrgans.Add(t);

                    OrganOrLimb c = new OrganOrLimb();
                    c.partType = (OrgansAndLimbs)4;
                    c.name = "chest";
                    animate.limbsAndOrgans.Add(c);

                    OrganOrLimb lLu = new OrganOrLimb();
                    lLu.partType = (OrgansAndLimbs)5;
                    lLu.isLeftOfPair = true;
                    lLu.name = "left lung";
                    animate.limbsAndOrgans.Add(lLu);

                    OrganOrLimb rLu = new OrganOrLimb();
                    rLu.partType = (OrgansAndLimbs)5;
                    rLu.name = "right lung";
                    animate.limbsAndOrgans.Add(rLu);

                    OrganOrLimb s = new OrganOrLimb();
                    s.partType = (OrgansAndLimbs)6;
                    s.name = "stomach";
                    animate.limbsAndOrgans.Add(s);

                    OrganOrLimb hrt = new OrganOrLimb();
                    hrt.partType = (OrgansAndLimbs)7;
                    hrt.name = "heart";
                    animate.limbsAndOrgans.Add(hrt);

                    OrganOrLimb lE = new OrganOrLimb();
                    lE.partType = (OrgansAndLimbs)8;
                    lE.isLeftOfPair = true;
                    lE.name = "left eye";
                    animate.limbsAndOrgans.Add(lE);

                    OrganOrLimb rE = new OrganOrLimb();
                    rE.partType = (OrgansAndLimbs)8;
                    rE.name = "right eye";
                    animate.limbsAndOrgans.Add(rE);

                    this.gameObject.GetComponent<CharacterData>().canSee = true;
                    this.gameObject.GetComponent<Controller>().canMove = true;
                    this.gameObject.GetComponent<CharacterData>().canAttack = true;
                    this.gameObject.GetComponent<CharacterData>().canCast = true;
                    this.gameObject.GetComponent<Controller>().canAct = true;
                }

                modifiedWeight = animate.weight;
                foreach (OrganOrLimb l in animate.limbsAndOrgans) {
                    l.maxHealth = this.gameObject.GetComponent<CharacterData>().maxHealth;
                    l.health = this.gameObject.GetComponent<CharacterData>().maxHealth;
                }

                EvaluateHealth();
            } else {
                modifiedWeight = inanimate.weight;
            }
        }

        public void CarryMomentum () {
            if (xMomentum != 0 || yMomentum != 0) {
                this.gameObject.GetComponent<Controller>().canAct = false;
                int smackDamage = 0;

                int maxDist = Mathf.Abs(xMomentum) + Mathf.Abs(yMomentum);
                if (maxDist > 8) {
                    maxDist = 8;
                }
                int xTravelled = 0;
                int yTravelled = 0;

                for (int i = 1; i <= maxDist; i++) {
                    if (xMomentum > xTravelled) {
                        xTravelled++;
                    } else if (xMomentum < xTravelled) {
                        xTravelled--;
                    }
                    if (yMomentum > yTravelled) {
                        yTravelled++;
                    } else if (yMomentum < yTravelled) {
                        yTravelled--;
                    }
                    Node newNode = GridHandler.instance.GetNode(Mathf.RoundToInt(this.GetComponentInParent<CoordinateHolder>().corX) + xTravelled, Mathf.RoundToInt(this.GetComponentInParent<CoordinateHolder>().corY) + yTravelled);
                    if (newNode != null) {
                        if (newNode.worldObject.GetComponent<CoordinateHolder>().isWall != true) {
                            flightNodes.Add(newNode);
                        } else {
                            smackDamage += xMomentum + yMomentum;
                            xMomentum = 0;
                            yMomentum = 0;
                            break;
                        }
                    }
                }
                xMomentum -= xTravelled;
                yMomentum -= yTravelled;
                
                this.gameObject.GetComponent<Controller>().launchSpeed = 0.5f;
                this.gameObject.GetComponent<Controller>().landNode = flightNodes[flightNodes.Count - 1];
                this.gameObject.GetComponent<Controller>().isLaunching = true;
            }
        }

        public void EvaluateWeight () {
            if (modifiedWeight > animate.weight * 2) {
                this.gameObject.GetComponent<CharacterData>().moveDistMod = -1 * this.gameObject.GetComponent<CharacterData>().baseMoveDist;
            } else if (modifiedWeight > animate.weight) {
                this.gameObject.GetComponent<Controller>().isWeightless = false;
                this.gameObject.GetComponent<CharacterData>().moveDistMod = -1 & Mathf.RoundToInt(0.5f * this.gameObject.GetComponent<CharacterData>().baseMoveDist);
            } else if (modifiedWeight < animate.weight / 2) {
                this.gameObject.GetComponent<CharacterData>().moveDistMod = -1 * this.gameObject.GetComponent<CharacterData>().baseMoveDist;
                this.gameObject.GetComponent<Controller>().isWeightless = true;
            } else if (modifiedWeight < animate.weight) {
                this.gameObject.GetComponent<Controller>().isWeightless = false;
                this.gameObject.GetComponent<CharacterData>().moveDistMod = this.gameObject.GetComponent<CharacterData>().baseMoveDist;
            } else if (modifiedWeight == animate.weight) {
                this.gameObject.GetComponent<Controller>().isWeightless = false;
                this.gameObject.GetComponent<CharacterData>().moveDistMod = 0;
            }
        }

        public void EvaluateTemperature () {
            if (isAnimate == true) {
                if (isClothed == true && clothing.Count > 0) {
                    foreach (InanimateObject c in clothing) {
                        if ((int)c.objectMaterial == 0 && c.temperature > 0) {
                            c.temperature -= 5; 
                        } else if ((int)c.objectMaterial == 1 && c.temperature > 0) {
                            c.temperature -= 10;
                        } else if ((int)c.objectMaterial == 2 && c.temperature > 0) {
                            c.temperature -= 20;
                        } else if ((int)c.objectMaterial == 3 && c.temperature > 0) {
                            c.temperature -= 15;
                        } else if ((int)c.objectMaterial == 4 && c.temperature > 0) {
                            c.temperature -= 10;
                        }

                        if ((int)c.objectMaterial == 0 && c.temperature < 0) {
                            c.temperature += 5; 
                        } else if ((int)c.objectMaterial == 1 && c.temperature < 0) {
                            c.temperature += 10;
                        } else if ((int)c.objectMaterial == 2 && c.temperature < 0) {
                            c.temperature += 20;
                        } else if ((int)c.objectMaterial == 3 && c.temperature < 0) {
                            c.temperature += 15;
                        } else if ((int)c.objectMaterial == 4 && c.temperature < 0) {
                            c.temperature += 10;
                        }
                        if (c.temperature < 5 && c.temperature > -5) {
                            c.temperature = 0;
                        }

                        animate.temperature += c.temperature / 5;
                    }
                }

                if (this.gameObject == CharacterData.instance.gameObject) {
                    if (EquipmentRenderer.instance.equippedWeapon != null) {
                        if (EquipmentRenderer.instance.equippedWeapon.GetComponent<ItemData>().objectProperties.temperature >= 100 + CharacterData.instance.endurance * 5) {
                            GameObject equippedWeapon = EquipmentRenderer.instance.equippedWeapon;
                            EquipmentRenderer.instance.Unequip(4, equippedWeapon);

                            Inventory.instance.bagItems.Remove(equippedWeapon);
                            equippedWeapon.transform.parent = null;
                            equippedWeapon.GetComponent<SpriteRenderer>().enabled = true;
                            AnnouncerManager.instance.ReceiveText("Your weapon's hilt grows so hot that it singes your hands, clattering to the ground.", true);
                        }
                    } else if (EquipmentRenderer.instance.equippedWeapon != null) {
                        if (EquipmentRenderer.instance.equippedWeapon.GetComponent<ItemData>().objectProperties.temperature <= -100 - CharacterData.instance.endurance * 5) {
                            GameObject equippedWeapon = EquipmentRenderer.instance.equippedWeapon;
                            EquipmentRenderer.instance.Unequip(4, equippedWeapon);

                            Inventory.instance.bagItems.Remove(equippedWeapon);
                            equippedWeapon.transform.parent = null;
                            equippedWeapon.GetComponent<SpriteRenderer>().enabled = true;
                            AnnouncerManager.instance.ReceiveText("Your weapon's hilt grows so cold that it burns your hand. You reflexively drop it.", true);
                        }
                    }
                }

                if (animate.temperature > 100) {
                    TakeBioDamage(animate.temperature - 100, (DamageType)3);
                }
                if (animate.temperature < -100) {
                    TakeBioDamage(-100 - animate.temperature, (DamageType)6);
                }
            }
        }

        public void EvaluateAll () {
            CarryMomentum();
            EvaluateWeight();
            EvaluateTemperature();

            if (isAnimate == true) {
                if (modifiedWeight == animate.weight && xMomentum == 0 && yMomentum == 0) {
                    this.gameObject.GetComponent<Controller>().canAct = true;
                    this.gameObject.GetComponent<Controller>().isWeightless = false;
                } else if (modifiedWeight < animate.weight * 2 && modifiedWeight > animate.weight / 2 && xMomentum == 0 && yMomentum == 0) {
                    this.gameObject.GetComponent<Controller>().canAct = true;
                    this.gameObject.GetComponent<Controller>().isWeightless = false;
                } else {
                    this.gameObject.GetComponent<Controller>().canAct = false;
                    floatPosition = this.gameObject.transform.position + new Vector3 (0f, 0.05f, 0f);
                    this.gameObject.GetComponent<Controller>().isWeightless = true;
                }
            }
        }

        public void EvaluateBloodStream () {
            float toxinBuildUp = this.gameObject.GetComponent<CharacterData>().toxinCounter;
            if (toxinBuildUp != 0) {
                if ((int)animate.size == 0 && toxinBuildUp > 5) {
                    TakeBioDamage(5, (DamageType)5);
                } else if ((int)animate.size == 1 && toxinBuildUp > 10) {
                    TakeBioDamage(5, (DamageType)5);
                } else if ((int)animate.size == 1 && toxinBuildUp > 20) {
                    TakeBioDamage(5, (DamageType)5);
                } else if ((int)animate.size == 2 && toxinBuildUp > 40) {
                    TakeBioDamage(5, (DamageType)5);
                } else if ((int)animate.size == 3 && toxinBuildUp > 80) {
                    TakeBioDamage(5, (DamageType)5);
                } else if ((int)animate.size == 4 && toxinBuildUp > 100) {
                    TakeBioDamage(5, (DamageType)5);
                }
            }
        }

        public void TakeBioDamage (float damage, DamageType damageType) {
            if (this.gameObject.GetComponent<CharacterData>().isDead == false) {
            
            string descriptor = "";
            if (damage < 5) {
                descriptor = "lightly";
            } else if (damage > 15) {
                descriptor = "mortally";
            }
            
            if ((int)damageType == 0) {
                int whichLimb = Mathf.RoundToInt(Random.Range(0, animate.limbsAndOrgans.Count - 1));
                animate.limbsAndOrgans[whichLimb].health -= damage;
                AnnouncerManager.instance.ReceiveText(this.gameObject.GetComponent<CharacterData>().charName + " was " + descriptor + " pierced in their " + animate.limbsAndOrgans[whichLimb].name, false);
                MagicPixel m = MagicHandler.instance.FindNearestPixel(this.gameObject.transform.position + new Vector3(0f, 0.05f, 0f));
                MagicHandler.instance.SummonPixel(m, 1, (PixelType)4);
            } else if ((int)damageType == 1) {
                int whichLimb = Mathf.RoundToInt(Random.Range(0, animate.limbsAndOrgans.Count - 1));
                animate.limbsAndOrgans[whichLimb].health -= damage / 2;
                int whichLimbTwo = Mathf.RoundToInt(Random.Range(0, animate.limbsAndOrgans.Count - 1));
                animate.limbsAndOrgans[whichLimbTwo].health -= damage / 2;
                AnnouncerManager.instance.ReceiveText(this.gameObject.GetComponent<CharacterData>().charName + " was " + descriptor + " slashed across their " + animate.limbsAndOrgans[whichLimb].name + " and " + animate.limbsAndOrgans[whichLimbTwo].name, false);
                MagicPixel m = MagicHandler.instance.FindNearestPixel(this.gameObject.transform.position + new Vector3(0f, 0.05f, 0f));
                MagicHandler.instance.SummonPixel(m, 1, (PixelType)4);
            } else if ((int)damageType == 2) {
                float averageHealth = 0;
                foreach (OrganOrLimb o in animate.limbsAndOrgans) {
                    averageHealth += o.health;
                }
                averageHealth = averageHealth / animate.limbsAndOrgans.Count;
                AnnouncerManager.instance.ReceiveText(this.gameObject.GetComponent<CharacterData>().charName + " was " + descriptor + " bludgeoned, injuring their ", false);
                foreach (OrganOrLimb o in animate.limbsAndOrgans) {
                    if (o.health > averageHealth) {
                        o.health -= damage;
                        AnnouncerManager.instance.ReceiveText(o.name, false);
                    }
                }
                MagicPixel m = MagicHandler.instance.FindNearestPixel(this.gameObject.transform.position + new Vector3(0f, 0.05f, 0f));
                MagicHandler.instance.SummonPixel(m, 1, (PixelType)4);
            } else if ((int)damageType == 3) {
                foreach (OrganOrLimb o in animate.limbsAndOrgans) {
                    if ((int)o.partType <= 4) {
                        o.health -= damage;
                    }
                }
                AnnouncerManager.instance.ReceiveText(this.gameObject.GetComponent<CharacterData>().charName + " was " + descriptor + " burned.", false);
                MagicPixel m = MagicHandler.instance.FindNearestPixel(this.gameObject.transform.position + new Vector3(0f, 0.05f, 0f));
                MagicHandler.instance.SummonPixel(m, 1, (PixelType)2);
            } else if ((int)damageType == 4) {
                foreach (OrganOrLimb o in animate.limbsAndOrgans) {
                    if ((int)o.partType >= 5 && (int)o.partType != 8) {
                        o.health -= damage;
                    }
                }
                AnnouncerManager.instance.ReceiveText(this.gameObject.GetComponent<CharacterData>().charName + descriptor + " bleeds.", false);
                MagicPixel m = MagicHandler.instance.FindNearestPixel(this.gameObject.transform.position + new Vector3(0f, 0.05f, 0f));
                MagicHandler.instance.SummonPixel(m, 1, (PixelType)4);
            } else if ((int)damageType == 5) {
                foreach (OrganOrLimb o in animate.limbsAndOrgans) {
                    o.health -= damage;
                }
                AnnouncerManager.instance.ReceiveText(this.gameObject.GetComponent<CharacterData>().charName + " looks sickly and pale.", false);
                MagicPixel m = MagicHandler.instance.FindNearestPixel(this.gameObject.transform.position + new Vector3(0f, 0.05f, 0f));
                MagicHandler.instance.SummonPixel(m, 1, (PixelType)3);
            } else if ((int)damageType == 6) {
                foreach (OrganOrLimb o in animate.limbsAndOrgans) {
                    if ((int)o.partType <= 4) {
                        o.health -= damage;
                    }
                }
                AnnouncerManager.instance.ReceiveText(this.gameObject.GetComponent<CharacterData>().charName + " was " + descriptor + " burned by frost.", false);
                MagicPixel m = MagicHandler.instance.FindNearestPixel(this.gameObject.transform.position + new Vector3(0f, 0.05f, 0f));
                MagicHandler.instance.SummonPixel(m, 1, (PixelType)6);
            }
            
            if ((int)damageType != 5) {
                EvaluateHealth();
            }

            if (this.gameObject == CharacterData.instance.gameObject) {
                HealthBar.instance.CheckHealth();
            }
            }
        }

        public void EvaluateHealth () {
            if (this.gameObject.GetComponent<CharacterData>().isDead == false) {

            int eyeCount = 0;
            int loseLimb = 0;
            OrganOrLimb limbToLose = null;

            foreach (OrganOrLimb o in animate.limbsAndOrgans) {
                if ((int)o.partType == 0) {
                    if (o.health < o.maxHealth / 2 && this.gameObject.GetComponent<CharacterData>().martialMod >= -2) {
                        this.gameObject.GetComponent<CharacterData>().martialMod -= 2;
                    } else if (o.health <= 0 && this.gameObject.GetComponent<CharacterData>().hasLostLimb == false && this.gameObject.GetComponent<CharacterData>().canLoseLimbs == true) {
                        if (o.isLeftOfPair == true) {
                            loseLimb = 1;
                            limbToLose = o;
                        } else {
                            loseLimb = 2;
                            limbToLose = o;
                        }
                    }
                    if (o.health < 0) {
                        animate.mind.shock += 1;
                    }
                }
                if ((int)o.partType == 1) {
                    if (o.health < o.maxHealth / 2 && this.gameObject.GetComponent<CharacterData>().moveDistMod >= -2) {
                        this.gameObject.GetComponent<CharacterData>().moveDistMod -= 1;
                    } else if (o.health <= 0 && this.gameObject.GetComponent<CharacterData>().moveDistMod >= -4) {
                        this.gameObject.GetComponent<CharacterData>().moveDistMod -= -2;
                    }
                    if (o.health < 0) {
                        animate.mind.shock += 1;
                    }
                }
                if ((int)o.partType == 2) {
                    if (o.health < o.maxHealth / 2 && this.gameObject.GetComponent<CharacterData>().intelligenceMod > -4) {
                        this.gameObject.GetComponent<CharacterData>().intelligenceMod -= 2;
                    } else if (o.health == 0) {
                        this.gameObject.GetComponent<CharacterData>().isUnconscious = true;
                    }
                    if (o.health < 0) {
                        animate.mind.shock += 1;
                    }
                }
                if ((int)o.partType == 3) {
                    if (o.health < o.maxHealth / 2 && this.gameObject.GetComponent<CharacterData>().intelligenceMod > -4) {
                        this.gameObject.GetComponent<CharacterData>().athleticsMod -= 2;
                    } else if (o.health == 0) {
                        this.gameObject.GetComponent<CharacterData>().isDead = true;
                    }
                    if (o.health < 0) {
                        animate.mind.shock += 1;
                    }
                }
                if ((int)o.partType == 4 && this.gameObject.GetComponent<CharacterData>().strengthMod > -4) {
                    if (o.health < o.maxHealth / 2) {
                        this.gameObject.GetComponent<CharacterData>().strengthMod -= 2;
                    } else if (o.health == 0) {
                        this.gameObject.GetComponent<CharacterData>().isDead = true;
                    }
                    if (o.health < 0) {
                        animate.mind.shock += 1;
                    }
                }
                if ((int)o.partType == 5) {
                    if (o.health < o.maxHealth / 2 && this.gameObject.GetComponent<CharacterData>().enduranceMod > -4) {
                        this.gameObject.GetComponent<CharacterData>().enduranceMod -= 2;
                    } else if (o.health == 0 && this.gameObject.GetComponent<CharacterData>().enduranceMod > -8) {
                        this.gameObject.GetComponent<CharacterData>().enduranceMod -= 4;
                    }
                    if (o.health < 0) {
                        animate.mind.shock += 1;
                    }
                }
                if ((int)o.partType == 6) {
                    if (o.health < o.maxHealth / 2) {
                        this.gameObject.GetComponent<CharacterData>().toxinCounter += 5;
                    } else if (o.health == 0) {
                        this.gameObject.GetComponent<CharacterData>().bleedCounter += 10;
                    }
                    if (o.health < 0) {
                        animate.mind.shock += 1;
                    }
                }
                if ((int)o.partType == 7) {
                    if (o.health < o.maxHealth / 2) {
                        this.gameObject.GetComponent<CharacterData>().bleedCounter += 3;
                    } else if (o.health == 0) {
                        this.gameObject.GetComponent<CharacterData>().isDead = true;
                    }
                    if (o.health < 0) {
                        animate.mind.shock += 1;
                    }
                }
                if ((int)o.partType == 8) {
                    eyeCount++;
                    if (o.health < o.maxHealth / 2 && this.gameObject.GetComponent<CharacterData>().sightMod > 5) {
                        this.gameObject.GetComponent<CharacterData>().sightMod -= 1;
                    } else if (o.health == 0) {
                        eyeCount--;
                    }
                    if (o.health < 0) {
                        animate.mind.shock += 1;
                    }
                }
            }

            if (loseLimb == 1) {
                this.gameObject.GetComponent<CharacterData>().RemoveLimb(25);
                animate.limbsAndOrgans.Remove(limbToLose);
                this.gameObject.GetComponent<CharacterData>().canCast = false;
            } else if (loseLimb == 2) {
                this.gameObject.GetComponent<CharacterData>().RemoveLimb(75);
                animate.limbsAndOrgans.Remove(limbToLose);
                this.gameObject.GetComponent<CharacterData>().canAttack = false;
            }
            if (eyeCount > 0) {
                this.gameObject.GetComponent<CharacterData>().canSee = true;
                this.gameObject.GetComponent<CharacterData>().baseSight = eyeCount * 5;
            } else {
                this.gameObject.GetComponent<CharacterData>().canSee = false;
                if (this.gameObject == CharacterData.instance.gameObject) {
                    CharacterData.instance.sightMod = CharacterData.instance.baseSight - 0.01f;
                }
            }

            float averageHealth = 0;
            foreach (OrganOrLimb h in animate.limbsAndOrgans) {
                averageHealth += h.health;
            }
            averageHealth = averageHealth / animate.limbsAndOrgans.Count;
            animate.overallHealth = averageHealth;
            }
        }

        public void InitiateRelationships () {
            GameObject[] actors = GameObject.FindGameObjectsWithTag("Actor");
            foreach (GameObject g in actors) {
                Relationship newRelationship = new Relationship();
                newRelationship.relTarget = g;
                newRelationship.relationshipNumber = this.gameObject.GetComponent<CharacterData>().factionArray.relationships[g.GetComponent<CharacterData>().factionID].relationshipNum;
                if (this.gameObject.GetComponent<CharacterData>().factionArray.relationships[g.GetComponent<CharacterData>().factionID].atWar == false) {
                    newRelationship.relationshipNumber = newRelationship.relationshipNumber / 100;
                }
                newRelationship.relationshipNumber += animate.mind.anger;
                animate.mind.relationships.Add(newRelationship);
            }
        }

        public void EvaluatePsychology () {
            if (animate.mind.shock > 50) {
                this.gameObject.GetComponent<CharacterData>().isUnconscious = true;
                AnnouncerManager.instance.ReceiveText(this.gameObject.GetComponent<CharacterData>().charName + "fell unconscious due to shock.", true);
            }
        }

        public enum ObjectMaterial { wood, stone, metal, flesh, fabric }
        public enum ObjectSize { tiny, small, medium, large, titanic }
        public enum DamageType { pierce, slash, blunt, burn, bleed, toxin, freeze }
        public enum OrgansAndLimbs { arm, leg, head, torso, chest, lung, stomach, heart, eye }

        [System.Serializable]
        public class OrganOrLimb {
            public string name;
            public OrgansAndLimbs partType;
            public float health;
            public float maxHealth;
            public bool isLeftOfPair;
            public bool centerOfThought;
        }

        [System.Serializable]
        public class Mind {
            public PersonalityType personality;
            public float shock;
            public float sanity;
            public float stress;
            public float anger;
            public float sadness;
            public List<Relationship> relationships = new List<Relationship>();
        }

        [System.Serializable]
        public class Relationship {
            public GameObject relTarget;
            public float relationshipNumber;
        }

        public enum PersonalityType { Sanguine, Choleric, Melancholic, Phlegmatic }
    }
}
