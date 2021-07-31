using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PropertySection : BoardSection
{
    private GameObject housePrefab;
    private GameObject hotelPrefab;

    private GameObject[] houses;
    private GameObject hotel;

    protected Player ownedBy;
    public Player OwnedBy 
    {
        get {return ownedBy;}
        set {ownedBy = value;}
    }

    protected string group;
    public string Group 
    {
        get {return group;}
        set {group = value;}
    }

    protected int price;
    public int Price 
    {
        get {return price;}
        set {price = value;} //!todo: unless this is bought w a morgage
    }

    private int defaultRent;
    public int DefaultRent {
        get { return defaultRent; }
        set { defaultRent = value; }
    }

    private int rent; // unimproved property
    public int Rent
    {
        get {return rent;}
        set {rent = value;}
    }

    // when improved
    private int oneHouse;
    public int OneHouse
    {
        get {return oneHouse;}
        set {oneHouse = value;}
    }

    private int twoHouse;
    public int TwoHouse
    {
        get {return twoHouse;}
        set {twoHouse = value;}
    }

    private int threeHouse;
    public int ThreeHouse
    {
        get {return threeHouse;}
        set {threeHouse = value;}
    }

    private int fourHouse;
    public int FourHouse
    {
        get {return fourHouse;}
        set {fourHouse = value;}
    }

    // house cost
    private int houseCost;
    public int HouseCost
    {
        get {return houseCost;}
        set {houseCost = value;}
    }

    private int oneHotel;
    public int OneHotel
    {
        get {return oneHotel;}
        set {oneHotel = value;}
    }

    private int upgrades = 0;
    public int Upgrades
    {
        get {return upgrades;}
        set {upgrades = value;}
    }

    private int upgradeCost;
    public int UpgradeCost
    {
        get
        {
            switch (Upgrades)
            {
                case 0:
                    return OneHouse;
                case 1:
                    return TwoHouse;
                case 2:
                    return ThreeHouse;
                case 3:
                    return FourHouse;
                case 4:
                    return OneHotel;
                default:
                    return 0;               //THIS IS WHERE THE PROBLEM IS OF MONEY NOT CHANGING 
            }
        }
    }

    void Start() {
        DefaultRent = Rent;
        housePrefab = transform.GetComponent<UpgradePrefabLinker>().house;
        hotelPrefab = transform.GetComponent<UpgradePrefabLinker>().hotel;
        houses = new GameObject[4];
        for (int i =0; i < houses.Length; i++) {
            if (transform.eulerAngles.y % 180 != 0) {
                houses[i] = Instantiate(housePrefab, new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z - (i * 0.6f)), transform.rotation, transform);
                houses[i].SetActive(false);
            } else {
                houses[i] = Instantiate(housePrefab, new Vector3(transform.position.x - (i * 0.6f), transform.position.y, transform.position.z - 0.5f), transform.rotation, transform);
                houses[i].SetActive(false);
            }
        }
        hotel = Instantiate(hotelPrefab, transform.position, transform.rotation, transform);
        hotel.SetActive(false);
    }

    public void Buy(Player p)
    {
        OwnedBy = p;
    }

    public void Upgrade()
    {
        Upgrades++;

        if (Upgrades == 1)
        {
            Rent = OneHouse;    //(change this)
            houses[0].SetActive(true);
        }
        else if (Upgrades == 2)
        {
            Rent = TwoHouse;    //(change this) 
            houses[0].SetActive(true);
            houses[1].SetActive(true);
        }
        else if (Upgrades == 3)
        {
            Rent = ThreeHouse;    //(change this)
            houses[0].SetActive(true);
            houses[1].SetActive(true);
            houses[2].SetActive(true);
        }
        else if (Upgrades == 4) 
        {
            Rent = FourHouse;    //(change this)
            houses[0].SetActive(true);
            houses[1].SetActive(true);
            houses[2].SetActive(true);
            houses[3].SetActive(true);
        } 
        else     //hotel
        {
            Rent = OneHotel;    //(change this) 
            houses[0].SetActive(false);
            houses[1].SetActive(false);
            houses[2].SetActive(false);
            houses[3].SetActive(false);
            hotel.SetActive(true);
        }
        Debug.Log("Property has been upgraded");
    }

    public void Downgrade()
    {
        Upgrades--;
        
        if (Upgrades == 1)
        {
            Rent = OneHouse;    //(change this) 
            houses[1].SetActive(false);
        }
        else if (Upgrades == 2)
        {
            Rent = TwoHouse;    //(change this)
            houses[2].SetActive(false);
        }
        else if (Upgrades == 3)
        {
            Rent = ThreeHouse;    //(change this)
            houses[3].SetActive(false);
        } 
        else if (Upgrades == 4) {
            Rent = FourHouse;    //(change this)
            houses[0].SetActive(true);
            houses[1].SetActive(true);
            houses[2].SetActive(true);
            houses[3].SetActive(true);
            hotel.SetActive(false);
        } 
        else     //hotel
        {
            Rent = DefaultRent;
            houses[0].SetActive(false);
            Upgrades = 0;
        }
        Debug.Log("Property has been downgraded");
    }

    public void Sell()  //MORGAGE
    {
        //put property cost into owners money
        //OwnedBy.Money += Price;
        //todo 
        // avaliable = true;       //THEY STILL OWN IT BUT THEY DONT GET MONEY FROM IT - AND AT END OF GAME, ITS VALUE DOESNT GET ADDED TO YOUR FINAL SCORE
        ownedBy = null;
        Debug.Log("Property has been sold");
    }

    public bool mortgage = false;
    private int rentHold;
    public void Mortgage()
    {
        mortgage = true;
        rentHold = rent;
        rent = 0;
    }

    public void MortgageSell()
    {
        Sell();
        mortgage = false;
        rent = rentHold;
    }

    public void Unmortgage()
    {
        mortgage = false;
        rent = rentHold;
    }

    public string Show()
    {
        string s = "\n";
        s += (Id);
        s += ("\n");
        s += "Rent: "+ Rent;
        s += ("\n");
        s += "With 1 House: "+ OneHouse;
        s += ("\n");
        s += "With 2 House: "+ TwoHouse;
        s += ("\n");
        s += "With 3 House: "+ ThreeHouse;
        s += ("\n");
        s += "With 4 House: "+ FourHouse;
        s += ("\n");
        s += "With Hotel: "+ OneHotel;
        s += ("\n");
        s += "Mortgage value: "+ Price/2;
        s += ("\n");
        s += "House cost: "+ HouseCost;

        return s;
        //todo add house cost from notes
    }
}
