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
    class BookmakerFacade : FacadeBase<BookmakerDisplayModel>
    {
        public BookmakerFacade(ServiceFactory factory)
            : base(factory) { }

        public override IEnumerable<BookmakerDisplayModel> GetAll()
        {
            IEnumerable<BookmakerDisplayModel> bookmakerModels = null;

            using (IBookmakerService service = factory.CreateBookmakerService())
            {
                DataServiceMessage<IEnumerable<BookmakerDisplayDTO>> serviceMessage = service.GetAll();

                RaiseReveivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
                if (serviceMessage.IsSuccessful)
                {
                    IEnumerable<BookmakerDisplayDTO> bookmakerDTOs = serviceMessage.Data;
                    bookmakerModels = bookmakerDTOs.Select(bookmakerDTO => Mapper.Map<BookmakerDisplayDTO, BookmakerDisplayModel>(bookmakerDTO));
                }
                else
                {
                    bookmakerModels = new List<BookmakerDisplayModel>();
                }
            }

            return bookmakerModels;
        }
    }
}
