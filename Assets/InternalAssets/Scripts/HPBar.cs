using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    [SerializeField]
    private Slider _hpBar;
    private Creature _creature;

    private int startHp;
    private void Start()
    {
        _creature = transform.parent.GetComponent<Creature>();
        startHp = _creature.hp;
    }
    void Update()
    {
        _hpBar.value = (float)_creature.hp / (float)startHp;
    }
}
