namespace eWolfBootstrap.Interfaces
{
    public interface IPageBuilder
    {
        void Append(string text);

        string GetString();

        void Output();
    }
}