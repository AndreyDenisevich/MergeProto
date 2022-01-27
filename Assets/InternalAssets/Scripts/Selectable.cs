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

    private void CheckCreaturesOnPoint(Vector3 pos)
    {
        Vector2 snap = GameController.instance.GetGridSnap();
        RaycastHit[] hits = Physics.BoxCastAll(pos,new Vector3 (snap.x / 2f,0f, snap.y / 2f), Vector3.forward, Quaternion.Euler(0, 0, 0),0);
        if(hits.Length>1)
        {
            Creature creature1 = hits[0].transform.GetComponent<Creature>();
            Creature creature2 = hits[1].transform.GetComponent<Creature>();
            if (creature1.level != creature2.level||creature1.enemyOrFriendly!=creature2.enemyOrFriendly)
                transform.position = _posBeforeDrag;
            else
            {
                Creature creaturePrefab = GameController.instance.GetCreaturePrefab(_creature.level);
                Creature newCreature = Instantiate(creaturePrefab, pos, Quaternion.identity);
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
