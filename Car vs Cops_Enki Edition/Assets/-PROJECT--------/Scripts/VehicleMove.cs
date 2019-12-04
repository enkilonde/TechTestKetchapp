using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMove : MonoBehaviour
{

    public float speed = 5;
    public float angularSpeed = 10;

    public float targetAngle;

    public AnimationCurve boost;
    private float boostValue;
    private float boostMultiplier;

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ApplyBoost();
        Move();
    }

    protected virtual void Move()
    {
        float currentAngle = transform.rotation.eulerAngles.y;
        float adjustedAngle = targetAngle;
        while (adjustedAngle - currentAngle > 180)
        {
            adjustedAngle -= 360;
        }
        while (currentAngle - adjustedAngle > 180)
        {
            adjustedAngle += 360;
        }

        //UIDebug.WriteLine("c : " + currentAngle + "  ---- t : " + adjustedAngle);
        currentAngle = Mathf.MoveTowards(currentAngle, adjustedAngle, angularSpeed * Time.deltaTime);

        transform.rotation = Quaternion.Euler(0, currentAngle, 0);

        transform.position += transform.forward * speed * Time.deltaTime * boostMultiplier;
    }

    private void ApplyBoost()
    {
        if (boostValue <= 0)
        {
            boostMultiplier = 1;
            return;
        }
        boostValue = Mathf.Clamp(boostValue - Time.deltaTime, 0, 100);
        boostMultiplier = boost.Evaluate(boostValue);
    }

    public void Boost()
    {
        boostValue = boost.keys[boost.keys.Length-1].time;
    }
}
