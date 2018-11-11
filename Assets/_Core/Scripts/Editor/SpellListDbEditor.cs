using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpellListDb))]
public class SpellListDbEditor : Editor
{
    private SerializedProperty assetList;

    private void OnEnable()
    {
        assetList = serializedObject.FindProperty("items");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        var db = (SpellListDb)target;

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

            var guids = AssetDatabase.FindAssets("t:SpellConfig", null);
            foreach (var guid in guids)
            {
                var spell = AssetDatabase.LoadAssetAtPath<SpellConfig>(AssetDatabase.GUIDToAssetPath(guid));
                db.items.Add(spell);
            }
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }

        EditorGUILayout.PropertyField(assetList, new GUIContent("Assets"), true);
    }
}
