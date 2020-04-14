using Game.Behaviours;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoardPersistenceBehaviour))]
public class BoardEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var b = target as BoardPersistenceBehaviour;

        if (GUILayout.Button("Generate Random Level")) b.CreateRandomBoard();

        if (GUILayout.Button("Save Level")) b.SaveBoard();

        if (GUILayout.Button("Load Level")) b.LoadBoard();

        if (GUILayout.Button("Clear Level")) b.ClearBoard();


        DrawDefaultInspector();
    }
}