using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCreature : Creature
{
    public float speed = 5f;
    public float attackDistance = 1.5f;
    public float attackDelay = 2f;
    [SerializeField]
    private Creature _closestEnemy = null;

    private float elapsedtimeFromAttack = 0f;
    void Start()
    {
        elapsedtimeFromAttack = attackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedtimeFromAttack += Time.deltaTime;
        if(_closestEnemy==null||_closestEnemy.gameObject == null)
        {
            if (enemyOrFriendly == EnemyOrFriendly.friendly)
                _closestEnemy = GameController.instance.GetClosestEnemy(transform.position);
            else _closestEnemy = GameController.instance.GetClosestFriendly(transform.position);
        }
        else
        { 
            float dist = Vector3.Distance(transform.position, _closestEnemy.transform.position);
            if (dist > attackDistance)
            {
                transform.forward = (_closestEnemy.transform.position - transform.position).normalized;
                transform.Translate(0, 0, speed * Time.deltaTime);
            }
            else
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        if (elapsedtimeFromAttack >= attackDelay) 
        {
            _closestEnemy.hp -= damage;
            elapsedtimeFromAttack = 0f;
        }
    }
}
