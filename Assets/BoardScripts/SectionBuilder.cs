using System;
using UnityEngine;

// abstract builder class
abstract class SectionBuilder
{
    protected BoardSection section;
    // gets BoardSection instance
    public BoardSection Section => section;

    private bool canBeBought;
    public bool CanBeBought{
        get {return canBeBought;}
        set {canBeBought = value;}
    }

    // custom method to TryParse to int from input string
    protected int ParseInt(string s)
    {
        int i = 0;
            Int32.TryParse(s, out i);
        return i;
    }

    // abstract methods
    public abstract void SetFields(string[] data, GameObject Field);
}
