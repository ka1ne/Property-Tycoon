using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    bool onGround;
    public int sideVal;

    void OnTriggerStay(Collider col)
    {
        onGround = col.tag == "Ground";
    }

    void OnTriggerExit(Collider col)
    {
        onGround = !(col.tag == "Ground");
    }

    public bool isOnGround()
    {
        return onGround;
    }
}
