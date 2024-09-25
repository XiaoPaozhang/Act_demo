using cfg;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Act_demo
{
  public class ProcedureMenu : ProcedureBase
  {
    private bool m_StartGame = false;
    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
      base.OnEnter(procedureOwner);

      GameEntry.UI.OpenUIForm<MenuForm>(this);
    }

    protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
      base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
      if (m_StartGame)
      {
        //获取名为Fight的场景的id
        int nextSceneId = (int)SceneType.Fight;

        //跳转
        procedureOwner.SetData<VarInt32>("NextSceneId", nextSceneId);
        ChangeState<ProcedureChangeScene>(procedureOwner);
      }
    }

    public void StartGame()
    {
      m_StartGame = true;
    }
  }
}
