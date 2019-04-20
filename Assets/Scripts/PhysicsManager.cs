using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{

    bool physics = true;
    float physSpeed = 1;
    private float timer;

    static PhysicsManager phys;

    public void pause()
    {
        physics = false;
    }

    public void go(float speed)
    {
        physics = true;
        physSpeed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (phys == null)
        {
            phys = this;
        } else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (physics)
        {
            Time.timeScale = physSpeed;
        }
        else
        {
            Time.timeScale = 0.05f;
        }
    }

    public static PhysicsManager getPhys()
    {
        return phys;
    }
}
