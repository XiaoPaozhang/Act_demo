using System;
using GameFramework.Event;
using GameFramework.Fsm;
using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Act_demo
{
  public class Minion : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
  {
    public IFsm<Minion> minionFsm;//随从状态机
    public MinionData _minionData;//随从数据
    private Vector2 originalPos; // 卡牌的原始位置
    [SerializeField] private Text nameTxt;
    [SerializeField] private Text attackTxt;
    [SerializeField] private Text hpTxt;

    public void Init(MinionData minionData)
    {
      _minionData = minionData;

      minionData.Attack.Register(OnAttackChanged).UnRegisterWhenGameObjectDestroyed(this);
      minionData.CurrentHp.Register(OnCurrentHpChanged).UnRegisterWhenGameObjectDestroyed(this);
      GameEntry.Event.Subscribe(PurchaseConfirmationEventArgs.EventId, OnPurchaseConfirmation);

      nameTxt.text = minionData.Name;
      OnAttackChanged(minionData.Attack.Value);
      OnCurrentHpChanged(minionData.CurrentHp.Value);
    }

    private void OnDestroy()
    {
      GameEntry.Event.Unsubscribe(PurchaseConfirmationEventArgs.EventId, OnPurchaseConfirmation);
    }

    private void OnPurchaseConfirmation(object sender, GameEventArgs e)
    {
      PurchaseConfirmationEventArgs ne = (PurchaseConfirmationEventArgs)e;

      if (ne.MinionGameObject == this.gameObject)
      {
        if (ne.IsSuccess)
        {
          Destroy(ne.MinionGameObject);
        }
        else
        {
          //移动回原来位置
          transform.GetComponent<RectTransform>().anchoredPosition = originalPos;
        }
      }
    }

    private void OnAttackChanged(int obj)
    {
      attackTxt.text = _minionData.Attack.Value.ToString();
    }

    private void OnCurrentHpChanged(int obj)
    {
      hpTxt.text = _minionData.CurrentHp.Value.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      // selectCardIndex = transform.GetSiblingIndex(); // 获取卡牌的索引
      // transform.SetAsLastSibling(); // 将卡牌移动到最后
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      // throw new NotImplementedException();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      originalPos = transform.GetComponent<RectTransform>().anchoredPosition; // 获取卡牌的原始位置
    }

    public void OnDrag(PointerEventData eventData)
    {
      var rt = gameObject.GetComponent<RectTransform>();
      Vector3 globalMousePos;
      if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
        rt,
        eventData.position,
         eventData.pressEventCamera,
         out globalMousePos))
      {
        rt.position = globalMousePos;
      }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      GameEntry.Event.Fire(this, MinionDragEndEventArgs.Create(this.gameObject, eventData, _minionData));
    }
  }
}
