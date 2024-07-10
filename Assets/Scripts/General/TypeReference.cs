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
}
