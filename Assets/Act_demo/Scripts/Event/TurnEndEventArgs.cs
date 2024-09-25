using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Act_demo
{
  /// <summary>
  /// 回合结束事件。
  /// </summary>
  public class TurnEndEventArgs : GameEventArgs
  {
    /// <summary>
    /// 回合结束事件编号。
    /// </summary>
    public static readonly int EventId = typeof(TurnEndEventArgs).GetHashCode();

    /// <summary>
    /// 初始化回合结束事件的新实例。
    /// </summary>
    public TurnEndEventArgs()
    {
    }

    /// <summary>
    /// 获取回合结束事件编号。
    /// </summary>
    public override int Id
    {
      get
      {
        return EventId;
      }
    }

    /// <summary>
    /// 创建回合结束事件。
    /// </summary>
    /// <returns>创建的回合结束事件事件。</returns>
    // public static TurnEndEventArgs  Create()
    public static TurnEndEventArgs Create()
    {
      TurnEndEventArgs e = ReferencePool.Acquire<TurnEndEventArgs>();
      return e;
    }

    /// <summary>
    /// 清理回合结束事件。
    /// </summary>
    public override void Clear()
    {

    }
  }
}