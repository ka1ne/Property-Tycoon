using UnityEngine;
class CardBuilder
{
    private Card card;
    // gets PropertySection instance
    public  Card Card 
    {
        get {return card;}
        set {card = value;}
    }

    public void SetFields(string[] data, string type)
    {
        if (type == "PotLuck")
        {
            Card.Description =  data[0];
            Card.Action =  data[1];

            GameObject.Find("GameBoard").GetComponent<BoardData>().AddToPotLuck(Card);
        }
        else if (type == "OppKnocks")
        {
            Card.Description =  data[0];
            Card.Action =  data[1];

            GameObject.Find("GameBoard").GetComponent<BoardData>().AddToOppKnocks(Card);
        }
    }
}