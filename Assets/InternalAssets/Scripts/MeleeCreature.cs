using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCreature : Creature
{
    public enum SwordsmanOrSpearman { swordsman,spearman };
    public float speed = 5f;
    public float attackDistance = 1.5f;
    public SwordsmanOrSpearman swordsmanOrSpearman;

    private float _animationDelay = 0.5f;

    private bool _inAttack = false;

    void Update()
    {
        elapsedtimeFromAttack += Time.deltaTime;
        if (!_inAttack)
            FindClosestEnemy();
    }
    private void FixedUpdate()
    {
        ZeroVelocity();
        if (HaveEnemy() && !_isDead)
        {
            float dist = Vector3.Distance(transform.position, _closestEnemy.transform.position);
            if (!_inAttack)
                SetForwardToEnemy();
            if (dist > attackDistance && !_inAttack)
            {
                transform.Translate(0, 0, speed * Time.fixedDeltaTime);
                _animator.SetBool("Run", true);
            }
            else
            {
                _animator.SetBool("Run", false);
                if (CanAttack())
                    Attack();
            }
        }
        
    }

    private void Attack()
    {
        StartCoroutine(AttackDelay());
        elapsedtimeFromAttack = 0f;
    }

    private IEnumerator AttackDelay()
    {
        _inAttack = true;
        yield return new WaitForSeconds(Random.Range(0f,0.5f));
        if (swordsmanOrSpearman == SwordsmanOrSpearman.spearman)
            _animator.SetTrigger("AttackSpear");
        else
        {
            if (Random.Range(0, 2) == 0)
                _animator.SetTrigger("AttackSword1");
            else
                _animator.SetTrigger("AttackSword2");
        }
        yield return new WaitForSeconds(_animationDelay);
        if (_closestEnemy != null)
        {
            _closestEnemy.Hit();
            _closestEnemy.hp -= damage;
        }
        _inAttack = false;
    }
}
