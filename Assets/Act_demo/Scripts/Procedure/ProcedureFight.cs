using cfg.minion;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
namespace Act_demo
{
  /// <summary>
  /// 战斗流程
  /// </summary>
  public class ProcedureFight : ProcedureBase
  {
    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
      base.OnEnter(procedureOwner);
      //启动游戏
      GameTurnManager.Instance.Initialize();
    }

    protected override void OnDestroy(IFsm<IProcedureManager> procedureOwner)
    {
      base.OnDestroy(procedureOwner);
      //销毁游戏
      GameTurnManager.Instance.Destroy();
    }
  }
}
