using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoardSection : MonoBehaviour
{
    protected int position;
    public int Position { get; set;}

    protected string id;
    public string Id { get; set;}

    protected bool canBeBought;
    public bool CanBeBought{ get; set;}

    //!todo abstract method to get playerOnThis??
}
