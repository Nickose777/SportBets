using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SportBet.Contracts;
using SportBet.Models.Display;
using SportBet.Services.Contracts;
using SportBet.Services.Contracts.Services;
using SportBet.Services.DTOModels.Display;
using SportBet.Services.ResultTypes;

namespace SportBet.Facades
{
    class TournamentFacade : FacadeBase<TournamentDisplayModel>
    {
        public TournamentFacade(ServiceFactory factory)
            : base(factory) { }

        public override IEnumerable<TournamentDisplayModel> GetAll()
        {
            IEnumerable<TournamentDisplayModel> tournamentModels = null;

            using (ITournamentService service = factory.CreateTournamentService())
            {
                DataServiceMessage<IEnumerable<TournamentDisplayDTO>> serviceMessage = service.GetAll();
                RaiseReveivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);

                if (serviceMessage.IsSuccessful)
                {
                    IEnumerable<TournamentDisplayDTO> tournamentDTOs = serviceMessage.Data;
                    tournamentModels = tournamentDTOs.Select(tournamentDTO => Mapper.Map<TournamentDisplayDTO, TournamentDisplayModel>(tournamentDTO));
                }
                else
                {
                    tournamentModels = new List<TournamentDisplayModel>();
                }
            }

            return tournamentModels;
        }
    }
}
