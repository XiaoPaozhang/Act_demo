using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Act_demo
{
  /// <summary>
  /// 随从拖拽结束事件。
  /// </summary>
  public class MinionDragEndEventArgs : GameEventArgs
  {
    /// <summary>
    /// 随从拖拽结束编号。
    /// </summary>
    public static readonly int EventId = typeof(MinionDragEndEventArgs).GetHashCode();

    /// <summary>
    /// 初始化随从拖拽结束的新实例。
    /// </summary>
    public MinionDragEndEventArgs()
    {
      MinionGameObject = null;
      minionData = null;
      eventData = null;
    }

    /// <summary>
    /// 获取随从拖拽结束编号。
    /// </summary>
    public override int Id
    {
      get
      {
        return EventId;
      }
    }

    /// <summary>
    /// 获取随从对象
    /// </summary>
    public GameObject MinionGameObject
    {
      get;
      private set;
    }

    /// <summary>
    /// 卡牌索引
    /// /// </summary>
    /// <value></value>
    public MinionData minionData
    {
      get;
      private set;
    }

    /// <summary>
    /// 拖拽事件数据
    /// </summary>
    public PointerEventData eventData
    {
      get;
      private set;
    }

    /// <summary>
    /// 创建随从拖拽结束。
    /// </summary>
    /// <param name="usedCard">使用的卡牌数据。</param>
    /// <returns>创建的随从拖拽结束。</returns>
    // public static MinionDragEndEventArgs Create()
    public static MinionDragEndEventArgs Create(GameObject gameObject, PointerEventData eventData, MinionData minionData)
    {
      MinionDragEndEventArgs e = ReferencePool.Acquire<MinionDragEndEventArgs>();
      e.MinionGameObject = gameObject;
      e.minionData = minionData;
      e.eventData = eventData;
      return e;
    }

    /// <summary>
    /// 清理随从拖拽结束。
    /// </summary>
    public override void Clear()
    {
      MinionGameObject = null;
      minionData = null;
      eventData = null;
    }
  }
}