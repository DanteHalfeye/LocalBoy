
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ShowOnlyAttribute))]
public class ShowOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Disable the GUI, so the property is not editable
        GUI.enabled = false;

        // Draw the property as usual
        EditorGUI.PropertyField(position, property, label);

        // Re-enable the GUI for other properties to be editable
        GUI.enabled = true;
    }
}
