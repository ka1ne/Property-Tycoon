using System;
using UnityEngine;

public class Property : MonoBehaviour //this will be the script for each of the players in the game
{
    public int buyCost;
    public int oweCost;
    public int sellCost;    
    public int upgradeCost;
    public bool avaliable = true;
    public string owner;
    public int houseCount;
    public string colour;

    public void Start()
    {
        oweCost = 0;
        upgradeCost = 0;
        sellCost = 0;
        colour = "brown";
    }

    public void Buy(string name)
    {
        avaliable = false;
        owner = name;
    }

    public void Upgrade()
    {
        houseCount++;
        if (houseCount == 1)
        {
            oweCost = 5;    //(change this) 
        }
        else if (houseCount == 2)
        {
            oweCost = 10;    //(change this) 
        }
        else if (houseCount == 3)
        {
            oweCost = 15;    //(change this) 
        }
        else     //hotel
        {
            oweCost = 20;    //(change this) 
        }
        Debug.Log("Property has been upgraded");
    }

    public void Downgrade()
    {
        houseCount--;
        Debug.Log("Property has been downgraded");
        //oweCost needs to decrease by x amount
    }

    public void Sell()  //MORGAGE
    {
        //put property cost into owners money
        owner = "";
        avaliable = true;      
        Debug.Log("Property has been sold");
    }
}


