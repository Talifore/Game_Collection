using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackjackGameManager : MonoBehaviour
{
    Dealer dealer;
    Player[] players = new Player[1];
    int Order = 0;
    public Sprite[] CardFace;
    public GameObject CardPrefab;
    public GameObject House;
    public GameObject Guest;
    public Text DealScore;
    public Text PlayerScore;
    public List<GameObject> deltCards;
    // Start is called before the first frame update
    void Start()
    {
        players[0] = new Player();
        dealer = new Dealer();
        deltCards = new List<GameObject>();
    }

    Sprite getCardSprite(Card card)
    {
        int index = (int)card.value - 1;
        switch (card.suit)
        {
            case CardSuit.Clubs:
                index += 0;
                break;
            case CardSuit.Spades:
                index += 13;
                break;
            case CardSuit.Hearts:
                index += 26;
                break;
            case CardSuit.Diamonds:
                index += 39;
                break;
        }
        return CardFace[index];
    }

    // Update is called once per frame
    void Update()
    {
        if (Order >= players.Length)
        {
            if(dealer.Score() < 18)
            {
                Card temp = dealer.Draw();
                GameObject obj = Instantiate(CardPrefab, House.transform);
                obj.GetComponent<Image>().sprite = getCardSprite(temp);
                deltCards.Add(obj);
                dealer.hit(temp);
                Debug.Log("Dealer's Hand Currently Contains:");
                for (int L = 0; L < dealer.House.hand.Count; L++)
                {
                    Debug.LogFormat("{0} of {1}", dealer.House.hand[L].value, dealer.House.hand[L].suit);
                }
                Debug.Log("Dealer's Current Score: " + dealer.Score());
            }
        }
        DealScore.text = "Score: " + dealer.Score();
        PlayerScore.text = "Score: " + players[0].Score();
    }

    public void NewGame()
    {
        foreach(GameObject obj in deltCards)
        {
            Destroy(obj);
        }
        deltCards = new List<GameObject>();
        players[0] = new Player();
        dealer = new Dealer();
        for (int n = 0; n < 2; n++)
        {
            Card temp = dealer.Draw();
            GameObject obj = Instantiate(CardPrefab, House.transform);
            obj.GetComponent<Image>().sprite = getCardSprite(temp);
            deltCards.Add(obj);
            dealer.hit(temp);
            Debug.Log("Dealer's Hand Currently Contains:");
            for (int L = 0; L < dealer.House.hand.Count; L++)
            {
                Debug.LogFormat("{0} of {1}", dealer.House.hand[L].value, dealer.House.hand[L].suit);
            }
            Debug.Log("Dealer's Current Score: " + dealer.Score());
            for (int i = 0; i < players.Length; i++)
            {
                temp = dealer.Draw();
                obj = Instantiate(CardPrefab, Guest.transform);
                obj.GetComponent<Image>().sprite = getCardSprite(temp);
                deltCards.Add(obj);
                players[i].hit(temp);
                Debug.LogFormat("Player {0} Hand Currently Contains:", i);
                for (int L = 0; L < players[i].playerHand.hand.Count; L++)
                {
                    Debug.LogFormat("{0} of {1}", players[i].playerHand.hand[L].value, players[i].playerHand.hand[L].suit);
                }
                Debug.Log("Player's Current Score: " + players[i].Score());
            }
        }
        Order = 0;
    }

    public void Hit()
    {
        Card temp = dealer.Draw();
        GameObject obj = Instantiate(CardPrefab, Guest.transform);
        obj.GetComponent<Image>().sprite = getCardSprite(temp);
        deltCards.Add(obj);
        players[Order].hit(temp);
        Debug.LogFormat("Player {0} Hand Currently Contains:", Order);
        for (int L = 0; L < players[Order].playerHand.hand.Count; L++)
        {
            Debug.LogFormat("{0} of {1}", players[Order].playerHand.hand[L].value, players[Order].playerHand.hand[L].suit);
        }
        Debug.Log("Player's Current Score: " + players[Order].Score());
    }

    public void Stand()
    {
        Order++;
    }
}

public class Dealer
{
    public Deck DealDeck;
    public Queue<Card> ReadyDeck;
    public Hand House;

    public Dealer()
    {
        this.newDeck();
        House = new Hand();
    }
    
    public void newDeck()
    {
        ReadyDeck = new Queue<Card>();
        DealDeck = new Deck();
        DealDeck.Shuffle();
        for (int n = 0; n < DealDeck.CardDeck.Count; n++)
        {
            ReadyDeck.Enqueue(DealDeck.CardDeck[n]);
        }
    }
    public Card Draw()
    {
        return ReadyDeck.Dequeue();
    }
    public void hit(Card addIn)
    {
        House.hand.Add(addIn);
    }

    public int Score()
    {
        return House.score();
    }
}

public class Player
{
    public Hand playerHand;

    public Player()
    {
        playerHand = new Hand();
    }
    
    public void hit(Card addIn)
    {
        playerHand.hand.Add(addIn);
    }

    public int Score()
    {
        return playerHand.score();
    }
}

public class Hand
{
    public List<Card> hand;

    public Hand()
    {
        hand = new List<Card>();
    }

    public int score()
    {
        Queue<Card> aceTemp = new Queue<Card>();
        int total = 0;
        for(int n = 0; n < hand.Count; n++)
        {
            int temp = (int)hand[n].value;
            if(temp > 10)
            {
                temp = 10;
            }
            if(temp == 1)
            {
                temp = 0;
                aceTemp.Enqueue(hand[n]);
            }
            total += temp;
        } 
        while(aceTemp.Count != 0)
        {
            Card temp = aceTemp.Dequeue();
            if(total + 11 > 21)
            {
                total += 1;
            }
            else
            {
                total += 11;
            }
        }
        return total;
    }
}

public class Deck
{
    public List<Card> CardDeck;

    public Deck()
    {
        CardDeck = new List<Card>();
        for (int value = 1; value < 14; value += 1)
        {
            for (int suit = 1; suit < 4; suit += 1)
            {
                CardDeck.Add(new Card((CardValue)value, (CardSuit)suit));
            }
        }
    }

    public void Shuffle()
    {
        for (int n = CardDeck.Count - 1; n >= 0; n--)
        {
            Card temp = CardDeck[n];
            int shuff = (int)Random.Range(0, n);
            CardDeck[n] = CardDeck[shuff];
            CardDeck[shuff] = temp;
        }
    }

}

public struct Card
{
    public CardValue value;
    public CardSuit suit;
    //readonly bool isPlayed;

    public Card(CardValue value, CardSuit suit)
    {
        this.value = value;
        this.suit = suit;
        //this.isPlayed = false;
    }
}

public enum CardSuit
{
    Hearts = 2,
    Spades = 1,
    Diamonds = 3,
    Clubs = 0
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