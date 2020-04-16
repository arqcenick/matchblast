using System.Collections;
using System.Collections.Generic;
using Game.Behaviours;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PowerUp", menuName = "GameData/PowerUp", order = 1)]
    public class PowerUpData : ScriptableObject
    {
        public Sprite PowerUpIcon => _powerUpIcon;

        public Sprite MatchIcon => _matchIcon;

        public int ExplosionRadius => explosionRadius;

        public int MinRequiredMatchToOccur => _minRequiredMatchToOccur;

        public MatchType PowerUpType => _powerUpType;
        
        [SerializeField] private MatchType _powerUpType;
        [SerializeField] private int _minRequiredMatchToOccur;
        [SerializeField] private int explosionRadius;
        [SerializeField] private Sprite _matchIcon;
        [SerializeField] private Sprite _powerUpIcon;

    }
}


