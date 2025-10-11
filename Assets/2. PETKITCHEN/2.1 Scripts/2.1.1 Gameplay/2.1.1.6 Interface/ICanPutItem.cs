
using UnityEngine;

public interface ICanPutItem
{
    /// <summary>
    /// Kiểm tra xem có thể đặt item vào đây không.
    /// </summary>
    /// <param name="item">Item cần kiểm tra.</param>
    /// <returns>True nếu có thể đặt item, ngược lại là false.</returns>
    bool CanPut(IItem item);

    /// <summary>
    /// Đặt item vào vị trí này.
    /// </summary>
    /// <param name="item">Item cần đặt.</param>
    void PutItem(UIInGameItem item);

    /// <summary>
    /// Trả về item hiện có (nếu có).
    /// </summary>
    /// <returns>Item đang ở vị trí này hoặc null nếu trống.</returns>
    IItem GetCurrentItem();

    /// <summary>
    /// Xóa item hiện tại khỏi vị trí này.
    /// </summary>
    void RemoveItem();

    public RectTransform GetRect();
}