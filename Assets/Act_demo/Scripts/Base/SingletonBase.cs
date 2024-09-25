using System;
using System.Reflection;
using UnityEngine;
//单例基类
public abstract class SingletonBase<T> where T : class
{
  private static T instance;

  protected static readonly object lockObj = new object();

  public static T Instance
  {
    get
    {
      if (instance == null)
      {
        lock (lockObj)
        {
          if (instance == null)
          {
            Type type = typeof(T);
            ConstructorInfo info = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic,
                                                        null,
                                                        Type.EmptyTypes,
                                                        null);
            if (info != null)
              instance = info.Invoke(null) as T;
            else
              Debug.LogError("No default constructor found for " + type.Name);
          }
        }
      }
      return instance;
    }
  }
}
