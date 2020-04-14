using Game.Behaviours;
using UnityEngine;

[RequireComponent(typeof(LevelManager))]
public class LevelManagerUI : MonoBehaviour
{
    private LevelManager _levelManager;

    private void Awake()
    {
        _levelManager = GetComponent<LevelManager>();
    }

    public void StartLevel()
    {
        _levelManager.StartLevel();
    }
}