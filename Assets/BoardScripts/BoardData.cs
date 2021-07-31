using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;

// main app
public class BoardData : MonoBehaviour
{
    // board data csv (Assets/Resources/Data/CSV/BoardData.csv)
    string Path = "Data/CSV/BoardData";

    // card data csv (Assets/Resources/Data/CSV/CardData.csv)
    string CardPath = "Data/CSV/CardData";

    // list to which we add BoardSections
    private GameObject[] fields;

    // setup card stacks
    private List<Card> PotLuck;
    private List<Card> OppKnocks;

    // Start is called before the first frame update
    public void StartGame(GameObject[] fields)
    {
        GameObject TheBoard = GameObject.Find("Board");

        // load csv from path as TextAsset
        TextAsset data = LoadData(Path);
        // split data on new lines and commas
        string[] splitData = SplitData(data);

        // convert data array to list
        List<string> dataList = ((string[])splitData).ToList();

        AssembleSections(dataList, fields);

        // ---- now for cards

        // load csv from path as TextAsset
        //TextAsset cardData = LoadData(CardPath);
        // split data on new lines and commas
        //string[] cardSplitData = SplitData(cardData);

        // convert data array to list
        //List<string> cardDataList = new List<string>();
        PotLuck = new List<Card>();
        OppKnocks = new List<Card>();
        Card tmpcrd = null;
        for (int i = 0; i < 16; i++) {
            switch (i) {
                case 1:
                    tmpcrd = new Card();
                    tmpcrd.Description = "You inherit £100";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "Bank pays you a divided of £50";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 2:
                    tmpcrd = new Card();
                    tmpcrd.Description = "You have won 2nd prize in a beauty contest, collect £20";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "You have won a lip sync battle. Collect £100";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 3:
                    tmpcrd = new Card();
                    tmpcrd.Description = "Go back to Crapper Street";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "Advance to Turing Heights";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 4:
                    tmpcrd = new Card();
                    tmpcrd.Description = "Student loan refund. Collect £20";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "Advance to Han Xin Gardens. If you pass GO, collect £200";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 5:
                    tmpcrd = new Card();
                    tmpcrd.Description = "Bank error in your favour. Collect £200";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "Fined £15 for speeding";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 6:
                    tmpcrd = new Card();
                    tmpcrd.Description = "Pay bill for text books of £100";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "Pay university fees of £150";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 7:
                    tmpcrd = new Card();
                    tmpcrd.Description = "Mega late night taxi bill pay £50";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "Take a trip to Hove station. If you pass GO collect £200";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 8:
                    tmpcrd = new Card();
                    tmpcrd.Description = "Advance to go";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "Loan matures, collect £150";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 9:
                    tmpcrd = new Card();
                    tmpcrd.Description = "From sale of Bitcoin you get £50";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "You are assessed for repairs, £40/house, £115/hotel";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 10:
                    tmpcrd = new Card();
                    tmpcrd.Description = "Pay a £10 fine or take opportunity knocks";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "Advance to GO";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 11:
                    tmpcrd = new Card();
                    tmpcrd.Description = "Pay insurance fee of £50";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "You are assessed for repairs, £25/house, £100/hotel";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 12:
                    tmpcrd = new Card();
                    tmpcrd.Description = "Savings bond matures, collect £100";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "Go back 3 spaces";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 13:
                    tmpcrd = new Card();
                    tmpcrd.Description = "Go to jail. Do not pass GO, do not collect £200";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "Advance to Skywalker Drive. If you pass GO collect £200";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 14:
                    tmpcrd = new Card();
                    tmpcrd.Description = "Received interest on shares of £25";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "Go to jail. Do not pass GO, do not collect £200";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 15:
                    tmpcrd = new Card();
                    tmpcrd.Description = "It's your birthday. Collect £10 from each player";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "Drunk in charge of a skateboard. Fine £20";
                    OppKnocks.Add(tmpcrd);
                    break;

                case 16:
                    tmpcrd = new Card();
                    tmpcrd.Description = "Get out of jail free";
                    PotLuck.Add(tmpcrd);
                    tmpcrd = new Card();
                    tmpcrd.Description = "Get out of jail free";
                    OppKnocks.Add(tmpcrd);
                    break;
            }
        }

    }
    private TextAsset LoadData(string p)
    {
        TextAsset d = Resources.Load<TextAsset>(p);
        
        return d;
    }

    private string[] SplitData(TextAsset d)
    {
        // split on newlines and commas
        string[] splitData = Regex.Split(d.text, "[,\r\n]");
        return splitData;
    }

    public string[] TakeFields(List<string> data, int noFields)
    {
        
        IEnumerable<string> list = data.Take(noFields);
        
        string[] fields = list.ToArray();

        return fields;
    }

    private void AssembleSections(List<string> data, GameObject[] fields)
    {
        SectionBuilder builder;
        Director director = new Director();

        //!todo store notes part!!
        
        // Construct BoardSections
        for (int i=0; i<40; i++)
        {
            string[] FieldSet = TakeFields(data, 16);

            data.RemoveRange(0, 16);
            
            // if PropertySection
            if (FieldSet[5] == "Yes")
            {
                builder = new PropertySectionBuilder();
                director.ConstructSection(FieldSet, builder, fields[i]);
            }
            else if (FieldSet[5] == "No")
            {
                builder = new ActionSectionBuilder();
                director.ConstructSection(FieldSet, builder, fields[i]);
            }
        }
    }

    private void SetupCards(List<string> data)
    {
        CardBuilder builder;
        Director director = new Director();

        // trim csv
        data.RemoveRange(0,5);
        
        // setup PotLuck cards
        string type = "PotLuck";
        for (int i=0; i<16; i++)
        {
            string[] FieldSet = TakeFields(data, 2);

            builder = new CardBuilder();
            director.ConstructCard(FieldSet, builder, type);

            data.RemoveRange(0, 2);
        }

        // trim csv to get to next card set
        data.RemoveRange(0,5);

        // setup OppKnocks cards
        type = "OppKnocks";
        for (int i=0; i<16; i++)
        {
            string[] FieldSet = TakeFields(data, 2);

            builder = new CardBuilder();
            director.ConstructCard(FieldSet, builder, type);

            data.RemoveRange(0, 2);
        }
    }

    // helper functions for CardBuilder
    public void AddToPotLuck(Card c)
    {
        PotLuck.Add(c);
    }
    public void AddToOppKnocks(Card c)
    {
        OppKnocks.Add(c);
    }

    public Card GetPotLuck()
    {
        return PotLuck[Random.Range(0,PotLuck.Count)];
    }
    public Card GetOppKnocks()
    {
        return OppKnocks[Random.Range(0, OppKnocks.Count)];
    }
}
