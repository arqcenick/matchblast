using System;
using System.Collections;
using System.Collections.Generic;
using Game.Behaviours;
using Game.Events;
using TMPro;
using UnityEngine;


namespace Game.UI
{
    public class PlayerCounterUI : OpenCloseUI
    {
        [SerializeField] private TextMeshProUGUI _health;
        [SerializeField] private TextMeshProUGUI _stars;
        [SerializeField] private TextMeshProUGUI _level;


        private void Awake()
        {
            UIEvent<HidePlayerCounters>.Instance.AddListener(Hide);
            UIEvent<ShowPlayerCounters>.Instance.AddListener(Show);
            UIEvent<ShowPlayerCounters>.Instance.AddListener(UpdateCounters);

        }

        private void Start()
        {
            UpdateCounters();
        }

        private void OnDestroy()
        {
            UIEvent<HidePlayerCounters>.Instance.RemoveListener(Hide);
            UIEvent<ShowPlayerCounters>.Instance.RemoveListener(Show);
            UIEvent<ShowPlayerCounters>.Instance.RemoveListener(UpdateCounters);
        }

        private void UpdateCounters()
        {
            _health.SetText(PlayerServices.GetPlayerHealth() + " X");
            _stars.SetText(PlayerServices.GetPlayerStars() + " X");
            _level.SetText("Level: " + (PlayerServices.GetCurrentLevel() + 1).ToString());
        }
    }
}
