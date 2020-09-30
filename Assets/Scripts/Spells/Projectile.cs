using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class Projectile : MonoBehaviour
    {
        public float speed;
        public Vector3 destination;
        public PixelType pixelType;
        float checkTime;
        void Awake () {
            checkTime = Time.fixedTime;
        }

        void Update () {
            transform.position = Vector3.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);

            if (Vector3.Distance(this.transform.position, destination) < 0.05f) {
                Destroy(this.gameObject);
            }

            if (checkTime + 0.1f < Time.fixedTime) {
                checkTime = Time.fixedTime;
                MagicPixel nearestPixel = MagicHandler.instance.FindNearestPixel(this.gameObject.transform.position);
                MagicHandler.instance.SummonPixel(nearestPixel, 1, pixelType);
            }
        }
    }
}
