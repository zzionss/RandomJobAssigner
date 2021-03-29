using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

[CustomPropertyDrawer(typeof(Job))]
public class JobDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Job target = null;

        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);
        
        // Draw label
        //position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        float nameWidth = 100;
        float padding = 10;
        Rect nameRect = new Rect(position.x, position.y, nameWidth, position.height);
        Rect countRect = new Rect(position.x + nameWidth +padding, position.y, position.width - nameWidth - padding * 2, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative(nameof(target.name)), GUIContent.none);
        EditorGUI.PropertyField(countRect, property.FindPropertyRelative(nameof(target.count)), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();

    }
}
