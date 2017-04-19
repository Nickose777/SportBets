﻿using System;
using System.Collections.Generic;
using System.Linq;
using SportBet.Core.Entities;
using SportBet.Data.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Create;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;
using SportBet.Services.DTOModels.Edit;

namespace SportBet.Services.Providers.TournamentServices
{
    public class AdminTournamentService : ITournamentService
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminTournamentService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Create(TournamentCreateDTO tournamentCreateDTO)
        {
            string message = "";
            bool success = true;

            string name = tournamentCreateDTO.Name;
            string sportName = tournamentCreateDTO.SportName;
            DateTime dateOfStart = tournamentCreateDTO.DateOfStart;
            
            if (success = ValidateDate(dateOfStart, ref message))
            {
                try
                {
                    SportEntity sportEntity = unitOfWork.Sports.Get(sportName);
                    if (sportEntity != null)
                    {
                        bool exists = unitOfWork.Tournaments.Exists(name, sportEntity.Id, dateOfStart);
                        if (!exists)
                        {
                            TournamentEntity tournamentEntity = new TournamentEntity
                            {
                                Name = name,
                                DateOfStart = dateOfStart,
                                SportId = sportEntity.Id
                            };

                            unitOfWork.Tournaments.Add(tournamentEntity);
                            unitOfWork.Commit();

                            message = "Created new tournament";
                        }
                        else
                        {
                            message = "Such tournament already exists";
                            success = false;
                        }
                    }
                    else
                    {
                        message = "No such sport found";
                        success = false;
                    }
                }
                catch (Exception ex)
                {
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                    success = false;
                }
            }

            return new ServiceMessage(message, success);
        }

        public ServiceMessage Update(TournamentEditDTO tournamentEditDTO)
        {
            string message = "";
            bool success = true;

            string oldName = tournamentEditDTO.OldName;
            string oldSportName = tournamentEditDTO.OldSportName;
            DateTime oldDateOfStart = tournamentEditDTO.OldDateOfStart;

            string newName = tournamentEditDTO.NewName;
            string newSportName = tournamentEditDTO.NewSportName;
            DateTime newDateOfStart = tournamentEditDTO.NewDateOfStart;

            if (success = ValidateDate(newDateOfStart, ref message))
            {
                try
                {
                    TournamentEntity tournamentEntity = unitOfWork.Tournaments.Get(oldName, oldSportName, oldDateOfStart);
                    if (tournamentEntity != null)
                    {
                        SportEntity sportEntity = unitOfWork.Sports.Get(newSportName);
                        if (sportEntity != null)
                        {
                            tournamentEntity.Name = newName;
                            tournamentEntity.SportId = sportEntity.Id;
                            tournamentEntity.DateOfStart = newDateOfStart;

                            unitOfWork.Commit();

                            message = "Edited tournament";
                        }
                        else
                        {
                            message = "Such sport was not found";
                            success = false;
                        }
                    }
                    else
                    {
                        message = "Such tournament was not found";
                        success = false;
                    }
                }
                catch (Exception ex)
                {
                    message = ExceptionMessageBuilder.BuildMessage(ex);
                    success = false;
                }
            }

            return new ServiceMessage(message, success);
        }

        public DataServiceMessage<IEnumerable<TournamentDisplayDTO>> GetAll()
        {
            string message = "";
            bool success = true;
            IEnumerable<TournamentDisplayDTO> tournamentDisplayDTOs = null;

            try
            {
                IEnumerable<TournamentEntity> tournamentEntities = unitOfWork.Tournaments.GetAll();
                tournamentDisplayDTOs = tournamentEntities
                    .Select(tournamentEntity =>
                    {
                        return new TournamentDisplayDTO
                        {
                            Name = tournamentEntity.Name,
                            SportName = tournamentEntity.Sport.Type,
                            DateOfStart = tournamentEntity.DateOfStart
                        };
                    })
                    .OrderBy(t => t.Name)
                    .ToList();

                message = "Got all tournaments";
            }
            catch (Exception ex)
            {
                message = ExceptionMessageBuilder.BuildMessage(ex);
                success = false;
            }

            return new DataServiceMessage<IEnumerable<TournamentDisplayDTO>>(tournamentDisplayDTOs, message, success);
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        private bool ValidateDate(DateTime dateTime, ref string message)
        {
            bool isValid = true;

            if (dateTime < DateTime.Now)
            {
                message = "Invalid date: cannot start tournament in the past";
                isValid = false;
            }

            return isValid;
        }
    }
}
