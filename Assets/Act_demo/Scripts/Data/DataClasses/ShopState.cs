using cfg;
using UnityGameFramework.Runtime;
using QFramework;

namespace Act_demo
{
  /// <summary>
  /// 商店数据
  /// </summary>
  public class ShopState
  {
    public BindableProperty<int> Star { get; private set; }//星级
    public BindableProperty<int> RefreshCost { get; private set; }//升级所需要的金币数量
    public BindableProperty<int> UpgradeCost { get; private set; }//升级所需要的金币数量
    public ShopState(int star)
    {
      Star = new BindableProperty<int>(star);
      RefreshCost = new BindableProperty<int>(0);
      UpgradeCost = new BindableProperty<int>(0);

      Star.Register(newValue =>
        {
          UpdateStar(newValue);
        });

      UpdateStar(Star.Value);
    }

    /// <summary>
    /// 更新商店星级，并重新计算升级所需的费用
    /// </summary>
    /// <param name="newStar">新的星级</param>
    public void UpdateStar(int newStar)
    {
      Shop shopConfig = GetShopConfig(newStar);
      RefreshCost.Value = shopConfig.RefreshCost;
      UpgradeCost.Value = shopConfig.UpgradeCost;

    }

    private Shop GetShopConfig(int star)
    {
      Tables tables = GameEntry.LubanComponent.GetTables<Tables>();
      Shop shop = tables.TbShop.Get(star);
      if (shop == null)
      {
        Log.Error("Cfg not found: TbShop", shop);
        return null;
      }

      return shop;
    }
  }
}

