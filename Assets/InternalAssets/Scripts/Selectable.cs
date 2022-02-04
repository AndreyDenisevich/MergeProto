using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;
using Lean.Common;

[RequireComponent(typeof(LeanDragTranslate))]
[RequireComponent(typeof(LeanSelectableByFinger))]
[RequireComponent(typeof(LeanConstrainToAxis))]
[RequireComponent(typeof(Creature))]
public class Selectable : MonoBehaviour
{
    private Collider _collider;
    private Vector3 _posBeforeDrag;
    private Creature _creature;
    void Start()
    {
        _creature = GetComponent<Creature>();
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void SetClosestPos()
    {
        transform.position = GameController.instance.GetClosestPos(transform.position);
    }

    public void Selected()
    {
        _posBeforeDrag = transform.position;
    }

    public void Deselected()
    {
        SetClosestPos();
        CheckCreaturesOnPoint(transform.position);
    }

    public void Disable()
    {
        LeanConstrainToAxis[] _constraints = GetComponents<LeanConstrainToAxis>();
        for (int i = 0; i < _constraints.Length; i++)
            _constraints[i].enabled = false;
        GetComponent<LeanSelectableByFinger>().enabled = false;
        GetComponent<LeanDragTranslate>().enabled = false;
        this.enabled = false;
    }
    private void CheckCreaturesOnPoint(Vector3 pos)
    {
        Vector2 snap = GameController.instance.GetGridSnap();
        RaycastHit[] hits = Physics.BoxCastAll(pos,new Vector3 (snap.x / 2f,0f, snap.y / 2f), Vector3.forward, Quaternion.Euler(0, 0, 0),0);
        if(hits.Length>1)
        {
            Creature creature1 = hits[0].transform.GetComponent<Creature>();
            Creature creature2 = hits[1].transform.GetComponent<Creature>();
            if (creature1.level != creature2.level||creature1.enemyOrFriendly!=creature2.enemyOrFriendly||creature1.meleeOrRange!=creature2.meleeOrRange)
                transform.position = _posBeforeDrag;
            else
            {
                Creature creaturePrefab;
                if (_creature.meleeOrRange == Creature.MeleeOrRange.melee)
                    creaturePrefab = GameController.instance.GetMeleeCreaturePrefab(_creature.level);
                else creaturePrefab = GameController.instance.GetRangeCreaturePrefab(_creature.level);
                Creature newCreature = Instantiate(creaturePrefab, pos, Quaternion.identity);
                GameController.instance.PlayMergeParticles(pos);
                GameController.instance.MergeCreatures(newCreature, creature1, creature2);
                if (creature1 == _creature)
                {
                    Destroy(creature2.gameObject);
                    Destroy(creature1.gameObject);
                }
                else
                {
                    Destroy(creature1.gameObject);
                    Destroy(creature2.gameObject);
                }
            }
        }
    }
}
