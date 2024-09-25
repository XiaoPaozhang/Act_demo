using System;

namespace Act_demo
{
  // 存档数据管理器类
  public class ArchiveDataMgr<T> where T : class
  {
    private readonly IDataStorage<T> _dataStorage;

    public ArchiveDataMgr(IDataStorage<T> dataStorage)
    {
      _dataStorage = dataStorage;
    }

    public T CreateSave(T data, string saveID)
    {
      _dataStorage.Save(saveID, data);
      return data;
    }

    public T LoadSave(string saveID)
    {
      try
      {
        return _dataStorage.Load(saveID);
      }
      catch (Exception ex)
      {
        // 处理加载存档时的异常
        Console.WriteLine($"加载存档时出错: {ex.Message}");
        return null;
      }
    }

    public void UpdateSave(T data, string saveID)
    {
      _dataStorage.Save(saveID, data);
    }

    public void DeleteSave(string saveID)
    {
      _dataStorage.Delete(saveID);
    }
  }
}