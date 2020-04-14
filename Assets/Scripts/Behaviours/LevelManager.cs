using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Data;
using Game.Events;
using Game.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Behaviours
{
    public static class PlayerServices
    {
        private static IPlayer _player;

        private static bool _initialized;

        public static void Initialize(IPlayer player)
        {
            _player = player;
            _initialized = true;
        }
        public static int GetPlayerHealth()
        {
            Debug.Assert(_initialized);
            return _player.GetHealth();
        }

        public static void SetPlayerHealth(int health)
        {
            Debug.Assert(_initialized);
            _player.SetHealth(health);
        }
    }

    public interface IPlayer
    {
        int GetHealth();

        void SetHealth(int health);
    }
    
    public class LevelManager : MonoBehaviour, IPlayer
    {
        
        [SerializeField]
        private List<BoardSettings> _levels;

        private int _currentLevel;
        private int _currentHealth;
        private int _currentStars;

        private DateTime _currentDate;
        private DateTime _lastDate;

        [SerializeField] private int _healthRegenIntervalSeconds = 3600;
        private TimeSpan _requiredSpan;

        public void StartLevel()
        {
            UIEvent<WillSceneChangeEvent>.Instance.Invoke();
            transform.DOMove(transform.position, 0.5f).OnComplete(() => { SceneManager.LoadScene(1);});
        }

        public void NextLevel()
        {
            _currentLevel++;
            StartLevel();
        }

        public void RestartLevel()
        {
            StartLevel();
        }

        public void MainMenu()
        {
            UIEvent<WillSceneChangeEvent>.Instance.Invoke();
            transform.DOMove(transform.position, 0.5f).OnComplete(() => { SceneManager.LoadScene(0);});
        }
        
        private void Awake()
        {

            PlayerServices.Initialize(this);
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            UIEvent<NextLevelEvent>.Instance.AddListener(NextLevel);
            UIEvent<RetryLevelEvent>.Instance.AddListener(RestartLevel);
            UIEvent<MainMenuEvent>.Instance.AddListener(MainMenu);

            
            
            DontDestroyOnLoad(gameObject);
            _requiredSpan = TimeSpan.FromSeconds(_healthRegenIntervalSeconds);
 
            _currentLevel = PlayerPrefs.GetInt("Level");
            _currentStars = PlayerPrefs.GetInt("Stars");
            _currentHealth =  PlayerPrefs.GetInt("Health");
            int initialized = PlayerPrefs.GetInt("init");

            if (initialized == 0)
            {
                _currentLevel = 0;
                _currentStars = 0;
                _currentHealth = 5;
            
                PlayerPrefs.SetInt("Level", _currentLevel);
                PlayerPrefs.SetInt("Stars", _currentStars);
                PlayerPrefs.SetInt("Health", _currentHealth);
                PlayerPrefs.SetInt("init", 1);
                PlayerPrefs.Save();
            }

            
     
        }
        
        private void Start()
        {
            _currentDate = DateTime.Now;
            try
            {
                long oldDate = Convert.ToInt64(PlayerPrefs.GetString("date_time"));
                _lastDate = DateTime.FromBinary(oldDate);
            }
            catch (Exception e)
            {
                _lastDate = DateTime.Now;
            }
            UIEvent<SceneReadyEvent>.Instance.Invoke();

        }
        
        private void Update()
        {
            TimeSpan span = DateTime.Now - _lastDate;
            int healthRegen = (int) Math.Floor(span.TotalMilliseconds / _requiredSpan.TotalMilliseconds);
            if (healthRegen >= 1)
            {
                AddHealth(healthRegen);
                _lastDate = DateTime.Now;
            }
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == 1)
            {
                FindObjectOfType<BoardPersistenceBehaviour>().SetBoardSetting(_levels[_currentLevel]);
                FindObjectOfType<BoardBehaviour>().onPlayerWin += OnPlayerWin;
                FindObjectOfType<BoardBehaviour>().onPlayerLose += OnPlayerLose;
            }
            
            UIEvent<SceneReadyEvent>.Instance.Invoke();
        }
        
        private void OnSceneUnloaded(Scene scene)
        {
            PlayerPrefs.SetInt("Level", _currentLevel);
            PlayerPrefs.SetInt("Stars", _currentStars);
            PlayerPrefs.SetInt("Health", _currentHealth);
            SimpleObjectPool.Reset();
            // if (scene.buildIndex == 1)
            // {
            //     FindObjectOfType<BoardBehaviour>().onPlayerWin -= OnPlayerWin;
            //     FindObjectOfType<BoardBehaviour>().onPlayerLose -= OnPlayerLose;
            // }
        }
        
        private void OnPlayerWin(int stars)
        {
            _currentStars += stars;
            _currentLevel++;
            
        }

        private void OnPlayerLose()
        {
            _currentHealth--;
        }

        private void OnApplicationQuit()
        {
            PlayerPrefs.SetString("date_time", DateTime.Now.ToBinary().ToString());
            PlayerPrefs.SetInt("Level", _currentLevel);
            PlayerPrefs.SetInt("Stars", _currentStars);
            PlayerPrefs.SetInt("Health", _currentHealth);
            PlayerPrefs.SetInt("init", 1);

        }
        
        private void AddHealth(int health)
        {
            //Play health gain animation
            SetHealth(Math.Min(5, _currentHealth + health));
        }

        public int GetHealth()
        {
            return _currentHealth;
        }

        public void SetHealth(int health)
        {
            _currentHealth = health;
        }
    }
    
    

}

