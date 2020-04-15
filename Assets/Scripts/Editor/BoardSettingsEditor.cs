using Game.Behaviours;
using UnityEditor;
using UnityEngine;
using UnityEngine.WSA;

namespace Game.Data
{
    [CustomEditor(typeof(BoardSettings))]
    public class BoardSettingsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            
            var b = (BoardSettings) target;
            try
            {
                //b.SetObjectives(new Dictionary<TileColor, int>());
                for (var i = 0; i < (int) TileColor.Count; i++)
                {
                    var color = (TileColor) i;
                    if (b.Objectives.ContainsKey(color))
                        b.Objectives[color] = EditorGUILayout.IntField(color + " Tiles:", b.Objectives[color]);
                    else
                        b.Objectives.Add(color, EditorGUILayout.IntField(color + " Tiles:", 0));
                }


                for (var i = 0; i < (int) TileColor.Count; i++)
                {
                    if (b.Available.Count <= i)
                    {
                        b.Available.Add(false);
                    }

                    b.Available[i] = EditorGUILayout.Toggle(((TileColor) i).ToString(), b.Available[i]);
                }



                b.SetAvailable(b.Available);
                b.SetObjectives(b.Objectives);
                DrawDefaultInspector();
                if (GUILayout.Button("Save"))
                {
                    b.SaveSettings();
                }
                if (GUILayout.Button("Load"))
                {
                    b.LoadSettings();
                }
            }
            catch
            {
                b.LoadSettings();
            }

            

        }
        
    }
}