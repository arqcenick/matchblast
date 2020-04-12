using System;
using System.Collections;
using System.Collections.Generic;
using Game.Behaviours;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardPersistenceBehaviour))]
public class BoardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BoardPersistenceBehaviour b = target as BoardPersistenceBehaviour;
        
        if (GUILayout.Button("Generate Random Level"))
        {
            b.CreateRandomBoard();
        }
        
        if (GUILayout.Button("Save Level"))
        {
            b.SaveBoard();
        }
        
        if (GUILayout.Button("Load Level"))
        {
            b.LoadBoard();
        }
        
        if (GUILayout.Button("Clear Level"))
        {
            b.ClearBoard();
        }

        

        DrawDefaultInspector();

    }
}
