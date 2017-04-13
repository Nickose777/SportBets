using System;

namespace SportBet.EventHandlers.Create
{
    public delegate void CountryEventHandler(object sender, CountryEventArgs e);

    public class CountryEventArgs : EventArgs
    {
        public string ContryName { get; private set; }

        public CountryEventArgs(string countryName)
        {
            this.ContryName = countryName;
        }
    }
}
