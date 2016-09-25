using UnityEngine;
using System.Collections;

public class buttonClick : MonoBehaviour {

	public GameObject box;

	public void Remove_Click() {
		Debug.Log ("Hello, world");
		Destroy (box);
	}
}
