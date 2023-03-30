#if UNITY_EDITOR

using Code.Enemy;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

[CustomEditor(typeof(Enemy))]
public class EnemyCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Open Enemy Editor"))
        {
            EnemyEditorWindow.Open((Enemy)target);
        }
    }
}
#endif
