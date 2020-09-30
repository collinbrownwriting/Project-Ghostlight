using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    
    //[ExecuteAlways]
    public class ChunkData : MonoBehaviour
    {
        //General
        public ChunkType type;
        public string regionName;
        public bool isProcGen;
        public int offset;
        public int naturalWealth;
        public PixelType magicResource;
        public int claimants;
        public int desiredBy;
        public int defenseRating;
        public bool launchNode;
        public int securityDamage;
        public int magicalDamage;

        //Character-Related Stuff
        public HistoricalFigure histOwner;
        public HistoricalFigure magicOwner;

        public float corX;
        public float corY;
        public Node thisNode;
        float z;

        //Faction Related
        public FactionHandler.FactionArray faction;
        public FactionHandler.FactionArray school;

        public int viewType;

        //public Sprite oceanSprite;
        public Sprite forestSprite;
        public Sprite desertSprite;
        public Sprite plainSprite;
        public Sprite mountainSprite;
        public Sprite ashSprite;
        public Sprite parchmentSprite;

        public Sprite emptyFaction;
        public Sprite blueFaction;
        public Sprite darkBlueFaction;
        public Sprite cyanFaction;
        public Sprite darkCyanFaction;
        public Sprite greenFaction;
        public Sprite darkGreenFaction;
        public Sprite orangeFaction;
        public Sprite darkOrangeFaction;
        public Sprite purpleFaction;
        public Sprite darkPurpleFaction;
        public Sprite redFaction;
        public Sprite darkRedFaction;
        public Sprite yellowFaction;
        public Sprite darkYellowFaction;
        public Sprite paleBlueFaction;
        public Sprite paleRedFaction;
        public Sprite paleGreenFaction;
        public Sprite palePurpleFaction;


        public Sprite linedBlueFaction;
        public Sprite linedCyanFaction;
        public Sprite linedGreenFaction;
        public Sprite linedOrangeFaction;
        public Sprite linedPinkFaction;
        public Sprite linedPurpleFaction;
        public Sprite linedRedFaction;
        public Sprite linedYellowFaction;
        public Sprite paleCyanFaction;
        public Sprite paleOrangeFaction;

        public void SendNode () {
            faction = FactionHandler.instance.factionArray[0];
            school = FactionHandler.instance.factionArray[0];

            float magicThreshold = Random.Range(1, 100);
            if (magicThreshold > 75f) {
                int pCount = Mathf.RoundToInt(Random.Range(1, 5));
                magicResource = (PixelType)pCount;
            } else {
                magicResource = (PixelType)0;
            }

            naturalWealth = Random.Range(1, 100);


            Node node = new Node();

            corX = Mathf.RoundToInt((this.gameObject.transform.position.x) / 14.5f);
            if (corX % 2 == 1)
            {
                z = corX;
                corY = Mathf.RoundToInt((this.gameObject.transform.position.y + 8.5f) / 17f + ((z / 2) - 0.5f));
            }
            else
            {
                z = corX;
                corY = Mathf.RoundToInt((this.gameObject.transform.position.y) / 17f + (z / 2));
            }

            this.gameObject.name = "Tile" + corX + "/" + corY;
            
            node.x = Mathf.RoundToInt(corX);
            node.y = Mathf.RoundToInt(corY);
            node.worldObject = this.gameObject;
            thisNode = node;
            MapGeneration.instance.grid[node.x, node.y] = node;
        }

        public void Update () {
            viewType = Manager.instance.worldMapViewType;

            Vector3 location = this.transform.position;
            int locrend = -1 * (Mathf.RoundToInt(location.y / 0.085f) * 4) + offset;

            this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = locrend;

            if (viewType == 1) {
                if ((int)type == 0) {
                    offset = 3000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = null;
                }
                if ((int)type == 1) {
                    offset = 2000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = desertSprite;
                }
                if ((int)type == 2) {
                    offset = 2000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = forestSprite;
                }
                if ((int)type == 3) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = plainSprite;
                }
                if ((int)type == 4) {
                    offset = 4000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = mountainSprite;
                }
                if ((int)type == 5) {
                    offset = 5000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = ashSprite;
                }
                if ((int)type == 6) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = parchmentSprite;
                }
            }

            if (viewType == 2) {
                if (faction.factionID == 0) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = emptyFaction;
                }
                if (faction.factionID == 1) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = blueFaction;
                }
                if (faction.factionID == 2) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = darkBlueFaction;
                }
                if (faction.factionID == 3) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = cyanFaction;
                }
                if (faction.factionID == 4) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = darkCyanFaction;
                }
                if (faction.factionID == 5) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = greenFaction;
                }
                if (faction.factionID == 6) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = darkGreenFaction;
                }
                if (faction.factionID == 7) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = orangeFaction;
                }
                if (faction.factionID == 8) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = darkYellowFaction;
                }
                if (faction.factionID == 9) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = purpleFaction;
                }
                if (faction.factionID == 10) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = darkPurpleFaction;
                }
                if (faction.factionID == 11) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = redFaction;
                }
                if (faction.factionID == 12) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = darkRedFaction;
                }
                if (faction.factionID == 13) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = yellowFaction;
                }
                if (faction.factionID == 14) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = darkOrangeFaction;
                }
                if (faction.factionID == 15) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = paleBlueFaction;
                }
                if (faction.factionID == 16) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = paleRedFaction;
                }
                if (faction.factionID == 17) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = paleGreenFaction;
                }
                if (faction.factionID == 18) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = palePurpleFaction;
                }
                if (faction.factionID == 19) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedBlueFaction;
                }
                if (faction.factionID == 20) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedCyanFaction;
                }
                if (faction.factionID == 21) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedGreenFaction;
                }
                if (faction.factionID == 22) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedOrangeFaction;
                }
                if (faction.factionID == 23) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedPinkFaction;
                }
                if (faction.factionID == 23) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedPurpleFaction;
                }
                if (faction.factionID == 24) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedRedFaction;
                }
                if (faction.factionID == 25) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedYellowFaction;
                }
                if (faction.factionID == 26) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = paleCyanFaction;
                }
                if (faction.factionID == 27) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = paleOrangeFaction;
                }
            }

            if (viewType == 3) {
                if (school.factionID == 0) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = emptyFaction;
                }
                if (school.factionID == 1) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = blueFaction;
                }
                if (school.factionID == 2) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = darkBlueFaction;
                }
                if (school.factionID == 3) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = cyanFaction;
                }
                if (school.factionID == 4) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = darkCyanFaction;
                }
                if (school.factionID == 5) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = greenFaction;
                }
                if (school.factionID == 6) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = darkGreenFaction;
                }
                if (school.factionID == 7) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = orangeFaction;
                }
                if (school.factionID == 8) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = darkYellowFaction;
                }
                if (school.factionID == 9) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = purpleFaction;
                }
                if (school.factionID == 10) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = darkPurpleFaction;
                }
                if (school.factionID == 11) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = redFaction;
                }
                if (school.factionID == 12) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = darkRedFaction;
                }
                if (school.factionID == 13) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = yellowFaction;
                }
                if (school.factionID == 14) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = darkOrangeFaction;
                }
                if (school.factionID == 15) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = paleBlueFaction;
                }
                if (school.factionID == 16) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = paleRedFaction;
                }
                if (school.factionID == 17) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = paleGreenFaction;
                }
                if (school.factionID == 18) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = palePurpleFaction;
                }
                if (school.factionID == 19) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedBlueFaction;
                }
                if (school.factionID == 20) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedCyanFaction;
                }
                if (school.factionID == 21) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedGreenFaction;
                }
                if (school.factionID == 22) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedOrangeFaction;
                }
                if (school.factionID == 23) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedPinkFaction;
                }
                if (school.factionID == 23) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedPurpleFaction;
                }
                if (school.factionID == 24) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedRedFaction;
                }
                if (school.factionID == 25) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = linedYellowFaction;
                }
                if (school.factionID == 26) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = paleCyanFaction;
                }
                if (school.factionID == 27) {
                    offset = 1000;
                    this.gameObject.GetComponent<SpriteRenderer>().sprite = paleOrangeFaction;
                }
            }
        }
    }
    public enum ChunkType { Ocean, Desert, Forest, Plain, Mountain, Ash, Parchment }
}
