using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;

    public bool active;
    bool hasLanded;
    bool thrown;

    Vector3 initPos;

    [Range(0.5f,1f)]
    public float rotSpeed = 0.8f;

    public int rollPower = 500;
    public DiceSide[] diceSides;

    private int diceVal;
    private int rPMin; //rollPowerMin
    private float swStartTime;
    private Vector3 swStartPos;

    void Start()
    {
        active = false;
        diceVal = 0;
        rb = GetComponent<Rigidbody>();
        initPos = transform.position;
        rPMin = Mathf.FloorToInt(rollPower * 0.8f);
        transform.Rotate(Random.Range(0, 150), Random.Range(0, 150), Random.Range(0, 150));
        rb.useGravity = false;
    }

    void Update()
    {
        if (active) {
            if (Input.touchCount > 0) {
                if (Input.GetTouch(0).phase == TouchPhase.Began) {
                    rollDice();
                }
            } else if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                rollDice();
            }

            if (!thrown && !hasLanded) {
                transform.Rotate(rotSpeed, 0, rotSpeed);
            }

            if (rb.IsSleeping() && !hasLanded && thrown) {
                hasLanded = true;
                rb.useGravity = false;
                rb.isKinematic = true;
                sideCheck();
            } else if (rb.IsSleeping() && hasLanded && diceVal == 0) {
                foreach (GameObject o in GameObject.FindGameObjectsWithTag("Dice")) {
                    o.GetComponent<Dice>().rollAgain();
                }
            }

            if (rb.IsSleeping() && hasLanded && diceVal != 0) {
                active = false;
            }
        }
    }

    void rollDice()
    {
        if(!thrown && !hasLanded)
        {
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(rPMin,rollPower), Random.Range(rPMin, rollPower), Random.Range(rPMin, rollPower));
        }
    }

    public void resetDice()
    {
        transform.position = initPos;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;
    }

    void rollAgain()
    {
        resetDice();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(rPMin, rollPower), Random.Range(rPMin, rollPower), Random.Range(rPMin, rollPower));
    }

    void sideCheck()
    {
        diceVal = 0;
        int tmpDV = 0;
        int touchCount = 0;
        foreach (DiceSide s in diceSides){
            if (s.isOnGround())
            {
                tmpDV = s.sideVal;
                touchCount++;
            }
        }
        if (touchCount == 1) {
            diceVal = tmpDV;
        } else {
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("Dice")) {
                o.GetComponent<Dice>().rollAgain();
            }
        }
        Debug.Log("Rolled: " + diceVal);
    }

    public int getVal()
    {
        return diceVal;
    }

    public void resetVal()
    {
        diceVal = 0;
    }
}
