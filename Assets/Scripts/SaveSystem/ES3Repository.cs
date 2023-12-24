namespace SaveSystem
{
    public class ES3Repository<TData> : IRepository<TData>
    {
        private string Key => typeof(TData).FullName;

        public bool TryGetData(out TData data)
        {
            if (ES3.KeyExists(Key))
            {
                 data = ES3.Load<TData>(Key);
                 return true;
            }

            data = default;
            return false;
        }

        public void SetData(TData data)
        {
            ES3.Save(Key, data);
        }
    }
}
