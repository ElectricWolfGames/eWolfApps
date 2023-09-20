namespace eWolfBootstrap.Interfaces
{
    public interface IPageBuilder
    {
        void Text(string text);

        string GetString();

        void Output();
    }
}