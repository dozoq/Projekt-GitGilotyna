#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
 
[CustomPropertyDrawer(typeof(ComponentReferenceInspectorAttribute))]
public class ComponentReferenceInspectorPropertyDrawer : PropertyDrawer
{
 
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float h = EditorGUIUtility.singleLineHeight;
 
        if(property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue != null)
        {
            h += EditorGUIUtility.singleLineHeight;
            var so = new SerializedObject(property.objectReferenceValue);
            var iterator = so.GetIterator();
            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                h += EditorGUI.GetPropertyHeight(iterator, true);
            }
        }
        return h+15;
    }
 
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if(property.propertyType != SerializedPropertyType.ObjectReference)
        {
            EditorGUI.LabelField(position, "Mismatched PropertyDrawer.");
            return;
        }
 
        var obj = property.objectReferenceValue;
        var objRect = GetRect(ref position, EditorGUIUtility.singleLineHeight);
        EditorGUI.ObjectField(objRect, property, label);
 
        if(obj != null)
        {
            var headerRect = GetRect(ref position, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(headerRect, "Component Ref");
            var so = new SerializedObject(property.objectReferenceValue);
            // if (EditorGUI.LinkButton(
            //         new Rect(EditorGUIUtility.labelWidth, headerRect.y, EditorGUIUtility.fieldWidth*1.5f,
            //             EditorGUIUtility.singleLineHeight-1), "Read Docs"))
            // {
            //     
            // }
            var iterator = so.GetIterator();
            for (bool enterChildren = true; iterator.NextVisible(enterChildren); enterChildren = false)
            {
                float h = EditorGUI.GetPropertyHeight(iterator, true);
                Rect r = GetRect(ref position, h);
                EditorGUI.PropertyField(r, iterator);
            }
        }
    }
 
    private static Rect GetRect(ref Rect position, float height)
    {
        var result = new Rect(position.xMin, position.yMin, position.width, height);
        position = new Rect(position.xMin, position.yMin + height, position.width, Mathf.Max(0f, position.height - height));
        return result;
    }
 
}
#endif
