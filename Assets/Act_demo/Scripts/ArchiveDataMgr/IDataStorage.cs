
// 定义数据存储泛型接口
public interface IDataStorage<T> where T : class
{
  void Save(string saveID, T data);
  T Load(string saveID);
  void Delete(string saveID);
}