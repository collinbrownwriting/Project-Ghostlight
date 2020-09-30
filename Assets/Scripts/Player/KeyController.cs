using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster
{
    public class KeyController : MonoBehaviour
    {
        Controller cont;
        float doubleTimer;
        bool firstClick;
        public float delay;

        private void Start()
        {
            firstClick = false;
            cont = this.gameObject.GetComponent<Controller>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (firstClick != true)
                {
                    firstClick = true;
                    doubleTimer = Time.time;
                }
            }
            if (firstClick == true && Time.time - doubleTimer > delay)
            {
                cont.MoveAndStuff();
            }
        }
    }
}
