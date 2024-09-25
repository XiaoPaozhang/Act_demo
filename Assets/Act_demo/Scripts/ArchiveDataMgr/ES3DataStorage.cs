
using System;
using ES3Internal;

namespace Act_demo
{
  // 实现泛型接口的ES3数据存储类
  public class ES3DataStorage<T> : IDataStorage<T> where T : class
  {
    private string _folderPath;

    public ES3DataStorage(string folderPath)
    {
      _folderPath = folderPath;
    }

    public void Save(string saveID, T data)
    {
      // 保存数据到文件
      ES3.Save(saveID, data, $"{_folderPath}/{saveID}.es3");
    }

    public T Load(string saveID)
    {
      // 检查文件是否存在
      if (ES3.FileExists($"{_folderPath}/{saveID}.es3"))
      {
        // 加载数据
        return ES3.Load<T>(saveID, $"{_folderPath}/{saveID}.es3");
      }
      else
      {
        throw new Exception("存档文件不存在。");
      }
    }

    public void Delete(string saveID)
    {
      // 删除文件
      ES3.DeleteFile($"{_folderPath}/{saveID}.es3");
    }
  }
}