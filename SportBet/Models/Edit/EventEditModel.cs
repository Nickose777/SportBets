using System;
using System.Collections.Generic;
using SportBet.Models.Base;

namespace SportBet.Models.Edit
{
    public class EventEditModel : EventBaseModel
    {
        public string OldNotes { get; set; }

        public string NewNotes { get; set; }

        public DateTime NewDateOfEvent { get; set; }

        public List<ParticipantBaseModel> NewParticipants { get; set; }
    }
}
