using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class DetailMovement : MonoBehaviour
    {
        public bool oscillatesY;
        public bool oscillatesX;
        public Vector2 oscDirect;
        public float oscFreq;
        public float startRand;

        void Start () {
            startRand = Random.Range(0,0.05f);
        }

        void Update () {
            if (oscillatesY == true) {
                float newY = Mathf.PingPong(Time.time * oscFreq, oscDirect.y - startRand);
                this.gameObject.transform.position = new Vector2 (this.transform.position.x, this.transform.position.x + newY);
            }
        }
    }
}
