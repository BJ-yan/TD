namespace Core.Interfaces
{
    public interface IStatefulSlot
    {
        bool NextState();
        void ResetState();
    }
}