//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using UnityEngine;

namespace Act_demo
{
  /// <summary>
  /// 游戏入口。
  /// </summary>
  public partial class GameEntry : MonoBehaviour
  {
    /// <summary>
    /// 获取配置表数据组件。
    /// </summary>
    public static LubanTableComponent LubanComponent
    {
      get;
      private set;
    }


    private static void InitCustomComponents()
    {
      LubanComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<LubanTableComponent>();

    }
  }
}
