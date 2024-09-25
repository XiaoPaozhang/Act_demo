//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using cfg;
using GameFramework;
using GameFramework.Event;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace Act_demo
{
  /// <summary>
  /// 切换场景流程
  /// </summary>
  public class ProcedureChangeScene : ProcedureBase
  {
    public static SceneType defaultSceneType = SceneType.Fight;
    private int m_NextSceneId;
    private bool m_IsChangeSceneComplete = false;

    protected override void OnEnter(ProcedureOwner procedureOwner)
    {
      base.OnEnter(procedureOwner);

      m_IsChangeSceneComplete = false;

      GameEntry.Event.Subscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
      GameEntry.Event.Subscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
      GameEntry.Event.Subscribe(LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
      GameEntry.Event.Subscribe(LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);

      // 停止所有声音
      GameEntry.Sound.StopAllLoadingSounds();
      GameEntry.Sound.StopAllLoadedSounds();

      // 隐藏所有实体
      GameEntry.Entity.HideAllLoadingEntities();
      GameEntry.Entity.HideAllLoadedEntities();

      // 卸载所有场景
      string[] loadedSceneAssetNames = GameEntry.Scene.GetLoadedSceneAssetNames();
      for (int i = 0; i < loadedSceneAssetNames.Length; i++)
      {
        GameEntry.Scene.UnloadScene(loadedSceneAssetNames[i]);
      }

      // 还原游戏速度
      GameEntry.Base.ResetNormalGameSpeed();

      //获取下一个场景id
      m_NextSceneId = procedureOwner.GetData<VarInt32>("NextSceneId");

      GameEntry.Scene.LoadScene(AssetUtility.GetSceneAsset(((SceneType)m_NextSceneId).ToString()), this);
    }

    protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
    {
      GameEntry.Event.Unsubscribe(LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
      GameEntry.Event.Unsubscribe(LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
      GameEntry.Event.Unsubscribe(LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
      GameEntry.Event.Unsubscribe(LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);

      base.OnLeave(procedureOwner, isShutdown);
    }

    protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
      base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

      if (!m_IsChangeSceneComplete)
      {
        return;
      }

      // 判断场景id切换到对应流程
      switch ((SceneType)m_NextSceneId)
      {
        case SceneType.Menu:
          ChangeState<ProcedureMenu>(procedureOwner);
          break;
        case SceneType.Fight:
          // Log.Info("切换战斗流程");
          ChangeState<ProcedureFight>(procedureOwner);
          break;
        default:
          Log.Info($"错误的场景ID: {m_NextSceneId}");
          break;
      }
    }



    private void OnLoadSceneSuccess(object sender, GameEventArgs e)
    {
      LoadSceneSuccessEventArgs ne = (LoadSceneSuccessEventArgs)e;
      if (ne.UserData != this)
      {
        return;
      }

      Log.Info("Load scene '{0}' OK.", ne.SceneAssetName);

      m_IsChangeSceneComplete = true;
    }

    private void OnLoadSceneFailure(object sender, GameEventArgs e)
    {
      LoadSceneFailureEventArgs ne = (LoadSceneFailureEventArgs)e;
      if (ne.UserData != this)
      {
        return;
      }

      Log.Error("Load scene '{0}' failure, error message '{1}'.", ne.SceneAssetName, ne.ErrorMessage);
    }

    private void OnLoadSceneUpdate(object sender, GameEventArgs e)
    {
      LoadSceneUpdateEventArgs ne = (LoadSceneUpdateEventArgs)e;
      if (ne.UserData != this)
      {
        return;
      }

      Log.Info("Load scene '{0}' update, progress '{1}'.", ne.SceneAssetName, ne.Progress.ToString("P2"));
    }

    private void OnLoadSceneDependencyAsset(object sender, GameEventArgs e)
    {
      LoadSceneDependencyAssetEventArgs ne = (LoadSceneDependencyAssetEventArgs)e;
      if (ne.UserData != this)
      {
        return;
      }

      Log.Info("Load scene '{0}' dependency asset '{1}', count '{2}/{3}'.", ne.SceneAssetName, ne.DependencyAssetName, ne.LoadedCount.ToString(), ne.TotalCount.ToString());
    }
  }
}
