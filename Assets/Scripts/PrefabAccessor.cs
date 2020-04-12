using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

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
    
    [SerializeField]
    private List<TileBehaviour> _prefabs;

}
