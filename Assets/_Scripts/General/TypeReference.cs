using System;
using UnityEngine;

[System.Serializable]
public class TypeReference
{
    public string typeName;

    public Type GetTypeFromName()
    {
        return string.IsNullOrEmpty(typeName) ? null : Type.GetType(typeName);
    }

    public bool IsOfSameType(GameObject gameObject) {
        Type type = GetTypeFromName();
        if (type == null)
        {
            return false;
        }

        Component component = gameObject.GetComponent(type);
        return component != null;
    }
}
