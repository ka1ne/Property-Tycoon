using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    private string[,] playersloaddata;
    public int pi; //playerindex

    public TextMeshProUGUI[] gameP;
    public GameObject[] playercards;
    public TextMeshProUGUI[] token; //HERE

    public int playerCount = 0;

    [SerializeField]
    private GameObject[] tokens;

    private bool firstturn;
    private bool nextPlayer;
    private bool boardload;
    private bool gamestart;
    private bool ordered;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        playersloaddata = null;
        pi = 0;
        firstturn = true;
        nextPlayer = false;
        ordered = false;
        gamestart = false; //Change to false, need to switch on when game scene is fully loaded
    }

    void Update() {
        if (playerCount == 1) {
            for (int i = 0; i < players.Length; i++) {
                if (!players[i].GetComponent<Player>().pBankrupt) {
                    playerCount = 10;
                    players[0].GetComponent<Player>().winText.text = "Congratulations " + players[i].GetComponent<Player>().playername + ", you won Property Tycoon!";
                    players[0].GetComponent<Player>().winCanvas.SetActive(true);
                    Debug.Log("Winner!");
                }
            }
        }

        if (SceneManager.GetActiveScene().name.Equals("Menu") && playersloaddata != null) {
            SceneManager.LoadScene("GameBoard");
        }

        if (SceneManager.GetActiveScene().name.Equals("GameBoard") && boardload == false) {
            boardload = true;
            LoadPlayers();
            playerCardsLink();
        }

        if (gamestart) {
            gamestart = false;
            OrderPlayers();
        }

        if (ordered) {
            ordered = false;
            if (players[pi].GetComponent<Player>().loaded) {
                players[pi].GetComponent<Player>().active = true;
                firstturn = false;
            } else {
                ordered = true;
            }
        }

        if (nextPlayer) {
            nextPlayer = false;
            incrementPindex();
            players[pi].GetComponent<Player>().active = true;
        }

        if (!firstturn && !players[pi].GetComponent<Player>().active) {
            nextPlayer = true;
        }

    }

    void incrementPindex() {
        pi = (pi + 1) % players.Length;
        while (players[pi].GetComponent<Player>().pBankrupt) {
            pi = (pi + 1) % players.Length;
        }
    }

    void OrderPlayers() {
        int[] randvals = new int[players.Length];

        for (int i = 0; i < randvals.Length; i++) {
            randvals[i] = Mathf.FloorToInt(Random.Range(1f, players.Length));
            for (int j = 1; j <= i; j++) {
                if (randvals[j-1] < randvals[j]) {
                    GameObject temp = players[j-1];
                    players[j - 1] = players[j];
                    players[j] = temp;

                    int temp2 = randvals[j - 1];
                    randvals[j - 1] = randvals[j];
                    randvals[j] = temp2;
                }
            }
        }
        SetNames();
        ordered = true;
    }

    void LoadPlayers() {
        players = new GameObject[playersloaddata.GetLength(0)];
        for (int i = 0; i < playersloaddata.GetLength(0); i++) {
            switch (playersloaddata[i,2]) {
                case "boot":
                    players[i] = Instantiate(tokens[0], new Vector3(-15.81f, 0.67f, -16.07f), Quaternion.identity);
                    players[i].GetComponent<Player>().token = "boot";
                    break;

                case "cat":
                    players[i] = Instantiate(tokens[1], new Vector3(-17.539f, 0.54f, -16.07f), Quaternion.identity);
                    players[i].GetComponent<Player>().token = "cat";
                    break;

                case "spoon":
                    players[i] = Instantiate(tokens[2], new Vector3(-18.802f, 0.766f, -18.213f), Quaternion.identity);
                    players[i].GetComponent<Player>().token = "spoon";
                    break;
               
                case "smartphone":
                    players[i] = Instantiate(tokens[3], new Vector3(-15.81f, 0.667f, -18.415f), Quaternion.identity);
                    players[i].GetComponent<Player>().token = "smartphone";
                    break;

                case "goblet":
                    players[i] = Instantiate(tokens[4], new Vector3(-17.539f, 0.585f, -18.415f), Quaternion.identity);
                    players[i].GetComponent<Player>().token = "goblet";
                    break;

                case "hatstand":
                    players[i] = Instantiate(tokens[5], new Vector3(-18.802f, 0.57f, -16.252f), Quaternion.identity);
                    players[i].GetComponent<Player>().token = "hatstand";
                    break;

                default:
                    Debug.LogError("Erronous player token!");
                    break;
            }
            players[i].GetComponent<Player>().playername = playersloaddata[i, 0];
            players[i].GetComponent<Player>().NPC = playersloaddata[i, 1].Equals("NPC");
        }
        playerCount = players.Length;
        gamestart = true;
    }

    public void InputPlayersData(string[,] plist) {
        playersloaddata = plist;
    }

    public void SetNames()  //this is called when the game scene loaded
    {
        switch (players.Length) {
            case 2:
                gameP[0].text = players[0].GetComponent<Player>().playername;    //these are being applied to the UI's names
                players[0].GetComponent<Player>().playerIndex = 0;
                token[0].text = players[0].GetComponent<Player>().token;
                gameP[1].text = players[1].GetComponent<Player>().playername;
                players[1].GetComponent<Player>().playerIndex = 1;
                token[1].text = players[1].GetComponent<Player>().token;


                playercards[0].SetActive(true);
                playercards[0].transform.localPosition = new Vector3(-190, -97, 4); //this is the position the p1 needs to be in when the layout has 2 players
                playercards[1].SetActive(true);
                playercards[1].transform.localPosition = new Vector3(0, -97, 4);
                break;

            case 3:
                gameP[0].text = players[0].GetComponent<Player>().playername;
                players[0].GetComponent<Player>().playerIndex = 0;
                token[0].text = players[0].GetComponent<Player>().token;

                gameP[1].text = players[1].GetComponent<Player>().playername;
                players[1].GetComponent<Player>().playerIndex = 1;
                token[1].text = players[1].GetComponent<Player>().token;

                gameP[2].text = players[2].GetComponent<Player>().playername;
                players[2].GetComponent<Player>().playerIndex = 2;
                token[2].text = players[2].GetComponent<Player>().token;


                playercards[0].SetActive(true);
                playercards[0].transform.localPosition = new Vector3(-280, -97, 4);
                playercards[1].SetActive(true);
                playercards[1].transform.localPosition = new Vector3(-92, -97, 4);
                playercards[2].SetActive(true);
                playercards[2].transform.localPosition = new Vector3(98, -97, 4);
                break;

            case 4:
                gameP[0].text = players[0].GetComponent<Player>().playername;
                players[0].GetComponent<Player>().playerIndex = 0;
                token[0].text = players[0].GetComponent<Player>().token;

                gameP[1].text = players[1].GetComponent<Player>().playername;
                players[1].GetComponent<Player>().playerIndex = 1;
                token[1].text = players[1].GetComponent<Player>().token;

                gameP[2].text = players[2].GetComponent<Player>().playername;
                players[2].GetComponent<Player>().playerIndex = 2;
                token[2].text = players[2].GetComponent<Player>().token;

                gameP[3].text = players[3].GetComponent<Player>().playername;
                players[3].GetComponent<Player>().playerIndex = 3;
                token[3].text = players[3].GetComponent<Player>().token;


                playercards[0].SetActive(true);
                playercards[0].transform.localPosition = new Vector3(-374, -97, 4);
                playercards[1].SetActive(true);
                playercards[1].transform.localPosition = new Vector3(-187, -97, 4);
                playercards[2].SetActive(true);
                playercards[2].transform.localPosition = new Vector3(3, -97, 4);
                playercards[3].SetActive(true);
                playercards[3].transform.localPosition = new Vector3(190, -97, 4);
                break;

            case 5:
                gameP[0].text = players[0].GetComponent<Player>().playername;
                players[0].GetComponent<Player>().playerIndex = 0;
                token[0].text = players[0].GetComponent<Player>().token;

                gameP[1].text = players[1].GetComponent<Player>().playername;
                players[1].GetComponent<Player>().playerIndex = 1;
                token[1].text = players[1].GetComponent<Player>().token;

                gameP[2].text = players[2].GetComponent<Player>().playername;
                players[2].GetComponent<Player>().playerIndex = 2;
                token[2].text = players[2].GetComponent<Player>().token;

                gameP[3].text = players[3].GetComponent<Player>().playername;
                players[3].GetComponent<Player>().playerIndex = 3;
                token[3].text = players[3].GetComponent<Player>().token;

                gameP[4].text = players[4].GetComponent<Player>().playername;
                players[4].GetComponent<Player>().playerIndex = 4;
                token[4].text = players[4].GetComponent<Player>().token;


                playercards[0].SetActive(true);
                playercards[0].transform.localPosition = new Vector3(-462, -97, 4);
                playercards[1].SetActive(true);
                playercards[1].transform.localPosition = new Vector3(-273, -97, 4);
                playercards[2].SetActive(true);
                playercards[2].transform.localPosition = new Vector3(-83, -97, 4);
                playercards[3].SetActive(true);
                playercards[3].transform.localPosition = new Vector3(102, -97, 4);
                playercards[4].SetActive(true);
                playercards[4].transform.localPosition = new Vector3(289, -97, 4);
                break;

            case 6:
                gameP[0].text = players[0].GetComponent<Player>().playername;
                players[0].GetComponent<Player>().playerIndex = 0;
                token[0].text = players[0].GetComponent<Player>().token;

                gameP[1].text = players[1].GetComponent<Player>().playername;
                players[1].GetComponent<Player>().playerIndex = 1;
                token[1].text = players[1].GetComponent<Player>().token;

                gameP[2].text = players[2].GetComponent<Player>().playername;
                players[2].GetComponent<Player>().playerIndex = 2;
                token[2].text = players[2].GetComponent<Player>().token;

                gameP[3].text = players[3].GetComponent<Player>().playername;
                players[3].GetComponent<Player>().playerIndex = 3;
                token[3].text = players[3].GetComponent<Player>().token;

                gameP[4].text = players[4].GetComponent<Player>().playername;
                players[4].GetComponent<Player>().playerIndex = 4;
                token[4].text = players[4].GetComponent<Player>().token;

                gameP[5].text = players[5].GetComponent<Player>().playername;
                players[5].GetComponent<Player>().playerIndex = 5;
                token[5].text = players[5].GetComponent<Player>().token;


                playercards[0].SetActive(true);
                playercards[0].transform.localPosition = new Vector3(-571, -97, 4);
                playercards[1].SetActive(true);
                playercards[1].transform.localPosition = new Vector3(-383, -97, 4);
                playercards[2].SetActive(true);
                playercards[2].transform.localPosition = new Vector3(-192, -97, 4);
                playercards[3].SetActive(true);
                playercards[3].transform.localPosition = new Vector3(-6, -97, 4);
                playercards[4].SetActive(true);
                playercards[4].transform.localPosition = new Vector3(179, -97, 4);
                playercards[5].SetActive(true);
                playercards[5].transform.localPosition = new Vector3(363, -97, 4);
                break;

            default:
                Debug.LogError("Incorrect number of players!");
                break;
        }
    }

    void playerCardsLink() {
        GameObject uilinker = GameObject.Find("UILinker");
        gameP = uilinker.GetComponent<UILinker>().gameP;
        playercards = uilinker.GetComponent<UILinker>().playercards;
        token = uilinker.GetComponent<UILinker>().token;    //HERE
    }
}
