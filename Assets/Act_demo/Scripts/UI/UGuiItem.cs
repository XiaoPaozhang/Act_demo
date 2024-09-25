using UnityEngine;
using UnityEngine.UI;

namespace Act_demo
{
  public class UGuiItem : MonoBehaviour
  {
    private Canvas m_CachedCanvas = null;
    private CanvasGroup m_CanvasGroup = null;
    public int OriginalDepth
    {
      get;
      private set;
    }
    private void Start()
    {
      OnInit();
    }

    protected virtual void OnInit()
    {
      m_CachedCanvas = gameObject.GetOrAddComponent<Canvas>();
      m_CachedCanvas.overrideSorting = true;
      OriginalDepth = m_CachedCanvas.sortingOrder;

      m_CanvasGroup = gameObject.GetOrAddComponent<CanvasGroup>();

      RectTransform transform = GetComponent<RectTransform>();
      transform.anchorMin = Vector2.zero;
      transform.anchorMax = Vector2.one;
      transform.anchoredPosition = Vector2.zero;
      // transform.sizeDelta = Vector2.zero;

      gameObject.GetOrAddComponent<GraphicRaycaster>();
    }
  }
}
