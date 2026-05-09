namespace Core
{
    public class LibraryController
    {
        private LibraryConfiguration _config;

        public LibraryController(string name, int maxBooks, bool openOnWeekends)
        {
            _config = new LibraryConfiguration(name, maxBooks, openOnWeekends);
        }

        public void ShowConfig()
        {
            System.Console.WriteLine("[Composition] " + _config.ToString());
        }
    }
}