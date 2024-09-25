using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace Act_demo
{
  /// <summary>
  /// 卡牌被使用事件。
  /// </summary>
  public class CardUsedEventArgs : GameEventArgs
  {
    /// <summary>
    /// 卡牌被使用事件编号。
    /// </summary>
    public static readonly int EventId = typeof(CardUsedEventArgs).GetHashCode();

    /// <summary>
    /// 初始化卡牌被使用事件的新实例。
    /// </summary>
    public CardUsedEventArgs()
    {
      UsedCard = null;
    }

    /// <summary>
    /// 获取卡牌被使用事件编号。
    /// </summary>
    public override int Id
    {
      get
      {
        return EventId;
      }
    }

    /// <summary>
    /// 获取使用的卡牌。
    /// </summary>
    public Card UsedCard
    {
      get;
      private set;
    }

    /// <summary>
    /// 是否使用卡牌成功
    /// </summary>
    /// <value></value>
    public bool IsUsedSuccess
    {
      get;
      private set;
    }

    /// <summary>
    /// 卡牌索引
    /// /// </summary>
    /// <value></value>
    public int CardIndex
    {
      get;
      private set;
    }

    /// <summary>
    /// 创建卡牌被使用事件。
    /// </summary>
    /// <param name="usedCard">使用的卡牌数据。</param>
    /// <returns>创建的卡牌被使用事件。</returns>
    // public static CardUsedEventArgs Create()
    public static CardUsedEventArgs Create(Card usedCard, bool isUsedSuccess, int cardIndex)
    {
      CardUsedEventArgs e = ReferencePool.Acquire<CardUsedEventArgs>();
      e.UsedCard = usedCard;
      e.IsUsedSuccess = isUsedSuccess;
      e.CardIndex = cardIndex;
      return e;
    }

    /// <summary>
    /// 清理卡牌被使用事件。
    /// </summary>
    public override void Clear()
    {
      UsedCard = null;
    }
  }
}