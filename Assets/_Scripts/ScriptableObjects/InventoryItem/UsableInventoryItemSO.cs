using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/new Usable Inventory Item")]
public class UsableInventoryItemSO : InventoryItemSO
{
    public TypeReference usableType;
    
    private void OnValidate()
    {
        Type type = usableType.GetTypeFromName();
        if (type != null && !typeof(ItemUsable).IsAssignableFrom(type))
        {
            Debug.LogError($"{type} does not inherit from ItemUsable.");
            usableType.typeName = null;
        }
    }

    public ItemUsable CreateUsableInstance()
    {
        Type type = usableType.GetTypeFromName();
        if (type == null)
        {
            Debug.LogError("Usable type is not set or is invalid.");
            return null;
        }

        return Activator.CreateInstance(type) as ItemUsable;
    }
}
