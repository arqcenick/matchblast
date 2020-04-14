using System.Collections.Generic;
using Game.Behaviours;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class MoveCounterUI : MonoBehaviour
    {
        private BoardBehaviour _boardBehaviour;

        [SerializeField] private TextMeshProUGUI _counter;

        private void Awake()
        {
            _boardBehaviour = FindObjectOfType<BoardBehaviour>();
            _boardBehaviour.onLevelStarted += InitializeText;
            _boardBehaviour.onPlayerMoved += SetCount;
        }

        private void InitializeText(Dictionary<TileColor, int> objectives, int moveCount)
        {
            _counter.SetText(moveCount.ToString());
        }

        private void SetCount(int count)
        {
            _counter.SetText(count.ToString());
        }

        private void OnDestroy()
        {
            _boardBehaviour.onLevelStarted -= InitializeText;
            _boardBehaviour.onPlayerMoved -= SetCount;
        }
    }
}