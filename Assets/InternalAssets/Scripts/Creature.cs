using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public enum EnemyOrFriendly { friendly,enemy};
    public int level = 1;
    public int hp = 100;
    public int damage = 30;
    public float attackDelay = 2f;
    public EnemyOrFriendly enemyOrFriendly;

    protected float elapsedtimeFromAttack = 0f;
    protected Creature _closestEnemy = null;

    private void Start()
    {
        elapsedtimeFromAttack = attackDelay;
    }

    protected void FindClosestEnemy()
    {
            if (enemyOrFriendly == EnemyOrFriendly.friendly)
                _closestEnemy = GameController.instance.GetClosestEnemy(transform.position);
            else _closestEnemy = GameController.instance.GetClosestFriendly(transform.position);
    }

    protected void SetForwardToEnemy()
    {
        transform.forward = (_closestEnemy.transform.position - transform.position).normalized;
    }

    protected bool CanAttack()
    {
        return elapsedtimeFromAttack >= attackDelay;
    }
    protected bool HaveEnemy()
    {
        return _closestEnemy != null;
    }
}
