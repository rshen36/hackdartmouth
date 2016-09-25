using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class CanvasAndText : MonoBehaviour {
	private GameObject myGO;
	private GameObject childGO;

	void Start () {
		// create game object and child object
		myGO = new GameObject ();
		childGO = new GameObject ();

		// give them names for fun
		myGO.name = "TestCanvas";
		childGO.name = "ChildObject";

		// set the child object as a child of the parent
		childGO.transform.parent = myGO.transform;

		// add a canvas to the parent
		myGO.AddComponent<Canvas> ();
		// add a recttransform to the child
		childGO.AddComponent<RectTransform> ();

		// make a reference to the parent canvas and use the ref to set its properties
		Canvas myCanvas = myGO.GetComponent<Canvas> ();
		myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;


		// add a text component to the child
		childGO.AddComponent<Text> ();
		// make a reference to the child rect transform and set its values
		RectTransform childRectTransform = childGO.GetComponent<RectTransform> ();
		RectTransform parentRectTransform = myGO.GetComponent<RectTransform> ();

		// set child anchors for resizing behaviour
		childRectTransform.anchoredPosition3D = new Vector3(0f,0f,0f);
		childRectTransform.sizeDelta = new Vector2 (0f, 0f);
		childRectTransform.anchorMin = new Vector2 (0f,0f);
		childRectTransform.anchorMax = new Vector2 (1f, 1f);

		// set text font type and material at runtime from font stored in Resources folder
		Text textComponent = childGO.GetComponent<Text> ();
		Material newMaterialRef = Resources.Load<Material> ("Arial");
		Font myFont = Resources.Load<Font> ("Arial");

		textComponent.font = myFont;
		textComponent.material = newMaterialRef;

		// set the font text
		textComponent.text = "Hello World";
	}
}