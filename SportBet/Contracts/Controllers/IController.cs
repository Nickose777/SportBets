﻿using SportBet.EventHandlers;

namespace SportBet.Contracts.Controllers
{
    public interface IController
    {
        event ServiceMessageEventHandler ReceivedMessage;
    }
}
