﻿using System;
using System.Windows;
using SportBet.CommonControls.Sports.UserControls;
using SportBet.CommonControls.Sports.ViewModels;
using SportBet.Contracts;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.ResultTypes;
using SportBet.WindowFactories;
using SportBet.Models.Edit;
using SportBet.Services.DTOModels.Edit;
using AutoMapper;
using SportBet.Contracts.Controllers;
using SportBet.Models.Create;
using SportBet.Services.DTOModels.Create;

namespace SportBet.Controllers
{
    class SportController : SubjectBase, ISubject, ISportController
    {
        private readonly FacadeBase<string> facade;

        public SportController(ServiceFactory factory, FacadeBase<string> facade)
            : base(factory)
        {
            this.facade = facade;
            facade.ReceivedMessage += RaiseReceivedMessageEvent;
        }

        public void Add()
        {
            UIElement element = GetAddElement();
            Window window = WindowFactory.CreateByContentsSize(element);

            window.Show();
        }

        public void Display()
        {
            UIElement element = GetDisplayElement();
            Window window = WindowFactory.CreateByContentsSize(element);

            window.Show();
        }

        public UIElement GetAddElement()
        {
            SportCreateViewModel viewModel = new SportCreateViewModel();
            SportCreateControl control = new SportCreateControl(viewModel);

            viewModel.SportCreated += (s, e) =>
            {
                SportCreateModel sportCreateModel = e.Sport;
                SportCreateDTO sportCreateDTO = Mapper.Map<SportCreateModel, SportCreateDTO>(sportCreateModel);

                using (ISportService service = factory.CreateSportService())
                {
                    ServiceMessage serviceMessage = service.Create(sportCreateDTO);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        viewModel.SportName = String.Empty;
                        Notify();
                    }
                }
            };

            return control;
        }

        public UIElement GetDisplayElement()
        {
            SportListViewModel viewModel = new SportListViewModel(this, facade);
            SportListControl control = new SportListControl(viewModel);

            viewModel.SportSelected += (s, e) => EditSport(e.SportName);
            viewModel.SportDeleteRequest += (s, e) => Delete(e.SportName);

            return control;
        }

        private void EditSport(string sportName)
        {
            SportEditViewModel viewModel = new SportEditViewModel(sportName);
            SportEditControl control = new SportEditControl(viewModel);
            Window window = WindowFactory.CreateByContentsSize(control);

            viewModel.SportEdited += (s, e) =>
            {
                SportEditModel sportEditModel = e.Sport;
                SportEditDTO sportEditDTO = Mapper.Map<SportEditModel, SportEditDTO>(sportEditModel);

                using (ISportService service = factory.CreateSportService())
                {
                    ServiceMessage serviceMessage = service.Update(sportEditDTO);
                    RaiseReceivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                    if (serviceMessage.IsSuccessful)
                    {
                        window.Close();
                        Notify();
                    }
                }
            };

            window.Show();
        }

        private void Delete(string sportName)
        {
            using (ISportService service = factory.CreateSportService())
            {
                ServiceMessage serviceMessage = service.Delete(sportName);
                RaiseReceivedMessageEvent(serviceMessage);

                if (serviceMessage.IsSuccessful)
                {
                    Notify();
                }
            }
        }
    }
}
