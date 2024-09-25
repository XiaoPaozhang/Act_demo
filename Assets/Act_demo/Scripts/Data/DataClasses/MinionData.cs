using cfg;
using UnityGameFramework.Runtime;
using QFramework;

namespace Act_demo
{
  public class MinionData
  {
    // 序列编号
    public int SerialId { get; private set; } = 0;
    // 编号
    public int Id { get; private set; }
    // 名称
    public string Name { get; private set; }
    // 图标
    public string Icon { get; private set; }
    // 最大血量
    public BindableProperty<int> MaxHp { get; private set; }
    // 攻击力
    public BindableProperty<int> Attack { get; private set; }
    // 当前血量
    public BindableProperty<int> CurrentHp { get; private set; }
    // 关联卡牌ID
    public BindableProperty<int> AssociatedCardId { get; private set; }


    public MinionData(int minionId)
    {
      // 读表
      Tables tables = GameEntry.LubanComponent.GetTables<Tables>();
      cfg.Minion minion = tables.TbMinion.Get(minionId);
      if (minion == null)
      {
        Log.Error("Cfg not found: TbMinion.Id={0}", minionId);
        return;
      }

      // 自动属性初始化
      Id = minion.Id;
      Name = minion.Name;
      Icon = minion.Icon;
      MaxHp = new BindableProperty<int>(minion.Hp);
      Attack = new BindableProperty<int>(minion.Attack);
      CurrentHp = new BindableProperty<int>(minion.Hp);
      AssociatedCardId = new BindableProperty<int>(minion.AssociatedCardId);
    }

    //获取序列编号,不可能出现重复

    public int GetSerialId()
    {
      return ++SerialId;
    }
  }
}