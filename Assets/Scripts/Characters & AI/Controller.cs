using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster
{

    public class Controller : MonoBehaviour
    {

        GridHandler gridBase;
        public bool isTurn;
        public bool hasTakenTurn;
        public bool canAct;
        public bool moveSelect;
        public bool moveTake;
        public bool attackSelect;
        public bool align;
        public bool isAttacking;

        public bool canMove;
        public int actionCount;
        public int maxActionCount;

        public Node targNode;
        public float targX;
        public float targY;
        public float thisX;
        public float thisY;

        public int myPosX;
        public int myPosY;
        public bool hasAligned;
        public float oldDist;
        public float newDist;

        public float delay;
        public float timer;

        public Vector2 destination;
        public Vector2 myPosition;
        public bool isMoving;
        public int moveCount;
        public List<Node> pathway;

        public Node currentNode;
        public Node tempDestNode;
        public GameObject[] actors;
        public GameObject[] tiles;
        public GameObject actionBar;
        public GameObject rndObj;
        public RoundController rnd;
        public bool hasFinishedWalking;

        public bool farPlay;
        public bool semiPlay;
        public bool nearPlay;
        public bool herePlay;
        
        public bool willHostile;
        public bool hostile;
        public float alertDist;
        public bool freeRoam;
        public bool isRoaming;
        public bool cutRoam;
        public bool isFlipped;
        public bool bookOpen;

        public List<GameObject> items = new List<GameObject>();
        public List<GameObject> pickableItems = new List<GameObject>();
        public List<Node> attackableNodes = new List<Node>();
        public List<GameObject> attackableEnemies = new List<GameObject>();
        public bool grabbingItems;
        public Inventory inventory;
        public bool isRepairing;
        public bool attackAnim;
        public static Controller instance;
        public bool isCasting;
        public bool isCursing;
        public bool isWeightless;
        public bool hasCalcedLaunch;
        public bool isLaunching;
        public Node landNode;
        public float launchSpeed;
        bool hasAn;
        public GameObject enemTarg;
        public bool fleeingMove;
        public int stunCounter;
        bool againstWall;

        void Awake () {
            if (this.gameObject.GetComponent<CharacterData>().isPlayer == true){
                instance = this;
            }
        }

        void Start()
        {
            canAct = true;
            inventory = this.gameObject.GetComponent<Inventory>();
            actors = GameObject.FindGameObjectsWithTag("Actor");
            rndObj = GameObject.FindGameObjectWithTag("Round Controller");
            rnd = rndObj.GetComponent<RoundController>();
            gridBase = GridHandler.GetInstance();
            hasAligned = false;
            moveCount = -1;
            actionCount = 0;
            maxActionCount = this.GetComponent<CharacterData>().maxActionCount;
            timer = 0f;
            againstWall = false;

            if (this.gameObject.GetComponent<CharacterData>().isPlayer == true){
                freeRoam = true;
            } else {
                freeRoam = false;
            }
        }
        void Update()
        {
            if (this.gameObject.GetComponent<CharacterData>().isUnconscious == true) {
                willHostile = false;
                hostile = false;
                canAct = false;
            }

            if (hostile == false && willHostile == true && FactionHandler.instance.hasSetUp == true && GridHandler.instance.hasGenerated == true){
                if (this.gameObject.GetComponent<CharacterData>().canSee == true) {
                    actors = GameObject.FindGameObjectsWithTag("Actor");
                    foreach (GameObject g in actors) {
                        foreach (PhysicalProperties.Relationship r in this.gameObject.GetComponent<PhysicalProperties>().animate.mind.relationships) {
                            if (r.relTarget == g && r.relationshipNumber < -100) {
                                if (Vector3.Distance(g.transform.position, this.gameObject.transform.position) < this.gameObject.GetComponent<CharacterData>().hostileDist){
                                    hostile = true;
                                    rnd.Startuptime();
                                }
                            } else if (r.relTarget == g && r.relationshipNumber < -25 && this.gameObject.GetComponent<CharacterData>().factionArray.relationships[g.gameObject.GetComponent<CharacterData>().factionID].atWar == true) {
                                if (Vector3.Distance(g.transform.position, this.gameObject.transform.position) < this.gameObject.GetComponent<CharacterData>().hostileDist){
                                    hostile = true;
                                    rnd.Startuptime();
                                }
                            }
                        }
                    }
                }
            } else if (willHostile == false && hostile == true) {
                hostile = false;
            } else if (hostile == true && willHostile == true && rnd.inCombat == false) {
                rnd.Startuptime();
            }



            myPosition = this.transform.position;

            if (isLaunching == true) {
                if (hasAn == false) {
                    hasAn = true;
                }
                Vector3 weightlessMod = new Vector3 (0, 0, 0);

                if (isWeightless == false) {
                    transform.position = Vector3.Lerp(myPosition, landNode.worldObject.transform.position + this.gameObject.GetComponent<CharacterData>().alignMod, 0.1f * launchSpeed);
                } else {
                    weightlessMod = new Vector3 (myPosition.x, this.gameObject.GetComponent<PhysicalProperties>().floatPosition.y, 0f);
                    transform.position = Vector3.Lerp(myPosition, landNode.worldObject.transform.position + this.gameObject.GetComponent<CharacterData>().alignMod + weightlessMod, 0.1f * launchSpeed);
                }

                if (Vector2.Distance(landNode.worldObject.transform.position + this.gameObject.GetComponent<CharacterData>().alignMod + weightlessMod, this.transform.position) < 0.05f) {
                    this.transform.parent = landNode.worldObject.transform;
                    if (this.gameObject.GetComponent<PhysicalProperties>().xMomentum == 0 && this.gameObject.GetComponent<PhysicalProperties>().yMomentum == 0) {
                        canAct = true;
                        stunCounter++;
                        isLaunching = false;
                        landNode = null;
                        launchSpeed = 0f;
                        hasAn = false;
                    }
                }
            }
            // if (isWeightless == true) {
            //     transform.position = Vector3.Lerp(this.transform.position, this.gameObject.GetComponent<PhysicalProperties>().floatPosition, 0.05f);
            // }

            if (Input.GetButtonDown("Pick Up") && grabbingItems == false && isMoving == false && bookOpen == false && this.gameObject.GetComponent<CharacterData>().isDead == false && isCasting == false){
                grabbingItems = true;
                items = new List<GameObject>();
                foreach(GameObject g in GameObject.FindGameObjectsWithTag("Item")){
                    if (g.GetComponent<ItemData>().isInInven == false){
                        items.Add(g);
                    }
                }

                int t = 0;
                if (items != null){
                    pickableItems = new List<GameObject>();
                    foreach (GameObject i in items){
                        if (Vector2.Distance(i.transform.position, this.transform.position) <= 0.25f && i.GetComponent<ItemData>().isInInven == false){
                            pickableItems.Add(i);
                            t++;
                        }
                    }
                }
            } else if (Input.GetButtonDown("Pick Up") && grabbingItems == true){
                grabbingItems = false;
                items = null;
                pickableItems = null;
            }

            if (isMoving == true){
                grabbingItems = false;
                items = null;
                pickableItems = null;
            }

            if (this.gameObject.GetComponent<CharacterData>().isDead == true){
                isTurn = false;
                rnd.NextRound();
            }

            if (hasTakenTurn == true && isTurn == true)
            {
                if (this.gameObject.GetComponent<CharacterData>().bleedCounter > 0) {
                    this.gameObject.GetComponent<PhysicalProperties>().TakeBioDamage(5f, (PhysicalProperties.DamageType)4);
                    this.gameObject.GetComponent<CharacterData>().bleedCounter--;
                }

                isTurn = false;
                Invoke("AllowTurn", 1f);
            }

            tiles = GameObject.FindGameObjectsWithTag("Tile");

            if (this.transform.parent != null && isMoving == false && isLaunching == false && isWeightless == false)
            {
                this.transform.position = this.transform.parent.position  + this.gameObject.GetComponent<CharacterData>().alignMod;
            }

            if (isMoving == true)
            {
                if (myPosition.x > destination.x)
                {
                    isFlipped = true;
                    this.GetComponent<SpriteRenderer>().flipX = true;
                }
                if (myPosition.x < destination.x)
                {
                    isFlipped = false;
                    this.GetComponent<SpriteRenderer>().flipX = false;
                }

                if (Manager.instance.walkType == 1) {
                    transform.position = Vector3.MoveTowards(transform.position, destination, this.gameObject.GetComponent<CharacterData>().speed * Time.deltaTime);
                } else if (Manager.instance.walkType == 2) {
                    transform.position = Vector3.Lerp(myPosition, destination, this.GetComponent<CharacterData>().speed * 10 * Time.deltaTime);
                }
                if (Vector2.Distance(myPosition, destination) < 0.01f)
                {
                    if (isRoaming == true){
                        FreeMovement(pathway);
                    }
                    if (isRoaming == false && isRepairing == false){
                        Movement(pathway);
                    }
                }
            }

            if (stunCounter > 0 && isTurn == true && hasTakenTurn == false) {
                stunCounter--;
                actionCount = 0;
                hasTakenTurn = true;
                AnnouncerManager.instance.ReceiveText(this.gameObject.GetComponent<CharacterData>().charName + " is stunned.", false);
            }

            if (isTurn == true && hasTakenTurn == false && canAct == false) {
                actionCount = 0;
                hasTakenTurn = true;
            }

            if (isTurn == true && hasTakenTurn == false && isLaunching == false && canAct == true)
            {
                if (this.GetComponent<CharacterData>().isPlayer == false)
                {
                    if (actionCount < maxActionCount && isMoving == false && isAttacking == false)
                    {
                        actors = null;
                        actors = GameObject.FindGameObjectsWithTag("Actor");
                        enemTarg = NearestEnemyCalc().worldObject.transform.GetChild(0).gameObject;

                        if (hostile == true) {

                        Pathfinding.Pathfinder path = new Pathfinding.Pathfinder();

                        Node startNode = gridBase.GetNode(Mathf.RoundToInt(this.GetComponentInParent<CoordinateHolder>().corX), Mathf.RoundToInt(this.GetComponentInParent<CoordinateHolder>().corY));
                        Node end = targNode;

                        path.startPosition = startNode;
                        path.endPosition = end;
                        path.pather = this.gameObject;

                        pathway = path.FindPath();

                        if (pathway != null && pathway.Count > 2)
                        {
                            farPlay = true;
                        } else if (pathway != null && pathway.Count == 2){
                            semiPlay = true;
                        }
                        else if (pathway != null && pathway.Count == 1)
                        {
                            nearPlay = true;
                        }
                        else if (pathway.Count == 0)
                        {
                            herePlay = true;
                        } else if (farPlay != true && semiPlay != true && nearPlay != true && herePlay != true) {
                            Invoke ("cannotActRepair",1.5f);
                        }
                    } else if (actionCount >= maxActionCount) {
                        Invoke ("cannotActRepair",1.5f);
                    }
                    
                    }
                    }

                if (this.GetComponent<CharacterData>().isPlayer == true && CharacterData.instance.isDead == false)
                {
                    if (actionCount < maxActionCount && isMoving == false && EquipmentRenderer.instance.testInven == false && bookOpen == false  && isCasting == false && isAttacking == false)
                    {
                        if (moveTake == true && this.gameObject.GetComponent<CharacterData>().moveDistance > 0 && canMove == true)
                        {
                            moveSelect = false;
                            moveTake = false;

                            FindSelectedTile();

                            Pathfinding.Pathfinder path = new Pathfinding.Pathfinder();

                            Node startNode = gridBase.GetNode(Mathf.RoundToInt(this.GetComponentInParent<CoordinateHolder>().corX), Mathf.RoundToInt(this.GetComponentInParent<CoordinateHolder>().corY));
                            Node end = targNode;

                            path.startPosition = startNode;
                            path.endPosition = end;
                            path.pather = this.gameObject;

                            pathway = path.FindPath();

                            bool canPath = true;

                            if (pathway == null) {
                                canPath = false;
                            } else if (pathway != null) {
                                if (pathway[0] == startNode) {
                                    canPath = false;
                                }
                            }


                            if (pathway.Count > 0 && canPath == true)
                            {
                                actionCount++;
                                actionBar.GetComponent<ActionBar>().CutAction();
                                Movement(pathway);
                            } else if (canPath != true) {
                                AnnouncerManager.instance.ReceiveText("You cannnot move there.", true);
                            }
                        }
                        
                        if (isAttacking == false && Input.GetButtonDown("ShiftA") && EquipmentRenderer.instance.weaponEquip.GetComponent<Animator>().enabled == true && this.gameObject.GetComponent<CharacterData>().canAttack == true) {
                            isAttacking = true;
                            Node launchNode = gridBase.GetNode(Mathf.RoundToInt(this.GetComponentInParent<CoordinateHolder>().corX), Mathf.RoundToInt(this.GetComponentInParent<CoordinateHolder>().corY));
                            attackableNodes = new List<Node>();
                            attackableEnemies = new List<GameObject>();
                            for (int x = -1 * Mathf.RoundToInt(CharacterData.instance.range); x <= Mathf.RoundToInt(CharacterData.instance.range); x++) {
                                for (int y = -1 * Mathf.RoundToInt(CharacterData.instance.range); y <= Mathf.RoundToInt(CharacterData.instance.range); y++) {
                                    if (x == 0 && y == 0) {
                                        //Nothing
                                    } else {
                                        Node searchPos = gridBase.GetNode(launchNode.x + x, launchNode.y + y);
                                        if (searchPos != null && searchPos.worldObject.transform.childCount > 0) {
                                            attackableNodes.Add(searchPos);
                                            if (searchPos.worldObject.transform.GetChild(0).transform.gameObject.tag == "Actor") {
                                                attackableEnemies.Add(searchPos.worldObject.transform.GetChild(0).transform.gameObject);
                                            }
                                        }
                                    }
                                }
                            }

                            foreach (Node n in attackableNodes) {
                                n.worldObject.GetComponent<GridHighlight>().enabled = false;
                                n.worldObject.GetComponent<SpriteRenderer>().enabled = true;
                                n.worldObject.GetComponent<SpriteRenderer>().color = Color.red;
                            }
                            foreach (GameObject g in attackableEnemies) {
                                g.GetComponent<NPCHandler>().recepientAttack = true;
                            }
                        } else if (isAttacking == true && Input.GetButtonDown("ShiftA")) {
                            isAttacking = false;
                            foreach (Node n in attackableNodes) {
                                n.worldObject.GetComponent<GridHighlight>().enabled = true;
                                n.worldObject.GetComponent<SpriteRenderer>().enabled = false;
                                n.worldObject.GetComponent<SpriteRenderer>().color = Color.white;
                            }
                            foreach (GameObject g in attackableEnemies) {
                                g.GetComponent<Renderer>().material = g.GetComponent<CharacterData>().stand;
                            }
                            attackableEnemies = null;
                            attackableNodes = null;
                        }
                    }

                } else if (CharacterData.instance.isDead == true){
                    //Do nothing for now.
                }
            }

            if (this.GetComponent<CharacterData>().isPlayer == true && freeRoam == true && isMoving == false && isAttacking == false && ItemManager.instance.isRummaging == false && bookOpen == false && isCasting == false)
            {
                canMove = true;
                if (moveTake == true)
                {
                    moveSelect = false;
                    moveTake = false;

                    FindSelectedTile();

                    Pathfinding.Pathfinder path = new Pathfinding.Pathfinder();

                    Node startNode = gridBase.GetNode(Mathf.RoundToInt(this.GetComponentInParent<CoordinateHolder>().corX), Mathf.RoundToInt(this.GetComponentInParent<CoordinateHolder>().corY));
                    Node end = targNode;

                    path.startPosition = startNode;
                    path.endPosition = end;
                    path.pather = this.gameObject;

                    pathway = path.FindPath();

                    if (pathway.Count > 0)
                    {
                        isRoaming = true;
                        FreeMovement(pathway);
                    }
                }
            }

            if (align)
            {
                align = false;
            }

            if (Input.GetButtonDown("Pass Round") && this.GetComponent<CharacterData>().isPlayer == true && isMoving == false && isAttacking == false && ItemManager.instance.isRummaging == false && bookOpen == false && isCasting == false) {
                actionCount++;
                actionBar.GetComponent<ActionBar>().CutAction();

                if (RoundController.instance.inCombat == false && actionCount >= maxActionCount) {
                    RoundController.instance.ArtificialRound();
                }
                if (RoundController.instance.inCombat == true && actionCount >= maxActionCount) {
                    actionCount = 0;
                    hasTakenTurn = true;
                }

            }

        }

        public void cannotActRepair () {
            actionCount = 0;
            hasTakenTurn = true;
        }

        public void PlayerAttack (GameObject g) {
            actionCount++;
            attackAnim = true;

            if (g.transform.position.x < this.transform.position.x) {
                isFlipped = true;
            } else {
                isFlipped = false;
            }

            actionBar.GetComponent<ActionBar>().CutAction();
            g.GetComponent<CharacterData>().HoldDamage(CharacterData.instance.attack, CharacterData.instance.activeDamageType);
            if (attackableNodes.Count > 0) {
                foreach (Node n in attackableNodes) {
                    n.worldObject.GetComponent<GridHighlight>().enabled = true;
                    n.worldObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
                foreach (GameObject c in attackableEnemies) {
                    g.GetComponent<NPCHandler>().recepientAttack = false;
                }
            }

            Invoke("ResolveAttack", 0.5f);
        }
        public void ResolveAttack () {
            attackAnim = false;
            isAttacking = false;
            attackableNodes = null;

            if (actionCount >= maxActionCount)
            {
                actionCount = 0;
                hasTakenTurn = true;
            }
        }

        public void Alignment()
        {
            //This will align the charcters with the tiles that match their coordinates,
            //not their local or world coordinates but the ones in the controller.
            if (hasAligned == false)
            {
                Node newnode = gridBase.GetNode(myPosX, myPosY);
                if (newnode != null)
                {
                    this.transform.parent = newnode.worldObject.transform;
                    hasAligned = true;
                }
                else
                {
                    Debug.Log("This nulled.");
                }
            }
        }

        public void MoveAndStuff ()
        {
            if (moveSelect != true && isMoving != true)
            {
                moveSelect = true;
                if (moveTake != true)
                {
                    moveTake = true;
                }
            }
        }

        public void AllowTurn () {
            rnd.NextRound();
        }

        public Node NearestEnemyCalc()
        {
            if (this.gameObject.GetComponent<CharacterData>().canSee == true) {
            //This is pretty self explanatory.
            float distanceToClosestEnemy = Mathf.Infinity;
            GameObject closestEnemy = null;
            actors = GameObject.FindGameObjectsWithTag("Actor");

            foreach (GameObject g in actors)
            {
                foreach (PhysicalProperties.Relationship r in this.gameObject.GetComponent<PhysicalProperties>().animate.mind.relationships) {
                    if (r.relTarget == g && r.relationshipNumber < -500) {
                        Transform parent = g.transform.parent;
                        float enemparentX = parent.gameObject.GetComponent<CoordinateHolder>().corX;
                        float enemparentY = parent.gameObject.GetComponent<CoordinateHolder>().corY;

                        Transform thisparent = this.transform.parent;
                        float thisparentX = thisparent.gameObject.GetComponent<CoordinateHolder>().corX;
                        float thisparentY = thisparent.gameObject.GetComponent<CoordinateHolder>().corY;

                        float distX = enemparentX - thisparentX;
                        float distY = enemparentY - thisparentY;

                        float distanceToEnemy = Mathf.Sqrt(distX * distX + distY * distY);

                        if (distanceToEnemy < distanceToClosestEnemy)
                        {
                            distanceToClosestEnemy = distanceToEnemy;
                            closestEnemy = g;
                        }
                    }
                    if (r.relTarget == g && r.relationshipNumber < -25 && this.gameObject.GetComponent<CharacterData>().factionArray.relationships[g.gameObject.GetComponent<CharacterData>().factionID].atWar == true) {
                        Transform parent = g.transform.parent;
                        float enemparentX = parent.gameObject.GetComponent<CoordinateHolder>().corX;
                        float enemparentY = parent.gameObject.GetComponent<CoordinateHolder>().corY;

                        Transform thisparent = this.transform.parent;
                        float thisparentX = thisparent.gameObject.GetComponent<CoordinateHolder>().corX;
                        float thisparentY = thisparent.gameObject.GetComponent<CoordinateHolder>().corY;

                        float distX = enemparentX - thisparentX;
                        float distY = enemparentY - thisparentY;

                        float distanceToEnemy = Mathf.Sqrt(distX * distX + distY * distY);

                        if (distanceToEnemy < distanceToClosestEnemy) {
                            distanceToClosestEnemy = distanceToEnemy;
                            closestEnemy = g;
                        }
                    }
                }
            }

            if (closestEnemy != null)
            {

                int targNodeX = Mathf.RoundToInt(closestEnemy.GetComponentInParent<CoordinateHolder>().corX);
                int targNodeY = Mathf.RoundToInt(closestEnemy.GetComponentInParent<CoordinateHolder>().corY);

                targNode = gridBase.GetNode(targNodeX, targNodeY);

                return targNode;
            } else if (hostile == true) {
                hostile = false;
                cannotActRepair();
                return null;
            } else {
                return null;
            }
            } else if (hostile == true) {
                cannotActRepair();
                return null;
            } else {
                return null;
            }
        }

        public Node FindSelectedTile()
        {
            if (tiles != null)
            {

                if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SelectData>().selectedTile != null)
                {
                    int x = Mathf.RoundToInt(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SelectData>().selectedTile.GetComponent<CoordinateHolder>().corX);
                    int y = Mathf.RoundToInt(GameObject.FindGameObjectWithTag("MainCamera").GetComponent<SelectData>().selectedTile.GetComponent<CoordinateHolder>().corY);

                    targNode = gridBase.GetNode(x, y);
                }
                else
                {
                    return null;
                }
            }
            if (targNode != null)
            {
                return targNode;
            }
            else
            {
                Debug.Log("this is returning null");
                return null;
            }
        }

        public void Movement(List<Node> p)
        {
            if (moveCount < p.Count && moveCount < this.GetComponent<CharacterData>().moveDistance && hasFinishedWalking == false)
            {
                moveCount = moveCount + 1;
            }
            if (moveCount > p.Count && hasFinishedWalking == false)
            {
                moveCount = p.Count;
            }
            if (p.Count > 1 && moveCount + 1 >= p.Count - 1 && this.GetComponent<CharacterData>().isPlayer == false && hasFinishedWalking == false && fleeingMove == false)
            {
                hasFinishedWalking = true;
                isMoving = true;
                currentNode = p[p.Count - 2];
                tempDestNode = currentNode;
                destination = currentNode.worldObject.transform.position + this.gameObject.GetComponent<CharacterData>().alignMod;
                Invoke("ParentNode", 0.5f);
            }
            else if (moveCount + 1 >= p.Count && this.GetComponent<CharacterData>().isPlayer == true && hasFinishedWalking == false || moveCount + 1 >= p.Count && this.GetComponent<CharacterData>().isPlayer == false && hasFinishedWalking == false && fleeingMove == true)
            {
                hasFinishedWalking = true;
                isMoving = true;
                currentNode = p[p.Count - 1];
                tempDestNode = currentNode;
                destination = currentNode.worldObject.transform.position + this.gameObject.GetComponent<CharacterData>().alignMod;
                Invoke("ParentNode", 0.5f);
            }
            else if (this.GetComponent<CharacterData>().moveDistance <= moveCount + 1 && hasFinishedWalking == false)
            {
                hasFinishedWalking = true;
                isMoving = true;
                currentNode = p[moveCount];
                tempDestNode = currentNode;
                destination = currentNode.worldObject.transform.position + this.gameObject.GetComponent<CharacterData>().alignMod;
                Invoke("ParentNode", 0.5f);
            }
            else if (hasFinishedWalking == false)
            {
                currentNode = p[moveCount];
                isMoving = true;
                destination = currentNode.worldObject.transform.position + this.gameObject.GetComponent<CharacterData>().alignMod;
            }
        }

        public void FreeMovement(List<Node> p)
        {
            if (rnd.inCombat == true){
                cutRoam = true;
                isMoving = true;
                currentNode = p[moveCount];
                tempDestNode = currentNode;
                destination = currentNode.worldObject.transform.position + this.gameObject.GetComponent<CharacterData>().alignMod;
                Invoke("ParentNode", 0.5f);
            }
            if (moveCount < p.Count && cutRoam == false)
            {
                moveCount = moveCount + 1;
                rnd.ArtificialTickUp();
            }
            if (moveCount > p.Count && cutRoam == false)
            {
                moveCount = p.Count;
            }
            if (moveCount >= p.Count - 1 && cutRoam == false)
            {
                isMoving = true;
                currentNode = p[p.Count - 1];
                tempDestNode = currentNode;
                destination = currentNode.worldObject.transform.position + this.gameObject.GetComponent<CharacterData>().alignMod;
                Invoke("ParentNode", 0.5f);
            }
            else
            {
                currentNode = p[moveCount];
                isMoving = true;
                destination = currentNode.worldObject.transform.position + this.gameObject.GetComponent<CharacterData>().alignMod;
            }
        }

        public void RepairFunction () {
            isRepairing = true;
            isMoving = true;
            currentNode = pathway[0];
            tempDestNode = currentNode;
            destination = currentNode.worldObject.transform.position;

            Invoke("ParentNode", 0.5f);
        }

        public void ParentNode()
        {
            this.transform.parent = tempDestNode.worldObject.transform;
            isRepairing = false;
            isMoving = false;
            moveCount = -1;
            pathway = null;
            isRoaming = false;
            hasFinishedWalking = false;
            fleeingMove = false;

            if (actionCount >= maxActionCount)
            {
                actionCount = 0;
                hasTakenTurn = true;
            }
            if (this.GetComponent<CharacterData>().isPlayer == true)
            {
                actionBar.GetComponent<ActionBar>().CutAction();
            }
        }

        //I'm going to include various movements here, mostly those that are caused by spells, so that I can easily call them here rather than rewriting them each time.

        public void LaunchBack (float mag, GameObject caster) {
            //mag = Magnitude & caster = The actor with which we calculate the launch angle, which is usually just the one who cast the spell.
            
            Vector2 direct = this.transform.position - caster.transform.position;

            RaycastHit2D[] hit = Physics2D.RaycastAll(this.transform.position, direct, mag);
            Debug.DrawRay(this.transform.position, direct, Color.red, 100, false);
 

            bool hitWall = false;

            foreach (RaycastHit2D h in hit) {
                if (h.collider.gameObject.tag != "Tile") {
                    hitWall = true;
                    againstWall = true;
                } else if (h.collider.gameObject.GetComponent<CoordinateHolder>().isWall == true) {
                    hitWall = true;
                    againstWall = true;
                }
            }

            if (hitWall == false) {
                Node sendNode = null;
                float tempDist = 0f;
                foreach (RaycastHit2D h in hit) {
                    if (h.collider.gameObject.tag == "Tile") {
                
                        Node n = GridHandler.instance.GetNode(Mathf.RoundToInt(h.collider.gameObject.GetComponent<CoordinateHolder>().corX), Mathf.RoundToInt(h.collider.gameObject.GetComponent<CoordinateHolder>().corY));
                        if (Vector2.Distance(this.gameObject.transform.position, n.worldObject.transform.position) > tempDist) {
                            tempDist = Vector2.Distance(this.gameObject.transform.position, n.worldObject.transform.position);
                            sendNode = n;
                        }
                    }
                }

                if (sendNode != null) {
                    launchSpeed = mag;
                    landNode = sendNode;
                    isLaunching = true;
                    if (againstWall == true) {
                        againstWall = false;
                        stunCounter += 1;
                    }
                } else if (mag > 0.01f) {
                    LaunchBack(mag - 0.01f, caster);
                }
            } else if (mag > 0.01f) {
                LaunchBack(mag -0.01f, caster);
            } else {
                //Fall down.
            }
        }
    }
}

