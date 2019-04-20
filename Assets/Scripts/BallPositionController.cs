using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class BallPositionController : MonoBehaviour
{
    [SerializeField]
    Transform attachedTransform;
    [SerializeField]
    Transform originalTransform;
    [SerializeField]
    Transform ball;
    [SerializeField]
    float maxOffset = 1;
    private Interactable interactable;
    [SerializeField]
    SteamVR_Action_Boolean grabAction;
    [SerializeField]
    SteamVR_Input_Sources hand;
    bool held = false;
    LineRenderer line;
    [SerializeField]
    Transform[] lineVertices = new Transform[2];
    [SerializeField]
    GameObject ballToThrow;
    [SerializeField]
    float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ConfigureBallLine();
        Attached();
    }

    void Attached()
    {
        if (interactable.attachedToHand)
        {
            held = true;
        }
        else if (!interactable.attachedToHand)
        {
            if (held)
                FireBall();
            held = false;
        }
        if (held)
        {
            Vector3 handPos = attachedTransform.position;
            float dist = Vector3.Distance(originalTransform.position, handPos);
            if (dist < maxOffset)
            {
                SetTransform(attachedTransform);
            }
            else
            {
                Vector3 offset = handPos - originalTransform.position;
                Vector3 newPos = Vector3.ClampMagnitude(offset, maxOffset);
                attachedTransform.localPosition = originalTransform.position + newPos;
                SetTransform(attachedTransform);
            }
        }
        else
        {
            SetTransform(originalTransform);
        }
    }

    void FireBall()
    {
        GameObject instantiatedBall = Instantiate(ballToThrow, originalTransform.position, transform.rotation);
        float speed = maxSpeed * (Mathf.Clamp(Vector3.Distance(originalTransform.position, transform.position), 0, maxOffset) / maxOffset);
        instantiatedBall.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(GetNormalizedPosition() - originalTransform.position) * speed * -1, ForceMode.Impulse);
    }

    Vector3 GetNormalizedPosition()
    {
        float dist = Vector3.Distance(originalTransform.position, attachedTransform.position);
        if (held)
        {
            if (dist < maxOffset)
            {
                return attachedTransform.position;
            }
            else
            {
                Vector3 offset = attachedTransform.position - originalTransform.position;
                Vector3 newPos = Vector3.ClampMagnitude(offset, maxOffset);
                return originalTransform.position + newPos;
            }
        } else
        {
            return originalTransform.position;
        }
    }

    private void ConfigureBallLine()
    {
        line.positionCount = 3;
        Vector3[] points = new Vector3[4];
        points[0] = lineVertices[0].position;
        points[1] = GetNormalizedPosition();
        points[2] = lineVertices[1].position;
        line.SetPositions(points);
    }

    void SetTransform(Transform toSet)
    {
        ball.position = toSet.position;
        ball.rotation = toSet.rotation;
    }

    public void AttachBall(Transform hand)
    {
        attachedTransform = hand;
    }
}
