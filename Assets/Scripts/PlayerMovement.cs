using UnityEngine;

public class PlayerMovement : MonoBehaviour //this will be the script for each of the players in the game to move around the board using checkpoint type transform position locations
{
    public bool forceMove;

    public bool active;
    public bool moving;

    private int boardIndex;
    public int BoardIndex {
        get { return boardIndex; }
    }
    private int tempIndex;
    public GameObject boardLocation;
    private Vector3 waypoint;
    public float roSpeed;      
    public float speed;    
    public Quaternion[] cornerRots;

    private bool started;
    private bool rolled;

    private BoardController bc;
    private DiceController dc;

    void Start()
    {
        bc = GameObject.Find("GameBoard").GetComponent<BoardController>();
        dc = GameObject.Find("DiceContainer").GetComponent<DiceController>();
        active = false;
        moving = false;
        started = false;
        rolled = false;
        cornerRots = new Quaternion[4];
        cornerRots[0] = Quaternion.Euler(0f,0f,0f);
        cornerRots[1] = Quaternion.Euler(0f, 90f, 0f);
        cornerRots[2] = Quaternion.Euler(0f, 180f, 0f);
        cornerRots[3] = Quaternion.Euler(0f, 270f, 0f);
        boardIndex = 0;
        tempIndex = boardIndex;
        boardLocation = bc.getField(boardIndex);
        waypoint = boardLocation.transform.position;
        waypoint.y = this.transform.position.y;
        forceMove = false;
    }

    //PLEASE NOTE - FOR THE CORNER ROTATION TO WORK, APPLY THE CORRECT CORNER TAG ('Corner1','Corner2' etc) TO THE WAYPOINT 1 POSITION AFTER THE CORNER! 

    void Update()
    {
        if (active && !started) {
            Debug.Log(this + " is " + active + " active");
            started = true;
            dc.newRoll();
        } else if (active && started && !rolled) {
            if (dc.getValue() != 0) {
                rolled = true;
                tempIndex = (boardIndex + 1)%40;
                boardIndex = (boardIndex + dc.getValue()) % 40;
                boardLocation = bc.getField(boardIndex);
                waypoint = bc.getField(tempIndex).transform.position;
                waypoint.y = this.transform.position.y;
            }
        } else if (active && rolled) {
            if (transform.position != new Vector3(boardLocation.transform.position.x, transform.position.y, boardLocation.transform.position.z)) {
                transform.position = Vector3.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);

                if (this.transform.position == waypoint) {
                    tempIndex = (tempIndex + 1)%40;
                    if (bc.getField(tempIndex).CompareTag("Start") && !boardLocation.CompareTag("Start")) {
                        transform.GetComponent<Player>().firstLoop = false;
                        transform.GetComponent<Player>().Money += 200;
                    }
                    waypoint = bc.getField(tempIndex).transform.position;
                    waypoint.y = this.transform.position.y;
                }

                //Solve issue with tokens rotating right off, need to update boardLocation every time
                if (bc.getField(tempIndex).tag == "Start" && transform.rotation != cornerRots[0]) {
                    transform.rotation = Quaternion.Lerp(transform.rotation, cornerRots[0], Time.deltaTime * roSpeed);
                } else if (bc.getField(tempIndex).tag == "JailV" && transform.rotation != cornerRots[1]) {
                    transform.rotation = Quaternion.Lerp(transform.rotation, cornerRots[1], Time.deltaTime * roSpeed);
                } else if (bc.getField(tempIndex).tag == "Parking" && transform.rotation != cornerRots[2]) {
                    transform.rotation = Quaternion.Lerp(transform.rotation, cornerRots[2], Time.deltaTime * roSpeed);
                } else if (bc.getField(tempIndex).tag == "GoToJail" && transform.rotation != cornerRots[3]) {
                    transform.rotation = Quaternion.Lerp(transform.rotation, cornerRots[3], Time.deltaTime * roSpeed);
                }
            } else {
                rolled = false;
                started = false;
                active = false;
            }
        }

        if (!active && forceMove) {
            forceMove = false;
            if (boardLocation.CompareTag("JailV")) {
                Vector3 jailtransform = boardLocation.transform.position;
                jailtransform.y = transform.position.y;
                jailtransform.x += 0.7f;
                jailtransform.z -= 0.9f;
                transform.position = jailtransform;
            } else {
                Vector3 target = boardLocation.transform.position;
                target.y = transform.position.y;
                transform.position = target;
            }
        }
    }

    public void updateBoardLocation(int fieldIndex) {
        boardLocation = bc.getField(fieldIndex);
        boardIndex = fieldIndex;
        forceMove = true;
    }
        
}