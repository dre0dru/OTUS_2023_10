namespace SaveSystem
{
    public interface IRepository
    {
        bool TryGetData<TData>(out TData data);
        void SetData<TData>(TData data);
    }
}
