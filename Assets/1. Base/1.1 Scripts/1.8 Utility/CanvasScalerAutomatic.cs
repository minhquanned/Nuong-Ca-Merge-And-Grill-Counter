using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasScaler))]
public class CanvasScalerAutomatic : MonoBehaviour
{
    private CanvasScaler canvasScaler;
    
    [SerializeField] private Vector2 referenceResolution = new Vector2(1920, 1080); // Độ phân giải tham chiếu
    [SerializeField] private float matchWidthOrHeight = 0.5f; // Giá trị từ 0 (width) đến 1 (height)
    
    private void Awake()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        ApplyScaling();
    }

    private void ApplyScaling()
    {
        if (canvasScaler == null) return;

        // Thiết lập chế độ scale
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        
        // Gán độ phân giải tham chiếu
        canvasScaler.referenceResolution = referenceResolution;
        
        // Thiết lập matchWidthOrHeight (0 = width, 1 = height, 0.5 = cân bằng)
        canvasScaler.matchWidthOrHeight = Mathf.Clamp01(matchWidthOrHeight);
    }

    // Hàm public để điều chỉnh matchWidthOrHeight từ ngoài
    public void SetMatchWidthOrHeight(float value)
    {
        matchWidthOrHeight = Mathf.Clamp01(value);
        ApplyScaling();
    }

    // Hàm public để thay đổi độ phân giải tham chiếu
    public void SetReferenceResolution(Vector2 resolution)
    {
        referenceResolution = resolution;
        ApplyScaling();
    }

#if UNITY_EDITOR
    // Tự động cập nhật trong Editor để xem trước
    private void OnValidate()
    {
        if (canvasScaler == null)
            canvasScaler = GetComponent<CanvasScaler>();
        ApplyScaling();
    }
#endif
}