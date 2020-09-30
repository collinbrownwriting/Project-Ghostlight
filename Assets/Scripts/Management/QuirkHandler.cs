using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridMaster {
    public class QuirkHandler : MonoBehaviour
    {
        public int maxQuirks;
        public int quirkBurden;
        public List<QuirkSlot.Quirk> acquiredQuirks = new List<QuirkSlot.Quirk>();
        public List<QuirkSlot.Quirk> possibleQuirks = new List<QuirkSlot.Quirk>();

        public GameObject acquiredQuirkPanel;
        public GameObject possibleQuirkPanel;

        public Text quirkName;
        public Text quirkDesc;
        public Text quirkCost;

        public Text pointsLeft;

        void Update () {
            int pointsLeftInt = maxQuirks - quirkBurden;
            pointsLeft.text = "Points Left: " + pointsLeftInt.ToString();
        }

        public static QuirkHandler instance;
        void Awake () {
            instance = this;
        }
    }
}
