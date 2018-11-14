using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ClassListDb))]
public class ClassListDbEditor : Editor
{
    private SerializedProperty assetList;

    private void OnEnable()
    {
        assetList = serializedObject.FindProperty("items");
    }

    public override void OnInspectorGUI()
    {
       // DrawDefaultInspector();
        var db = (ClassListDb)target;

        serializedObject.Update();

        if (GUILayout.Button("Clear List"))
        {
            Undo.RecordObject(target, "Clear List");
            db.items.Clear();
            serializedObject.ApplyModifiedProperties();
        }

        if (GUILayout.Button("Rebuild List"))
        {
            db.items.Clear();

            Undo.RecordObject(target, "Rebuild List");

            var guids = AssetDatabase.FindAssets("t:ClassConfig", null);
            foreach (var guid in guids)
            {
                var classConfig = AssetDatabase.LoadAssetAtPath<ClassConfig>(AssetDatabase.GUIDToAssetPath(guid));
                db.items.Add(classConfig);
            }

            serializedObject.ApplyModifiedProperties();
        }

        EditorGUILayout.PropertyField(assetList, new GUIContent("Assets"), true);
    }
}
