using UnityEngine;
using System.Collections;

/// http://answers.unity3d.com/questions/624224/create-a-textmesh-in-c.html
/// http://stackoverflow.com/questions/32927695/unity3d-add-text-to-a-prefab

public class TextHologram : MonoBehaviour {
    Mesh CreateTextMesh(String text, int coords) {
        GameObject Text = new GameObject();
        Transform txtMeshTransform = (Transform)Instantiate(yourTextPrefabHere);
        TextMesh txtMesh = txtMeshTransform.GetComponent<TextMesh>();
        txtMesh.text = "New text set through script";
        txtMesh.color = Color.red;
    }
}
