using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridMaster {
    public class BasicEnemyAI : MonoBehaviour
    {
        public Controller controller;
        public PhysicalProperties.DamageType damageType;
        public Animator anim;
        public List<Node> pathway;
        bool fledCheck;

        public void Start(){
            controller = this.gameObject.GetComponent<Controller>();
            anim = this.gameObject.GetComponent<Animator>();
        }

        public void Update(){
            if (this.gameObject.GetComponent<Controller>().isTurn == true && this.gameObject.GetComponent<Controller>().canAct == true && this.gameObject.GetComponent<CharacterData>().canAttack == true){
                if (controller.farPlay == true && this.gameObject.GetComponent<Controller>().canMove == true && this.gameObject.GetComponent<CharacterData>().moveDistance > 0){
                    controller.Movement(controller.pathway);
                    controller.actionCount++;
                    controller.farPlay = false;
                } else if (controller.semiPlay == true && this.gameObject.GetComponent<Controller>().canMove == true && this.gameObject.GetComponent<CharacterData>().moveDistance > 0){ 
                    controller.RepairFunction();
                    controller.actionCount++;
                    controller.semiPlay = false;
                } else if (controller.nearPlay == true){
                    BasicAttack();
                    controller.nearPlay = false;
                } else if (controller.herePlay == true){
                    controller.cannotActRepair();
                    controller.herePlay = false;
                } else if (controller.farPlay == true || controller.semiPlay == true) {
                    controller.cannotActRepair();
                }
            } else if (this.gameObject.GetComponent<Controller>().isTurn == true && this.gameObject.GetComponent<CharacterData>().canAttack == false && fledCheck == false) {
                controller.farPlay = false;
                controller.semiPlay = false;
                controller.nearPlay = false;
                controller.herePlay = false;
                fledCheck = true;
                Flee(5, this.gameObject.GetComponent<Controller>().enemTarg);
            } else if (this.gameObject.GetComponent<Controller>().isTurn == true) {
                fledCheck = false;
                controller.cannotActRepair();
            }
        }

        

        public void BasicAttack(){
            controller.isAttacking = true;

            if (Random.Range(1, 20) + 5 > (10 + (controller.enemTarg.GetComponent<CharacterData>().evasion * 0.5f))){
                controller.targNode.worldObject.transform.GetChild(0).GetComponent<CharacterData>().HoldDamage(this.gameObject.GetComponent<CharacterData>().martial / 2, damageType);
                //controller.targNode.worldObject.transform.GetChild(0).GetComponent<UIEffectsController>().DamageAlert();
            } else {
                AnnouncerManager.instance.ReceiveText(this.gameObject.GetComponent<CharacterData>().charName + " misses.", false);
            }

            if (controller.myPosition.x > controller.targNode.worldObject.transform.position.x)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            if (controller.myPosition.x < controller.targNode.worldObject.transform.position.x)
            {
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
            Invoke("FinishAttack", 1f);
        }

        public void FinishAttack(){
            controller.farPlay = false;
            controller.semiPlay = false;
            controller.nearPlay = false;
            controller.herePlay = false;
            controller.isAttacking = false;
            controller.actionCount++;

            if (controller.actionCount >= controller.maxActionCount){
                controller.actionCount = 0;
                controller.hasTakenTurn = true;
            }
        }

        public void Flee (float mag, GameObject caster) {
            //I'm stealing large swathes of this code from the launching code I wrote earlier, hence some of the variables.
            Vector2 direct = this.transform.position - caster.transform.position;

            RaycastHit2D[] hit = Physics2D.RaycastAll(this.transform.position, direct, mag);
            Debug.DrawRay(this.transform.position, direct, Color.red, 100, false);
 

            bool hitWall = false;

            foreach (RaycastHit2D h in hit) {
                if (h.collider.gameObject.tag != "Tile") {
                    hitWall = true;
                } else if (h.collider.gameObject.GetComponent<CoordinateHolder>().isWall == true) {
                    hitWall = true;
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
                    Pathfinding.Pathfinder path = new Pathfinding.Pathfinder();

                    Node startNode = GridHandler.instance.GetNode(Mathf.RoundToInt(this.GetComponentInParent<CoordinateHolder>().corX), Mathf.RoundToInt(this.GetComponentInParent<CoordinateHolder>().corY));
                    Node end = sendNode;

                    path.startPosition = startNode;
                    path.endPosition = end;
                    path.pather = this.gameObject;

                    controller.pathway = path.FindPath();
                    controller.fleeingMove = true;

                    if (controller.pathway != null) {
                        controller.Movement(controller.pathway);
                    }

                    controller.actionCount++;
                    fledCheck = false;

                } else if (mag > 0.01f) {
                    Flee(mag - 0.01f, caster);
                } else {
                    fledCheck = false;
                    controller.cannotActRepair();
                }
            } else if (mag > 0.01f) {
                Flee(mag -0.01f, caster);
            } else {
                fledCheck = false;
                controller.cannotActRepair();
            }
        }
    }
}