/*
\n










*/

using UnityEngine;

public class Drones : Gun
{

    private Vector3 currentVelocity;

    [Space(10)]
    public Transform targetPosition;
    public float smoothTime;

    // Behaviour messages
    new void Update()
    {
        base.Update();

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition.position, ref currentVelocity, smoothTime);
    }

    public void Reset()
    {
        activateGun = false;
    }
}
