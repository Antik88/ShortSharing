using Email.Service.DAL.Entities;
using Email.Service.DAL.Enums;
using Email.Service.DAL.Repository;

namespace Email.Service.BLL.Service;

public class TemplateService(ITemplateRepository templateRepository) : ITemplateService
{
    public async Task<TemplateEntity> FetchTemplateAsync(RentTemplateType templateType)
    {
        var template = await templateRepository.FetchTemplateAsync(templateType);

        return template;
    }

    public async Task<List<TemplateEntity>> GetAll()
    {
        return await templateRepository.GetAll();
    }
}
