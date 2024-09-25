//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using cfg;
using UnityGameFramework.Runtime;
namespace Act_demo
{
  public static class EntityExtension
  {
    // 关于 EntityId 的约定：
    // 0 为无效
    // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
    // 负值用于本地生成的临时实体（如特效、FakeObject等）
    private static int s_SerialId = 0;

    public static Entity GetGameEntity(this EntityComponent entityComponent, int entityId)
    {
      UnityGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
      if (entity == null)
      {
        return null;
      }

      return (Entity)entity.Logic;
    }

    public static void HideEntity(this EntityComponent entityComponent, Entity entity)
    {
      entityComponent.HideEntity(entity.Entity);
    }

    public static void AttachEntity(this EntityComponent entityComponent, Entity entity, int ownerId, string parentTransformPath = null, object userData = null)
    {
      entityComponent.AttachEntity(entity.Entity, ownerId, parentTransformPath, userData);
    }

    // public static void ShowMinion(this EntityComponent entityComponent, MinionData data)
    // {
    //   entityComponent.ShowEntity(typeof(Minion), "Minion", Constant.AssetPriority.Minion, data);
    // }

    private static void ShowEntity(this EntityComponent entityComponent, Type logicType, string entityGroup, int priority, EntityData data)
    {
      if (data == null)
      {
        Log.Warning("Data is invalid.");
        return;
      }

      entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(data.AssetName), entityGroup, priority, data);
    }

    public static int GenerateSerialId(this EntityComponent entityComponent)
    {
      return --s_SerialId;
    }
  }
}
