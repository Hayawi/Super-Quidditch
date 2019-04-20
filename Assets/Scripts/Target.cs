using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    float points = 5;
    [SerializeField]
    GameObject pointText;
    [SerializeField]
    bool startingTarget = false;

    GameManager gm;

    private void Start()
    {
        gm = GameManager.gm;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (startingTarget)
        {
            gm.StartGame();
            Destroy(transform.parent.gameObject);
        } else
        {
            GameObject instPointText = Instantiate(pointText, transform.position, transform.rotation);
            instPointText.transform.rotation = Quaternion.Euler(0, instPointText.transform.rotation.eulerAngles.y, instPointText.transform.rotation.eulerAngles.z);
            instPointText.GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Impulse);
            instPointText.GetComponent<TextMesh>().text = "Scored " + points + " Points!";
            gm.pointsAchieved(points);
            Destroy(instPointText, 3);
            Destroy(collision.gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}
