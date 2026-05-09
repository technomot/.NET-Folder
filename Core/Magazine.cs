using System;

namespace Core
{
    public class Magazine : LibraryItem, ISearchable
    {
        public int IssueNumber { get; set; }
        public string Category { get; set; }

        public Magazine(string title, string author, int issueNumber, string category, DateTime publishedDate, bool isAvailable)
            : base(title, author, publishedDate, isAvailable)
        {
            IssueNumber = issueNumber;
            Category = category;
        }

        public override string GetItemType()
        {
            return "Magazine";
        }

        public override string GetInfo()
        {
            return $"[Magazine] {Title} | Issue: {IssueNumber} | Category: {Category} | Available: {IsAvailable}";
        }

        public bool ContainsKeyword(string keyword)
        {
            return Title.Contains(keyword) || Category.Contains(keyword);
        }

        public string GetSummary()
        {
            return $"{Title} — Issue #{IssueNumber}, Category: {Category}";
        }
    }
}