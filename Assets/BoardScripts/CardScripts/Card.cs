using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    protected string description;
    public string Description
    {
        get{return description;}
        set{description = value;}
    }

    protected string action;
    public string Action
    {
        get{return action;}
        set{action = value;}
    }
}
