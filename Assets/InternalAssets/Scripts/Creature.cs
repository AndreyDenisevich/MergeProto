using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public enum EnemyOrFriendly { friendly,enemy}
    public enum MeleeOrRange { melee,range}
    public int level = 1;
    public int hp = 100;
    public int damage = 30;
    public float attackDelay = 2f;
    public EnemyOrFriendly enemyOrFriendly;
    public MeleeOrRange meleeOrRange;

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
        //float angle = Vector3.Angle(transform.forward, enemyForward); 
        //StopAllCoroutines();
        //StartCoroutine(SetForward(angle));
    }

    protected bool CanAttack()
    {
        return elapsedtimeFromAttack >= attackDelay;
    }
    protected bool HaveEnemy()
    {
        return _closestEnemy != null;
    }
    private IEnumerator SetForward(float angle)
    {
        float progress = 0f, elapsedTime = 0f, duration = 1f;
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(startRot.eulerAngles.x, startRot.eulerAngles.y + angle, startRot.eulerAngles.z);
        while (progress <= 1f)
        {
            transform.rotation = Quaternion.Lerp(startRot, endRot, progress);
            elapsedTime += Time.unscaledDeltaTime;
            progress = elapsedTime / duration;
            yield return null;
        }
        transform.rotation = endRot;
    }
}
