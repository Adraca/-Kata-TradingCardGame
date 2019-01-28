using System;
using System.Collections.Generic;
using System.Linq;

namespace TradingCardGame.Domain
{
    public class Match
    {
        public List<Player> Players { get; set; }
        public Player ActivePlayer { get; set; }
        public bool IsOver { get; set; }

        public Match(Player one, Player two)
        {
            Players = new List<Player>(2)
            {
                one,
                two
            };

            int selectedPlayer = new Random().Next(Players.Count);
            ActivePlayer = Players[selectedPlayer];

            Players.ForEach(p => p.StartingDraw());
            Players.ForEach(p =>
            {
                if (p != ActivePlayer)
                {
                    p.DrawCard();
                }
            });
        }

        public void EndTurn()
        {
            ActivePlayer = Players.First(p => p != ActivePlayer);
            if (ActivePlayer.Mana.Total + Rules.ManaGainPerTurn < 10)
            {
                ActivePlayer.Mana.GainTotal(Rules.ManaGainPerTurn);
            }

            ActivePlayer.Mana.Refill();

            ActivePlayer.DrawCard();
        }

        public void PlayCard(int cardIndex)
        {
            var target = Players.First(p => p != ActivePlayer);
            var playedCard = ActivePlayer.Hand[cardIndex];
            ActivePlayer.Mana.Use(playedCard);
            target.Health -= playedCard;

            ActivePlayer.Hand.RemoveAt(cardIndex);
            CheckIsOver();
        }

        private void CheckIsOver()
        {
            if (Players.Any(p => p.Health <= 0))
            {
                IsOver = true;
            }
        }
    }
}
