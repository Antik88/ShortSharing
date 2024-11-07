using Email.Service.DAL.Entities;
using Email.Service.DAL.Enums;

namespace Email.Service.BLL.Service;

public interface ITemplateService
{
    Task<TemplateEntity> FetchTemplateAsync(RentTemplateType templateType);
    Task<List<TemplateEntity>> GetAll();
}