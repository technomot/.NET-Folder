using System;
using System.IO;

namespace Core
{
    public class ResourceManager : IDisposable
    {
        private StreamWriter _writer;
        private string _filePath;
        private bool _disposed = false;

        public ResourceManager(string filePath)
        {
            _filePath = filePath;
            _writer = new StreamWriter(filePath, append: true);
            Console.WriteLine($"[ResourceManager] File opened: {filePath}");
        }

        public void Log(string message)
        {
            if (_disposed)
                throw new ObjectDisposedException("ResourceManager");

            _writer.WriteLine($"{DateTime.Now:HH:mm:ss} — {message}");
            Console.WriteLine($"[Log] {message}");
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _writer.Close();
                _disposed = true;
                Console.WriteLine($"[ResourceManager] File closed: {_filePath}");
            }
        }
    }
}