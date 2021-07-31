using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public int rollAmount;
    public int doubleCount = 0;
   
    //make a priority queue 

    public interface IPriorityQueue<T>
    {
        /// Inserts and item with a priority
        void Insert(T item, int priority);

        /// Returns the element with the highest priority
        T Top();

        /// Deletes and returns the element with the highest priority
        T Pop();
    }

    public void First()
    {
        //players.Add(GameObject.GetComponent<Player>());     //just add the first rolled player
    }

    public void Order(int roll) //for each starting roll, place the player object in the list where it stops being the lowest
    {
       // while (roll < )
       // players.Add
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
