using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
using Game.Behaviours;



[ExecuteInEditMode]
public class PrefabAccessor : MonoBehaviour
{
    
    public static PrefabAccessor Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PrefabAccessor>();
            }
            return instance;
        }
    }

    private static PrefabAccessor instance;


    public List<TileBehaviour> Prefabs => _prefabs;
    public List<Sprite> PowerUpSprites => _powerUpSprites;
    
    [SerializeField]


    private List<TileBehaviour> _prefabs;
    
    [SerializeField]
    private List<Sprite> _powerUpSprites;

}
