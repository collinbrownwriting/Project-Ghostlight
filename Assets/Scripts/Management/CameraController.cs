using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class CameraController : MonoBehaviour
    {
        public Vector3 cameraOffset;
        public float speed;
        public float scrollSpeed;
        public bool following;
        public GameObject player;
        public float trackTime;

        public Spell dragSpell;
        public bool isDragging;
        public GameObject tileOver;

        void Update () {
            if (AnnouncerManager.instance.fadingOut == true) {
                cameraOffset = new Vector3 (0, 0, 0);
            } else {
                cameraOffset = new Vector3 (0.35f, 0.1f, 0);
            }
            
            speed = this.gameObject.GetComponent<Camera>().orthographicSize * 7f;
            scrollSpeed = this.gameObject.GetComponent<Camera>().orthographicSize * 20f;

            if(Input.GetMouseButton(2) && following == false) {
                float newX = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
                float newY = Input.GetAxis("Mouse Y") * speed * Time.deltaTime;
                transform.position -= new Vector3 (newX, newY, 0);
            }

            if(Input.GetAxis("Mouse ScrollWheel") != 0){
                this.gameObject.GetComponent<Camera>().orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;
            }
            if (following == false && Input.GetButtonDown("Camera Track")) {
                following = true;
            } else if (following == true && Input.GetButtonDown("Camera Track")) {
                following = false;
            }

            if (following == true) {
                //float trackX = Mathf.SmoothDamp(this.transform.position.x, playerShip.transform.position.x, ref placeVel, trackTime);
                //float trackY = Mathf.SmoothDamp(this.transform.position.y, playerShip.transform.position.y, ref placeVel, trackTime);
                Vector3 target = new Vector3(player.transform.position.x, player.transform.position.y, -10f);

                this.gameObject.transform.position = Vector3.Lerp(this.transform.position, target + cameraOffset, trackTime * Time.deltaTime);
            }

            if (isDragging == true && Input.GetButtonDown("Fire1")) {
                isDragging = false;
                Invoke("ClearDragSpell", 0.5f);
            }
        }

        void ClearDragSpell () {
            dragSpell = null;
        }

        public static CameraController instance;
        public static CameraController GetInstance()
        {
            return instance;
        }
        void Awake () {
            instance = this;
        }
    }
}
