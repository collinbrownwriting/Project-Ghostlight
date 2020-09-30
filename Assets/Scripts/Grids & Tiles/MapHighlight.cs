using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster
{

    public class MapHighlight : MonoBehaviour
    {
        public bool selectedTile;
        public bool isMousing;
        public bool isPathing;


        void Start()
        {
            selectedTile = false;
            isPathing = false;
        }


        void OnMouseOver()
        {
            isMousing = true;
        }

        void OnMouseExit()
        {
            isMousing = false;
        }

        public void Unselect()
        {
            selectedTile = false;
        }
        public void Select()
        {
            selectedTile = true;
        }

    }
}