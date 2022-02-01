using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCreature : Creature
{
    public float speed = 5f;
    public float attackDistance = 1.5f;

    // Update is called once per frame
    void Update()
    {
        elapsedtimeFromAttack += Time.deltaTime;
        if (!HaveEnemy())
            FindClosestEnemy();
        else
        { 
            float dist = Vector3.Distance(transform.position, _closestEnemy.transform.position);
            SetForwardToEnemy();
            if (dist > attackDistance)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
            }
            else
            {
                if (CanAttack())
                    Attack();
            }
        }
    }

    private void Attack()
    {
        _closestEnemy.hp -= damage;
        elapsedtimeFromAttack = 0f;
    }
}
