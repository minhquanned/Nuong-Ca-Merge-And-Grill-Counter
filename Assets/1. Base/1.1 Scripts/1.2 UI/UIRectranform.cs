using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UIRectranform : MonoBehaviour
{
    public RectTransform _rectTransform { get; private set; }

    protected virtual void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Làm RectTransform co giãn full parent (stretch full).
    /// </summary>
    public void StretchFull()
    {
        _rectTransform.anchorMin = Vector2.zero;
        _rectTransform.anchorMax = Vector2.one;
        _rectTransform.offsetMin = Vector2.zero;
        _rectTransform.offsetMax = Vector2.zero;
    }

    /// <summary>
    /// Đặt RectTransform vào giữa, không co giãn theo parent.
    /// </summary>
    public void AnchorCenter()
    {
        _rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        _rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        _rectTransform.anchoredPosition = Vector2.zero;
    }

    /// <summary>
    /// Lấy Rect (kích thước thực tế) của RectTransform trong local space.
    /// </summary>
    public Rect GetLocalRect()
    {
        return _rectTransform.rect;
    }

    /// <summary>
    /// Lấy Rect (kích thước thực tế) trong world space.
    /// </summary>
    public Rect GetWorldRect()
    {
        Vector3[] corners = new Vector3[4];
        _rectTransform.GetWorldCorners(corners);
        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];

        return new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y
        );
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetWidthHeight(float width, float height)
    {
        Vector2 size = _rectTransform.sizeDelta;
        size.x = width;
        size.y = height;
        _rectTransform.sizeDelta = size;
    }
}
