using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{   
    public static SoundManager Instance;

    public SoundRandomizer MatchFailRandomizer => _matchFailRandomizer;

    public SoundRandomizer MatchTileRandomizer => _matchTileRandomizer;


    [SerializeField] private AudioSource _source;

    [SerializeField]
    private List<AudioClip> _matchTileSounds;

    private SoundRandomizer _matchTileRandomizer;


    [SerializeField]
    private List<AudioClip> _matchFailSounds;

    private SoundRandomizer _matchFailRandomizer;
    

    
    
    public void PlayRandomMatchSound()
    {
        _source.DOKill();
        _source.clip = _matchTileRandomizer.GetRandomClip();

        _source.volume = 0.5f;
        _source.Play();
        _source.DOFade(0.0f, 0.1f);
    }
    
    public void PlayRandomFailSound()
    {
        _source.clip = _matchFailRandomizer.GetRandomClip();
        _source.volume = 0.5f;
        _source.Play();
        _source.DOFade(0.0f, 0.1f);
    }
    
    private void Awake()
    {
        Instance = this;
        _matchFailRandomizer = new SoundRandomizer(_matchFailSounds);
        _matchTileRandomizer = new SoundRandomizer(_matchTileSounds);
    }

    public class SoundRandomizer
    {
        private List<AudioClip> _audioClips;
        private int _lastIndex;
        public SoundRandomizer(List<AudioClip> clips)
        {
            _audioClips = clips;
            _lastIndex = -1;
        }

        public AudioClip GetRandomClip()
        {
            int resultingIndex;
            do
            {
                resultingIndex = Random.Range(0, _audioClips.Count);

            } while (resultingIndex == _lastIndex);

            _lastIndex = resultingIndex;
            return _audioClips[_lastIndex];
        }
    }

    public void Mute(bool isMute)
    {
        _source.enabled = !isMute;
    }
}
