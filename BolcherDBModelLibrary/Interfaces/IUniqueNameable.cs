namespace BolcherDBModelLibrary.Interfaces
{
    public interface IUniqueNameable
    {
        bool HasUniqueName(int id, string name);
    }
}
