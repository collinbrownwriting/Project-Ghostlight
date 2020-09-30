using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class RenderLevel : MonoBehaviour
    {
        public int offset;
        public float backOffset;
        public Vector2 location;
        public bool isBackground;
        public float distTrans;
        public float distProc;
        public int width;
        public GameObject shadow;
        public Vector3 shadowOffsetVector;

        public GameObject[] actors;

        public bool hasActorBehind;

        public bool isBeyondSight;
        public Material outofSight;
        public Material standardShade;
        public bool hasBeenSeen;
        public bool npcInt;

        private void Start() {
            actors = GameObject.FindGameObjectsWithTag("Actor");

            if (this.gameObject.GetComponent<CharacterData>() != null) {
                if (this.gameObject.GetComponent<CharacterData>().width == 1) {
                    width = 1;
                } else if (this.gameObject.GetComponent<CharacterData>().width <= 5) {
                    width = 2;
                } else {
                    width = Mathf.RoundToInt(this.gameObject.GetComponent<CharacterData>().width / 2);
                }
            }
            if (shadow != null) {
                GameObject newShadow = Instantiate(shadow);
                newShadow.transform.parent = this.gameObject.transform;
                newShadow.transform.position = new Vector3 (this.gameObject.transform.position.x - shadowOffsetVector.x, this.gameObject.transform.position.y - shadowOffsetVector.y, this.gameObject.transform.position.z);
                newShadow.gameObject.transform.localScale = new Vector3 (0.3f * width, 0.3f * width, 0.3f * width);
            }
        }

        void Update()
        {
            location = this.transform.position;
            int locrend = -1 * (Mathf.RoundToInt(location.y / 0.085f) * 4) + offset;

            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = locrend;

            if (hasBeenSeen == true && isBeyondSight == true && Vector2.Distance(this.transform.position, CharacterData.instance.gameObject.transform.position) > CharacterData.instance.sightDist * Manager.instance.sightThreshold) {
                this.gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                this.gameObject.GetComponent<Renderer>().material = outofSight;
            } else if (isBeyondSight == true && Vector2.Distance(this.transform.position, CharacterData.instance.gameObject.transform.position) <= CharacterData.instance.sightDist  * Manager.instance.sightThreshold) {
                hasBeenSeen = true;
                this.gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                if (npcInt == false) {
                    this.gameObject.GetComponent<Renderer>().material = standardShade;
                }
            } else if (hasBeenSeen == false && isBeyondSight == true) {
                this.gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                this.gameObject.GetComponent<Renderer>().material = standardShade;
            }

            if (isBackground == true) {
                actors = GameObject.FindGameObjectsWithTag("Actor");
                foreach (GameObject g in actors) {
                    if (Vector2.Distance (this.transform.position, g.transform.position) <= distProc && Vector2.Distance (this.transform.position, g.transform.position) <= distTrans * (g.transform.position.y - this.transform.position.y) && g.transform.position.y >= this.transform.position.y + backOffset) { 
                        hasActorBehind = true;
                    }
                }

                if (hasActorBehind == true) {
                    this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
                } else {
                    this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                }

                hasActorBehind = false;

            }
        }
    }
}