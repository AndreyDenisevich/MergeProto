using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCreature : Creature
{
    // Update is called once per frame
    [SerializeField]
    private Projectile _projectilePrefab;
    [SerializeField]
    private Transform _projectileSpawnPoint;
    void Update()
    {
        elapsedtimeFromAttack += Time.deltaTime;
        FindClosestEnemy();
        if (HaveEnemy()&&!_isDead)  
        {
            SetForwardToEnemy();
            if (CanAttack())
                Attack();
        }
    }

    private void Attack()
    {
        StartCoroutine(AnimationDelay());
        elapsedtimeFromAttack = 0f;
    }
    private IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(0.25f);
        _animator.SetTrigger("Attack");
        Projectile newProjectile = Instantiate(_projectilePrefab, _projectileSpawnPoint.position, transform.rotation);
        newProjectile.damage = damage;
        newProjectile.enemy = _closestEnemy;
    }
}
