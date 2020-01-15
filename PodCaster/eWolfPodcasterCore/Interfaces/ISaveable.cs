namespace eWolfPodcasterCore.Interfaces
{
    public interface ISaveable
    {
        string GetFileName { get; }

        bool Modifyed { get; set; }
    }
}