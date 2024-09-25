//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using UnityEngine;

namespace Act_demo
{
  [Serializable]
  public abstract class EntityData
  {
    [SerializeField]
    private int m_Id = 0;
    [SerializeField]
    private string m_AssetName;

    [SerializeField]
    private Vector3 m_Position = Vector3.zero;

    [SerializeField]
    private Quaternion m_Rotation = Quaternion.identity;

    public EntityData(int entityId)
    {
      m_Id = entityId;
    }

    /// <summary>
    /// 实体编号。
    /// </summary>
    public int Id
    {
      get
      {
        return m_Id;
      }
    }

    /// <summary>
    /// 实体资源名称。
    /// </summary>
    public string AssetName
    {
      get
      {
        return m_AssetName;
      }
      protected set
      {
        m_AssetName = value;
      }
    }
    /// <summary>
    /// 实体位置。
    /// </summary>
    public Vector3 Position
    {
      get
      {
        return m_Position;
      }
      set
      {
        m_Position = value;
      }
    }

    /// <summary>
    /// 实体朝向。
    /// </summary>
    public Quaternion Rotation
    {
      get
      {
        return m_Rotation;
      }
      set
      {
        m_Rotation = value;
      }
    }
  }
}
