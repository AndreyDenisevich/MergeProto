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

    private bool _inAnimationDelay = false;
    void Update()
    {
        elapsedtimeFromAttack += Time.deltaTime;
        if (!HaveEnemy())
            FindClosestEnemy();
        else
        { 
            float dist = Vector3.Distance(transform.position, _closestEnemy.transform.position);
            if (!_inAnimationDelay)
                SetForwardToEnemy();
            if (dist > attackDistance&&!_inAnimationDelay)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
                _animator.SetBool("Run",true);
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
        if (swordsmanOrSpearman == SwordsmanOrSpearman.spearman)
            _animator.SetTrigger("AttackSpear");
        else
        {
            if (Random.Range(0, 2) == 0)
                _animator.SetTrigger("AttackSword1");
            else
                _animator.SetTrigger("AttackSword2");
        }
        //StopAllCoroutines();
        StartCoroutine(AnimationDelay());
       // _closestEnemy.hp -= damage;
        elapsedtimeFromAttack = 0f;
    }

    private IEnumerator AnimationDelay()
    {
        _inAnimationDelay = true;
        yield return new WaitForSeconds(_animationDelay);
        _closestEnemy.hp -= damage;
        _inAnimationDelay = false;
    }

    public void Die()
    {
        StartCoroutine(DieDelay());
    }

    private IEnumerator DieDelay()
    {
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
    }
}
