using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//this class is all about setting up the players in-game from what they have decided in the menu.

public class PlayerNames : MonoBehaviour
{
    public TMP_InputField menuP1, menuP2, menuP3, menuP4, menuP5, menuP6;   //6 name inputfield on menu
    public TMP_InputField[] menu = new TMP_InputField[6];   //array of the menu input fields
    public Button b11, b12, b13, b14, b15, b16, b21, b22, b23, b24, b25, b26, b31, b32, b33, b34, b35, b36, b41, b42, b43, b44, b45, b46, b51, b52, b53, b54, b55, b56, b61, b62, b63, b64, b65, b66;  //all unconfirm buttons for tokens and players
    public TextMeshProUGUI gameP1, gameP2, gameP3, gameP4, gameP5, gameP6;    //6 text boxes on game
    public string nameP1, nameP2, nameP3, nameP4, nameP5, nameP6;    //names that will be crossed over to the other scenes
    static string[,] players;   //2D array of the final/actual players
    static string[,] temp;    //2D array of all the possible players
    public Toggle Toggle1, Toggle2, Toggle3, Toggle4, Toggle5, Toggle6; //6 npc toggles on menu
    public Toggle[] toggle = new Toggle[6]; //array of all the menu toggles
    static public int count;    //counter to know the size of the actual player amount
    public GameObject p1, p2, p3, p4, p5, p6;   //game player cards to be moved around and add data too (names tokens etc)
    public GameObject PlayerWarning;    //message that warns the player that they havent confirmed enough players
    public float timeLeft = 5.0f;   //countdown for the warning message that appears on the screen
    public string sceneName;    //the name of the current scene which determines when certain things will run


