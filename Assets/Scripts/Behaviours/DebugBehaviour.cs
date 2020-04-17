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
                if (_timer > 1f)
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
            Vector2 nativeSize = new Vector2(450, 900);

            int x1 = Mathf.FloorToInt(10 * Screen.width / nativeSize.x);
            int x2 = Mathf.FloorToInt(70 * Screen.width / nativeSize.x);
            int x3 = Mathf.FloorToInt(130 * Screen.width / nativeSize.x);

            int y1 = Mathf.FloorToInt(80 * Screen.height / nativeSize.y);;
            int y2 = Mathf.FloorToInt(100 * Screen.height / nativeSize.y);;
            int y3 = Mathf.FloorToInt(120 * Screen.height / nativeSize.y);;


            int w1 =  Mathf.FloorToInt(50 * Screen.width / nativeSize.x);;
            int w2 =  Mathf.FloorToInt(200 * Screen.width / nativeSize.x);;

            int h1 = Mathf.FloorToInt(20 * Screen.height / nativeSize.y);;;

            GUIStyle buttonStyle = GUI.skin.GetStyle("Button");
            GUIStyle labelStyle = GUI.skin.GetStyle("Label");
            buttonStyle.fontSize =  Mathf.FloorToInt( 12 * Screen.width / nativeSize.x);
            labelStyle.fontSize = buttonStyle.fontSize;
            if (_enabled)
            {
                            var lm = FindObjectOfType<LevelManager>();
                                            
                            
                            
                            
                            FieldInfo levelField = lm.GetType().GetField(
                                "_currentLevel", BindingFlags.Instance | BindingFlags.NonPublic);
                            FieldInfo healthField = lm.GetType().GetField(
                                "_currentHealth", BindingFlags.Instance | BindingFlags.NonPublic);
                            FieldInfo starField = lm.GetType().GetField(
                                "_currentStars", BindingFlags.Instance | BindingFlags.NonPublic);
                            if(GUI.Button(new Rect(x1, y1, w1, h1), "Level+", buttonStyle))
                            {
                                int value =Mathf.Min((int) (levelField.GetValue(lm)) + 1, 4);
                                levelField.SetValue(lm, value);
                            }
                            if(GUI.Button(new Rect(x2, y1, w1, h1), "Level-", buttonStyle))
                            {
                                int value =Mathf.Max((int) (levelField.GetValue(lm)) - 1, 0);
                                levelField.SetValue(lm, value);
                            }
                            if(GUI.Button(new Rect(x1, y2, w1, h1), "Health+", buttonStyle))
                            {
                                int value =Mathf.Min((int) (healthField.GetValue(lm)) + 1, 5);
                                healthField.SetValue(lm, value);
                            }
                            if(GUI.Button(new Rect(x2, y2, w1, h1), "Health-", buttonStyle))
                            {
                                int value =Mathf.Max((int) (healthField.GetValue(lm)) - 1, 0);
                                healthField.SetValue(lm, value);
                            }
                            if(GUI.Button(new Rect(x1, y3, 2.2f*w1 , h1), "Clear PlayerPrefs", buttonStyle))
                            {
                                PlayerPrefs.SetInt("init", 0);
                                healthField.SetValue(lm, 5);
                                levelField.SetValue(lm, 0);
                                starField.SetValue(lm, 0);
                                lm.LoadMainMenu();
                                
                            }
                            
                            
                            
                            GUI.Label(new Rect(x3, y1, w2, h1), "CurrentLevel: " + levelField.GetValue(lm).ToString(), labelStyle);
                            GUI.Label(new Rect(x3, y2, w2, h1), "CurrentHealth : " + healthField.GetValue(lm).ToString(), labelStyle);
            }



        }
    }
}