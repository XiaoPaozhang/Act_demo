using System;
using System.Collections.Generic;
using DG.Tweening;
using GameFramework.Event;
using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Act_demo
{
  /// <summary>
  /// 玩家信息界面
  /// </summary>
  public class PlayerInfoForm : UGuiForm
  {
    private PlayerInfoData playerInfoData;//玩家数据
    private GameObject[] manaImages;// 用于存储法力值点引用的数组
    private List<Card> cardsCache = new List<Card>();// 手牌缓存
    [SerializeField] public Minion minionPrefabs;//随从预制体
    [SerializeField] private Card cardPrefabs;//手牌预制体
    [SerializeField] private Transform minionMgrTf;//场地随从摆放位置
    [SerializeField] private RectTransform cardGroupTf;//拥有的手牌
    [SerializeField] private Text manaTxt;//法力值文本
    [SerializeField] private GameObject manaMgr;//法力值槽
    [SerializeField] private Button turnBtn;//回合结束按钮
    [SerializeField] private Text hpTxt;//血量文本
    [SerializeField] private Transform cardStartPosT;//手牌起始位置
    [SerializeField] private RectTransform handArea;//手牌区域
    private int _currentCardCount;//当前卡牌数量
    public float cardWidth;//卡牌宽度

    [SerializeField, Header("移动到目标位置延迟时间")] private float moveDuration;
    [SerializeField, Header("卡牌旋转动画持续时间")] private float dealDuration;
    [SerializeField, Header("每张卡牌动画间隔时间")] private float delayBetweenCards;

    //-----------------------------------------------------------------------

    protected override void OnOpen(object userData)
    {
      base.OnOpen(userData);
      playerInfoData = userData as PlayerInfoData;
      if (playerInfoData == null)
      {
        Log.Error("PlayerInfoForm 没有接受到数据");
        return;
      }
      // 监听数据变化 更新UI
      playerInfoData.CurrentHp.Register(UpdateCurrentHpText).UnRegisterWhenGameObjectDestroyed(this);
      playerInfoData.MaxHp.Register(UpdateMaxHpText).UnRegisterWhenGameObjectDestroyed(this);
      playerInfoData.CurrentMp.Register(UpdateCurrentMpText).UnRegisterWhenGameObjectDestroyed(this);
      playerInfoData.MaxMp.Register(UpdateMaxMpText).UnRegisterWhenGameObjectDestroyed(this);

      //初始化法力值点数
      InitMana(playerInfoData.CurrentMp.Value, playerInfoData.MaxMp.Value);

      turnBtn.onClick.AddListener(turnBtnClick);

      Subscribe(CardDragEventArgs.EventId, OnCardDrag);
      Subscribe(CardUsedEventArgs.EventId, OnCardUsed);
      Subscribe(MinionDragEndEventArgs.EventId, OnMinionDragEnd);
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
      base.OnClose(isShutdown, userData);
    }

    private void InitMana(int currentMp, int maxMp)
    {
      // 创建一个长度为10的数组来存储引用
      manaImages = new GameObject[10];

      // 初始化并隐藏所有法力值点
      for (int i = 0; i < 10; i++)
      {
        // 查找每个法力值点并存储引用
        GameObject manaImage = transform.Find($"manaMgr/mana ({i})").gameObject;
        manaImages[i] = manaImage;
      }

      UpdateCurrentHpText(playerInfoData.CurrentHp.Value);
      UpdateMaxHpText(playerInfoData.MaxHp.Value);
      UpdateMpDisplay(playerInfoData.CurrentMp.Value, playerInfoData.MaxMp.Value);
    }

    private void UpdateCurrentHpText(int value)
    {
      hpTxt.text = value.ToString();
    }
    private void UpdateMaxHpText(int value)
    {
      //Todo
    }

    private void UpdateCurrentMpText(int currentMp)
    {
      UpdateMpDisplay(currentMp, playerInfoData.MaxMp.Value);
    }

    private void UpdateMaxMpText(int maxMp)
    {
      UpdateMpDisplay(playerInfoData.CurrentMp.Value, maxMp);
    }

    //法力值点的显示状态
    private void UpdateMpDisplay(int currentMp, int maxMp)
    {
      manaTxt.text = $"{currentMp}/{maxMp}";
      // 显示当前的法力值点
      for (int i = 0; i < 10; i++)
      {
        if (i < currentMp)
        {
          manaImages[i].SetActive(true);
        }
        else
        {
          manaImages[i].SetActive(false);
        }

      }
    }

    private void OnCardDrag(object sender, GameEventArgs e)
    {
      CardDragEventArgs ne = (CardDragEventArgs)e;
      CardRemoveDisplay(ne.DraggedCard);
    }

    private void OnCardUsed(object sender, GameEventArgs e)
    {
      CardUsedEventArgs ne = (CardUsedEventArgs)e;

      //是否成功使用
      if (ne.IsUsedSuccess)
      {
        // CardRemoveDisplay(ne.UsedCard);
      }
      else
      {
        //插入卡牌
        cardsCache.Insert(ne.CardIndex, ne.UsedCard);
        _currentCardCount++;

        // 更新卡牌位置
        UpdateCardTransformDisplay();
      }
    }

    private void Update()
    {
      // 获取鼠标位置
      PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
      eventDataCurrentPosition.position = Input.mousePosition;

      // 使用 EventSystem 的默认摄像机进行转换
      bool isInside = RectTransformUtility.ScreenPointToLocalPointInRectangle(
          handArea,
          eventDataCurrentPosition.position,
          EventSystem.current.currentInputModule?.GetComponent<Camera>(),
          out Vector2 localPoint);

      if (isInside)
      {
        Debug.Log("Mouse is inside the hand area.");
      }
      else
      {
        Debug.Log("Mouse is outside the hand area.");
      }
    }

    private void OnMinionDragEnd(object sender, GameEventArgs e)
    {
      MinionDragEndEventArgs ne = (MinionDragEndEventArgs)e;
      if (ne.minionData == null)
      {
        return;
      }

      // 判断鼠标是否移入可放置区域
      bool isSuccess = RectTransformUtility.RectangleContainsScreenPoint(
          handArea,
          ne.eventData.position,
          ne.eventData.pressEventCamera
          );

      if (isSuccess)
      {
        CardData[] cardDatas = new CardData[1];
        cardDatas[0] = new CardData(ne.minionData.AssociatedCardId.Value);
        DrawCardDisplay(cardDatas);
      }

      GameEntry.Event.Fire(this, PurchaseConfirmationEventArgs.Create(ne.MinionGameObject, isSuccess));

    }
    //移除卡牌显示效果
    private void CardRemoveDisplay(Card card)
    {
      // 移除卡牌
      cardsCache.Remove(card);
      _currentCardCount--;

      // 更新卡牌位置
      UpdateCardTransformDisplay();
    }

    //历程开始时显示法力值
    public void showMana()
    {
      //显示法力值显示
      manaMgr.gameObject.SetActive(true);
      manaTxt.gameObject.SetActive(true);
    }

    //更新随从显示
    public void UpdateMinionDisplay(MinionData minionData)
    {
      Minion minion = Instantiate(minionPrefabs);
      minion.Init(minionData);
      minion.transform.SetParent(minionMgrTf);
    }

    //回合结束按钮点击事件
    private void turnBtnClick()
    {
      //隐藏法力值显示
      manaMgr.gameObject.SetActive(false);
      manaTxt.gameObject.SetActive(false);

      //发送事件
      GameEntry.Event.Fire(this, new TurnEndEventArgs());
    }

    //抽取卡牌
    public void DrawCardDisplay(CardData[] cardDatas)
    {
      _currentCardCount += cardDatas.Length;

      for (int i = 0; i < cardDatas.Length; i++)
      {
        //实例化
        Card card = Instantiate(cardPrefabs);
        //加入缓存
        cardsCache.Add(card);
        //设置显示数据
        card.Init(cardDatas[i]);

        //设置初始位置在(在屏幕外,配合动画)
        card.transform.position = cardStartPosT.position;
        card.transform.SetParent(cardGroupTf);
        card.transform.localScale = Vector3.one;
        //默认显示背面(配合动画)
        card.transform.Find("CardBack").gameObject.SetActive(true);

        DealCard(card.gameObject, i * delayBetweenCards, GetCardPosition(i));
      }
    }

    /// <summary>
    /// 这是游戏刚开始发牌的动画,需要一个
    /// </summary>
    /// <param name="card">卡牌游戏对象</param>
    /// <param name="delay">延迟时间</param>
    /// <param name="targetPos">目标位置</param>
    private void DealCard(GameObject card, float delay, Vector3 targetPos)
    {
      Quaternion initialRotation = card.transform.rotation;

      Sequence sequence = DOTween.Sequence();

      sequence.AppendInterval(delay);
      sequence.Append(card.transform.DOMove(targetPos, moveDuration));
      sequence.Join(card.transform.DORotate(new Vector3(0, 90, 0), dealDuration / 2));
      sequence.AppendCallback(() => card.transform.Find("CardBack").gameObject.SetActive(false));
      sequence.Join(card.transform.DORotate(initialRotation.eulerAngles, dealDuration / 2));
    }

    // 卡牌位置的显示更新
    public void UpdateCardTransformDisplay()
    {
      for (int i = 0; i < cardsCache.Count; i++)
      {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(cardsCache[i].transform.DOMove(GetCardPosition(i), moveDuration));
      }
    }

    // 计算卡牌位置的辅助方法
    private Vector3 GetCardPosition(int index)
    {
      // 计算总宽度
      float totalWidth = _currentCardCount * cardWidth;
      // 计算第一张卡牌的起始位置
      float startingX = cardGroupTf.position.x - totalWidth / 2 + cardWidth / 2;
      float cardX = startingX + index * cardWidth;
      return new Vector3(cardX, cardGroupTf.position.y, cardGroupTf.position.z);
    }

  }
}
