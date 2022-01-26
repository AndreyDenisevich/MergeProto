using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Selectable _currentSelectable;
    [SerializeField]
    private LayerMask _groundMask;
    void Start()
    {
        
    }
    void Update()
    {
        Ray ray;
        RaycastHit raycastHit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin,ray.direction*100f);
        if (Physics.Raycast(ray, out raycastHit))
        {
            Selectable obj;
            obj = raycastHit.transform.GetComponent<Selectable>();
            if (obj != null)
            {
                _currentSelectable = obj;
            }
            else
            {
                _currentSelectable = null;
            }
        }
        else
        {       
                _currentSelectable = null;
        }
        if (Input.GetMouseButton(0)&&_currentSelectable!=null)
        {
            Physics.Raycast(ray,out raycastHit, Mathf.Infinity,_groundMask);
            _currentSelectable.transform.position = raycastHit.point;
        }
    }
}
