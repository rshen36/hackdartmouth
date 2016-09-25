using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Text))]

public class Typer : MonoBehaviour {

	public string msg = "";
	public TextAsset textFile;
//	public string[] textLines;

	private Text textComp;
	public float startDelay = 0.5f;
	public float typeDelay = 0.01f;
	public AudioClip putt;

	public void SetMessage (string myMsg) {
		msg = myMsg;
		StartCoroutine ("TypeIn");
//		print ("hello my dear");
	}

	void Start () {
//		if (textFile != null) {
////				textLines = (textFile.text.Split ('\n'));
//			msg = textFile.text;
//		StartCoroutine ("TypeIn");
//	}
	}
	
	void Awake () {
		textComp = GetComponent<Text> ();
	}

	public IEnumerator TypeIn() {
		yield return new WaitForSeconds (startDelay);

		for (int i = 0; i < msg.Length; i++) {
			textComp.text = msg.Substring (0, i);
//			GetComponent<AudioSource> ().PlayOneShot (CLIP);
			yield return new WaitForSeconds (typeDelay);
		}
	}

	public IEnumerator TypeOff() {
		for (int i = msg.Length; i >= 0; i--) {
			textComp.text = msg.Substring (0, i); 
			yield return new WaitForSeconds (typeDelay);
		}
	}
			
}
