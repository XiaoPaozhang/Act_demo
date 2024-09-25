using GameFramework.Event;
using GameFramework.Fsm;
using Unity.VisualScripting;
using UnityGameFramework.Runtime;

namespace Act_demo
{
  public class GameRecruitMinionState : FsmState<GameTurnManager>
  {
    private IFsm<GameTurnManager> _fsm;
    private UIComponent uIComponent;
    private EventComponent eventComponent;
    private MinionData[] _minionDatas;// 随从数据

    protected override void OnEnter(IFsm<GameTurnManager> fsm)
    {
      base.OnEnter(fsm);
      _fsm = fsm;
      uIComponent = GameEntry.UI;
      eventComponent = GameEntry.Event;

      // 订阅UI开启
      eventComponent.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
      eventComponent.Subscribe(TurnEndEventArgs.EventId, OnTurnEndEventArgs);

      // 游戏运行回合数+1
      fsm.Owner.turnCount++;

      // 刷新随从商品
      _fsm.Owner.shopData.RefreshMinionShop(fsm.Owner.turnCount);

      // 打开UI面板并传入数据
      uIComponent.OpenUIForm<ShopInfoForm>(_fsm.Owner.shopData);

      fsm.Owner.deckData = new DeckData();
      fsm.Owner.deckData.PushedCard(2);
      CardData[] cards = fsm.Owner.deckData.DrawCard(7);
      fsm.Owner.playerInfoForm.DrawCardDisplay(cards);
    }

    private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
    {
      OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
      UIFormLogic uIFormLogic = ne.UIForm.Logic;

      // 打开 UI 面板成功后，把 UI 的 form 赋值给 owner
      _fsm.Owner.shopInfoForm = (ShopInfoForm)uIFormLogic;
    }

    private void OnTurnEndEventArgs(object sender, GameEventArgs e)
    {
      TurnEndEventArgs ne = (TurnEndEventArgs)e;
      ChangeState<GameFightState>(_fsm);
    }

    protected override void OnLeave(IFsm<GameTurnManager> fsm, bool isShutdown)
    {
      base.OnLeave(fsm, isShutdown);
      eventComponent.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
      eventComponent.Unsubscribe(TurnEndEventArgs.EventId, OnTurnEndEventArgs);
    }
  }
}