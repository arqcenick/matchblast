using System;
using System.Collections;
using System.Collections.Generic;
using Game.Behaviours;
using UnityEditor;
using UnityEngine;

namespace Game.Data
{
    [CustomEditor(typeof(BoardSettings))]
    public class BoardSettingsEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            BoardSettings b = (BoardSettings) target;
            if (b.Objectives == null)
            {
                b.LoadSettings();
                //b.SetObjectives(new Dictionary<TileColor, int>());
            }
            for (int i = 0; i < 5; i++)
            {
                TileColor color = (TileColor) i;
                if(b.Objectives.ContainsKey(color))
                {
                    b.Objectives[color] = EditorGUILayout.IntField(color +" Tiles:", b.Objectives[color]);
                }
                else
                {
                    b.Objectives.Add(color, EditorGUILayout.IntField(color+ " Tiles:", 0));

                }
            }
            
            b.SetObjectives(b.Objectives);
            DrawDefaultInspector();
        }

        private void OnValidate()
        {
            BoardSettings b = (BoardSettings) target;
            b.SaveSettings();
        }
    }

}
