namespace MauiBlazor.Shared.Utils
{
    public interface IFileUtils
    {
        public Task<string> SaveFileAsync(string filePath, CancellationToken cancellationToken, string defaultFileName = "test.txt");
    }
}
