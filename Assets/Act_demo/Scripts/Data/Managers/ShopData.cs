using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Act_demo
{
  public class ShopData
  {
    public ShopState ShopState { get; private set; }

    public List<MinionData> Minions { get; private set; } // 商店中所有可购买的随从
    public ShopData(int star)
    {
      ShopState = new ShopState(star);
      Minions = new List<MinionData>();
    }

    /// <summary>
    /// 计算添加随从数量
    /// </summary>
    /// <param name="turnCount"></param>
    public int calculateAddMinionCount(int turnCount)
    {
      // 计算抽取数量,抽取数量为初始回合为3, 每个第四回合+1
      return 3 + turnCount / 4;
    }
    /// <summary>
    /// 刷新随从商店
    /// </summary>
    /// <param name="turnCount">回合数</param>
    /// <returns>添加的随从商品数据</returns>
    public MinionData[] RefreshMinionShop(int turnCount)
    {
      // 如果商店中有随从, 则将其回收到随从池中
      if (Minions.Count > 0)
      {
        foreach (var minion in Minions)
        {
          GameTurnManager.Instance.minionPool.AddMinionToPool(minion.Id);
        }
        // 清空商店
        Minions.Clear();
        //洗牌
        GameTurnManager.Instance.minionPool.Shuffle();
      }

      // 计算抽取数量
      int _drawMinionsCount = calculateAddMinionCount(turnCount);
      // 抽取随从
      int[] minionIds = GameTurnManager.Instance.minionPool.Draw(_drawMinionsCount);
      // 添加到商店中
      MinionData[] minionDatas = new MinionData[minionIds.Length];
      for (int i = 0; i < minionIds.Length; i++)
      {
        MinionData minionData = new MinionData(minionIds[i]);
        Minions.Add(minionData);
        minionDatas[i] = minionData;
      }
      return minionDatas;
    }

    /// <summary>
    /// 刷新随从商品列表()
    /// </summary>
    public void RefreshMinionList()
    {

    }
  }
}
