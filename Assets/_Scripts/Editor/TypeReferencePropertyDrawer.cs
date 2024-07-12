using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(TypeReference))]
public class TypeReferencePropertyDrawer : PropertyDrawer
{
    private const string DropdownNoneOption = "<None>";
    private static string[] typeOptions;

    // Static constructor to cache the type options
    static TypeReferencePropertyDrawer()
    {
        typeOptions = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => typeof(ItemUsable).IsAssignableFrom(t) && !t.IsAbstract)
            .Select(t => t.FullName)
            .Prepend(DropdownNoneOption)
            .ToArray();
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        
        SerializedProperty typeNameProperty = property.FindPropertyRelative("typeName");
        string typeName = typeNameProperty.stringValue;

        int selectedIndex = Array.IndexOf(typeOptions, typeName);
        if (selectedIndex == -1) selectedIndex = 0;

        selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, typeOptions);
        
        typeNameProperty.stringValue = selectedIndex > 0 ? typeOptions[selectedIndex] : null;

        EditorGUI.EndProperty();
    }
}
