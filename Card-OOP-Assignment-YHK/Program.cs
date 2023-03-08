using System;
using System.Collections.Generic;
using System.Linq;

public class Card
{
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    public enum Rank
    {
        Ace = 1,
        Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
        Jack, Queen, King
    }

    public Suit SuitType { get; private set; }
    public Rank RankValue { get; private set; }

    public Card(Suit suitType, Rank rankValue)
    {
        SuitType = suitType;
        RankValue = rankValue;
    }

    public override string ToString()
    {
        return $"{RankValue} of {SuitType}";
    }
}

public class Deck
{
    private List<Card> cards;

    public Deck()
    {
        cards = new List<Card>();
        foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
        {
            foreach (Card.Rank rank in Enum.GetValues(typeof(Card.Rank)))
            {
                cards.Add(new Card(suit, rank));
            }
        }
    }

    public void Shuffle()
    {
        Random random = new Random();
        cards = cards.OrderBy(card => random.Next()).ToList();
    }

    public Card DrawCard()
    {
        if (cards.Count == 0)
        {
            throw new InvalidOperationException("No cards left in the deck.");
        }
        Card card = cards[0];
        cards.RemoveAt(0);
        return card;
    }

    public List<Card> DrawCards(int numCards)
    {
        List<Card> drawnCards = new List<Card>();
        for (int i = 0; i < numCards; i++)
        {
            drawnCards.Add(DrawCard());
        }
        return drawnCards;
    }
}

public class CardGame
{
    private Deck deck;

    public CardGame()
    {
        deck = new Deck();
    }

    public void PlayGame()
    {
        Console.WriteLine("Welcome to the card shuffler!");
        Console.WriteLine("Please select the type of shuffle you would like to perform:");
        Console.WriteLine("1 - Fischer-Yates Shuffle");
        Console.WriteLine("2 - Riffle Shuffle");
        Console.WriteLine("Press any other key to exit");
        string input = Console.ReadLine();
        int shuffleType;
        if (int.TryParse(input, out shuffleType))
        {
            switch (shuffleType)
            {
                case 1:
                    deck.Shuffle();
                    Console.WriteLine("Random shuffle complete.");
                    break;
                case 2:
                    deck = new Deck();
                    Console.WriteLine("Deck reset to unshuffled state.");
                    break;
                default:
                    Console.WriteLine("Invalid selection. Exiting program.");
                    return;
            }

            Console.WriteLine($"Deck has {deck.DrawCards(0).Count} cards.");

            Console.WriteLine("Drawing a card...");
            Card drawnCard = deck.DrawCard();
            Console.WriteLine($"You drew {drawnCard}.");

            Console.WriteLine("Drawing 5 cards...");
            List<Card> drawnCards = deck.DrawCards(5);
            Console.WriteLine("You drew:");
            foreach (Card card in drawnCards)
            {
                Console.WriteLine(card);
            }
        }
        else
        {
            Console.WriteLine("Invalid selection. Exiting program.");
        }
    }
}

public class Program
{
    static void Main(string[] args)
    {
        CardGame game = new CardGame();
        game.PlayGame();
    }
}
