using System;
using SportBet.Models.Edit;

namespace SportBet.EventHandlers.Edit
{
    public delegate void CountryEditEventHandler(object sender, CountryEditEventArgs e);

    public class CountryEditEventArgs : EventArgs
    {
        public CountryEditModel Country { get; private set; }

        public CountryEditEventArgs(CountryEditModel country)
        {
            this.Country = country;
        }
    }
}
