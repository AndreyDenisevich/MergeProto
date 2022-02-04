using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 10f;
    private int _damage = 30;
    
    private Creature _enemy;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy != null)
            transform.forward = (new Vector3(_enemy.transform.position.x,transform.position.y,_enemy.transform.position.z) - transform.position).normalized;
        else Destroy(this.gameObject);
        transform.Translate(0, 0, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Creature>()==_enemy)
        {
            _enemy.hp -= _damage;
            _enemy.Hit();
            Destroy(this.gameObject);
        }
    }

    public int damage
    {
        set
        {
            _damage = value;
        }
    }
    public Creature enemy
    {
        set
        {
            _enemy = value;
        }
    }
}
