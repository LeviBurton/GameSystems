using UnityEditor;
using UnityEngine;
using System;

// Place this file inside Assets/Editor
[CustomEditor(typeof(SaveGameIdSystem))]
public class SaveGameIdEditor : Editor
{
    private SerializedProperty saveGameId;

    private void OnEnable()
    {
        saveGameId = serializedObject.FindProperty("SaveGameId");
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate Id"))
        {
            saveGameId.stringValue = Guid.NewGuid().ToString();
            Undo.RecordObject(target, "Generated new Id");
        }

        serializedObject.ApplyModifiedProperties();
    }
}