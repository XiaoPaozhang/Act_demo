
using GameFramework.Event;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;

namespace Act_demo
{
  /// <summary>
  /// 游戏初始化
  /// </summary>
  public class GameInitState : FsmState<GameTurnManager>
  {
    private IFsm<GameTurnManager> _fsm;
    private UIComponent uIComponent;
    private EventComponent eventComponent;
    private GameTurnManager owner;//状态机拥有者

    protected override void OnEnter(IFsm<GameTurnManager> fsm)
    {
      // 进入本状态时调用
      base.OnEnter(fsm);
      _fsm = fsm;

      uIComponent = GameEntry.UI;
      eventComponent = GameEntry.Event;

      // 订阅ui开启
      eventComponent.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

      fsm.Owner.turnCount = 0;
      //初始化玩家数据
      fsm.Owner.playerInfo1 = new PlayerInfoData(30001);
      fsm.Owner.playerInfo2 = new PlayerInfoData(30002);

      // 随从池初始化
      _fsm.Owner.minionPool = new MinionPool(_fsm.Owner.playerInfo1.Star.Value);
      _fsm.Owner.minionPool.Shuffle();

      // 商店初始化
      _fsm.Owner.shopData = new ShopData(_fsm.Owner.playerInfo1.Star.Value);

      //打开ui面板并传入数据
      uIComponent.OpenUIForm<PlayerInfoForm>(fsm.Owner.playerInfo1, "Front");

    }

    private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
    {
      OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
      UIFormLogic uIFormLogic = ne.UIForm.Logic;

      // 打开 UI 面板成功后，把 UI 的 form 赋值给 owner
      _fsm.Owner.playerInfoForm = (PlayerInfoForm)uIFormLogic;

      // 两个 UI 面板都打开成功后，切换到下一个状态
      if (_fsm.Owner.playerInfoForm != null)
        // 切换到下一个状态
        ChangeState<GameRecruitMinionState>(_fsm);
    }

    protected override void OnLeave(IFsm<GameTurnManager> fsm, bool isShutdown)
    {
      base.OnLeave(fsm, isShutdown);
      eventComponent.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
    }
  }
}
