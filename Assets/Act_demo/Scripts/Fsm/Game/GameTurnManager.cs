
using GameFramework.Fsm;
using UnityEngine.Events;

namespace Act_demo
{
  /// <summary>
  /// 游戏回合流程类,管理局内的游戏回合流程,拥有fsm
  /// </summary>
  public class GameTurnManager : SingletonBase<GameTurnManager>
  {
    private IFsm<GameTurnManager> fsm;
    public PlayerInfoForm playerInfoForm;//玩家信息展示ui
    public EnemyInfoForm enemyInfoForm;//敌人信息展示ui
    public ShopInfoForm shopInfoForm;//商店信息展示ui
    public PlayerInfoData playerInfo1;//我方数据
    public PlayerInfoData playerInfo2;//敌方数据
    public ShopData shopData;//商店管理器
    public DeckData deckData;//牌库数据
    public MinionPool minionPool;//随从池数据
    public int turnCount; //游戏运行回合数
    private GameTurnManager() { }
    /// <summary>
    /// 游戏初始化
    /// </summary>
    public void Initialize()
    {
      //创建状态机,注册状态
      fsm = GameEntry.Fsm.CreateFsm<GameTurnManager>(
        this,
       new GameInitState(),
       new GameRecruitMinionState(),
       new GameFightState()
       );
      //启动状态机
      fsm.Start<GameInitState>();
    }

    //放置随从
    public void MinionsPlace(MinionData minionData, UnityAction callback = null)
    {
      if (playerInfo1.Minions.Count >= 7)

        //数据更改
        playerInfo1.Minions.Add(minionData);

      //通知ui更新
      playerInfoForm.UpdateMinionDisplay(minionData);

      callback?.Invoke();
    }

    public void Destroy()
    {
      //销毁状态机
      GameEntry.Fsm.DestroyFsm(fsm);
    }
  }
}
