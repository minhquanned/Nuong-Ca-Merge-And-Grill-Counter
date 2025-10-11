using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIDragHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool isPause;
    private Vector2 offset;

    private RectTransform rect;

    public UnityEvent OnPointerDownEvent;
    public UnityEvent OnPointerUpEvent;

    void Awake()
    {
        rect = transform as RectTransform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isPause) return;
        rect.position = eventData.position - offset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(isPause) return;
        OnPointerDownEvent?.Invoke();

        offset = eventData.position - (Vector2)rect.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(isPause) return;
        OnPointerUpEvent?.Invoke();

        offset = Vector2.zero;
    }

    public void Resume()
    {
        isPause = false;
    }

    public void Pause()
    {
        isPause = transform;
    }
}
