using Email.Service.BLL.Interfaces;
using Email.Service.DAL.Repository;
using MassTransit;
using SharingMessages;

namespace Email.Service.BLL.Service;

public class RentService(IRentRepository rents) : IRentService
{
    public Task CreateRent(ConsumeContext<RentRecord> context)
    {
        return rents.CreateRent(context);
    }
}
