using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class RoundWearOff : MonoBehaviour
    {
        public bool destroysWhen;
        public bool blocksNode;
        public int length;
        public float lastSeconds;
        public bool hasSeconds;
        public SpellEffect spellEffect;
        public int specificEffect;
        public GameObject target;
        public Node node;
        //public float corX;
        //public float corY;

        void Update () {
            if (length <= 0) {
                if (destroysWhen == true) {
                    if (blocksNode == true) {
                        node.worldObject.GetComponent<CoordinateHolder>().isWall = false;
                    }
                    //Wear off the effect of the spell.
                    if ((int)spellEffect == 1) {
                        target.GetComponent<PhysicalProperties>().modifiedWeight = target.GetComponent<PhysicalProperties>().animate.weight;
                        target.GetComponent<PhysicalProperties>().EvaluateWeight();
                    }

                    Destroy(this.gameObject);
                }
            }
            if (lastSeconds != 0 && hasSeconds == false) {
                hasSeconds = true;
                Invoke("DestroyInSeconds", lastSeconds);
            }
        }

        void DestroyInSeconds () {
            Destroy(this.gameObject);
        }

        public void RoundCount () {
            length -= 1;
        }
    }
}
