public interface IDataPersistence<T>
{
    void LoadData(T data);
    void SaveData(T data);
}
