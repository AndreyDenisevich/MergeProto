using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RangeCreature : Creature
{
    // Update is called once per frame
    public enum RangeType { archer, assasin, mage }
    [Inject]
    private PoolManager _poolManager;
    [SerializeField]
    private Transform _projectileSpawnPoint;
    [SerializeField]
    private RangeType type;

    private ObjectPool _connectedPool;

    private void Start()
    {
        _connectedPool = _poolManager.GetPool(type);
    }
    void Update()
    {
        elapsedtimeFromAttack += Time.deltaTime;
        FindClosestEnemy();
        if (HaveEnemy() && !_isDead)
        {
            SetForwardToEnemy();
            if (CanAttack())
                Attack();
        }
    }

    private void Attack()
    {
        StartCoroutine(AttackDelay());
        elapsedtimeFromAttack = 0f;
    }
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.5f));
        _animator.SetTrigger("Attack");
        Projectile newProjectile = _connectedPool.GetObjectFromPool();
        newProjectile.transform.position = _projectileSpawnPoint.position;
        newProjectile.transform.forward = _projectileSpawnPoint.forward;
        newProjectile.damage = damage;
        newProjectile.enemy = _closestEnemy;
    }
}
