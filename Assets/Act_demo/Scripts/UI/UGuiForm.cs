//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;

namespace Act_demo
{
  public abstract class UGuiForm : UIFormLogic
  {
    public const int DepthFactor = 100;
    private const float FadeTime = 0.3f;
    private Canvas m_CachedCanvas = null;
    private CanvasGroup m_CanvasGroup = null;
    private List<Canvas> m_CachedCanvasContainer = new List<Canvas>();
    private Dictionary<int, EventHandler<GameEventArgs>> m_eventHandlers = new Dictionary<int, EventHandler<GameEventArgs>>();

    public int OriginalDepth
    {
      get;
      private set;
    }

    public int Depth
    {
      get
      {
        return m_CachedCanvas.sortingOrder;
      }
    }

    public void Subscribe(int EventId, EventHandler<GameEventArgs> handler)
    {
      GameEntry.Event.Subscribe(EventId, handler);

      if (!m_eventHandlers.ContainsKey(EventId))
      {
        m_eventHandlers.Add(EventId, handler);
      }
    }
    public void UnsubscribeAll()
    {
      //取消所有订阅
      foreach (var item in m_eventHandlers)
      {
        GameEntry.Event.Unsubscribe(item.Key, item.Value);
      }

      m_eventHandlers.Clear();
    }

    protected virtual T Find<T>(string path) where T : Component
    {
      // 检查path是否为空
      if (string.IsNullOrEmpty(path))
      {
        Debug.LogWarning("查找路径为空或无效。");
        return null;
      }

      // 使用transform.Find查找Transform
      Transform childTransform = transform.Find(path);
      if (childTransform == null)
      {
        Debug.LogWarning($"未找到路径为 '{path}' 的Transform。");
        return null;
      }

      // 尝试获取组件
      T component = childTransform.GetComponent<T>();
      if (component == null)
      {
        Debug.LogWarning($"在路径 '{path}' 的Transform上未找到类型为 '{typeof(T).Name}' 的组件。");
        return null;
      }

      return component;
    }

    public void Close()
    {
      Log.Info("Close {0}", this.GetType().Name);
      Close(false);
    }

    public void Close(bool ignoreFade)
    {
      StopAllCoroutines();

      if (ignoreFade)
      {
        GameEntry.UI.CloseUIForm(this);
      }
      else
      {
        StartCoroutine(CloseCo(FadeTime));
      }
    }

    protected override void OnInit(object userData)
    {
      base.OnInit(userData);

      m_CachedCanvas = gameObject.GetOrAddComponent<Canvas>();
      m_CachedCanvas.overrideSorting = true;
      OriginalDepth = m_CachedCanvas.sortingOrder;

      m_CanvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();

      RectTransform transform = GetComponent<RectTransform>();
      transform.anchorMin = Vector2.zero;
      transform.anchorMax = Vector2.one;
      transform.anchoredPosition = Vector2.zero;
      transform.sizeDelta = Vector2.zero;

      gameObject.GetOrAddComponent<GraphicRaycaster>();
    }


    protected override void OnRecycle()
    {
      base.OnRecycle();
    }

    protected override void OnOpen(object userData)
    {
      base.OnOpen(userData);
      // Log.Info("Open {0}", this.GetType().Name);
      m_CanvasGroup.alpha = 0f;
      StopAllCoroutines();
      StartCoroutine(m_CanvasGroup.FadeToAlpha(1f, FadeTime));
    }

    protected override void OnClose(bool isShutdown, object userData)
    {
      base.OnClose(isShutdown, userData);
      UnsubscribeAll();
    }

    protected override void OnPause()
    {
      base.OnPause();
    }

    protected override void OnResume()
    {
      base.OnResume();

      // Log.Info("OnResume {0}", this.GetType().Name);
      m_CanvasGroup.alpha = 0f;
      StopAllCoroutines();
      StartCoroutine(m_CanvasGroup.FadeToAlpha(1f, FadeTime));
    }

    protected override void OnCover()
    {
      base.OnCover();
    }

    protected override void OnReveal()
    {
      base.OnReveal();
    }

    protected override void OnRefocus(object userData)
    {
      base.OnRefocus(userData);
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
      base.OnUpdate(elapseSeconds, realElapseSeconds);
    }

    protected override void OnDepthChanged(int uiGroupDepth, int depthInUIGroup)
    {
      int oldDepth = Depth;
      base.OnDepthChanged(uiGroupDepth, depthInUIGroup);
      int deltaDepth = UGuiGroupHelper.DepthFactor * uiGroupDepth + DepthFactor * depthInUIGroup - oldDepth + OriginalDepth;
      GetComponentsInChildren(true, m_CachedCanvasContainer);
      for (int i = 0; i < m_CachedCanvasContainer.Count; i++)
      {
        m_CachedCanvasContainer[i].sortingOrder += deltaDepth;
      }

      m_CachedCanvasContainer.Clear();
    }

    private IEnumerator CloseCo(float duration)
    {
      yield return m_CanvasGroup.FadeToAlpha(0f, duration);
      GameEntry.UI.CloseUIForm(this);
    }
  }
}
