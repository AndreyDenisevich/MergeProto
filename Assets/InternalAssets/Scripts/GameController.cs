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

    [SerializeField]
    private List<Creature> _friendlyCreatures;
    [SerializeField]
    private List<Creature> _enemyCreatures;

    private List<Creature> _creaturesToDestroy = new List<Creature>();

    private bool _isBattleStarted = false;
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
        if(Input.GetKeyDown(KeyCode.Space)&&!_isBattleStarted)
        {
            _isBattleStarted = true;
            ActivateCreatures();
        }
        if (_isBattleStarted)
        {
            if (_enemyCreatures.Count > 0)
                foreach (Creature creature in _enemyCreatures)
                {
                    if (creature.hp <= 0)
                        _creaturesToDestroy.Add(creature);
                }
            foreach (Creature creature in _creaturesToDestroy)
            {
                Destroy(creature.gameObject);
                _enemyCreatures.Remove(creature);
            }
            _creaturesToDestroy.Clear();

            if (_friendlyCreatures.Count > 0)
                foreach (Creature creature in _friendlyCreatures)
                {
                    if (creature.hp <= 0)
                        _creaturesToDestroy.Add(creature);
                }
            foreach (Creature creature in _creaturesToDestroy)
            {
                Destroy(creature.gameObject);
                _friendlyCreatures.Remove(creature);
            }
            _creaturesToDestroy.Clear();
        }
    }

    private void ActivateCreatures()
    {
        foreach(Creature creature in _enemyCreatures)
        {
            creature.enabled = true;
        }
        foreach (Creature creature in _friendlyCreatures)
        {
            creature.enabled = true;
        }
    }

    public void MergeCreatures(Creature newCreature,Creature creature1,Creature creature2)
    {
        _friendlyCreatures.Remove(creature1);
        _friendlyCreatures.Remove(creature2);
        _friendlyCreatures.Add(newCreature);
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

    public Creature GetClosestEnemy(Vector3 pos)
    {
        return GetClosestCreature(pos, _enemyCreatures);
    }
    public Creature GetClosestFriendly(Vector3 pos)
    {
        return GetClosestCreature(pos, _friendlyCreatures);
    }
    private Creature GetClosestCreature(Vector3 pos,List<Creature> creatures)
    {
        Creature closestCreature = null;
        float minDistance = -1f;
        if (creatures.Count > 0)
        {
            foreach (Creature creature in creatures)
            {
                float dist = Vector3.Distance(pos, creature.transform.position);
                if (minDistance > dist || minDistance<0)
                {
                    minDistance = dist;
                    closestCreature = creature;
                }
            }
        }
        return closestCreature;
    }
}
