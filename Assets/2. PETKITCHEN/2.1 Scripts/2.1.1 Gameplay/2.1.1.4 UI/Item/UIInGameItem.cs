using UnityEngine;
using UnityEngine.UI;

public enum EInGameItemType
{
    CanDragable,
    CanNotDragable,
}

public class UIInGameItem : UIRectranform, IItem
{
    private UIDragHandler uIDragHandler;
    private DropItemController dropItemController;

    private ICanPutItem latestSeat;
    private ICanPutItem currentSeat;

    private SO_ItemBase itemDatabase;

    [SerializeField] private Image itemImage;

    protected override void Awake()
    {
        base.Awake();

        uIDragHandler = GetComponent<UIDragHandler>();
    }

    public void AssignManager(DropItemController dropItemController)
    {
        this.dropItemController = dropItemController;
    }

    public void UpdateData(SO_ItemBase itemData)
    {
        itemDatabase = itemData;
        if (itemImage != null)
        {
            this.itemImage.sprite = itemData.Item2DImage;
        }
    }

    public void AllowCanDragable(bool isAllow)
    {
        if (isAllow)
        {
            uIDragHandler.Resume();
        }
        else
        {
            uIDragHandler.Pause();
        }
    }

    void OnEnable()
    {
        uIDragHandler.OnPointerDownEvent.AddListener(OnStartDrag);
        uIDragHandler.OnPointerUpEvent.AddListener(OnDropItem);
    }

    void OnDisable()
    {
        uIDragHandler.OnPointerDownEvent.RemoveListener(OnStartDrag);
        uIDragHandler.OnPointerUpEvent.RemoveListener(OnDropItem);
    }

    private void OnStartDrag()
    {
        dropItemController.StartDragItem(this);
    }

    private void OnDropItem()
    {
        dropItemController.DropItem(this);
    }

    #region Public Methods

    public void SetICanPutItem(ICanPutItem canPutItem)
    {
        latestSeat = currentSeat;
        currentSeat = canPutItem;
    }

    public ICanPutItem GetCurrentICanPutItem()
    {
        return currentSeat;
    }

    public int GetID()
    {
        if (itemDatabase != null)
            return itemDatabase.ID;
        return -1;
    }

    public void ItemIsPutedTo(RectTransform rect, bool isStrectFull)
    {
        _rectTransform?.SetParent(rect);

        if (isStrectFull)
        {
            AnchorCenter();
            SetWidthHeight(rect.sizeDelta.x, rect.sizeDelta.y);
        }
        else
        {
            SetWidthHeight(_rectTransform.sizeDelta.x * 1.2f, _rectTransform.sizeDelta.y * 1.2f);
        }

        _rectTransform.localScale = Vector3.one;
    }

    public void TeleportToCurrentSeat()
    {
        ItemIsPutedTo(currentSeat.GetRect(), true);
    }    

    #endregion
}
