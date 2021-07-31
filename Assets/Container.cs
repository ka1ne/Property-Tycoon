using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    Rigidbody rb;

    bool hasLanded;
    bool thrown;

    Vector3 initPos;

    public float swipeMod;
    public DiceSide[] diceSides;

    private int diceVal;
    private float swStartTime;
    private Vector3 swStartPos;
    private float orgDistance;
    private float orgHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        orgDistance = (transform.position - Camera.main.transform.position).magnitude;
        orgHeight = transform.position.y;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            swStartTime = Time.time;
            swStartPos = transform.position;
        }

        if (Input.GetMouseButton(0))
        {
            rb.useGravity = false;
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = orgDistance;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.y = orgHeight;
            transform.position = worldPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 dir = (transform.position - swStartPos).normalized;
            Debug.Log(dir);
            dir /= Time.time - swStartTime;
            rb.useGravity = true;
            rb.AddForce(dir * swipeMod);
        }
    }

    void rollDice(Vector3 dir)
    {
        if (!thrown && !hasLanded)
        {
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(dir * swipeMod);
        }
        else if (thrown && hasLanded)
        {
            resetDice();
        }
    }

    void resetDice()
    {
        transform.position = initPos;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;
    }

    void sideCheck()
    {
        diceVal = 0;
        foreach (DiceSide s in diceSides)
        {
            if (s.isOnGround())
            {
                diceVal = s.sideVal;
            }
        }
    }

    public int getVal()
    {
        return diceVal;
    }
}
