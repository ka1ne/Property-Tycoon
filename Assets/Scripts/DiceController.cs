using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    public float speed;
    public int doubles;

    [SerializeField]
    private float waitTime;
    private float timerC; //timer countdown/counter

    public float upPos;
    public float lowPos;

    private GameObject[] dice;
    private bool goingDown;
    private bool goingUp;
    private bool wait;
    private int rollval;
    private Vector3 target;
    private Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        timerC = waitTime;
        goingDown = false;
        goingUp = false;
        doubles = 0;
        int dicenum = this.transform.childCount;
        dice = new GameObject[dicenum];
        for (int i = 0; i < dicenum; i++)
        {
            dice[i] = this.transform.GetChild(i).gameObject;
        }
        transform.position = new Vector3(0f, upPos, 0f);
        target = new Vector3(0f, lowPos, 0f);
        initPos = transform.position;
        rollval = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!goingDown && !goingUp && transform.position.y == lowPos)
        {
            /*int checksum = 0;
            foreach (GameObject d in dice)
            {
                if (d.GetComponent<Dice>().getVal() != 0) checksum++;
            }
            if (checksum == 2)
            {
                wait = true;
                StartCoroutine(Waiter(1f));
            }*/
            if (!dice[0].GetComponent<Dice>().active && !dice[1].GetComponent<Dice>().active) {
                if (dice[0].GetComponent<Dice>().getVal() != 0 && dice[1].GetComponent<Dice>().getVal() != 0) {
                    timerC -= Time.deltaTime;
                    if (timerC < 0f) {
                        goingUp = true;
                        foreach (GameObject d in dice) {
                            d.GetComponent<Dice>().resetDice();
                        }
                        timerC = waitTime;
                    }
                }
            }
        }

        if (goingDown && transform.position.y != lowPos)
        {
            if (speed > 0.1) speed -= 0.01f;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
        else if (goingDown && transform.position.y == lowPos)
        {
            goingDown = false;
            foreach (GameObject d in dice)
            {
                d.GetComponent<Dice>().active = true;
            }
        }
        else if (goingUp && transform.position.y != upPos)
        {
            if (speed < 20f) speed += 0.01f;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, initPos, step);
        }
        else if (goingUp && transform.position.y == upPos)
        {
            goingUp = false;
            int[] dicevals = new int[2];
            for (int i = 0; i < dice.Length; i++)
            {
                dicevals[i] = dice[i].GetComponent<Dice>().getVal();
                rollval += dicevals[i];
                dice[i].GetComponent<Dice>().resetVal();
                dice[i].GetComponent<Dice>().active = false;
            }

            if (dicevals[0] == dicevals[1]) {
                if (doubles == 3) {
                    //Rolled doubles second time - go to jail
                    doubles = 0;
                    Debug.Log("YOU GOIN TO JAIL");
                } else {
                    doubles++;
                }
            } else {
                doubles = 0;
            }
        }
    }

    IEnumerator Waiter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        foreach (GameObject d in dice)
        {
            d.GetComponent<Dice>().resetDice();
        }
        goingUp = true;
        wait = false;
    }

    public void newRoll()
    {
        rollval = 0;
        goingDown = true;
        goingUp = false;
    }

    public int getValue()
    {
        return rollval;
    }
}
