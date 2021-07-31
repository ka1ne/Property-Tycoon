using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour //this will be the script for each of the players in the game
{
    //Highlight material effect
    private MeshRenderer mshrend;
    [SerializeField]
    private float matduration;
    [SerializeField]
    private Material hlMatLo;
    [SerializeField]
    private Material hlMatHi;
    [SerializeField]
    private Material defaultMat;

    public string playername;
    public string PlayerName {
        get { return playername; }
        set { playername = value; }
    }
    public string token;
    public bool NPC;
    public bool loaded;

    public bool active;
    private bool moving;
    public bool turnstart;      //HERE
    private bool fdouble;
    private bool sdouble;
    private bool tdouble;
    private bool imprisoned;
    private int jailTCount;

    private int jailFieldIndex;

    private DiceController dc;

    private PlayerMovement pm;

    [SerializeField] GameManager gm;
    //Property p;


    private int money;
    public int Money {
        get { return money; }
        set { money = value; }
    }
    private List<PropertySection> OwnedProperty;

    private List<Card> PlayerCards;
    public bool bankrupt;
    public bool Bankrupt {
        get { return bankrupt; }
        set { bankrupt = value; }
    }
    public List<GameObject> properties;
    private List<GameObject> cards;



    public GameObject UImessages; //parent for all the UI message stuff
    public GameObject textBox;  //the text message box
    public GameObject purchasable;  //parent for the purchasable things (2 button options and tile image)
    private GameObject payingForJail;
    private bool tradeAllowed;
    private GameObject tradeMMenu;
    private GameObject tradeOfferMenu;
    private GameObject tradeConfirm;
    private GameObject tradeMyProperty;
    private GameObject tradeTheirProperty;
    private PropertySection propertyOffered;
    private PropertySection propertyExpected;
    private TMP_InputField myMoneyField;
    private TMP_InputField theirMoneyField;
    private int moneyOffered;
    private int moneyExpected;
    private int tradeMyCurProperty;
    private int tradeTheirCurProperty;
    private int tradePointer;
    public GameObject tileLandedOn, tileLandedOnAuction; //tile image used in purchasable
    public TextMeshProUGUI text;    //the text that appears in the box
    public Button buy, auction;     //buttons for purchasable
    public GameObject[] bottomPanels = new GameObject[6];   //colour panels at the bottom of the screen that indicates whos in control
    public static int panelPos = 0;    //used for the bottomPanels to know which to be active
    public Sprite[] tiles = new Sprite[3];   //THIS IS JUST A TEMPORTY SPRITE ARRAY WHICH WE'LL CREATE LATER ONCE WE HAVE ALL THE PICTURES OF THE TILES ON THE BOARD
    public GameObject[] gameTiles = new GameObject[39]; //all the tile game objects
    public GameObject clickToRoll, beforeEndTurn, viewProperties, auctionOptions;  //these are the bottompanel texts
    public GameObject property;//, spriteProperties;   //image of propertyView and Properties object  
    public List<Sprite> ourProperties = new List<Sprite>(); //list of all your properties that you own 
    public GameObject opportunity, pot; //the images that will have the value of the card they pulled
    private TextMeshProUGUI oppoText, potText;
    public Sprite[] opportunityKnocksValues = new Sprite[15];   //array of all the community chest values the game will randomly pick from. change this value to equal the amound of cards we actually have in the game
    public Sprite[] potLuckValues = new Sprite[15];   //array of all the chance values the game will randomly pick from. change this value to equal the amound of cards we actually have in the game
    //add the stuff for trading
    public int curBidWinner, continuePlayerPointer, curViewProperty;
    public static int curBid, bids;
    public GameObject Warning;
    //public GameObject WarningBox;    //message that warns the player that they havent confirmed enough players
    public float timeLeft = 5.0f;   //countdown for the warning message that appears on the screen
    public TextMeshProUGUI WarningText;
    public TextMeshProUGUI m;   //money link to the scene text
    public List<TextMeshProUGUI> cardsMoney = new List<TextMeshProUGUI>();
    public int playerIndex; //this equals whatever index was given to it in gm
    private bool warningTime;
    private bool withdraw;
    public bool pBankrupt;
    public GameObject winCanvas;
    public TextMeshProUGUI winText;
    public bool firstLoop;
    private bool nroll1;
    private bool nroll2;
    private GameObject freeParkingField;

    private void Awake()
    {
        UImessages = GameObject.Find("messages");      
        textBox = GameObject.Find("messageBox");
        purchasable = GameObject.Find("purchasable");
        payingForJail = GameObject.Find("jailPay");
        tradeMMenu = GameObject.Find("TradePlayer");
        tradeOfferMenu = GameObject.Find("TradeOffer");
        tradeConfirm = GameObject.Find("TradeConfirm");
        myMoneyField = GameObject.Find("MyMoney").GetComponent<TMP_InputField>();
        theirMoneyField = GameObject.Find("TheirMoney").GetComponent<TMP_InputField>();
        tradeMyProperty = GameObject.Find("MyProperty");
        tradeTheirProperty = GameObject.Find("TheirProperty");
        tileLandedOn = GameObject.Find("tileLandedOn");
        tileLandedOnAuction = GameObject.Find("tileLandedOnAuction");
        text = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
        buy = GameObject.Find("buy").GetComponent<Button>();
        auction = GameObject.Find("auction").GetComponent<Button>();
        bottomPanels[0] = GameObject.Find("green");
        bottomPanels[1] = GameObject.Find("purple");
        bottomPanels[2] = GameObject.Find("blue");
        bottomPanels[3] = GameObject.Find("red");
        bottomPanels[4] = GameObject.Find("orange");
        bottomPanels[5] = GameObject.Find("yellow");
        clickToRoll = GameObject.Find("roll");
        beforeEndTurn = GameObject.Find("beforeEndTurn");
        viewProperties = GameObject.Find("viewProperties");
        auctionOptions = GameObject.Find("auctionMenu");
        property = GameObject.Find("Image");
        opportunity = GameObject.Find("opportunity");
        pot = GameObject.Find("pot");
        Warning = GameObject.Find("Warning");
        WarningText = GameObject.Find("WarningText").GetComponent<TextMeshProUGUI>();
        winCanvas = GameObject.Find("Win");
        winText = GameObject.Find("WinText").GetComponent<TextMeshProUGUI>();

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        mshrend = transform.GetComponent<MeshRenderer>();

        warningTime = true;
        UImessages.SetActive(false);    //1
        purchasable.SetActive(false);   //2
        payingForJail.SetActive(false);
        tileLandedOnAuction.SetActive(false);   //3
        bottomPanels[1].SetActive(false);   //4
        bottomPanels[2].SetActive(false);   //5
        bottomPanels[3].SetActive(false);   //6
        bottomPanels[4].SetActive(false);   //7
        bottomPanels[5].SetActive(false);   //8
        beforeEndTurn.gameObject.SetActive(false); //9
        viewProperties.gameObject.SetActive(false);    //10
        auctionOptions.gameObject.SetActive(false);    //11
        opportunity.SetActive(false);   //13
        pot.SetActive(false);   //14
        Warning.SetActive(false);
        tradeMMenu.SetActive(false);
        tradeOfferMenu.SetActive(false);
        tradeConfirm.SetActive(false);
        Warning.SetActive(false);
        winCanvas.SetActive(false);
        oppoText = opportunity.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>();
        potText = pot.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>();

        freeParkingField = GameObject.FindWithTag("Parking");


        if (playerIndex == 0)
        {
            m = GameObject.Find("money1").GetComponent<TextMeshProUGUI>();
        }
        else if (playerIndex == 1)
        {
            m = GameObject.Find("money2").GetComponent<TextMeshProUGUI>();
        }
        else if (playerIndex == 2)
        {
            m = GameObject.Find("money3").GetComponent<TextMeshProUGUI>();
        }
        else if (playerIndex == 3)
        {
            m = GameObject.Find("money4").GetComponent<TextMeshProUGUI>();
        }
        else if (playerIndex == 4)
        {
            m = GameObject.Find("money5").GetComponent<TextMeshProUGUI>();
        }
        else if (playerIndex == 5)
        {
            m = GameObject.Find("money6").GetComponent<TextMeshProUGUI>();
        }


        dc = GameObject.Find("DiceContainer").GetComponent<DiceController>();
        pm = this.GetComponent<PlayerMovement>();
        jailFieldIndex = 10;
        active = false; //if the player exists
        turnstart = true;   //their turn
        moving = false; //if theyre currently moving around the board and havent rolled a double
        fdouble = true; // Rolled doubles 1 time flag
        sdouble = false; // Rolled doubles 2 times flag
        tdouble = false; // Rolled doubles 3 times flag = player should be sent to jail
        imprisoned = false; // Flag to check if player is in jail
        jailTCount = 0; // Number of turns spent in jail
        money = 1500;   //how much money the player had
        OwnedProperty = new List<PropertySection>();    //all the properties that this player owns
        cards = new List<GameObject>(); 
        loaded = true;

        tradeAllowed = true;
        withdraw = false;
        pBankrupt = false;
        firstLoop = true;  //HERE
        nroll1 = false;
        nroll2 = false;
    }

    void Update() {
        m.text = money.ToString();
        if (active) {
            float matLerp = Mathf.PingPong(Time.time, matduration) / matduration;
            mshrend.material.Lerp(hlMatLo, hlMatHi, matLerp);

            if (!imprisoned) {
                if (turnstart && !moving) {
                    turnstart = false;
                    moving = true;
                    tradeAllowed = true;
                    pm.active = true;
                }

                if (moving && dc.doubles == 1 && fdouble && !pm.active) {
                    fdouble = false;
                    sdouble = true;
                    nroll1 = true;
                }

                if (moving && dc.doubles == 2 && sdouble && !pm.active) {
                    sdouble = false;
                    tdouble = true;
                    nroll2 = true;
                }

                if (moving && dc.doubles == 3 && tdouble && !pm.active) {
                    tdouble = false;
                    //Send player to jail - display some message;
                    pm.updateBoardLocation(jailFieldIndex);
                    imprisoned = true;
                }


                if (dc.doubles != 3 && !pm.active && moving) {    //what happens when you land on stuff
                    moving = false;
                    Debug.Log("hello");
                    UImessages.SetActive(false);
                    textBox.transform.localScale = new Vector2(1, 1);   //width then height


                    if (pm.boardLocation.CompareTag("Property"))    //Landed on property
                    {
                        if (pm.boardLocation.GetComponent<PropertySection>().OwnedBy != null) {
                            if (pm.boardLocation.GetComponent<PropertySection>().OwnedBy == this) {
                                if (nroll1 || nroll2) {
                                    nroll1 = false;
                                    nroll2 = false;
                                    pm.active = true;
                                    moving = true;
                                } else {
                                    BeforeTurnEnd();
                                }
                            } else {
                                UImessages.SetActive(false);
                                Warning.SetActive(true);
                                WarningText.text = "This property is owned by " + pm.boardLocation.GetComponent<PropertySection>().OwnedBy.playername + ". You've paid £" + pm.boardLocation.GetComponent<PropertySection>().Rent + "!";
                                Debug.Log("You had - " + money);
                                money -= pm.boardLocation.GetComponent<PropertySection>().Rent;
                                m.text = money.ToString();
                                if (nroll1 || nroll2) {
                                    nroll1 = false;
                                    nroll2 = false;
                                    pm.active = true;
                                    moving = true;
                                } else {
                                    BeforeTurnEnd();
                                }
                            }
                        } else {
                            if (!firstLoop) {
                                UImessages.SetActive(true);
                                textBox.transform.localScale = new Vector2(1, 1);   //width then height
                                text.text = "This property is purchasable for £" + pm.boardLocation.GetComponent<PropertySection>().Price;
                                purchasable.SetActive(true);
                                tileLandedOn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pm.boardLocation.GetComponent<PropertySection>().Show();

                                switch (pm.boardLocation.GetComponent<PropertySection>().Group) {
                                    case "Brown":
                                        // change colour to match that of the PropertySection group
                                        tileLandedOn.GetComponent<Image>().color = new Color32(153, 51, 0, 100);
                                        break;
                                    case "Blue":
                                        // change colour to match that of the PropertySection group
                                        tileLandedOn.GetComponent<Image>().color = new Color32(0, 153, 255, 100);
                                        break;
                                    case "Purple":
                                        // change colour to match that of the PropertySection group
                                        tileLandedOn.GetComponent<Image>().color = new Color32(153, 51, 255, 100);
                                        break;
                                    case "Orange":
                                        // change colour to match that of the PropertySection group
                                        tileLandedOn.GetComponent<Image>().color = new Color32(255, 102, 0, 100);
                                        break;
                                    case "Red":
                                        // change colour to match that of the PropertySection group
                                        tileLandedOn.GetComponent<Image>().color = new Color32(255, 51, 0, 100);
                                        break;
                                    case "Yellow":
                                        // change colour to match that of the PropertySection group
                                        tileLandedOn.GetComponent<Image>().color = new Color32(255, 255, 0, 100);
                                        break;
                                    case "Green":
                                        // change colour to match that of the PropertySection group
                                        tileLandedOn.GetComponent<Image>().color = new Color32(0, 204, 0, 100);
                                        break;
                                    case "Deep blue":
                                        // change colour to match that of the PropertySection group
                                        tileLandedOn.GetComponent<Image>().color = new Color32(0, 0, 204, 100);
                                        break;
                                    default:
                                        // change colour to match that of the PropertySection group
                                        tileLandedOn.GetComponent<Image>().color = new Color32(0, 0, 204, 100);
                                        break;
                                }
                            } else {
                                if (nroll1 || nroll2) {
                                    nroll1 = false;
                                    nroll2 = false;
                                    pm.active = true;
                                    moving = true;
                                } else {
                                    BeforeTurnEnd();
                                }
                            }
                        }
                    } else if (pm.boardLocation.CompareTag("Start"))    //if the player lands on 'go' and will be rewarded 200
                      {
                        money += 200;  
                        firstLoop = false;
                        if (nroll1 || nroll2) {
                            nroll1 = false;
                            nroll2 = false;
                            pm.active = true;
                            moving = true;
                        } else {
                            BeforeTurnEnd();
                        }
                    } 
                    else if (pm.boardLocation.CompareTag("GoToJail"))  //if the player lands on 'jail' and will be put in jail
                    {
                        pm.updateBoardLocation(jailFieldIndex);
                        BeforeTurnEnd();
                    } else if (pm.boardLocation.CompareTag("PotLuck")) {
                        UImessages.SetActive(false);
                        pot.SetActive(true);

                        var card = GameObject.Find("GameBoard").GetComponent<BoardData>().GetPotLuck();
                        potText.text = card.Description;
                        StartCoroutine(cardMsgWaiter());

                        switch (card.Description) {
                            case "You inherit £100":
                                this.Money += 100;
                                m.text = this.Money.ToString();
                                // bank.Money -= 100;
                                break;

                            case "You have won 2nd prize in a beauty contest, collect £20":
                                this.Money += 20;
                                m.text = this.Money.ToString();
                                // bank.Money -= 20;
                                break;

                            case "Go back to Crapper Street":
                                pm.updateBoardLocation(1);
                                break;

                            case "Student loan refund. Collect £20":
                                this.Money += 20;
                                m.text = this.Money.ToString();
                                // bank.Money -= 20;
                                break;

                            case "Bank error in your favour. Collect £200":
                                this.Money += 200;
                                m.text = this.Money.ToString();
                                // bank.Money -= 200;
                                break;

                            case "Pay bill for text books of £100":
                                this.Money -= 100;
                                m.text = this.Money.ToString();
                                // bank.Money += 100;
                                break;

                            case "Mega late night taxi bill pay £50":
                                this.Money -= 50;
                                m.text = this.Money.ToString();
                                // bank.Money += 50;
                                break;

                            case "Advance to go":
                                pm.updateBoardLocation(0);
                                break;

                            case "From sale of Bitcoin you get £50":
                                this.Money += 50;
                                m.text = this.Money.ToString();
                                // bank.Money -= 50;
                                break;

                            case "Pay a £10 fine or take opportunity knocks":
                                // if fine paid take 10 from player and add it to free parking
                                this.Money -= 10;
                                m.text = this.Money.ToString();
                                break;

                            case "Pay insurance fee of £50":
                                this.Money -= 50;
                                m.text = this.Money.ToString();
                                freeParkingField.GetComponent<FreeParking>().Amount += 50;
                                break;

                            case "Savings bond matures, collect £100":
                                this.Money += 100;
                                m.text = this.Money.ToString();
                                // bank.Money -= 100;
                                break;

                            case "Go to jail. Do not pass GO, do not collect £200":
                                pm.updateBoardLocation(10);
                                this.Money -= 200;
                                m.text = this.Money.ToString();
                                //todo now in jail
                                break;

                            case "Received interest on shares of £25":
                                this.Money += 25;
                                m.text = this.Money.ToString();
                                // bank.Money -= 25;
                                break;

                            case "It's your birthday. Collect £10 from each player":
                                this.Money += 10;
                                m.text = this.Money.ToString();
                                //todo foreach other player -= their money by 10
                                break;

                            case "Get out of jail free":
                                GetOutOfJailFree goojfCard = new GetOutOfJailFree();
                                PlayerCards.Add(goojfCard);
                                break;
                        }

                        if (nroll1 || nroll2) {
                            nroll1 = false;
                            nroll2 = false;
                            pm.active = true;
                            moving = true;
                        } else {
                            BeforeTurnEnd();
                        }
                    } else if (pm.boardLocation.CompareTag("Opportunity")) {
                        UImessages.SetActive(false);
                        opportunity.SetActive(true);

                        var card = GameObject.Find("GameBoard").GetComponent<BoardData>().GetOppKnocks();
                        oppoText.text = card.Description;
                        StartCoroutine(cardMsgWaiter());

                        switch (card.Description) {
                            case "Bank pays you a divided of £50":
                                this.Money += 50;
                                m.text = this.Money.ToString();
                                // bank.Money -= 50;
                                break;

                            case "You have won a lip sync battle. Collect £100":
                                this.Money += 100;
                                m.text = this.Money.ToString();
                                // bank.Money -= 100;
                                break;

                            case "Advance to Turing Heights":
                                pm.updateBoardLocation(39);
                                break;

                            case "Advance to Han Xin Gardens. If you pass GO, collect £200":
                                pm.updateBoardLocation(24);
                                if (pm.BoardIndex > 25) {
                                    money += 200;
                                    m.text = this.Money.ToString();
                                }
                                break;

                            case "Fined £15 for speeding":
                                this.Money -= 15;
                                m.text = this.Money.ToString();
                                freeParkingField.GetComponent<FreeParking>().Amount += 15;
                                break;

                            case "Pay university fees of £150":
                                this.Money -= 100;
                                m.text = this.Money.ToString();
                                // bank.Money += 100;
                                break;

                            case "Take a trip to Hove station. If you pass GO collect £200":
                                pm.updateBoardLocation(15);
                                if (pm.BoardIndex > 16) {
                                    money += 200;
                                    m.text = this.Money.ToString();
                                }
                                break;

                            case "Loan matures, collect £150":
                                this.Money += 150;
                                m.text = this.Money.ToString();
                                // bank.Money -= 150;
                                break;

                            case "You are assessed for repairs, £40/house, £115/hotel":
                                foreach(PropertySection p in OwnedProperty) {
                                    if (p.Upgrades < 5) {
                                        money -= 40 * p.Upgrades;
                                        m.text = this.Money.ToString();
                                    } else {
                                        money -= 115;
                                        m.text = this.Money.ToString();
                                    }
                                }
                                break;

                            case "Advance to GO":
                                pm.updateBoardLocation(0);
                                money += 200;
                                m.text = this.Money.ToString();
                                break;

                            case "You are assessed for repairs, £25/house, £100/hotel":
                                foreach (PropertySection p in OwnedProperty) {
                                    if (p.Upgrades < 5) {
                                        money -= 25 * p.Upgrades;
                                        m.text = this.Money.ToString();
                                    } else {
                                        money -= 100;
                                        m.text = this.Money.ToString();
                                    }
                                }
                                break;

                            case "Go back 3 spaces":
                                pm.updateBoardLocation(pm.BoardIndex - 3);
                                break;

                            case "Advance to Skywalker Drive. If you pass GO collect £200":
                                pm.updateBoardLocation(11);
                                if (pm.BoardIndex > 11) {
                                    money += 200;
                                    m.text = this.Money.ToString();
                                }
                                break;

                            case "Go to jail. Do not pass GO, do not collect £200":
                                pm.updateBoardLocation(jailFieldIndex);
                                imprisoned = true;
                                break;

                            case "Drunk in charge of a skateboard. Fine £20":
                                this.Money -= 20;
                                m.text = this.Money.ToString();
                                freeParkingField.GetComponent<FreeParking>().Amount += 20;
                                break;

                            case "Get out of jail free":
                                GetOutOfJailFree goojfCard = new GetOutOfJailFree();
                                PlayerCards.Add(goojfCard);
                                break;
                        }

                        if (nroll1 || nroll2) {
                            nroll1 = false;
                            nroll2 = false;
                            pm.active = true;
                            moving = true;
                        } else {
                            BeforeTurnEnd();
                        }
                    } else if (pm.boardLocation.CompareTag("Parking")) {
                        //money += pm.boardLocation.GetComponent<FreeParking>().Amount;  //this 'buyCost' is just the variable that will hold the money that is collected from taxes for Free parking 
                        pm.boardLocation.GetComponent<FreeParking>().Amount = 0;
                        if (nroll1 || nroll2) {
                            nroll1 = false;
                            nroll2 = false;
                            pm.active = true;
                            moving = true;
                        } else {
                            BeforeTurnEnd();
                        }
                    } else if (pm.boardLocation.CompareTag("Tax")) {
                        UImessages.SetActive(false);
                        Warning.SetActive(true);  //width then height
                        if (pm.BoardIndex == 4) {
                            WarningText.text = "Income Tax! You have paid £" + 200;
                            money -= 200;
                            m.text = this.Money.ToString();
                        } else if (pm.BoardIndex == 38) {
                            WarningText.text = "Super Tax! You have paid £" + 100;
                            money -= 100;
                            m.text = this.Money.ToString();
                        }
                        //money -= pm.boardLocation.GetComponent<ActionSection>();
                        if (nroll1 || nroll2) {
                            nroll1 = false;
                            nroll2 = false;
                            pm.active = true;
                            moving = true;
                        } else {
                            BeforeTurnEnd();
                        }
                    } else {
                        if (nroll1 || nroll2) {
                            nroll1 = false;
                            nroll2 = false;
                            pm.active = true;
                            moving = true;
                        } else {
                            BeforeTurnEnd();
                        }
                    }
                }
            } else {
                int jfcount = 0;
                foreach (Card c in PlayerCards) {
                    if (c is GetOutOfJailFree) {
                        jfcount++;
                    }
                }
                if (turnstart && jfcount > 0) {
                    imprisoned = false;
                    int jcCount = 0;
                    int i = 0;
                    while (i < PlayerCards.Count && jcCount < 1) {
                        if (PlayerCards[i] is GetOutOfJailFree) {
                            PlayerCards.RemoveAt(i);
                            jcCount++;
                        }
                        i++;
                    }
                }if (turnstart && jailTCount < 2) {
                    turnstart = false;
                    jailTCount++;
                    text.text = "Pay £50 to get out of jail early?";
                    purchasable.SetActive(false);
                    UImessages.SetActive(true);
                    payingForJail.SetActive(true);
                }else if (turnstart && jailTCount > 2) {
                    turnstart = false;
                    jailTCount = 0;
                    imprisoned = false;
                    BeforeTurnEnd();
                }
            }        
        }

        //warning message for the player if they're doing something that won't work, e.g. trying to buy something but they don't have enough money

        if (Warning.activeInHierarchy && warningTime) {
            warningTime = false;
            StartCoroutine(warningWaiter());
        }
    }

    IEnumerator cardMsgWaiter() {
        yield return new WaitForSeconds(5);
        opportunity.SetActive(false);
        pot.SetActive(false);
    }

    IEnumerator warningWaiter() {
        yield return new WaitForSeconds(5);
        Warning.SetActive(false);
        warningTime = true;
    }

    public void SwitchPlayer()
    {
        active = false;
        turnstart = true;
        moving = false;
        fdouble = true;
        sdouble = false;
        nroll1 = false;
        nroll2 = false;
        mshrend.material = defaultMat;
    }

    //----------------------------------------------------purchasable UI-----------------------------------------------------

    public void PaidForJail() {
        jailTCount = 0;
        money -= 50;
        UImessages.SetActive(false);
        payingForJail.SetActive(false);
        imprisoned = false;
        Vector3 jailtransform = pm.boardLocation.transform.position;
        jailtransform.y = transform.position.y;
        jailtransform.z += 1.5f;
        transform.position = jailtransform;
    }

    public void StayingInJail() {
        UImessages.SetActive(false);
        payingForJail.SetActive(false);
    }

    //this will be ran if they press buy on purchasable
    public void Buy() {
        Debug.Log("Buy attempt...");

        if (money >= pm.boardLocation.GetComponent<PropertySection>().Price) {
            money -= pm.boardLocation.GetComponent<PropertySection>().Price;
            m.text = money.ToString();

            OwnedProperty.Add(pm.boardLocation.GetComponent<PropertySection>());
            pm.boardLocation.GetComponent<PropertySection>().Buy(this);

            Debug.Log(playername + " just bought this");
            Debug.Log(pm.boardLocation.GetComponent<PropertySection>().OwnedBy + " is the owner of this property now");
            Debug.Log(money + " left");
            Debug.Log(OwnedProperty[0] + " - please note that 'GO' is Field1");
        } else {
            Warning.SetActive(true);
            WarningText.text = "You can't afford this property!";
        }
        if (nroll1 || nroll2) {
            nroll1 = false;
            nroll2 = false;
            pm.active = true;
            moving = true;
        } else {
            BeforeTurnEnd();
        }
    }


    /// <summary>
    /// Auction is another button that is accessed like Buy, when pressed, it will open the auction method for this property location and activated the correct UI.
    /// </summary>

    //! UI hander for the auction menu
    /*!
      This will display all the buttons for the auction, current bid and the property sprite. 
    */
    public void Auction()   //add this to the Auction OnClick button
    {
        GameButtons.tempPi = gm.pi;
        Debug.Log("Auction method player index: "+gm.pi);
        purchasable.SetActive(false);
        curBid = 0;
        bids = 0;
        UImessages.SetActive(true);
        textBox.transform.localScale = new Vector2(1, 1);   //width then height
        text.text = "£" + curBid.ToString(); //this cost variable will be the amount that the property costs
        auctionOptions.gameObject.SetActive(true);
        tileLandedOnAuction.SetActive(true);
        tileLandedOnAuction.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pm.boardLocation.GetComponent<PropertySection>().Show();

        switch (pm.boardLocation.GetComponent<PropertySection>().Group) {
            case "Brown":
                // change colour to match that of the PropertySection group
                tileLandedOnAuction.GetComponent<Image>().color = new Color32(153, 51, 0, 100);
                break;
            case "Blue":
                // change colour to match that of the PropertySection group
                tileLandedOnAuction.GetComponent<Image>().color = new Color32(0, 153, 255, 100);
                break;
            case "Purple":
                // change colour to match that of the PropertySection group
                tileLandedOnAuction.GetComponent<Image>().color = new Color32(153, 51, 255, 100);
                break;
            case "Orange":
                // change colour to match that of the PropertySection group
                tileLandedOnAuction.GetComponent<Image>().color = new Color32(255, 102, 0, 100);
                break;
            case "Red":
                // change colour to match that of the PropertySection group
                tileLandedOnAuction.GetComponent<Image>().color = new Color32(255, 51, 0, 100);
                break;
            case "Yellow":
                // change colour to match that of the PropertySection group
                tileLandedOnAuction.GetComponent<Image>().color = new Color32(255, 255, 0, 100);
                break;
            case "Green":
                // change colour to match that of the PropertySection group
                tileLandedOnAuction.GetComponent<Image>().color = new Color32(0, 204, 0, 100);
                break;
            case "Deep blue":
                // change colour to match that of the PropertySection group
                tileLandedOnAuction.GetComponent<Image>().color = new Color32(0, 0, 204, 100);
                break;
            default:
                // change colour to match that of the PropertySection group
                tileLandedOnAuction.GetComponent<Image>().color = new Color32(0, 0, 204, 100);
                break;
        }
        clickToRoll.SetActive(false);
    }

    //----------------------------------------------------auction UI-----------------------------------------------------
    /// <summary>
    /// Connected to the Auction menu, this is the button that will bid £1 on the property provided that the player has enough money in their account to afford what would be the new bit amount.
    /// The array pointer is now using a temporary int called tempPi which is used to not only ensure the turns will go back to normal after the auction is over, but its also used to money whos winning etc.
    /// </summary>

    //! £1 bidding button for the auction
    /*!
      This is also ran through the GameButtons script for the temporary auction current player pointer depending on if the player has enough money to afford what the auction cost could be after their bid. 
    */
    public void OnePound() {
        if (money >= (curBid + 1)) {
            curBid += 1;
            bids++;
            text.text = "£" + curBid.ToString();
            curBidWinner = GameButtons.tempPi;
            Debug.Log("current bit winner is - " + curBidWinner);
            Debug.Log("player name is - " + gm.players[GameButtons.tempPi].GetComponent<Player>().playername);
            BottomPanel();
            NextTempPi();
        } else {
            Warning.SetActive(true);
            WarningText.text = "You can't afford to bid £1!";
        }
    }
    /// <summary>
    /// Connected to the Auction menu, this is the button that will bid £10 on the property provided that the player has enough money in their account to afford what would be the new bit amount.
    /// </summary>

    //! £10 bidding button for the auction
    /*!
      This is also ran through the GameButtons script for the temporary auction current player pointer depending on if the player has enough money to afford what the auction cost could be after their bid. 
    */
    public void TenPound() {
        if (money >= (curBid + 10)) {
            curBid += 10;
            bids++;
            text.text = "£" + curBid.ToString();
            curBidWinner = GameButtons.tempPi;
            Debug.Log("current bit winner is - " + curBidWinner);
            Debug.Log("player name is - " + gm.players[GameButtons.tempPi].GetComponent<Player>().playername);
            BottomPanel();
            NextTempPi();
        } else {
            Warning.SetActive(true);
            WarningText.text = "You can't afford to bid £10!";
        }
    }
    /// <summary>
    /// Connected to the Auction menu, this is the button that will bid £100 on the property provided that the player has enough money in their account to afford what would be the new bit amount.
    /// </summary>

    //! £100 bidding button for the auction
    /*!
      This is also ran through the GameButtons script for the temporary auction current player pointer depending on if the player has enough money to afford what the auction cost could be after their bid. 
    */
    public void HundredPound() {
        if (money >= (curBid + 100)) {
            curBid += 100;
            bids++;
            text.text = "£" + curBid.ToString();
            curBidWinner = GameButtons.tempPi;
            Debug.Log("current bit winner is - " + curBidWinner);
            Debug.Log("player name is - " + gm.players[GameButtons.tempPi].GetComponent<Player>().playername);
            BottomPanel();
            NextTempPi();
        }                       //looks like this works but i couldnt tell if it moved to the next player when they had run out of money?
        else {
            Warning.SetActive(true);
            WarningText.text = "You can't afford to bid £100!";
        }
    }
    /// <summary>
    /// If the player wishes to withdraw from the auction, this will what will happen.
    /// The bool withdraw for the location will become true so the cycle of biddings will know to skip this player
    /// </summary>

    //! Stop taking part in the auction
    /*!
      A button that takes the current player out of the cycle of bids by setting their bool to true.
    */
    public void Withdraw() {
        //withdraw the current pi from the bidding and skipping their BottomPanel - also, if player 1 started the bidding but player 2 won, the panel will need to go back to player 1's.
        withdraw = true;
        Debug.Log(gm.players[GameButtons.tempPi].GetComponent<Player>().playername + " withdrew");
        BottomPanel();
        NextTempPi();
    }
    /// <summary>
    /// NextTempPi is ran by all the auction UI buttons where it controls which player is now bidding.
    /// If there are still more than 2 player that have a false withdraw, the game will keep running the bidding.
    /// If a player has withdrawn, the method will change to the next player that is still playing and ensure that the correct panel is still showing.
    /// If there are less than 2 players still playing and there have been atleast 1 bid, the current bidder that is still active is the winner and have their current bit money taken and property added to the list.
    /// If there are less than 2 players still playing and there have been no bids, then this means that nobody bidded for the property so it isnt sold to anyone and the game continues to the next UI 
    /// </summary>

    //! Auction player changing
    /*!
      All the auction buttons run this, is players is higher than 2 the game continues, if its 1 and there has been a bid, the player wins, if there is 1 player and no bids, nobody has bought it. 
    */
    private void NextTempPi() {
        int players = 0;
        for (int i = 0; i < gm.players.Length; i++) //checks if there are still at least 2 players that havent withdrawn yet
        {
            if (!gm.players[i].GetComponent<Player>().withdraw && !gm.players[i].GetComponent<Player>().pBankrupt) {
                players++;
            }
        }
        Debug.Log("current players left in auction - " + players);
        if (players > 1)    //if there are enough players then it will go to the next active player
        {
            if (GameButtons.tempPi >= gm.players.Length - 1) {
                GameButtons.tempPi = 0;
            } else {
                GameButtons.tempPi++;
            }
            if (gm.players[GameButtons.tempPi].GetComponent<Player>().withdraw || gm.players[GameButtons.tempPi].GetComponent<Player>().pBankrupt) {
                Debug.Log("withdraw or bankrupt");
                Debug.Log("new bidder is - " + gm.players[GameButtons.tempPi].GetComponent<Player>().playername);
                BottomPanel();
                NextTempPi();
            }
            Debug.Log("new bidder is - " + gm.players[GameButtons.tempPi].GetComponent<Player>().playername);
        } else if (players == 1 && bids == 0) {
            for (int i = 0; i < gm.players.Length; i++) {
                gm.players[i].GetComponent<Player>().withdraw = false;
            }
            Debug.Log("no winner");
            bottomPanels[panelPos].SetActive(false);
            panelPos = gm.pi;
            bottomPanels[panelPos].SetActive(true);
            BeforeTurnEnd();
        } else if (players == 1 && bids > 0)   //only 1 player left, the winner
          {
            if (GameButtons.tempPi >= gm.players.Length - 1) {
                GameButtons.tempPi = 0;
            } else {
                GameButtons.tempPi++;
            }
            if (gm.players[GameButtons.tempPi].GetComponent<Player>().withdraw || gm.players[GameButtons.tempPi].GetComponent<Player>().pBankrupt) {
                NextTempPi();
            } else {
                gm.players[gm.pi].GetComponent<Player>().pm.boardLocation.GetComponent<PropertySection>().Buy(gm.players[GameButtons.tempPi].GetComponent<Player>());
                gm.players[GameButtons.tempPi].GetComponent<Player>().OwnedProperty.Add(gm.players[gm.pi].GetComponent<Player>().pm.boardLocation.GetComponent<PropertySection>());
                gm.players[GameButtons.tempPi].GetComponent<Player>().money -= curBid;
                gm.players[GameButtons.tempPi].GetComponent<Player>().m.text = gm.players[GameButtons.tempPi].GetComponent<Player>().money.ToString();
                Debug.Log("Winner is - " + gm.players[GameButtons.tempPi].GetComponent<Player>().playername);
                Debug.Log(gm.players[GameButtons.tempPi].GetComponent<Player>().OwnedProperty[0].GetComponent<PropertySection>().OwnedBy.GetComponent<Player>().playername + " is the owner of the property");
                bottomPanels[panelPos].SetActive(false);
                panelPos = gm.pi;
                bottomPanels[panelPos].SetActive(true);
                BeforeTurnEnd();
            }
        }
    }

    //----------------------------------------------------BetweenTurns UI-----------------------------------------------------

    /// <summary>
    /// Between turns is called at the start of each players turn instructing them on how to roll.
    /// The results of rolling the dice will then disable this methods information.
    /// As its the start of the next players turn, it will also change the current bottom panel that is being displayed to indicate which player turn it currently is.
    /// </summary>
    //this will be called when its the next players turn - START OF TURN
    public void BetweenTurns()
    {
        //UImessages.SetActive(true);
        //purchasable.SetActive(false);
        //Warning.SetActive(true);
        //WarningText.text = gm.players[gm.pi].GetComponent<player>().playername + ", it's your turn to roll!";
        //textBox.transform.localScale = new Vector2(1, 1);   //width then height
        //text.text = gm.players[gm.pi].GetComponent<player>().playername + ", it's your turn to roll!"; 
        clickToRoll.gameObject.SetActive(true);
        BottomPanel();      
        beforeEndTurn.gameObject.SetActive(false);
    }

    //----------------------------------------------------BeforeTurnEnd UI-----------------------------------------------------

    //this will be called before the end of the players turn - END OF TURN
    public void BeforeTurnEnd()
    {                   //BeforeTurnEnd can be accessed/activated through multiple UI elements so need to make sure all possibilities are off...
        UImessages.SetActive(false);
        StartCoroutine(turnEndWaiter());
        clickToRoll.gameObject.SetActive(false);
        viewProperties.gameObject.SetActive(false);
        auctionOptions.gameObject.SetActive(false);
        tileLandedOnAuction.SetActive(false);
        purchasable.SetActive(false);
        payingForJail.SetActive(false);
        beforeEndTurn.gameObject.SetActive(true);
    }

    IEnumerator turnEndWaiter() {
        yield return new WaitForSeconds(5);
        Warning.SetActive(false);
        opportunity.gameObject.SetActive(false);
        pot.gameObject.SetActive(false);
    }

    //----------------------------------------------------ViewProperties UI-----------------------------------------------------

    //this is called when the player presses the button to view their properties

    //! UI handler for the property viewing and editing
    /*!
      This UI screen will have all the buttons used to change various things about your owned properties.
    */
    public void ViewProperties() {
        beforeEndTurn.gameObject.SetActive(false);
        curViewProperty = 0;
        viewProperties.SetActive(true);

        foreach (var p in OwnedProperty) {
            // get string version of this PropertySection properties
            string s = p.Show();
            // showing properties of PropertySection on the in game card
            TextMeshProUGUI spriteInfo = GameObject.Find("OwnedPropertyScreen").GetComponent<TextMeshProUGUI>();
            spriteInfo.text = s;

            switch (p.Group) {
                case "Brown":
                    // change colour to match that of the PropertySection group
                    GameObject.Find("OPS").GetComponent<Image>().color = new Color32(153, 51, 0, 100);
                    break;
                case "Blue":
                    // change colour to match that of the PropertySection group
                    GameObject.Find("OPS").GetComponent<Image>().color = new Color32(0, 153, 255, 100);
                    break;
                case "Purple":
                    // change colour to match that of the PropertySection group
                    GameObject.Find("OPS").GetComponent<Image>().color = new Color32(153, 51, 255, 100);
                    break;
                case "Orange":
                    // change colour to match that of the PropertySection group
                    GameObject.Find("OPS").GetComponent<Image>().color = new Color32(255, 102, 0, 100);
                    break;
                case "Red":
                    // change colour to match that of the PropertySection group
                    GameObject.Find("OPS").GetComponent<Image>().color = new Color32(255, 51, 0, 100);
                    break;
                case "Yellow":
                    // change colour to match that of the PropertySection group
                    GameObject.Find("OPS").GetComponent<Image>().color = new Color32(255, 255, 0, 100);
                    break;
                case "Green":
                    // change colour to match that of the PropertySection group
                    GameObject.Find("OPS").GetComponent<Image>().color = new Color32(0, 204, 0, 100);
                    break;
                case "Deep blue":
                    // change colour to match that of the PropertySection group
                    GameObject.Find("OPS").GetComponent<Image>().color = new Color32(0, 0, 204, 100);
                    break;
                default:
                    // change colour to match that of the PropertySection group
                    GameObject.Find("OPS").GetComponent<Image>().color = new Color32(0, 0, 204, 100);
                    break;
            }

        }
        viewProperties.gameObject.SetActive(true);
        //when done viewing, need to go back to BeforeTurnEnd()
    }
    //! Change property sprite
    /*!
      This will change the current property sprite that you wish to edit.
    */
    public void NextProperty()          //is there an easier way to make the button work for a certain player?
    {
        curViewProperty++;
        if (curViewProperty >= OwnedProperty.Count) {
            curViewProperty = 0;
        }
        Debug.Log("property " + (curViewProperty + 1) + "/" + OwnedProperty.Count);
        property.GetComponent<Image>().sprite = ourProperties[curViewProperty]; //gm.players[gm.pi].GetComponent<player>().
    }
    //! Increase houses
    /*!
      This is the button that will increase the amount of houses the current property has, provided that they can afford it and that the house count isnt already maxed
    */
    public void UpgradeProperty()
    {
        if (OwnedProperty[curViewProperty].Upgrades >= 5)//pm.boardLocation.GetComponent<PropertySection>().Upgrades
        {   //maxed
            Warning.SetActive(true);
            WarningText.text = "You've already maxed this property!";
        } else if (money >= OwnedProperty[curViewProperty].UpgradeCost && !OwnedProperty[curViewProperty].mortgage)//pm.boardLocation.GetComponent<PropertySection>().UpgradeCost
          {   //upgradable            
            money -= OwnedProperty[curViewProperty].UpgradeCost; // pm.boardLocation.GetComponent<PropertySection>().UpgradeCost;
            OwnedProperty[curViewProperty].Upgrade(); // pm.boardLocation.GetComponent<PropertySection>().Upgrade(); //this will use the money shown on the card to take it out of the players money and change the upgrade status for next time and the rent amount
            m.text = money.ToString();
        } else if (!OwnedProperty[curViewProperty].mortgage) {   //not enough money
            Warning.SetActive(true);
            WarningText.text = "You can't afford to upgrade this property!";
        } else {
            Warning.SetActive(true);
            WarningText.text = "This property needs to be unmortgaged first before you can upgrade!";
        }
    }
    //! Decrease houses
    /*!
      This is the button that will decrease the amount of houses the current property has provided that there are houses currently on the property.
    */
    public void DowngradeProperty() {
        if (OwnedProperty[curViewProperty].Upgrades > 0) {//pm.boardLocation.GetComponent<PropertySection>().Upgrades
            OwnedProperty[curViewProperty].Downgrade(); //pm.boardLocation.GetComponent<PropertySection>().Downgrade(); //this will use the money shown on the card to put into the players money and change the upgrade status for next time and the rent amount
            money += OwnedProperty[curViewProperty].UpgradeCost / 2; //money += pm.boardLocation.GetComponent<PropertySection>().UpgradeCost / 2;
            m.text = money.ToString();
        } else if (OwnedProperty[curViewProperty].Upgrades <= 0 && !OwnedProperty[curViewProperty].mortgage)//if (pm.boardLocation.GetComponent<Property>().avaliable == true)
          {   //already mortgaged
            Warning.SetActive(true);
            WarningText.text = "This property can't be downgraded anymore!";
        } else {
            Warning.SetActive(true);
            WarningText.text = "This property is currently mortgaged!";
        }
    }
    //! Mortgage current property
    /*!
      When pressed, if there arent any houses on this property and it hasnt already been mortgaged, this will give you have the money from the bank and the rent will become free till its unmortgaged. If pressed again, this will unmortgage the house but you will pay a 10% extra fee.
    */
    public void Mortgage()  
    {
        if (OwnedProperty[curViewProperty].Upgrades == 0 && !OwnedProperty[curViewProperty].mortgage)//pm.boardLocation.GetComponent<PropertySection>().Upgrades == 0 && !pm.boardLocation.GetComponent<PropertySection>().mortgage)  
        {
            OwnedProperty[curViewProperty].Mortgage(); //pm.boardLocation.GetComponent<PropertySection>().Mortgage();   
            money += OwnedProperty[curViewProperty].Price / 2; //money += pm.boardLocation.GetComponent<PropertySection>().Price/2;   
            m.text = money.ToString();
            Warning.SetActive(true);
            WarningText.text = "You've mortgaged this property!";
            Debug.Log(OwnedProperty[curViewProperty]);
        } else if (OwnedProperty[curViewProperty].Upgrades != 0) {
            Warning.SetActive(true);
            WarningText.text = "You still own houses on this which you need to sell first!";
        } else {
            OwnedProperty[curViewProperty].Unmortgage();
            money -= (OwnedProperty[curViewProperty].Price / 2);
            m.text = money.ToString();
            Warning.SetActive(true);
            WarningText.text = "This property is now unmortgaged!";
        }
    }
    //! Sell the property back to the bank
    /*!
      When pressed, if there are no houses on the property, it will give you full price of the property or if it was already mortgaged, it'll return the other half of the cost.
    */
    public void Sell() {
        if (OwnedProperty[curViewProperty].Upgrades == 0 && OwnedProperty[curViewProperty].mortgage)//pm.boardLocation.GetComponent<PropertySection>().Upgrades == 0 && pm.boardLocation.GetComponent<PropertySection>().mortgage)
        {
            OwnedProperty[curViewProperty].MortgageSell();//pm.boardLocation.GetComponent<PropertySection>().Sell();   
            money += OwnedProperty[curViewProperty].Price / 2;//money += pm.boardLocation.GetComponent<PropertySection>().Price/2;   
            m.text = money.ToString();

            Warning.SetActive(true);
            WarningText.text = "You've sold this property!";
            OwnedProperty.RemoveAt(curViewProperty);
            ourProperties.RemoveAt(curViewProperty);
            if (OwnedProperty.Count > 0) {
                NextProperty();
            } else {
                BeforeTurnEnd();
            }
            //remove this property from their list of owned properties
        } else if (OwnedProperty[curViewProperty].Upgrades != 0) {
            Warning.SetActive(true);
            WarningText.text = "You still own houses on this which you need to sell first!";
        } else {
            OwnedProperty[curViewProperty].Sell();
            money += OwnedProperty[curViewProperty].Price;
            m.text = money.ToString();
            Warning.SetActive(true);
            WarningText.text = "You've sold this property!";
            OwnedProperty.RemoveAt(curViewProperty);
            ourProperties.RemoveAt(curViewProperty);
        }
    }

    //----------------------------------------------------Trade UI-----------------------------------------------------

    //this will be ran if the player pressed trade on BeforeTurnEnd
    public void TradePlayerMenu()
    {
        if (tradeAllowed) {
            tradePointer = 0;
            tradePointer = incrementTradePointer(tradePointer);
            beforeEndTurn.gameObject.SetActive(false);
            UImessages.SetActive(true);
            tradeMMenu.SetActive(true);
            textBox.transform.localScale = new Vector2(1, 1);
            text.text = "Begin trading with " + gm.players[tradePointer].GetComponent<Player>().PlayerName + "?";
        } else {
            UImessages.SetActive(true);
            text.text = "You're not allowed to trade in this turn anymore.";
            StartCoroutine(tradeWarningWaiter());
        }
        
    }

    public void TradeNextPlayer()
    {
        tradePointer = incrementTradePointer(tradePointer);
        text.text = "Begin trading with " + gm.players[tradePointer].GetComponent<Player>().PlayerName + "?";
    }

    public void TradeCancel()
    {
        tradeMMenu.SetActive(false);
        UImessages.SetActive(false);
        beforeEndTurn.gameObject.SetActive(true);
    }

    public void StartTrading() //this is accessed through the Start Trading button
    {
        UImessages.SetActive(false);
        tradeMMenu.SetActive(false);
        tradeOfferMenu.SetActive(true);
        tradeMyCurProperty = 0;
        tradeMyProperty.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = OwnedProperty[tradeMyCurProperty].GetComponent<PropertySection>().Show();

        switch (OwnedProperty[tradeMyCurProperty].GetComponent<PropertySection>().Group) {
            case "Brown":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(153, 51, 0, 100);
                break;
            case "Blue":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(0, 153, 255, 100);
                break;
            case "Purple":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(153, 51, 255, 100);
                break;
            case "Orange":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(255, 102, 0, 100);
                break;
            case "Red":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(255, 51, 0, 100);
                break;
            case "Yellow":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(255, 255, 0, 100);
                break;
            case "Green":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(0, 204, 0, 100);
                break;
            case "Deep blue":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(0, 0, 204, 100);
                break;
            default:
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(0, 0, 204, 100);
                break;
        }
        tradeTheirCurProperty = 0;
        tradeTheirProperty.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gm.players[tradePointer].GetComponent<Player>().OwnedProperty[tradeTheirCurProperty].GetComponent<PropertySection>().Show();

        switch (gm.players[tradePointer].GetComponent<Player>().OwnedProperty[tradeTheirCurProperty].GetComponent<PropertySection>().Group) {
            case "Brown":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(153, 51, 0, 100);
                break;
            case "Blue":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(0, 153, 255, 100);
                break;
            case "Purple":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(153, 51, 255, 100);
                break;
            case "Orange":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(255, 102, 0, 100);
                break;
            case "Red":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(255, 51, 0, 100);
                break;
            case "Yellow":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(255, 255, 0, 100);
                break;
            case "Green":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(0, 204, 0, 100);
                break;
            case "Deep blue":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(0, 0, 204, 100);
                break;
            default:
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(0, 0, 204, 100);
                break;
        }
        propertyOffered = null;
        propertyExpected = null;
        moneyOffered = 0;
        moneyExpected = 0;
        //use the ourProperties to pick properties to trade and can switch between their ourProperties
        //increase and decrease money that will be traded - must have enough
        //then the other player can say yes or no - if yes then complete but if no then it goes back to where the player can change their offer
        //when done trading, need to go back to BeforeTurnEnd()
    }
    public void MyNextTradeProperty()
    {
        tradeMyCurProperty = (tradeMyCurProperty + 1) % (properties.Count + 1);
        tradeMyProperty.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = OwnedProperty[tradeMyCurProperty].GetComponent<PropertySection>().Show();

        switch (OwnedProperty[tradeMyCurProperty].GetComponent<PropertySection>().Group) {
            case "Brown":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(153, 51, 0, 100);
                break;
            case "Blue":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(0, 153, 255, 100);
                break;
            case "Purple":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(153, 51, 255, 100);
                break;
            case "Orange":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(255, 102, 0, 100);
                break;
            case "Red":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(255, 51, 0, 100);
                break;
            case "Yellow":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(255, 255, 0, 100);
                break;
            case "Green":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(0, 204, 0, 100);
                break;
            case "Deep blue":
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(0, 0, 204, 100);
                break;
            default:
                // change colour to match that of the PropertySection group
                tradeMyProperty.GetComponent<Image>().color = new Color32(0, 0, 204, 100);
                break;
        }
    }
    public void TheirNextTradeProperty()
    {
        tradeTheirCurProperty = (tradeTheirCurProperty + 1) % (properties.Count + 1);
        tradeTheirProperty.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = gm.players[tradePointer].GetComponent<Player>().OwnedProperty[tradeTheirCurProperty].GetComponent<PropertySection>().Show();

        switch (gm.players[tradePointer].GetComponent<Player>().OwnedProperty[tradeTheirCurProperty].GetComponent<PropertySection>().Group) {
            case "Brown":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(153, 51, 0, 100);
                break;
            case "Blue":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(0, 153, 255, 100);
                break;
            case "Purple":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(153, 51, 255, 100);
                break;
            case "Orange":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(255, 102, 0, 100);
                break;
            case "Red":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(255, 51, 0, 100);
                break;
            case "Yellow":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(255, 255, 0, 100);
                break;
            case "Green":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(0, 204, 0, 100);
                break;
            case "Deep blue":
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(0, 0, 204, 100);
                break;
            default:
                // change colour to match that of the PropertySection group
                tradeTheirProperty.GetComponent<Image>().color = new Color32(0, 0, 204, 100);
                break;
        }
    }
    public void OfferBack()
    {
        tradeOfferMenu.SetActive(false);
        UImessages.SetActive(false);
        beforeEndTurn.gameObject.SetActive(true);
    }
    public void AddMyProperty() {
        propertyOffered = OwnedProperty[tradeMyCurProperty];
    }
    public void AddTheirProperty() {
        propertyExpected = gm.players[tradePointer].GetComponent<Player>().OwnedProperty[tradeTheirCurProperty];
    }
    public void AddMyMoney() {
        int tmpMoneyOff = 0;
        int.TryParse(myMoneyField.text, out tmpMoneyOff);
        if (tmpMoneyOff > 0) {
            if (money >= tmpMoneyOff) {
                moneyOffered = tmpMoneyOff;
            } else {
                //Show message for player's insuficient funds
                UImessages.SetActive(true);
                text.text = "You don't have sufficient funds to make this offer";
                StartCoroutine(tradeWarningWaiter());
            }
        } else {
            //show message for incorrect money value
            UImessages.SetActive(true);
            text.text = "Please input numeric, non-zero amount of money";
            StartCoroutine(tradeWarningWaiter());
        }
    }
    public void AddTheirMoney() {
        int tmpMoneyOff = 0;
        int.TryParse(theirMoneyField.text, out tmpMoneyOff);
        if (tmpMoneyOff > 0) {
            if (gm.players[tradePointer].GetComponent<Player>().money >= tmpMoneyOff) {
                moneyOffered = tmpMoneyOff;
            } else {
                //show message for their insufficient funds
                UImessages.SetActive(true);
                text.text = "That player doesn't have sufficient funds to accept this offer";
                StartCoroutine(tradeWarningWaiter());
            }
        } else {
            //show message for incorrect money value
            UImessages.SetActive(true);
            text.text = "Please input numeric, non-zero amount of money";
            StartCoroutine(tradeWarningWaiter());
        }
    }
    public void ConfirmOffer()    //confirm and deny buttons will appear
    {
        UImessages.SetActive(true);
        tradeConfirm.SetActive(true);
        text.text = "Do you want to accept this offer?";
    }
    public void TradingAgreed()
    {
        //confirm the trading deal
        if (propertyOffered != null) {
            propertyOffered.Buy(gm.players[tradePointer].GetComponent<Player>());
            gm.players[tradePointer].GetComponent<Player>().OwnedProperty.Add(propertyOffered);
            OwnedProperty.Remove(propertyOffered);
        }

        if (moneyOffered != 0) {
            gm.players[tradePointer].GetComponent<Player>().money += moneyOffered;
            money -= moneyOffered;
        }

        if (propertyExpected != null) {
            propertyExpected.Buy(this);
            OwnedProperty.Add(propertyExpected);
        }

        if (moneyExpected != 0) {
            money += moneyExpected;
            gm.players[tradePointer].GetComponent<Player>().money -= moneyExpected;
        }

        tradeAllowed = false;
        tradeOfferMenu.SetActive(false);
        tradeConfirm.SetActive(false);
        UImessages.SetActive(false);
        beforeEndTurn.gameObject.SetActive(true);
    }
    public void TradingDenied()
    {
        //deny the trading deal
        tradeAllowed = false;
        tradeOfferMenu.SetActive(false);
        tradeConfirm.SetActive(false);
        UImessages.SetActive(false);
        beforeEndTurn.gameObject.SetActive(true);
    }

    private int incrementTradePointer(int tp) {
        int temptp = (tp + 1) % gm.players.Length;
        while (temptp == playerIndex && !gm.players[temptp].GetComponent<Player>().bankrupt) {
            temptp = (temptp + 1) % gm.players.Length;
            Debug.Log("temptp");
        }
        return temptp;
    }

    IEnumerator tradeWarningWaiter() {
        yield return new WaitForSeconds(5);
        UImessages.SetActive(false);
    }

    //----------------------------------------------------BottomPanel UI-----------------------------------------------------

    //call this method when its the next players turn as it will switch the panel at the bottom to be the correct colour for the current player
    //! Player colour indicator 
    /*!
      This method is run whenever the current player is changed
    */
    public void BottomPanel() {
        bottomPanels[panelPos].SetActive(false);
        panelPos++;
        if (panelPos >= gm.players.Length)
        {
            panelPos = 0;
        }
        if (gm.players[panelPos].GetComponent<Player>().pBankrupt) {
            BottomPanel();
        }
        Debug.Log("panel position - " + panelPos);
        Debug.Log("out of - " + gm.players.Length);
        bottomPanels[panelPos].SetActive(true);
    }

    //--------------------------------------------------------Bankrupt-------------------------------------------------------
    //! Bankrupt 
    /*!
      This will run is the player presses the bankrupt button, removing them from the game.
    */
    public void GoBankrupt() {
        for (int i = 0; i < OwnedProperty.Count; i++) {
            OwnedProperty[i].Sell();
        }
        money -= money;
        m.text = "BANKRUPT";
        pBankrupt = true;
        gm.playerCount--;
        Debug.Log(gm.playerCount + " bankruptcount");
        Debug.Log(gm.players.Length + " player length");
        //BetweenTurns();
        gameObject.active = false;
    }
}