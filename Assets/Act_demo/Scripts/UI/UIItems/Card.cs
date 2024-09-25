using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Act_demo
{
  public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
  {
    private CardData _cardData;
    private int selectCardIndex; // 选中的卡牌的索引
    private Vector2 originalPos; // 卡牌的原始位置

    //UI组件
    [SerializeField] private Text starTxt;
    [SerializeField] private Text nameTxt;
    [SerializeField] private Text desTxt;
    [SerializeField] private Text attackTxt;
    [SerializeField] private Text hpTxt;
    [SerializeField] private Image borderLine;

    public float zoomFactor = 1.5f; // 放大的比例

    // protected override void OnInit()
    // {
    //   base.OnInit();
    // }
    public void Init(CardData cardData)
    {
      _cardData = cardData;
      // 监听数据变化 更新UI
      _cardData.Star.Register(UpdateStarText);
      _cardData.Name.Register(UpdateNameText);
      _cardData.Des.Register(UpdateDesText);
      _cardData.Attack.Register(UpdateAttackText);
      _cardData.CurrentHp.Register(UpdateHpText);

      UpdateStarText(_cardData.Star.Value);
      UpdateNameText(_cardData.Name.Value);
      UpdateDesText(_cardData.Des.Value);
      UpdateAttackText(_cardData.Attack.Value);
      UpdateHpText(_cardData.CurrentHp.Value);
    }

    private void UpdateStarText(int starValue)
    {
      starTxt.text = starValue.ToString();
    }

    private void UpdateNameText(string nameValue)
    {
      nameTxt.text = nameValue;
    }

    private void UpdateDesText(string desValue)
    {
      desTxt.text = desValue;
    }

    private void UpdateAttackText(int attackValue)
    {
      attackTxt.text = attackValue.ToString();
    }

    private void UpdateHpText(int hpValue)
    {
      hpTxt.text = hpValue.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      transform.DOScale(zoomFactor, 0.25f); // 放大卡牌
      selectCardIndex = transform.GetSiblingIndex(); // 获取卡牌的索引
      transform.SetAsLastSibling(); // 将卡牌移动到最后

      borderLine.enabled = true; // 显示边框
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      transform.DOScale(1, 0.25f); // 缩小卡牌
      transform.SetSiblingIndex(selectCardIndex); // 将卡牌移动到原来的位置

      borderLine.enabled = false; // 隐藏边框
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      originalPos = transform.GetComponent<RectTransform>().anchoredPosition; // 获取卡牌的原始位置

      GameEntry.Event.Fire(this, CardDragEventArgs.Create(this));
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
      RectTransform minionPlaceArea = transform.parent.parent.Find("minionMgr").GetComponent<RectTransform>(); // 随从可放置区域

      // 判断鼠标是否移入可放置区域
      if (RectTransformUtility.RectangleContainsScreenPoint(
        minionPlaceArea,
        eventData.position,
        eventData.pressEventCamera))
      {
        GameTurnManager.Instance.MinionsPlace(new MinionData(_cardData.AssociatedMinoinId), () =>
       {
         Destroy(gameObject); // 销毁卡牌
         GameEntry.Event.Fire(this, CardUsedEventArgs.Create(this, true, selectCardIndex));
       });
      }
      else
      {
        ResetCardPosition();
        GameEntry.Event.Fire(this, CardUsedEventArgs.Create(this, false, selectCardIndex));
      }

    }

    /// <summary>
    /// 重置卡牌位置
    /// </summary>
    private void ResetCardPosition()
    {
      transform.GetComponent<RectTransform>().anchoredPosition = originalPos; // 卡牌回到原始位置
                                                                              // transform.SetSiblingIndex(selectCardIndex); // 将卡牌移动到原来的层级位置
    }


  }
}
