using System;
using System.Reflection;
using UnityEngine;

namespace Game.Behaviours
{
    public class DebugBehaviour : MonoBehaviour
    {
        private bool _enabled;
        private float _timer;
        private void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                _timer += Time.deltaTime;
                if (_timer > 8f)
                {
                    _enabled = !_enabled;
                    _timer = 0f;
                }
            }
            else
            {
                _timer = 0;
            }
        }

        private void OnGUI()
        {

            if (_enabled)
            {
                            var lm = FindObjectOfType<LevelManager>();
                                            
                            FieldInfo levelField = lm.GetType().GetField(
                                "_currentLevel", BindingFlags.Instance | BindingFlags.NonPublic);
                            FieldInfo healthField = lm.GetType().GetField(
                                "_currentHealth", BindingFlags.Instance | BindingFlags.NonPublic);
                            if(GUI.Button(new Rect(10, 80, 50, 20), "Level+"))
                            {
                                int value =Mathf.Min((int) (levelField.GetValue(lm)) + 1, 4);
                                levelField.SetValue(lm, value);
                            }
                            if(GUI.Button(new Rect(70, 80, 50, 20), "Level-"))
                            {
                                int value =Mathf.Max((int) (levelField.GetValue(lm)) - 1, 0);
                                levelField.SetValue(lm, value);
                            }
                            if(GUI.Button(new Rect(10, 100, 50, 20), "Health+"))
                            {
                                int value =Mathf.Min((int) (healthField.GetValue(lm)) + 1, 5);
                                healthField.SetValue(lm, value);
                            }
                            if(GUI.Button(new Rect(70, 100, 50, 20), "Health-"))
                            {
                                int value =Mathf.Max((int) (healthField.GetValue(lm)) - 1, 0);
                                healthField.SetValue(lm, value);
                            }
                            
                            GUI.Label(new Rect(130, 80, 200, 50), "CurrentLevel: " + levelField.GetValue(lm).ToString());
                            GUI.Label(new Rect(130, 100, 200, 50), "CurrentHealth : " + healthField.GetValue(lm).ToString());
            }



        }
    }
}