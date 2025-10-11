using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridLayoutAutoResponsive : MonoBehaviour
{
    [SerializeField] private Vector2 ratioSpacing = new Vector2(0.05f, 0.05f); // Tỷ lệ spacing theo chiều rộng và chiều cao
    [SerializeField] private RectOffset ratioPadding; // Tỷ lệ padding theo phần trăm (0-100)

    private RectTransform rectTransform;
    private GridLayoutGroup gridLayout;

    private void Awake()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        StartCoroutine(StartResponsive());
    }

    private IEnumerator StartResponsive()
    {
        // Đợi cho đến khi kích thước của RectTransform được cập nhật
        while (rectTransform.rect.width == 0 || rectTransform.rect.height == 0)
        {
            yield return null;
        }

        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        UpdateSpacing(width, height);
        UpdatePadding(width, height);
        UpdateCellSize(width, height);
    }

    private void UpdateSpacing(float width, float height)
    {
        float spacingX = ratioSpacing.x * width;
        float spacingY = ratioSpacing.y * height;

        gridLayout.spacing = new Vector2(spacingX, spacingY);
    }

    private void UpdatePadding(float width, float height)
    {
        int paddingLeft = Mathf.RoundToInt(ratioPadding.left * width / 100f);
        int paddingRight = Mathf.RoundToInt(ratioPadding.right * width / 100f);
        int paddingTop = Mathf.RoundToInt(ratioPadding.top * height / 100f);
        int paddingBottom = Mathf.RoundToInt(ratioPadding.bottom * height / 100f);

        gridLayout.padding = new RectOffset(paddingLeft, paddingRight, paddingTop, paddingBottom);
    }

    private void UpdateCellSize(float width, float height)
    {
        float totalSpacingX = gridLayout.spacing.x * (Constant.k_defaultCol - 1);
        float totalSpacingY = gridLayout.spacing.y * (Constant.k_defaultRow - 1);

        float totalPaddingX = gridLayout.padding.left + gridLayout.padding.right;
        float totalPaddingY = gridLayout.padding.top + gridLayout.padding.bottom;

        float cellWidth = (width - totalSpacingX - totalPaddingX) / Constant.k_defaultCol;
        float cellHeight = (height - totalSpacingY - totalPaddingY) / Constant.k_defaultRow;

        gridLayout.cellSize = new Vector2(cellWidth, cellHeight);
    }
}
