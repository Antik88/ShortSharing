using Email.Service.DAL.Entities;
using Email.Service.DAL.Enums;

namespace Email.Service.DAL.Repository;

public interface ITemplateRepository
{
    public Task<TemplateEntity> FetchTemplateAsync(RentTemplateType templateType);
}
