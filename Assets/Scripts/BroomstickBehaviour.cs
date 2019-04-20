using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomstickBehaviour : MonoBehaviour
{
    Transform grabPoint;
    bool isGrabbed = false;

    public void Grabbed(Transform handPos)
    {
        grabPoint = handPos;
        isGrabbed = true;
    }

    public void NotGrabbed()
    {
        isGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed)
        {
            Vector3 broomPos = this.GetComponent<Transform>().position;
            Vector3 grabPos = grabPoint.GetComponent<Transform>().position;
            float zAngle = Mathf.Atan2(broomPos.y - grabPos.y, broomPos.x - grabPos.x) * 180 / Mathf.PI;
            float yAngle = Mathf.Atan2(broomPos.z - grabPos.z, broomPos.x - grabPos.x) * 180 / Mathf.PI;
            if (broomPos.x - grabPos.x < 0)
            {
                zAngle = -Mathf.Atan2(grabPos.y - broomPos.y, grabPos.x - broomPos.x) * 180 / Mathf.PI;
            }
            zAngle = Mathf.Clamp(zAngle, -30, 70);
            transform.rotation = Quaternion.Euler(new Vector3(0, -yAngle, zAngle));
        }
    }
}
