namespace SaveSystem
{
    public class ES3Repository : IRepository
    {
        public bool TryGetData<TData>(out TData data)
        {
            var key = GetKey<TData>();

            if (ES3.KeyExists(key))
            {
                 data = ES3.Load<TData>(key);
                 return true;
            }

            data = default;
            return false;
        }

        public void SetData<TData>(TData data)
        {
            var key = GetKey<TData>();

            ES3.Save(key, data);
        }

        private static string GetKey<TData>()
        {
            var key = typeof(TData).FullName;
            return key;
        }
    }
}
