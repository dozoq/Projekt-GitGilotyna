#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Code.Enemy;
using UnityEditor;
using UnityEngine;

public class EnemyEditorWindow : ExtendedEditorWindow
{

    private Vector2 scrollPosition;
    public static void Open(Enemy enemy)
    {
        EnemyEditorWindow window = GetWindow<EnemyEditorWindow>("Enemy Editor");
        window.serializedObject = new SerializedObject(enemy);
    }

    private void OnGUI()
    {
        if(serializedObject == null) return;
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));

        Dictionary<string, SerializedProperty> options = new Dictionary<string, SerializedProperty>();
        options.Add("Enemy Shared Context", serializedObject.FindProperty("enemyCtx"));
        options.Add("Loot", serializedObject.FindProperty("lootBag"));

        DrawSidebar(options);
        
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, "box", GUILayout.ExpandHeight(true));
        DrawFieldOrArray();
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
        
        Apply();
    }

    private void DrawFieldOrArray()
    {
        if (selectedProperty == null) return;
        if (selectedProperty.isArray)
            DrawProperties(selectedProperty, true);
        else
            DrawField(selectedProperty.name, true);
    }

}
#endif
