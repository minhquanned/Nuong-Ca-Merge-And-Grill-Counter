using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Scriptable Objects/SO_Item")]
public class SO_ItemBase : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string nameItem;
    [SerializeField] private string description;
    [SerializeField] private Sprite item2DImage;

    public int ID => id;
    public string Name => nameItem;
    public Sprite Item2DImage => item2DImage;

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Tìm tất cả assets kiểu SO_ItemBase trong project
        var allItems = UnityEditor.AssetDatabase.FindAssets("t:SO_ItemBase");

        foreach (var guid in allItems)
        {
            string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            SO_ItemBase otherItem = UnityEditor.AssetDatabase.LoadAssetAtPath<SO_ItemBase>(path);

            // Bỏ qua chính mình
            if (otherItem == this) continue;

            if (otherItem.id == this.id)
            {
                Debug.LogError($"Trùng ID {id} với item: {otherItem.name}", this);
            }
        }
    }
#endif
}
