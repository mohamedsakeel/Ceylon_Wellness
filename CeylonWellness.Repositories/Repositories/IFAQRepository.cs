namespace CeylonWellness.Repositories.Repositories
{
    public interface IFAQRepository
    {
        bool ContainsRelevantKeywords(string question);
        Task<string> QueryFAQ(string question);
    }
}