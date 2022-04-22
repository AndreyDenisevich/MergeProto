using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Creature : MonoBehaviour
{
    [Inject]
    private GameController _gameController;

    public enum EnemyOrFriendly { friendly,enemy }
    public enum MeleeOrRange { melee,range}
    public int level = 1;
    public int hp = 100;
    public int damage = 30;
    public float attackDelay = 2f;
    public EnemyOrFriendly enemyOrFriendly;
    public MeleeOrRange meleeOrRange;

    protected float elapsedtimeFromAttack = 0f;
    protected bool _isDead = false;
    protected Creature _closestEnemy = null;
    protected Animator _animator;

    private HPBar _hpBar;
    [SerializeField]
    private ParticleSystem _bloodParticles;
    [SerializeField]
    private ParticleSystem _dieBloodParticles;

    private Rigidbody _rb;
    private void Awake()
    {
        _hpBar = GetComponentInChildren<HPBar>();
        _animator = GetComponent<Animator>();
        elapsedtimeFromAttack = attackDelay;
        _rb = GetComponent<Rigidbody>();
    }

    protected void ZeroVelocity()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }

    protected void FindClosestEnemy()
    {
            if (enemyOrFriendly == EnemyOrFriendly.friendly)
                _closestEnemy = _gameController.GetClosestEnemy(transform.position);
            else _closestEnemy = _gameController.GetClosestFriendly(transform.position);
    }

    protected void SetForwardToEnemy()
    {
        transform.forward = Vector3.Lerp(transform.forward,
            (_closestEnemy.transform.position - transform.position).normalized, 0.25f);
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
    public void Die()
    {
        _isDead = true;
        GetComponent<Collider>().enabled = false;
        StartCoroutine(DieDelay());
    }

    public void Win()
    {
        _animator.SetBool("Win", true);
    }

    public void Hit()
    {
        _bloodParticles.Play();
    }
    private IEnumerator DieDelay()
    {
        _dieBloodParticles.Play();
        _hpBar.gameObject.SetActive(false);
        _animator.SetTrigger("Die");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
    public Rigidbody rb
    {
        get
        {
            return _rb;
        }
        set
        {
            _rb = value;
        }
    }
}
