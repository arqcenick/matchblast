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
            var b = (BoardSettings) target;
            if (b.Objectives == null)
                b.LoadSettings();
            //b.SetObjectives(new Dictionary<TileColor, int>());
            for (var i = 0; i < 5; i++)
            {
                var color = (TileColor) i;
                if (b.Objectives.ContainsKey(color))
                    b.Objectives[color] = EditorGUILayout.IntField(color + " Tiles:", b.Objectives[color]);
                else
                    b.Objectives.Add(color, EditorGUILayout.IntField(color + " Tiles:", 0));
            }

            if (GUILayout.Button("Save")) b.SaveSettings();

            b.SetObjectives(b.Objectives);
            DrawDefaultInspector();
        }

        private void OnValidate()
        {
            var b = (BoardSettings) target;
            b.SaveSettings();
        }
    }
}