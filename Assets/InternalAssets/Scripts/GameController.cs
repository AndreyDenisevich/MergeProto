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
    private ParticleSystem _mergeParticles;
    [SerializeField]
    private ParticleSystem[] _confetties;

    [SerializeField]
    private Creature[] _meleeCreaturePrefabs;
    [SerializeField]
    private Creature[] _rangeCreaturePrefabs;

    [SerializeField]
    private List<Creature> _friendlyCreatures;
    [SerializeField]
    private List<Creature> _enemyCreatures;

    private List<Creature> _creaturesToDestroy = new List<Creature>();

    private bool _isBattleStarted = false;

    private int _coinsCount = 10000;
    [SerializeField]
    private int _meleeCost = 100;
    [SerializeField]
    private int _rangeCost = 100;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        ActivateAnimators();
    }

    public void StartFight()
    {
        _isBattleStarted = true;
        ActivateCreatures();
    }

    private void ActivateAnimators()
    {
        foreach(Creature creature in _enemyCreatures)
        {
            StartCoroutine(RandomDelayForAnimator(creature.GetComponent<Animator>()));
        }
    }

    private IEnumerator RandomDelayForAnimator(Animator animator)
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        animator.enabled = true;
    }
    private void Win()
    {
        _isBattleStarted = false;
        UIManager.instance.Win();
        PlayWinAnimation(_friendlyCreatures);
        PlayConfetties();
    }

    private void Lose()
    {
        _isBattleStarted = false;
        UIManager.instance.Lose();
        PlayWinAnimation(_enemyCreatures);
        PlayConfetties();
    }

    private void PlayConfetties()
    {
        for(int i = 0;i<_confetties.Length;i++)
        {
            _confetties[i].gameObject.SetActive(true);
        }
    }
    private void PlayWinAnimation(List<Creature> creatures)
    {
        foreach (Creature creature in creatures)
            creature.Win();
    }
    void Update()
    {
        if (_isBattleStarted)
        {
            if (_enemyCreatures.Count > 0)
                foreach (Creature creature in _enemyCreatures)
                {
                    if (creature.hp <= 0)
                        _creaturesToDestroy.Add(creature);
                }
            else
                Win();
            foreach (Creature creature in _creaturesToDestroy)
            {
                creature.Die();
                _enemyCreatures.Remove(creature);
            }
            _creaturesToDestroy.Clear();

            if (_friendlyCreatures.Count > 0)
                foreach (Creature creature in _friendlyCreatures)
                {
                    if (creature.hp <= 0)
                        _creaturesToDestroy.Add(creature);
                }
            else
                Lose();
            foreach (Creature creature in _creaturesToDestroy)
            {
                creature.Die();
                _friendlyCreatures.Remove(creature);
            }
            _creaturesToDestroy.Clear();
        }
    }

    public void SpawnMelee()
    {
        if (_coinsCount >= _meleeCost)
        {
            Vector3 freePos;
            if (FindFreePosition(out freePos))
            {
                Creature creature = Instantiate(_meleeCreaturePrefabs[0], freePos, Quaternion.identity);
                _friendlyCreatures.Add(creature);
            }
            _coinsCount -= _meleeCost;
            _meleeCost += 10;
        }
    }

    public void SpawnRange()
    {
        if (_coinsCount >= _rangeCost)
        {
            Vector3 freePos;
            if (FindFreePosition(out freePos))
            {
                Creature creature = Instantiate(_rangeCreaturePrefabs[0], freePos, Quaternion.identity);
                _friendlyCreatures.Add(creature);
            }
            _coinsCount -= _rangeCost;
            _rangeCost += 10;
        }
    }

    private bool FindFreePosition(out Vector3 pos)
    {
        bool isAnyFree = false;
        pos = Vector3.zero;
        for (float x = _plane.MinX; x <= _plane.MaxX; x += _plane.SnapX)
        {
            for (float z = _plane.MinY; z <= _plane.MaxY; z += _plane.SnapY) 
            {
                if(CheckPosition(new Vector3(x,1f,z)))
                {
                    pos = new Vector3(x, 1f, z);
                    isAnyFree = true;
                    break;
                }
            }
            if (isAnyFree)
                break;
        }
        return isAnyFree;
    }

    private bool CheckPosition(Vector3 pos)
    {
        RaycastHit[] hits = Physics.BoxCastAll(pos, new Vector3(_plane.SnapX / 2f, 0f, _plane.SnapY / 2f), Vector3.forward, Quaternion.Euler(0, 0, 0), 0);
        return hits.Length == 0;
    }

    private void ActivateCreatures()
    {
        foreach(Creature creature in _enemyCreatures)
        {
            creature.enabled = true;
            creature.gameObject.layer = 0;
        }
        foreach (Creature creature in _friendlyCreatures)
        {
            creature.enabled = true;
            creature.gameObject.layer = 0;
        }
    }

    public void MergeCreatures(Creature newCreature,Creature creature1,Creature creature2)
    {
        _friendlyCreatures.Remove(creature1);
        _friendlyCreatures.Remove(creature2);
        _friendlyCreatures.Add(newCreature);
    }
    public void PlayMergeParticles(Vector3 position)
    {
        _mergeParticles.transform.position = position;
        _mergeParticles.Play();
    }
    public Creature GetMeleeCreaturePrefab(int level)
    {
        return _meleeCreaturePrefabs[level];
    }

    public Creature GetRangeCreaturePrefab(int level)
    {
        return _rangeCreaturePrefabs[level];
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

    public int coins
    {
        get
        {
            return _coinsCount;
        }
    }
    public int meleeCost
    {
        get
        {
            return _meleeCost;
        }
    }
    public int rangeCost
    {
        get
        {
            return _rangeCost;
        }
    }
}
