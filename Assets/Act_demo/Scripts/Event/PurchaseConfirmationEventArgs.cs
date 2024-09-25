using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Act_demo
{
  /// <summary>
  /// 随从拖拽结束事件。
  /// </summary>
  public class PurchaseConfirmationEventArgs : GameEventArgs
  {
    /// <summary>
    /// 随从拖拽结束编号。
    /// </summary>
    public static readonly int EventId = typeof(PurchaseConfirmationEventArgs).GetHashCode();

    /// <summary>
    /// 初始化随从拖拽结束的新实例。
    /// </summary>
    public PurchaseConfirmationEventArgs()
    {
      MinionGameObject = null;
      IsSuccess = false;
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
    /// 是否购买成功
    /// </summary>
    /// <value></value>
    public bool IsSuccess
    {
      get;
      private set;
    }

    /// <summary>
    /// 创建随从拖拽结束。
    /// </summary>
    /// <param name="usedCard">使用的卡牌数据。</param>
    /// <returns>创建的随从拖拽结束。</returns>
    // public static PurchaseConfirmationEventArgs Create()
    public static PurchaseConfirmationEventArgs Create(GameObject gameObject, bool isSuccess)
    {
      PurchaseConfirmationEventArgs e = ReferencePool.Acquire<PurchaseConfirmationEventArgs>();
      e.MinionGameObject = gameObject;
      e.IsSuccess = isSuccess;
      return e;
    }

    /// <summary>
    /// 清理随从拖拽结束。
    /// </summary>
    public override void Clear()
    {
      MinionGameObject = null;
      IsSuccess = false;
    }
  }
}