using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPBar : MonoBehaviour
{
    [SerializeField]
    private Slider _hpBar;
    [SerializeField]
    private Creature _creature;

    private int startHp;
    private void Start()
    {
        startHp = _creature.hp;
    }
    void Update()
    {
        _hpBar.value = (float)_creature.hp / (float)startHp;
        //transform.rotation = Quaternion.Euler(90, 0, 0);
    }
}
