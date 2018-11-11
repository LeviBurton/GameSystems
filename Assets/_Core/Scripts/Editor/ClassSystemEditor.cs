using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ClassSystem))]
public class ClassSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var classSystem = (ClassSystem)target;
        if (classSystem == null)
            return;

        EditorGUILayout.BeginVertical();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("classConfig"));


        if (classSystem.classConfig != null)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Description", classSystem.classConfig.Description);
            EditorGUILayout.EndHorizontal();

            if (classSystem.classRuntime != null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Runtime", classSystem.classRuntime.assetName);
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
