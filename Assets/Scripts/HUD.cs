using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Light light;
    [SerializeField]
    float min;
    [SerializeField]
    float max;
    [SerializeField]
    GameObject toSpawn;

    ArrayList list = new ArrayList();

    bool toggleMenu;
    [SerializeField]
    GameObject menu;

    private void Update()
    {
        if (SteamVR_Input.GetStateDown("default", "Teleport", SteamVR_Input_Sources.Any)) {
            toggleMenu = !toggleMenu;
            menu.SetActive(toggleMenu);
        }
    }

    public void OnGravityHandleHeld(LinearMapping value)
    {
        Physics.gravity = new Vector3(0, -9.8f * 2 * value.value, 0);
        print(Physics.gravity);
    }

    public void OnLightHandleHeld(LinearMapping value)
    {
        print(value.value);
        light.intensity = value.value * 2;
    }

    public void OnSpawnButtonPressed()
    {
        Vector3 position = new Vector3(Random.Range(min, max), Random.Range(0, max) + 168.7511f, Random.Range(min, max));
        list.Add(Instantiate(toSpawn, position, transform.rotation));
        ((GameObject)list[list.Count - 1]).GetComponent<Rigidbody>().isKinematic = false;
    }

    public void OnImpulseButtonPressed()
    {
        for (int i = 0; i < list.Count; i++) {
            Vector3 force = new Vector3(Random.Range(0, max / 10), Random.Range(0, max / 10), Random.Range(0, max / 10));
            ((GameObject)list[i]).GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
        }

    }

    public void OnXHandleHeld(LinearMapping value)
    {
        toSpawn.transform.localScale = (new Vector3(value.value, 1, 1));
    }

    public void OnYHandleHeld(LinearMapping value)
    {
        toSpawn.transform.localScale = (new Vector3(1, value.value, 1));
    }
}
