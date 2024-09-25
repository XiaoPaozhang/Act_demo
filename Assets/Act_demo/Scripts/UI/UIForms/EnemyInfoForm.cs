using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Act_demo
{
  public class EnemyInfoForm : UGuiForm
  {
    [SerializeField] public Minion minionPrefabs;//随从预制体
    [SerializeField] private Transform minionMgrTf;//场地随从摆放位置
    [SerializeField] private Text hpTxt;
    protected override void OnOpen(object userData)
    {
      base.OnOpen(userData);
      PlayerInfoData playerInfoData = userData as PlayerInfoData;
      if (playerInfoData == null)
      {
        Log.Error("EnemyInfoForm 没有接受到数据");
        return;
      }

      hpTxt.text = playerInfoData.CurrentHp.ToString();

      //初始化场地随从
      foreach (MinionData minionData in playerInfoData.Minions)
      {
        AddMinionDisplay(minionData);
      }
    }

    public void AddMinionDisplay(MinionData minionData)
    {
      Minion minion = Instantiate(minionPrefabs);
      minion.transform.SetParent(minionMgrTf);
      minion.Init(minionData);
    }
  }
}
