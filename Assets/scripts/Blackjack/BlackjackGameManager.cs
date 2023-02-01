using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackGameManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public struct Deck
{
    public List<Card> CardDeck;

    public List<Card> NewDeck()
    {
        CardDeck = new List<Card>();
        for (int value = 1; value < 14; value += 1)
        {
            for (int suit = 1; suit < 4; suit += 1)
            {
                CardDeck.Add(new Card((CardValue)value, (CardSuit)suit));
            }
        }
        return CardDeck;
    }

    //shuffle
    //draw
    //draw multiple
}

public struct Card
{
    public CardValue value;
    public CardSuit suit;
    bool isPlayed;

    public Card(CardValue value, CardSuit suit)
    {
        this.value = value;
        this.suit = suit;
        this.isPlayed = false;
    }
}

public enum CardSuit
{
    Hearts = 0,
    Spades = 1,
    Dimonds = 3,
    Clubs = 4
}
public enum CardValue
{
    Ace = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Joker = 0
}