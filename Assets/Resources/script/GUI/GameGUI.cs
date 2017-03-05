using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		GUI.Label(new Rect(Screen.width/2-100, 25, 200, 60), Score.Visitor+" - "+Score.Local, new GUIStyle(GUI.skin.label) {fontSize = 50, fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter});
	}

}
