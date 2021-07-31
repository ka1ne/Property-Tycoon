using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] fields = new GameObject[40];

    [HideInInspector]
    public bool fieldsLoaded;

    // Start is called before the first frame update
    void Awake()
    {
        this.GetComponent<BoardData>().StartGame(fields);
        //fieldsLoaded = true;
    }

    public GameObject getField(int index)
    {
        return fields[index]; ;
    }
}
