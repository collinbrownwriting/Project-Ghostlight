using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class FigureHandler : MonoBehaviour
    {
        public List<HistoricalFigure> historicalFigures = new List<HistoricalFigure>();
        public List<Culture> cultures = new List<Culture>();
        

        public HistoricalFigure GenerateHistoricalFigure (FactionHandler.FactionArray factionArray, int agePref, int genderPref, int personPref, int familyPref) {
            HistoricalFigure newFigure = new HistoricalFigure();

            newFigure.factionID = factionArray.factionID;

            if (genderPref == 0) {
                newFigure.gender = Mathf.RoundToInt(Random.Range(1, 3));
            } else {
                newFigure.gender = genderPref;
            }

            if (newFigure.gender == 1) {
                newFigure.name = cultures[factionArray.culture].possibleFemaleNames[Mathf.RoundToInt(Random.Range(0, cultures[factionArray.culture].possibleFemaleNames.Count - 1))];
            } else if (newFigure.gender == 2) {
                newFigure.name = cultures[factionArray.culture].possibleMaleNames[Mathf.RoundToInt(Random.Range(0, cultures[factionArray.culture].possibleMaleNames.Count - 1))];
            } else if (newFigure.gender == 3) {
                newFigure.name = cultures[factionArray.culture].allNames[Mathf.RoundToInt(Random.Range(0, cultures[factionArray.culture].allNames.Count - 1))];
            }

            if (agePref == 0) {
                newFigure.age = Mathf.RoundToInt(Random.Range(16, 30));
            } else {
                newFigure.age = agePref;
            }

            if (personPref == 0) {
                newFigure.mind = new PhysicalProperties.Mind();
                newFigure.mind.personality = (PhysicalProperties.PersonalityType)Mathf.RoundToInt(Random.Range(0, System.Enum.GetNames(typeof(PhysicalProperties.PersonalityType)).Length - 1));
            } else {
                newFigure.mind = new PhysicalProperties.Mind();
                newFigure.mind.personality = (PhysicalProperties.PersonalityType)personPref - 1;
            }

            newFigure.importance = 0;

            if (familyPref == -1) {
                newFigure.familyID = Mathf.RoundToInt(Random.Range(1, cultures[factionArray.culture].surnames.Count - 1));
            } else {
                newFigure.familyID = familyPref;
            }
            newFigure.name = newFigure.name + " " + cultures[factionArray.culture].surnames[newFigure.familyID];
            historicalFigures.Add(newFigure);
            factionArray.relatedFigures.Add(newFigure);
            factionArray.livingFigures.Add(newFigure);

            return newFigure;
        }

        public HistoricalFigure GenerateChild (FactionHandler.FactionArray factionArray, int familyPref) {
            HistoricalFigure littleBaby = new HistoricalFigure();

            littleBaby.gender = Mathf.RoundToInt(Random.Range(1, 3));
            if (littleBaby.gender == 1) {
                littleBaby.name = cultures[factionArray.culture].possibleFemaleNames[Mathf.RoundToInt(Random.Range(0, cultures[factionArray.culture].possibleFemaleNames.Count - 1))];
            } else if (littleBaby.gender == 2) {
                littleBaby.name = cultures[factionArray.culture].possibleMaleNames[Mathf.RoundToInt(Random.Range(0, cultures[factionArray.culture].possibleMaleNames.Count - 1))];
            } else if (littleBaby.gender == 3) {
                littleBaby.name = cultures[factionArray.culture].allNames[Mathf.RoundToInt(Random.Range(0, cultures[factionArray.culture].allNames.Count - 1))];
            }

            littleBaby.age = 1;
            littleBaby.mind = new PhysicalProperties.Mind();
            littleBaby.mind.personality = (PhysicalProperties.PersonalityType)Mathf.RoundToInt(Random.Range(0, System.Enum.GetNames(typeof(PhysicalProperties.PersonalityType)).Length - 1));
            littleBaby.factionID = factionArray.factionID;
            littleBaby.familyID = familyPref;
            littleBaby.name = littleBaby.name + " " + cultures[factionArray.culture].surnames[littleBaby.familyID];
            return littleBaby;
        }

        public static FigureHandler instance;
        void Awake () {
            instance = this;
        }
    }

    [System.Serializable]
    public class HistoricalFigure {
        public string name;
        public int age;
        public int gender;
        public int importance;
        public int domainSize;
        public int prowess;
        public int kills;
        public List<string> figKills = new List<string>();
        public int children;
        public List<string> figChildren = new List<string>();
        public bool isDead;
        public int factionID;
        public int familyID;
        public PhysicalProperties.Mind mind;
    }

    [System.Serializable]
    public class Culture {
        public string name;
        public List<string> possibleFemaleNames = new List<string>();
        public List<string> possibleMaleNames = new List<string>();
        public List<string> allNames = new List<string>();

        public List<string> surnames = new List<string>();

        void Start () {
            foreach (string s in possibleFemaleNames) {
                allNames.Add(s);
            }
            foreach (string s in possibleMaleNames) {
                allNames.Add(s);
            }
        }
    }
}
