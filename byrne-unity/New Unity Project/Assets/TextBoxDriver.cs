using UnityEngine;
using System.Collections;

public class TextBoxDriver : MonoBehaviour {

	Vector3 coord; 

	// Use this for initialization
	void Start () {
		print ("I started");


	}

	public void makeTextBox(Vector3 coord, string msg) {
		print ("In make text box");
		GameObject textBox = Instantiate (Resources.Load ("TextBox")) as GameObject;
		textBox.GetComponent<textPrefabScript> ().Init (msg);

		textBox.transform.position = coord;



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
