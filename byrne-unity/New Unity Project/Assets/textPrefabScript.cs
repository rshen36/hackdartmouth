using UnityEngine;
using System.Collections;

public class textPrefabScript : MonoBehaviour {

	Typer typer;

	// Use this for initialization
	void Start () {


	}

	public void Init (string msg) {
		print ("in init");

		Transform container = transform.Find ("Canvas/Container/Image/Text");
		GameObject textObj = container.gameObject;

		typer = textObj.GetComponent<Typer> ();
		typer.SetMessage (msg);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
