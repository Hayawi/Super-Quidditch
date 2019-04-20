using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandBehaviour : MonoBehaviour
{

    [SerializeField]
    SteamVR_Action_Boolean gripAction;
    [SerializeField]
    SteamVR_Action_Single triggerAction;
    [SerializeField]
    Hand hand;
    [SerializeField]
    GameObject broomstick;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject wand;

    PhysicsManager phys;

    bool holdingBroom = false;
    bool gripping = false;
    bool capableOfFlight = false;
    bool holdingWand = false;
    bool spellCast = false;
    float triggerSensitivity = 0;

    private void OnEnable()
    {
        if (hand == null)
            hand = this.GetComponent<Hand>();

        if (gripAction == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> No grab action assigned");
            return;
        }
        if (triggerAction == null)
        {
            Debug.LogError("<b>[SteamVR Interaction]</b> No grab action assigned");
        }
        triggerAction.AddOnChangeListener(OnTriggerActionChange, hand.handType);

        gripAction.AddOnChangeListener(OnGripActionChange, hand.handType);
    }

    private void OnTriggerActionChange(SteamVR_Action_Single fromAction, SteamVR_Input_Sources fromSource, float newAxis, float newDelta)
    {
        triggerSensitivity = newAxis;
    }

    private void OnGripActionChange(SteamVR_Action_Boolean actionIn, SteamVR_Input_Sources inputSource, bool newValue)
    {
        gripping = newValue;
    }



    // Start is called before the first frame update
    void Start()
    {
        phys = new PhysicsManager();
        if (phys == null)
        {
            phys = PhysicsManager.getPhys();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gripping && hand.GetComponent<Collider>().bounds.Intersects(broomstick.GetComponent<Collider>().bounds) && !holdingWand)
        {
            holdingBroom = true;
            capableOfFlight = true;
            broomstick.GetComponent<BroomstickBehaviour>().Grabbed(transform);
            phys.go(0.2f);
        }

        if (gripping && hand.GetComponent<Collider>().bounds.Intersects(wand.GetComponent<Collider>().bounds) && !holdingBroom)
        {
            holdingWand = true;
            phys.go(0.2f);
        }
        if (holdingWand && gripping)
        {
            wand.GetComponent<SpellCasting>().HoldWand(transform);
            if (!spellCast && triggerSensitivity > 0.5f)
            {
                phys.go(1);
                wand.GetComponent<SpellCasting>().BeginCast();
                spellCast = true;
            }

            if (spellCast && triggerSensitivity <= 0.5f)
            {
                spellCast = false;
                wand.GetComponent<SpellCasting>().EndCast();

            }
        } else if (holdingWand && !gripping)
        {
            holdingWand = false;
            wand.GetComponent<SpellCasting>().LooseWand();
            phys.pause();

        }

        if (triggerSensitivity > 0 && capableOfFlight)
        {
            phys.go(1);
            player.GetComponent<FlightBehaviour>().Fly(triggerSensitivity);
        }
        else if (capableOfFlight && !gripping)
        {
            capableOfFlight = false;
            player.GetComponent<FlightBehaviour>().Stop();
            phys.pause();
        }

        if (holdingBroom && !gripping)
        {
            holdingBroom = false;
            broomstick.GetComponent<BroomstickBehaviour>().NotGrabbed();
            player.GetComponent<FlightBehaviour>().Stop();
            phys.pause();
        }

    }
}
