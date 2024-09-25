using cfg;
using QFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Act_demo
{

  public class CardData
  {
    //编号
    public int Id { get; private set; }
    // 名称
    public BindableProperty<string> Name { get; private set; }
    // 图标
    public BindableProperty<string> Icon { get; private set; }
    // 最大血量
    public BindableProperty<int> MaxHp { get; private set; }
    // 当前血量
    public BindableProperty<int> CurrentHp { get; private set; }
    // 攻击力
    public BindableProperty<int> Attack { get; private set; }
    // 费用
    public BindableProperty<int> Cost { get; private set; }
    // 星级
    public BindableProperty<int> Star { get; private set; }
    // 描述
    public BindableProperty<string> Des { get; private set; }
    //关联随从编号
    public int AssociatedMinoinId { get; private set; }

    public CardData(int cardId)
    {
      // 读表
      Tables tables = GameEntry.LubanComponent.GetTables<Tables>();
      cfg.Card card = tables.TbCard.Get(cardId);
      if (card == null)
      {
        Log.Error("Cfg not found: TbCard", cardId);
        return;
      }

      Id = cardId;
      Name = new BindableProperty<string>(card.Name);
      Icon = new BindableProperty<string>(card.Icon);
      MaxHp = new BindableProperty<int>(card.Hp);
      CurrentHp = new BindableProperty<int>(Mathf.Clamp(card.Hp, 0, MaxHp.Value));
      Attack = new BindableProperty<int>(card.Attack);
      Cost = new BindableProperty<int>(card.Cost);
      Star = new BindableProperty<int>(card.Star);
      Des = new BindableProperty<string>(card.Des);
      AssociatedMinoinId = card.AssociatedMinoinId;
    }
  }
}
