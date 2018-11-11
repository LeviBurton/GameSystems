using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActionListDb))]
public class ActionListDbEditor : Editor
{
    private SerializedProperty assetList;

    private void OnEnable()
    {
        assetList = serializedObject.FindProperty("items");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var db = (ActionListDb)target;

        if (GUILayout.Button("Clear List"))
        {
            Undo.RecordObject(target, "Clear List");
            db.items.Clear();
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }

        if (GUILayout.Button("Rebuild List"))
        {
            db.items.Clear();

            Undo.RecordObject(target, "Rebuild List");

            var guids = AssetDatabase.FindAssets("t:ActionConfig", null);
            foreach (var guid in guids)
            {
                var asset = AssetDatabase.LoadAssetAtPath<ActionConfig>(AssetDatabase.GUIDToAssetPath(guid));
                db.items.Add(asset);
            }

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }

        EditorGUILayout.PropertyField(assetList, new GUIContent("Assets"), true);
    }
}
