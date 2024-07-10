using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/new Inventory Item")]
public class InventoryItemSO : ScriptableObject
{
    public bool isStackable;
    public Sprite sprite;
}
