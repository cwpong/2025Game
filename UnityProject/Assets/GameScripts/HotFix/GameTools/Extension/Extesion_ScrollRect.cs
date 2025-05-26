using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Extesion_ScrollRect
{
    /// <summary>
    /// 设置滚动窗完美高度
    /// </summary>
    /// <param name="scrollRect"></param>
    public static void SetContentPrefeerredHeight(this ScrollRect scrollRect)
    {
        var trans = scrollRect.content.transform as RectTransform;
        var layout = scrollRect.content.GetComponent<LayoutGroup>();
        if (layout != null)
        {
            trans.SetSizeHeight(layout.preferredHeight);
        }
    }

    /// <summary>
    /// 设置滚动窗完美宽度
    /// </summary>
    /// <param name="scrollRect"></param>
    public static void SetContentPrefeerredWidth(this ScrollRect scrollRect)
    {
        var trans = scrollRect.content.transform as RectTransform;
        var layout = scrollRect.content.GetComponent<LayoutGroup>();
        if (layout != null)
        {
            trans.SetSizeWidth(layout.preferredWidth);
        }
    }
}
