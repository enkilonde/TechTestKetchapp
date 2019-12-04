using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopVehicle : VehicleMove
{
    public Transform target;

    /// <summary>
    /// To avoid getting the playerTr multiple time in a game
    /// </summary>
    private static Transform playerTr;

    private Vector3 lastTargetpos;


    protected override void Start()
    {
        base.Start();

        if(target == null)
        {
            if (playerTr == null)
                playerTr = GameObject.FindGameObjectWithTag("Player").transform;
            target = playerTr;
        }

    }

    protected override void Move()
    {
        GetTargetPos();

        Vector3 direction = lastTargetpos - transform.position;
        direction.y = 0;
        direction.Normalize();

        //sin(angle) = direction.y
        //targetAngle = Mathf.Asin(direction.z) * Mathf.Rad2Deg;

        targetAngle = Vector2.SignedAngle(new Vector2(direction.x, direction.z), Vector2.down) + 180;



        base.Move();    
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
