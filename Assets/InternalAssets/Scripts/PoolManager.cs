using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    private ObjectPool _archerPool;
    [SerializeField]
    private ObjectPool _assasinPool;
    [SerializeField]
    private ObjectPool _magePool;
   
    public ObjectPool GetPool(RangeCreature.RangeType type)
    {
        switch(type)
        {
            case RangeCreature.RangeType.archer:
                return _archerPool;
            case RangeCreature.RangeType.assasin:
                return _assasinPool;
            case RangeCreature.RangeType.mage:
                return _magePool;
            default:
                return null;
        }
    }
}
