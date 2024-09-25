using System.Collections.Generic;
using cfg;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Act_demo
{
  public class DeckData
  {
    /// <summary>
    /// 牌库中的卡牌数据
    /// </summary>
    public List<CardData> CardDatas { get; private set; }
    private List<cfg.Card> cards;// 卡牌数据表
    public DeckData()
    {
      CardDatas = new List<CardData>();

      // 读表
      Tables tables = GameEntry.LubanComponent.GetTables<Tables>();
      cards = tables.TbCard.DataList;
      if (cards == null)
      {
        Log.Error("没有卡牌数据", cards);
        return;
      }
    }

    /// <summary>
    /// 根据最高星级筛选卡牌，塞进牌库
    /// </summary>
    /// <param name="star">最高星级</param>
    public void PushedCard(int maxStar)
    {
      foreach (cfg.Card card in cards)
      {
        //根据星级筛选,大于要求星级的不加入牌库
        if (card.Star > maxStar) continue;

        //每张牌塞进去3张
        for (int i = 0; i < 3; i++)
        {
          CardDatas.Add(new CardData(card.Id));
        }
      }
    }

    // 洗牌
    public void ShuffleDeck()
    {
      for (int i = CardDatas.Count - 1; i > 0; i--)
      {
        int j = Random.Range(0, i + 1); // 生成从0到i的随机数
        CardData temp = CardDatas[i];
        CardDatas[i] = CardDatas[j];
        CardDatas[j] = temp;
      }
    }

    // 抽卡
    public CardData[] DrawCard(int count)
    {
      //声明抽取的卡牌
      CardData[] resultCardDatas = new CardData[count];
      for (int i = 0; i < count; i++)
      {
        if (CardDatas.Count == 0)
        {
          Log.Error("牌库已空，无法抽卡");
          resultCardDatas[i] = null;
          continue;
        }
        else
        {
          CardData cardData = CardDatas[0];
          CardDatas.RemoveAt(0);
          resultCardDatas[i] = cardData;
        }
      }
      return resultCardDatas;
    }
  }
}
