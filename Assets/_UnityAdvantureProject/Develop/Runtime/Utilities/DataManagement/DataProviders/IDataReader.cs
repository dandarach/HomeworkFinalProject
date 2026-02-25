namespace Assets._UnityAdvantureProject.Develop.Runtime.Utilities.DataManagement.DataProviders
{
    public interface IDataReader<TData> where TData : ISaveData
    {
        void ReadFrom(TData data);
    }
}
