#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ExtendedEditorWindow : EditorWindow
{
    protected SerializedObject serializedObject;
    protected SerializedProperty serializedProperty;

    private string _selectedPropertyPath;
    protected SerializedProperty selectedProperty;

    protected void DrawProperties(SerializedProperty property, bool drawChildren)
    {
        string lastPropertyPath = string.Empty;
        foreach (SerializedProperty subproperty in property)
        {
            if (subproperty.isArray && subproperty.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUILayout.BeginHorizontal();
                subproperty.isExpanded = EditorGUILayout.Foldout(subproperty.isExpanded, subproperty.displayName);
                EditorGUILayout.EndHorizontal();

                if (subproperty.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DrawProperties(subproperty, drawChildren);
                    EditorGUI.indentLevel--;
                }
            }
            else
            {
                if(!string.IsNullOrEmpty(lastPropertyPath) && subproperty.propertyPath.Contains(lastPropertyPath)) continue;
                lastPropertyPath = subproperty.propertyPath;
                EditorGUILayout.PropertyField(subproperty, drawChildren);
            }
        }
    }

    protected void DrawSidebar(SerializedProperty property)
    {
        foreach (SerializedProperty subproperty in property)
        {
            if (GUILayout.Button(subproperty.displayName))
            {
                _selectedPropertyPath = subproperty.propertyPath;
            }

            if (!string.IsNullOrEmpty(_selectedPropertyPath))
            {
                selectedProperty = serializedObject.FindProperty(_selectedPropertyPath);
            }
        }
    }
    protected void DrawSidebar(Dictionary<string, SerializedProperty> options)
    {
        foreach (string option in options.Keys)
        {
            if (GUILayout.Button(option))
            {
                _selectedPropertyPath = options[option].propertyPath;
            }

            if (!string.IsNullOrEmpty(_selectedPropertyPath))
            {
                selectedProperty = serializedObject.FindProperty(_selectedPropertyPath);
            }
        }
    }

    protected void DrawField(string propertyName, bool relative)
    {
        if (relative && serializedProperty != null)
        {
            EditorGUILayout.PropertyField(serializedProperty.FindPropertyRelative(propertyName), true);
        }
        else
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty(propertyName), true);
        }
    }

    protected void Apply()
    {
        serializedObject.ApplyModifiedProperties();
    }
}

#endif
