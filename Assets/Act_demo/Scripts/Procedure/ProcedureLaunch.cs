
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;
namespace Act_demo
{
  public class ProcedureLaunch : ProcedureBase
  {

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
      base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

      // 运行一帧即切换到 Splash 展示流程
      ChangeState<ProcedureSplash>(procedureOwner);
    }
  }
}
