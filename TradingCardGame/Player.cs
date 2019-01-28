using System;
using System.Collections.Generic;
using System.Linq;

namespace TradingCardGame.Domain
{
    public class Player
    {
        public int Health { get; set; }
        public Mana Mana { get; set; }
        public string Name { get; private set; }

        public List<int> InitialDeck { get; private set; }
        public List<int> Deck { get; private set; }
        public List<int> Hand { get; private set; }

        private int _BleedOutCounter;

        public Player(string name)
        {
            Name = name;
            Health = Rules.DefaultHealth;
            Mana = new Mana();

            InitialDeck = new List<int>(5) { 0, 1, 2, 3, 4 };
            Deck = new List<int>(InitialDeck);
            Hand = new List<int>();
        }

        public void DrawCard()
        {
            if (Deck.Any())
            {
                int randomCard = new Random().Next(Deck.Count);

                if (Hand.Count < Rules.MaximumHandSize)
                {
                    Hand.Add(Deck[randomCard]);
                }

                Deck.RemoveAt(randomCard);
            }
            else
            {
                BleedOut();
            }
        }

        public void StartingDraw()
        {
            for (int i = 0; i < Rules.DefaultHandSize; i++)
            {
                DrawCard();
            }
        }

        private void BleedOut()
        {
            _BleedOutCounter++;
            Health = Health - _BleedOutCounter;
        }
    }
}
