using System.Collections.Generic;
using System.Windows;
using AutoMapper;
using SportBet.Contracts.Controllers;
using SportBet.Contracts.Observers;
using SportBet.Contracts.Subjects;
using SportBet.Controllers;
using SportBet.EventHandlers;
using SportBet.Models.Display;
using SportBet.Services.Contracts.Factories;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using SportBet.SuperuserControls.UserControls;
using SportBet.SuperuserControls.ViewModels;
using SportBet.WindowFactories;

namespace SportBet.Subjects
{
    class ClientDisplayManager : IClientSubject
    {
        public event ServiceMessageEventHandler ReceivedMessage;

        private readonly ServiceFactory factory;
        private List<IClientObserver> observers;

        public ClientDisplayManager(ServiceFactory factory)
        {
            this.factory = factory;
            observers = new List<IClientObserver>();
        }

        public void DisplayClients()
        {
            IClientController controller = new ClientController(factory);
            controller.ReceivedMessage += (s, e) => RaiseReceivedMessageEvent(s, e);

            ManageClientsViewModel viewModel = new ManageClientsViewModel(this, controller);
            ManageClientsControl control = new ManageClientsControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.ClientDeleteRequest += (s, e) =>
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this user?", "Confirm operation", MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    using (IClientService service = factory.CreateClientService())
                    {
                        ClientDisplayDTO deletedClient = Mapper.Map<ClientDisplayModel, ClientDisplayDTO>(e.Client);
                        ServiceMessage serviceMessage = service.Delete(deletedClient.Login);

                        RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
                    }

                    Notify();
                }
            };

            window.ShowDialog();
        }

        public void Subscribe(IClientObserver observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(IClientObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (IClientObserver observer in observers)
            {
                observer.Update();
            }
        }

        private void RaiseReceivedMessageEvent(bool succes, string message)
        {
            var handler = ReceivedMessage;
            if (handler != null)
            {
                ServiceMessageEventArgs e = new ServiceMessageEventArgs(succes, message);
                handler(this, e);
            }
        }
        private void RaiseReceivedMessageEvent(object sender, ServiceMessageEventArgs e)
        {
            var handler = ReceivedMessage;
            if (handler != null)
            {
                handler(sender, e);
            }
        }
    }
}
