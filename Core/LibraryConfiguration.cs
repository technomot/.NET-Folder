namespace Core
{
    public class LibraryConfiguration
    {
        public string LibraryName { get; set; }
        public int MaxBooksPerMember { get; set; }
        public bool IsOpenOnWeekends { get; set; }

        public LibraryConfiguration(string name, int maxBooks, bool openOnWeekends)
        {
            LibraryName = name;
            MaxBooksPerMember = maxBooks;
            IsOpenOnWeekends = openOnWeekends;
        }

        public override string ToString()
        {
            return $"Library: {LibraryName} | Max books: {MaxBooksPerMember} | Open on weekends: {IsOpenOnWeekends}";
        }
    }
}