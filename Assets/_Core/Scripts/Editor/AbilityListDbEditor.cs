using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbilityListDb))]
public class AbilityListDbEditor : Editor
{
    private SerializedProperty assetList;

    private void OnEnable()
    {
        assetList = serializedObject.FindProperty("items");
    }

    public override void OnInspectorGUI()
    {
        var db = (AbilityListDb)target;

        serializedObject.Update();

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

            var guids = AssetDatabase.FindAssets("t:AbilityConfig", null);
            foreach (var guid in guids)
            {
                var ability = AssetDatabase.LoadAssetAtPath<AbilityConfig>(AssetDatabase.GUIDToAssetPath(guid));
                db.items.Add(ability);
            }
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }

   
        EditorGUILayout.PropertyField(assetList, new GUIContent("Assets"), true);
    }
}
