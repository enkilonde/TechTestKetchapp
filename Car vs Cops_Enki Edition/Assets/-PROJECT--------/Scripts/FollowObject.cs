using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{

    public Transform target;
    public float lerp = -1;

    private Vector3 lastTargetpos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetTargetPos();

        if(lerp > 0)
        {
            transform.position = Vector3.Lerp(transform.position, lastTargetpos, lerp * Time.deltaTime);
        }
        else
        {
            transform.position = lastTargetpos;
        }

    }

    /// <summary>
    /// I use this to allow to follow the last player pos if the player get destroyed
    /// </summary>
    private void GetTargetPos()
    {
        if (target == null)
            return;
        lastTargetpos = target.position;
    }

}
