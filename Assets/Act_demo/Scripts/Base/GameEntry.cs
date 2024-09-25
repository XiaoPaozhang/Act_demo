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
    private void Start()
    {
      //初始化GF内置组件
      InitBuiltinComponents();
      InitCustomComponents();
    }
  }
}
