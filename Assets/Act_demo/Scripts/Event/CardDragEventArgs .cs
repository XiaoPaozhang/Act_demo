using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Act_demo
{
  /// <summary>
  /// 卡牌被使用事件。
  /// </summary>
  public class CardDragEventArgs : GameEventArgs
  {
    /// <summary>
    /// 卡牌被使用事件编号。
    /// </summary>
    public static readonly int EventId = typeof(CardDragEventArgs).GetHashCode();

    /// <summary>
    /// 初始化卡牌被使用事件的新实例。
    /// </summary>
    public CardDragEventArgs()
    {
      DraggedCard = null;
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
    /// 获取拖拽的卡牌。
    /// </summary>
    public Card DraggedCard
    {
      get;
      private set;
    }

    /// <summary>
    /// 创建卡牌被使用事件。
    /// </summary>
    /// <param name="DraggedCard">使用的卡牌数据。</param>
    /// <returns>创建的卡牌被使用事件。</returns>
    // public static CardDragEventArgs  Create()
    public static CardDragEventArgs Create(Card draggedCard)
    {
      CardDragEventArgs e = ReferencePool.Acquire<CardDragEventArgs>();

      e.DraggedCard = draggedCard;
      return e;
    }

    /// <summary>
    /// 清理卡牌被使用事件。
    /// </summary>
    public override void Clear()
    {
      DraggedCard = null;
    }
  }
}