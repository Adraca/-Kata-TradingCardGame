namespace TradingCardGame.Domain
{
    public class Mana
    {
        public int Slots { get; private set; }
        public int Total { get; private set; }

        public Mana()
        {
            Slots = Rules.DefaultMana;
            Total = Rules.DefaultMana;
        }

        public void Use(int mana)
        {
            if (mana > Slots)
            {
                throw new NotEnoughManaException();
            }

            Slots -= mana;
        }

        public void Gain(int mana)
        {
            if (mana + Total > Rules.MaximumMana)
            {
                throw new TooMuchManaException();
            }

            Slots += mana;
            Total += mana;
        }

        public void TemporalGain(int mana)
        {
            Slots += mana;
        }

        public void GainTotal(int mana)
        {
            if (mana + Total > Rules.MaximumMana)
            {
                throw new TooMuchManaException();
            }

            Total += mana;
        }

        public void Refill()
        {
            Slots = Total;
        }
    }
}
