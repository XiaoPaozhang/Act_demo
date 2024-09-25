using System;
using System.Collections.Generic;
using GameFramework.Event;
using QFramework;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Act_demo
{
  public class ShopInfoForm : UGuiForm
  {
    private ShopData _shopData;//商店管理器
    [SerializeField] public Minion minionPrefabs;//随从预制体
    [SerializeField] private Text starTxt;//星级
    [SerializeField] private RectTransform recoveryArea;//随从显示区域
    [SerializeField] private Text refreshCostTxt;//刷新所需费用
    [SerializeField] private Button refreshBtn;//刷新按钮
    [SerializeField] private Button lockBtn;//星级
    [SerializeField] private Transform minionMgrTf;//星级
    [SerializeField] private Text upgradeCostTxt;//升级所需费用
    [SerializeField] private Button upgradeBtn;//升级按钮
    protected override void OnOpen(object userData)
    {
      base.OnOpen(userData);

      _shopData = userData as ShopData;
      if (_shopData == null)
      {
        Log.Error("ShopMgr 没有接受到数据");
        return;
      }

      _shopData.ShopState.Star.Register(updateStarText).UnRegisterWhenGameObjectDestroyed(gameObject);
      _shopData.ShopState.UpgradeCost.Register(UpdateUpgradeCostText).UnRegisterWhenGameObjectDestroyed(gameObject);
      _shopData.ShopState.RefreshCost.Register(UpdateRefreshCostText).UnRegisterWhenGameObjectDestroyed(gameObject);
      GameTurnManager.Instance.playerInfo1.CurrentMp.Register(UpdateCurrentMp).UnRegisterWhenGameObjectDestroyed(gameObject);
      Subscribe(MinionDragEndEventArgs.EventId, OnMinionDragEnd);

      UpdateUpgradeCostText(_shopData.ShopState.UpgradeCost.Value);
      UpdateRefreshCostText(_shopData.ShopState.RefreshCost.Value);
      updateStarText(_shopData.ShopState.Star.Value);
      UpdateMinionDisplay(_shopData.Minions);
      UpdateCurrentMp(GameTurnManager.Instance.playerInfo1.CurrentMp.Value);

      upgradeBtn.onClick.AddListener(OnUpgradeClick);
      refreshBtn.onClick.AddListener(OnRefreshRateClick);
      lockBtn.onClick.AddListener(OnLockClick);
    }

    private void OnMinionDragEnd(object sender, GameEventArgs e)
    {
      MinionDragEndEventArgs ne = (MinionDragEndEventArgs)e;

      if (ne.MinionGameObject == null)
      {
        return;
      }

    }
    private void OnUpgradeClick()
    {
      // 扣除金币
      GameTurnManager.Instance.playerInfo1.CurrentMp.Value -= _shopData.ShopState.UpgradeCost.Value;
      GameTurnManager.Instance.shopData.ShopState.Star.Value++;
    }

    private void OnRefreshRateClick()
    {
      // 扣除金币
      GameTurnManager.Instance.playerInfo1.CurrentMp.Value -= _shopData.ShopState.RefreshCost.Value;
      // 刷新随从商品
      GameTurnManager.Instance.shopData.RefreshMinionShop(GameTurnManager.Instance.turnCount);
      // 刷新显示
      UpdateMinionDisplay(_shopData.Minions);
    }

    //检查金币是否足够
    private void CheckMp(int currentMp, int cost, Button btn, Text txt)
    {
      if (currentMp >= cost)
      {
        btn.interactable = true;
        txt.color = Color.white;
      }
      else
      {
        // 禁用按钮,费用变红
        btn.interactable = false;
        txt.color = Color.red;
      }
    }

    private void UpdateCurrentMp(int currentMp)
    {
      CheckMp(currentMp, _shopData.ShopState.RefreshCost.Value, refreshBtn, refreshCostTxt);
      CheckMp(currentMp, _shopData.ShopState.UpgradeCost.Value, upgradeBtn, upgradeCostTxt);
    }

    private void OnLockClick()
    {
      Log.Debug("锁定");
    }

    private void UpdateUpgradeCostText(int value)
    {
      upgradeCostTxt.text = value.ToString();
      // 不能升级
      if (value == 0)
      {
        // 禁用按钮
        upgradeBtn.interactable = false;
        // 隐藏按钮
        upgradeBtn.gameObject.SetActive(false);
      }
      else
      {
        // 启用按钮
        upgradeBtn.gameObject.SetActive(true);
      }

      // 检查玩家法力值是否满足升级费用
      CheckMp(GameTurnManager.Instance.playerInfo1.CurrentMp.Value, value, upgradeBtn, upgradeCostTxt);
    }

    private void UpdateRefreshCostText(int value)
    {
      refreshCostTxt.text = value.ToString();

      // 检查玩家法力值是否满足刷新费用
      CheckMp(GameTurnManager.Instance.playerInfo1.CurrentMp.Value, value, refreshBtn, refreshCostTxt);
    }

    private void updateStarText(int value)
    {
      starTxt.text = value.ToString();
    }

    // 更新随从显示
    public void UpdateMinionDisplay(List<MinionData> minionDatas)
    {
      // 清空显示
      foreach (Transform child in minionMgrTf)
      {
        Destroy(child.gameObject);
      }

      foreach (var minionData in minionDatas)
      {
        Minion minion = Instantiate(minionPrefabs);
        minion.Init(minionData);
        minion.transform.SetParent(minionMgrTf);
      }
    }
  }
}
