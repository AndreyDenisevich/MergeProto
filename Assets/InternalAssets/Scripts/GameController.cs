using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using Lean.Common;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;

    [SerializeField]
    private LeanPlane _plane;
    [SerializeField]
    private Creature[] _creaturePrefabs;
    void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        
    }
    void Update()
    {
       
    }

    public Creature GetCreaturePrefab(int level)
    {
        return _creaturePrefabs[level];
    }

    public Vector3 GetClosestPos(Vector3 pos)
    {
        return _plane.GetClosest(pos);
    }

    public Vector2 GetGridSnap()
    {
        return new Vector2(_plane.SnapX, _plane.SnapY);
    }
}
