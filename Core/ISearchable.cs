namespace Core
{
    public interface ISearchable
    {
        bool ContainsKeyword(string keyword);
        string GetSummary();
    }
}