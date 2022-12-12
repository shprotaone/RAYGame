using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Line))]
public class EditorLineCreator : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        //EditorGUILayout элементы
        Line lineCreator = (Line)target;

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace(); 
        Gizmos.color = Color.green;

        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

    }
}
