using System;
using System.Collections;
using System.Collections.Generic;
using Game.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class GoalCounterUIBehaviour : MonoBehaviour
    {

        [SerializeField]
        private List<GoalCounterElement> _goalCounters;

        private Dictionary<TileColor, int> _objectives;
        
        public void SetObjectives(Dictionary<TileColor, int> objectives)
        {
            _objectives = objectives;
            foreach (var kv in _objectives)
            {
                SetObjective(kv.Key, kv.Value);
            }
        }
        
        private BoardBehaviour _board;
        private void SetObjective(TileColor color, int count)
        {
            if (count != 0)
            {
                var element = _goalCounters[(int) color];
                element.gameObject.SetActive(true);
                element.SetTileColor(color);
                element.SetTileCount(count);
            }

        }

        private void ProcessTileDestroy(TileBehaviour tile)
        {
            if (tile.ColorIndex != TileColor.None)
            {
                _objectives[tile.ColorIndex] -= 1;
                SetObjective(tile.ColorIndex, _objectives[tile.ColorIndex]);
                if (_objectives[tile.ColorIndex] <= 0)
                {
                    _goalCounters[(int) tile.ColorIndex].gameObject.SetActive(false);
                }
            }

        }
        
        private void Awake()
        {
            _board = FindObjectOfType<BoardBehaviour>();
            _board.onTileDestroyed += ProcessTileDestroy;
            _board.onLevelStarted += SetObjectives;
        }
        
        
    }
}


