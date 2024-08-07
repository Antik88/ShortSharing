using Email.Service.DAL.Context;
using Email.Service.DAL.Entities;
using Email.Service.DAL.Enums;
using MongoDB.Driver;

namespace Email.Service.DAL.Repository;

public class TemplateRepository : ITemplateRepository
{
    private readonly IMongoCollection<TemplateEntity> _templates;

    public TemplateRepository(DbContext dbContext)
    {
        _templates = dbContext.Database.GetCollection<TemplateEntity>("templates");
    }

    public async Task<TemplateEntity> FetchTemplateAsync(RentTemplateType templateType)
    {
        var filter = Builders<TemplateEntity>.Filter.Eq(t => t.Type, templateType);

        var result = await _templates.Find(filter).FirstOrDefaultAsync();

        return result;
    }
}
