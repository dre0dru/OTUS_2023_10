namespace SaveSystem
{
    public interface IRepository<TData>
    {
        bool TryGetData(out TData data);
        void SetData(TData data);
    }
}
