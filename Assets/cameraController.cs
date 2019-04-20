using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour {

	[SerializeField]
	float movementSpeed = 5f;

    [SerializeField]
	public float mouseSensitivity = 15F;

	[SerializeField]
	GameObject light;

	[SerializeField]
	float lightChangingAmount = 0.1f;

	GameObject[] objectsInScene;
	int currentObject = 0;

	void Start() {
		objectsInScene = (GameObject[]) GameObject.FindObjectsOfType<GameObject>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (objectsInScene.Length == 0)
			objectsInScene = GameObject.FindGameObjectsWithTag("Untagged");

		GetComponent<Transform> ().Translate(Input.GetAxis ("Horizontal") * movementSpeed, 0, Input.GetAxis ("Vertical")  * movementSpeed);

		Rotate ();

		if (Input.GetAxis("Mouse ScrollWheel") > 0)
			light.GetComponent<Light> ().intensity += lightChangingAmount;
		else if (Input.GetAxis("Mouse ScrollWheel") < 0)
			light.GetComponent<Light> ().intensity += -lightChangingAmount;

		if (objectsInScene.Length > 0) {
			objectsInScene [currentObject].GetComponent<Transform> ().Translate (Input.GetAxis ("VerticalObj") * movementSpeed, 0, Input.GetAxis ("HorizontalObj") * movementSpeed);
			print (currentObject);
		}
		if (Input.GetAxis ("Fire1") > 0)
			currentObject++;

	}

	void Rotate(){
		float rotationX = (Input.GetAxis("Mouse X") * mouseSensitivity);
		float rotationY = (Input.GetAxis("Mouse Y") * mouseSensitivity);
		rotationY = Mathf.Clamp (rotationY, -90, 90);
		transform.localEulerAngles = new Vector3(-rotationY  + transform.localEulerAngles.x, rotationX  + transform.localEulerAngles.y, 0);
	}
		
}
