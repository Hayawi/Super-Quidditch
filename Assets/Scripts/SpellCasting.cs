using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{

    [SerializeField]
    Transform wandTip;
    [SerializeField]
    float maxDistance = 100;
    Transform parent;
    Vector3 startingCastPos;
    Collider objectHit;
    Vector3 hitLocation;

    public void BeginCast()
    {
        startingCastPos = wandTip.position;
        Vector3 direction = Vector3.Normalize(wandTip.position - transform.position);
        RaycastHit hitinfo = new RaycastHit();
        Physics.BoxCast(wandTip.position,new Vector3(2,2,2), direction, out hitinfo);
        if (!hitinfo.point.Equals(new Vector3(0,0,0)))
        {
            objectHit = hitinfo.collider;
            hitLocation = hitinfo.point;
            print("Hit " + objectHit.name);
            GameObject line = new GameObject();
            line.transform.position = wandTip.position;
            line.AddComponent<LineRenderer>();
            LineRenderer lr = line.GetComponent<LineRenderer>();
            lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
            lr.startColor = Color.black;
            lr.endColor = Color.black;
            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;
            lr.SetPosition(0, wandTip.position);
            lr.SetPosition(1, hitLocation);
            GameObject.Destroy(line, 1);
        }
    }

    public void EndCast()
    {
        Vector3 forceDirection = wandTip.position - startingCastPos;
        if (objectHit != null && objectHit.GetComponent<Rigidbody>() != null)
        {
            print("Out " + objectHit.name);
            objectHit.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(forceDirection, hitLocation, ForceMode.Impulse);
        }
    }

    public void HoldWand(Transform hold)
    {
        transform.SetParent(hold);
    }

    public void LooseWand()
    {
        transform.SetParent(parent);
    }

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
