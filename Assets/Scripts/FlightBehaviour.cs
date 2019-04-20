using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightBehaviour : MonoBehaviour
{
    [SerializeField]
    float maxSpeed = -10;

    float multiplier = 0;
    bool flying = false;

    [SerializeField]
    Transform broomstick;

    public void Fly(float multiplier)
    {
        this.multiplier = multiplier;
        flying = true;
    }

    public void Stop()
    {
        flying = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionOfFlight = broomstick.rotation.eulerAngles;

        if (flying)
            transform.Translate(maxSpeed * multiplier, 0, 0, broomstick);
    }
}
