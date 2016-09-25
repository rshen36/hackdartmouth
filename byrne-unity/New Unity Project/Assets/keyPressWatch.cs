using UnityEngine;
using System.Collections;

public class keyPressWatch : MonoBehaviour {

	public GameObject boxObject;
	private TextBoxDriver box;

	// Use this for initialization
	void Start () {
		box = boxObject.GetComponent<TextBoxDriver> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			print ("space pressed");


			box.makeTextBox(new Vector3(-0,-0,-0), "Armin here, this works.");
		}
	}
}
