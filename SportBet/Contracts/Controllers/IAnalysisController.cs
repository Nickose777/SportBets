﻿using System.Windows;

namespace SportBet.Contracts.Controllers
{
    public interface IAnalysisController : IReceiveMessage
    {
        UIElement GetIncomeElement();

        UIElement GetSportRatingElement();

        UIElement GetBookmakerAnalysisElement();

        UIElement GetClientAnalysisElement();
    }
}
