using UnityEngine;
using System.Collections;

public class TextHologramDriver : MonoBehaviour {

    static void Main(string[] args) {
        while (true) {
            if (Input.GetKeyDown("c")) { CreateMesh("cee", 100); }
            if (Input.GetKeyDown("f") { CreateMesh("eff", 300); }
            if Input.GetKeyDown("l") { CreateMesh("el", 600); }
            if Input.GetKey"escape") { Application.Quit(); }
        }
    }
}
