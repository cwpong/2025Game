using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extesion_Recttransfrom
{
    public static void SetSizeWidth(this RectTransform rectTransform, float width)
    {
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
    }

    public static void SetSizeHeight(this RectTransform rectTransform, float height)
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
    }

    public static void SetSizeDeltaY(this RectTransform rectTransform, float height)
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, height);
    }

    public static void SetSizeDeltaX(this RectTransform rectTransform, float width)
    {
        rectTransform.sizeDelta = new Vector2(width, rectTransform.sizeDelta.y);
    }

    public static void SetAnchorPosY(this RectTransform rectTransform, float y)
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
    }

    public static void SetAnchorPosX(this RectTransform rectTransform, float x)
    {
        rectTransform.anchoredPosition = new Vector2(x, rectTransform.anchoredPosition.y);
    }

    public static void AddAnchorPosY(this RectTransform rectTransform, float y)
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + y);
    }
    public static void AddAnchorPosX(this RectTransform rectTransform, float x)
    {
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + x, rectTransform.anchoredPosition.y);
    }
}
