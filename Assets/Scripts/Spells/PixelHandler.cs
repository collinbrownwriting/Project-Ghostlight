using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace GridMaster {
    public class PixelHandler : MonoBehaviour
    {
        public Sprite aether;
        public Sprite fire;
        public Sprite necro;
        public Sprite blood;
        bool isLight;
        Color newColor;
        public Light2D m_Light2D;

        public void SetType (int i) {
            if (i == 0) {
                //Do nothing.
            } else if (i == 1) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = aether;
                isLight = true;
                newColor = Color.cyan;
                
            } else if (i == 2) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = fire;
                isLight = true;
                newColor = Color.red;
            } else if (i == 3) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = necro;
                isLight = true;
                newColor = Color.green;
            } else if (i == 4) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = blood;
            }

            if (isLight == true) {
                m_Light2D.enabled = true;
                m_Light2D.color = newColor;
            }
        }
    }
}
