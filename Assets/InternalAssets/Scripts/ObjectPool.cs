using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private Projectile _projectilePrefab;
    [SerializeField]
    private int _poolSize;
    private Queue<Projectile> _pooledProjectiles;
    private List<Projectile> _activatedProjectiles;

    void Start()
    {
        _pooledProjectiles = new Queue<Projectile>();
        _activatedProjectiles = new List<Projectile>();
        for (int i = 0; i < _poolSize; i++)
        {
            Projectile obj = Instantiate(_projectilePrefab);
            obj.gameObject.SetActive(false);
            obj.connectedPool = this;
            _pooledProjectiles.Enqueue(obj);
        }
    }

    public Projectile GetObjectFromPool()
    {
        Projectile obj = _pooledProjectiles.Dequeue();
        obj.gameObject.SetActive(true);
        _activatedProjectiles.Add(obj);
        return obj;
    }

    public void ReturnObjectToPool(Projectile obj)
    {
        _activatedProjectiles.Remove(obj);
        obj.gameObject.SetActive(false);
        _pooledProjectiles.Enqueue(obj);
    }
}
