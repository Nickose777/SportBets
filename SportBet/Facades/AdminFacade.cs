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
    class AdminFacade : FacadeBase<AdminDisplayModel>
    {
        public AdminFacade(ServiceFactory factory)
            : base(factory) { }

        public override IEnumerable<AdminDisplayModel> GetAll()
        {
            IEnumerable<AdminDisplayModel> adminModels = null;

            using (IAdminService service = factory.CreateAdminService())
            {
                DataServiceMessage<IEnumerable<AdminDisplayDTO>> serviceMessage = service.GetAll();

                RaiseReveivedMessageEvent(serviceMessage.IsSuccessful, serviceMessage.Message);
                if (serviceMessage.IsSuccessful)
                {
                    IEnumerable<AdminDisplayDTO> adminDTOs = serviceMessage.Data;
                    adminModels = adminDTOs.Select(adminDTO => Mapper.Map<AdminDisplayDTO, AdminDisplayModel>(adminDTO));
                }
                else
                {
                    adminModels = new List<AdminDisplayModel>();
                }
            }

            return adminModels;
        }
    }
}