    //this start method will fill the inital arrays that will need to be used for the temporary 2D array. As it's starts, it will activate the next SetNames() method when the game is on the correct scene. 
    private void Start()
    {
        menu[0] = menuP1;  
        menu[1] = menuP2;
        menu[2] = menuP3;
        menu[3] = menuP4;
        menu[4] = menuP5;
        menu[5] = menuP6;
        toggle[0] = Toggle1;
        toggle[1] = Toggle2;
        toggle[2] = Toggle3;
        toggle[3] = Toggle4;
        toggle[4] = Toggle5;
        toggle[5] = Toggle6;
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName == "Game")
        {
            SetNames(); //so the machine knows when to apply the names
        }
    }

    //this method will firstly fill a 2D array of all the players on the menu, then it will add each column into the final 2D array as long as that column doesnt have an "unconfirmed" string.
    public void GetNames()  //this is called when the start button is pressed
    {
        count = 0;
        temp = new string[6, 3];
        for (int i = 0; i < 6; i++) //all possible players. adding all players to the temporary array to be checked at bottom
        {
            if (menu[i].text != "")  //name
            {
                temp[i, 0] = menu[i].text;   
            }
            else
            {
                temp[i, 0] = "Player" + (i+1) ;
            }
            if  (toggle[i].isOn)    //NPC?
            {
                temp[i, 1] = "NPC";
            }
            else
            {
                temp[i, 1] = "NotNPC";
            }
            if (i == 0) //player 1 token
            {
                if (b11.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "boot";
                    count++;
                }
                else if (b12.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "cat";
                    count++;
                }
                else if (b13.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "spoon";
                    count++;
                }
                else if (b14.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "smartphone";
                    count++;
                }
                else if (b15.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "goblet";
                    count++;
                }
                else if (b16.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "hatstand";
                    count++;
                }
                else
                {
                    temp[i, 2] = "unconfirmed";
                }
            }
            if (i == 1) //player 2 token
            {
                if (b21.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "boot";
                    count++;
                }
                else if (b22.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "cat";
                    count++;
                }
                else if (b23.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "spoon";
                    count++;
                }
                else if (b24.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "smartphone";
                    count++;
                }
                else if (b25.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "goblet";
                    count++;
                }
                else if (b26.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "hatstand";
                    count++;
                }
                else
                {
                    temp[i, 2] = "unconfirmed";
                }
            }
            if (i == 2) //player 3 token
            {
                if (b31.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "boot";
                    count++;
                }
                else if (b32.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "cat";
                    count++;
                }
                else if (b33.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "spoon";
                    count++;
                }
                else if (b34.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "smartphone";
                    count++;
                }
                else if (b35.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "goblet";
                    count++;
                }
                else if (b36.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "hatstand";
                    count++;
                }
                else
                {
                    temp[i, 2] = "unconfirmed";
                }
            }
            if (i == 3) //player 4 token
            {
                if (b41.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "boot";
                    count++;
                }
                else if (b42.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "cat";
                    count++;
                }
                else if (b43.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "spoon";
                    count++;
                }
                else if (b44.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "smartphone";
                    count++;
                }
                else if (b45.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "goblet";
                    count++;
                }
                else if (b46.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "hatstand";
                    count++;
                }
                else
                {
                    temp[i, 2] = "unconfirmed";
                }
            }
            if (i == 4) //player 5 token
            {
                if (b51.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "boot";
                    count++;
                }
                else if (b52.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "cat";
                    count++;
                }
                else if (b53.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "spoon";
                    count++;
                }
                else if (b54.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "smartphone";
                    count++;
                }
                else if (b55.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "goblet";
                    count++;
                }
                else if (b56.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "hatstand";
                    count++;
                }
                else
                {
                    temp[i, 2] = "unconfirmed";
                }
            }
            if (i == 5) //player 6 token
            {
                if (b61.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "boot";
                    count++;
                }
                else if (b62.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "cat";
                    count++;
                }
                else if (b63.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "spoon";
                    count++;
                }
                else if (b64.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "smartphone";
                    count++;
                }
                else if (b65.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "goblet";
                    count++;
                }
                else if (b66.gameObject.activeInHierarchy)
                {
                    temp[i, 2] = "hatstand";
                    count++;
                }
                else
                {
                    temp[i, 2] = "unconfirmed";
                }
            }
        }
        int tempamount = 0;
        int position = 0;
        players = new string[count, 3];
        while (tempamount < count)
        {
            if (temp[position, 2] != "unconfirmed")    
            {
                players[tempamount, 0] = temp[position, 0];
                players[tempamount, 1] = temp[position, 1];
                players[tempamount, 2] = temp[position, 2];
                tempamount++;
            }
            position++;
        }
        //Debug.Log(players[0,2]);    //dont forget the values are 1 less...
    }





    //this method will be called when the scene is currently Game. Based on the size of the final players 2D array, it will activate, fill and move the player tiles to the correct position on the canvas. 
    public void SetNames()  //this is called when the game scene loaded
    {
        if (count == 2) //2 players
        {
            gameP1.text = players[0, 0];    //these are being applied to the UI's names
            gameP2.text = players[1, 0];
            p1.SetActive(true);
            p1.transform.localPosition = new Vector3(-190, -97, 4); //this is the position the p1 needs to be in when the layout has 2 players
            p2.SetActive(true);
            p2.transform.localPosition = new Vector3(0, -97, 4);
        }
        else if (count == 3)
        {
            gameP1.text = players[0, 0];
            gameP2.text = players[1, 0];
            gameP3.text = players[2, 0];
            p1.SetActive(true);
            p1.transform.localPosition = new Vector3(-280, -97, 4);
            p2.SetActive(true);
            p2.transform.localPosition = new Vector3(-92, -97, 4);
            p3.SetActive(true);
            p3.transform.localPosition = new Vector3(98, -97, 4);
        }
        else if (count == 4)
        {
            gameP1.text = players[0, 0];
            gameP2.text = players[1, 0];
            gameP3.text = players[2, 0];
            gameP4.text = players[3, 0];
            p1.SetActive(true);
            p1.transform.localPosition = new Vector3(-374, -97, 4);
            p2.SetActive(true);
            p2.transform.localPosition = new Vector3(-187, -97, 4);
            p3.SetActive(true);
            p3.transform.localPosition = new Vector3(3, -97, 4);
            p4.SetActive(true);
            p4.transform.localPosition = new Vector3(190, -97, 4);
        }
        else if (count == 5)
        {
            gameP1.text = players[0, 0];
            gameP2.text = players[1, 0];
            gameP3.text = players[2, 0];
            gameP4.text = players[3, 0];
            gameP5.text = players[4, 0];
            p1.SetActive(true);
            p1.transform.localPosition = new Vector3(-462, -97, 4);
            p2.SetActive(true);
            p2.transform.localPosition = new Vector3(-273, -97, 4);
            p3.SetActive(true);
            p3.transform.localPosition = new Vector3(-83, -97, 4);
            p4.SetActive(true);
            p4.transform.localPosition = new Vector3(102, -97, 4);
            p5.SetActive(true);
            p5.transform.localPosition = new Vector3(289, -97, 4);
        }
        else
        {
            gameP1.text = players[0, 0];
            gameP2.text = players[1, 0];
            gameP3.text = players[2, 0];
            gameP4.text = players[3, 0];
            gameP5.text = players[4, 0];
            gameP6.text = players[5, 0];
            p1.SetActive(true);
            p1.transform.localPosition = new Vector3(-571, -97, 4);
            p2.SetActive(true);
            p2.transform.localPosition = new Vector3(-383, -97, 4);
            p3.SetActive(true);
            p3.transform.localPosition = new Vector3(-192, -97, 4);
            p4.SetActive(true);
            p4.transform.localPosition = new Vector3(-6, -97, 4);
            p5.SetActive(true);
            p5.transform.localPosition = new Vector3(179, -97, 4);
            p6.SetActive(true);
            p6.transform.localPosition = new Vector3(363, -97, 4);
        }
    }

    //this method is used to change to the Game scene once the menu start button has been pressed after it has collected all the data it needs into the static variable.
    public void Scene() //this changes the scene to the game when the menu start button is pressed
    {
        SceneManager.LoadScene("Game");
    }

    public void StartAllowed()
    {
        //make this the second OnClick even for the start button after GetNames...
        //if the player count is greater than 1 then it will run Scene
        if (count > 1)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().InputPlayersData(players);
        }
        else
        {
            PlayerWarning.SetActive(true);

            Debug.Log("lower than 2");
        }
    }

    void Update()
    {
        if (sceneName == "Menu")
        {
            if (PlayerWarning.activeInHierarchy == true)
            {
                timeLeft -= 1 * Time.deltaTime;
                if (timeLeft < 0)
                {
                    PlayerWarning.SetActive(false);
                    timeLeft = 5.0f;
                }
            }
        }
    }
}