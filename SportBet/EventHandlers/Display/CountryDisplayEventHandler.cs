using System;

namespace SportBet.EventHandlers.Display
{
    public delegate void CountryDisplayEventHandler(object sender, CountryDisplayEventArgs e);

    public class CountryDisplayEventArgs : EventArgs
    {
        public string CountryName { get; private set; }

        public CountryDisplayEventArgs(string countryName)
        {
            this.CountryName = countryName;
        }
    }
}
