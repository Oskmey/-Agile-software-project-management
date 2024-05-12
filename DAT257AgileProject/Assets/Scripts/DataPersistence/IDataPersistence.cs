public interface IDataPersistence<T>
{
    void LoadData(T gameData);
    void SaveData(T gameData);
}
