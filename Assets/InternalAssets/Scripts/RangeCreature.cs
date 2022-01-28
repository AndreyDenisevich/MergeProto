using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCreature : Creature
{
    // Update is called once per frame
    [SerializeField]
    private Projectile _projectilePrefab;
    void Update()
    {
        elapsedtimeFromAttack += Time.deltaTime;
        if (!HaveEnemy())
            FindClosestEnemy();
        else
        {
            SetForwardToEnemy();
            if (CanAttack())
                Attack();
        }
    }

    private void Attack()
    {
        Projectile newProjectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
        newProjectile.damage = damage;
        newProjectile.enemy = _closestEnemy;
        elapsedtimeFromAttack = 0f;
    }

}
