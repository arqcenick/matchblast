using System;
using System.Collections;
using System.Collections.Generic;
using Game.Behaviours;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardBehaviour))]
public class BoardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BoardBehaviour b = target as BoardBehaviour;
        
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
        
    }
}
