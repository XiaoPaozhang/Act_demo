using System.Collections.Generic;
using cfg;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Act_demo
{
  public class MinionPool
  {
    private readonly List<int> _minionIds; // 存储随从ID的列表
    private readonly List<cfg.Minion> _minionConfigurations;// 存储随从配置的列表
    private int _duplicateCount = 3; //随从重复添加次数

    public MinionPool()
    {
      _minionIds = new List<int>();

      // 读取随从数据表
      Tables tables = GameEntry.LubanComponent.GetTables<Tables>();
      _minionConfigurations = tables.TbMinion.DataList ?? new List<cfg.Minion>();

      if (_minionConfigurations.Count == 0)
      {
        Log.Error("配置表查询失败: TbMinion");
      }
    }

    public MinionPool(int maxStar) : this()
    {
      PopulatePool(maxStar);
    }

    /// <summary>
    /// 根据最高星级筛选卡牌，塞进牌库
    /// </summary>
    /// <param name="maxStar">最高星级</param>
    public void PopulatePool(int maxStar)
    {
      foreach (var minionConfig in _minionConfigurations)
      {
        if (minionConfig.Star > maxStar) continue;

        AddMinionToPool(minionConfig.Id, _duplicateCount);
      }
    }

    /// <summary>
    /// 向牌库中添加指定数量的随从ID
    /// </summary>
    /// <param name="minionId">随从ID</param>
    /// <param name="count">要添加的数量</param>
    public void AddMinionToPool(int minionId, int count = 1)
    {
      for (int i = 0; i < count; i++)
      {
        _minionIds.Add(minionId);
      }
    }

    /// <summary>
    /// 洗牌
    /// </summary>
    public void Shuffle()
    {
      int n = _minionIds.Count;
      while (n > 1)
      {
        n--;
        int k = Random.Range(0, n + 1);
        int value = _minionIds[k];
        _minionIds[k] = _minionIds[n];
        _minionIds[n] = value;
      }
    }

    /// <summary>
    /// 抽取随从ID
    /// </summary>
    /// <param name="count">要抽取的数量</param>
    /// <returns>抽取的随从ID数组</returns>
    public int[] Draw(int count)
    {
      int[] drawnMinionIds = new int[count];

      for (int i = 0; i < count; i++)
      {
        if (_minionIds.Count == 0)
        {
          Log.Error("随从池已空，无法抽取");
          drawnMinionIds[i] = -1; // 可以使用一个无效的ID表示未抽到随从
          continue;
        }
        else
        {
          drawnMinionIds[i] = _minionIds[0];
          _minionIds.RemoveAt(0);
        }
      }

      return drawnMinionIds;
    }
  }
}