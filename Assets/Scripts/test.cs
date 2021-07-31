using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public Toggle T;
    //------------------carry variable value between scenes----------------
    static int number;
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Menu")
        {
            number = 5;
        }
        if (sceneName == "Game")
        {
            Debug.Log(number);
        }
    //------------------carry variable value between scenes----------------
    }

    void Update()
    {
        
    }
}
