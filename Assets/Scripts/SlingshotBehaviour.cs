using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SlingshotBehaviour : MonoBehaviour
{

    [SerializeField]
    GameObject ball;
    [SerializeField]
    SteamVR_Action_Boolean grabAction;
    [SerializeField]
    Hand hand;
 
    bool grabbing = false;
    bool held = false;

    private void OnEnable()
    {
        if (hand == null)
            hand = this.GetComponent<Hand>();

        if (grabAction == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> No grab action assigned");
            return;
        }
        grabAction.AddOnChangeListener(OnGrabActionChange, hand.handType);
    }

    private void OnGrabActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
    {
        grabbing = newValue;
    }

    private void OnDisable()
    {
        if (grabAction != null)
            grabAction.RemoveOnChangeListener(OnGrabActionChange, hand.handType);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkGrabbing();
        SpawnAndAttachToHand();
    }

    private void SpawnAndAttachToHand()
    {
        if (held)
        {
            ball.GetComponent<BallPositionController>().AttachBall(hand.GetComponent<Transform>());
        }
        else
        {
            ball.GetComponent<BallPositionController>().AttachBall(null);
        }
    }

    private void checkGrabbing()
    {
        if (grabbing && hand.GetComponent<Collider>().bounds.Intersects(ball.GetComponent<Collider>().bounds))
        {
            held = true;
        }
        else if (!grabbing)
        {
            held = false;
        }
    }
}
