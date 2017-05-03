using System.Windows;
using SportBet.Contracts.Controllers;
using SportBet.ControllerFactories;
using SportBet.Contracts;
using System.Windows.Controls;

namespace SportBet.AnalyticControls
{
    /// <summary>
    /// Interaction logic for AnalyticMainWindow.xaml
    /// </summary>
    public partial class AnalyticMainWindow : LogWindowBase
    {
        private readonly IAccountController accountController;
        private readonly IAnalysisController analysisController;

        private UIElement lastElement;

        public AnalyticMainWindow(ControllerFactory controllerFactory, ILogger logger)
            : base(logger)
        {
            InitializeComponent();

            accountController = controllerFactory.CreateAccountController();
            analysisController = controllerFactory.CreateAnalysisController();

            accountController.ReceivedMessage += (s, e) => UpdateLogs(e.Success, e.Message);
            analysisController.ReceivedMessage += (s, e) => UpdateLogs(e.Success, e.Message);

            UpdateLogs(true, "Welcome, analytic");
        }

        private void AnalyseIncome_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = analysisController.GetIncomeElement();
            DisplayElement(element);
        }

        private void AnalyseSportRating_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = analysisController.GetSportRatingElement();
            DisplayElement(element);
        }

        private void AnalyseBookmakers_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = analysisController.GetBookmakerAnalysisElement();
            DisplayElement(element);
        }

        private void AnalyseClients_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = analysisController.GetClientAnalysisElement();
            DisplayElement(element);
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            UIElement element = accountController.GetPasswordElement();
            DisplayElement(element);
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            Expander expander = sender as Expander;

            if (expander != expander1)
            {
                expander1.IsExpanded = false;
            }
            if (expander != expander2)
            {
                expander2.IsExpanded = false;
            }
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            RaiseSignedOutEvent();
        }

        private void ShowLogs_Click(object sender, RoutedEventArgs e)
        {
            ShowLogWindow();
        }

        private void DisplayElement(UIElement element)
        {
            if (lastElement != null)
            {
                mainGrid.Children.Remove(lastElement);
            }

            Grid.SetRow(element, 0);
            Grid.SetColumn(element, 1);

            mainGrid.Children.Add(element);
            lastElement = element;
        }
    }
}
