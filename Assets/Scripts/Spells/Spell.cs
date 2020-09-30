using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {

    [System.Serializable]
    public class Spell {
        public string name;
        public int schoolOfOrigin;
        public PixelType element;
        public SpellEffect spellEffect;
        public int specificEffect;
        public SpellComponent spellComponent;
        public SpellQuirk spellQuirk;
        public bool offensive;
        public int modifier;
        public int tier;
        public int statRequirement;
        public int cost;
        public bool lost;
        public bool unique;
        public bool isInInven;

        public void Activate (GameObject caster) {
            bool willCast = false;
            if ((int)spellQuirk == 0) {
                if (caster.GetComponent<CharacterData>().intelligence >= statRequirement) {
                    willCast = true;
                }
            } else if ((int)spellQuirk >= 1) {
                willCast = true;
                //Other quirks will be implemented later.
            }

            if (caster.GetComponent<CharacterData>().canCast == true && caster.GetComponent<Controller>().actionCount + 1 <= caster.GetComponent<Controller>().maxActionCount && RoundController.instance.inCombat == true && caster.GetComponent<Controller>().isCasting == false && caster.GetComponent<Controller>().isTurn == true && willCast == true) {
                if (caster == CharacterData.instance.gameObject) {
                    AnnouncerManager.instance.ReceiveText("You cast " + name, false);
                    if (offensive == true) {
                        caster.GetComponent<Controller>().isCursing = true;
                    }
                    caster.GetComponent<Controller>().isCasting = true;
                }
                SpellHandler.instance.CastSpell(spellEffect, specificEffect, spellComponent, spellQuirk, element, modifier, statRequirement, cost, caster, null);

            } else if (caster.GetComponent<CharacterData>().canCast == true && caster.GetComponent<Controller>().freeRoam == true && willCast == true) {
                if (caster == CharacterData.instance.gameObject) {
                    AnnouncerManager.instance.ReceiveText("You cast " + name, false);
                    if (offensive == true) {
                        caster.GetComponent<Controller>().isCursing = true;
                    }
                    caster.GetComponent<Controller>().isCasting = true;
                }
                SpellHandler.instance.CastSpell(spellEffect, specificEffect, spellComponent, spellQuirk, element, modifier, statRequirement, cost, caster, null);
            } else if (caster == CharacterData.instance.gameObject) {
                AnnouncerManager.instance.ReceiveText("You do not meet the requirements for " + name, true);
            }
        }
    }
    public enum SpellEffect { biological, physical, transmutative, psychological }
    public enum SpellComponent { potion, blood, toxin, burn, freeze, explosive, debuff, social, limb, life }
    public enum SpellQuirk { runic, contractual, alchemical }
}
