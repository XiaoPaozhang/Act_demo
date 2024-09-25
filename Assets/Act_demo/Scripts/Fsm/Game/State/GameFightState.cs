using GameFramework.Event;
using GameFramework.Fsm;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Act_demo
{
  /// <summary>
  /// 游戏战斗
  /// </summary>
  public class GameFightState : FsmState<GameTurnManager>
  {
    private IFsm<GameTurnManager> _fsm;
    public PlayerInfoData firstAttacker;//首先攻击方
    private UIComponent uIComponent;
    private EventComponent eventComponent;
    protected override void OnEnter(IFsm<GameTurnManager> fsm)
    {
      base.OnEnter(fsm);
      _fsm = fsm;
      uIComponent = GameEntry.UI;
      eventComponent = GameEntry.Event;
      // 订阅ui开启
      eventComponent.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

      uIComponent.OpenUIForm<EnemyInfoForm>(fsm.Owner.playerInfo2);

      //随机选择一方为首先攻击方
      bool randomBool = Random.Range(0.0f, 1.0f) > 0.5f; // [0, 1)
      if (randomBool)
      {
        firstAttacker = fsm.Owner.playerInfo1;
      }
      else
      {
        firstAttacker = fsm.Owner.playerInfo2;
      }


      if (firstAttacker.Minions.Count > 0)
      {
        // firstAttacker.minions[0]
      }
    }

    private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
    {
      OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
      UIFormLogic uIFormLogic = ne.UIForm.Logic;

      // 打开 UI 面板成功后，把 UI 的 form 赋值给 owner
      _fsm.Owner.enemyInfoForm = (EnemyInfoForm)uIFormLogic;

      // 两个 UI 面板都打开成功后，切换到下一个状态
      if (_fsm.Owner.enemyInfoForm != null) { }

      // 切换到下一个状态
      // ChangeState<GameFightState>(_fsm);
    }


    protected override void OnLeave(IFsm<GameTurnManager> fsm, bool isShutdown)
    {
      base.OnLeave(fsm, isShutdown);
      eventComponent.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
    }
  }
}
