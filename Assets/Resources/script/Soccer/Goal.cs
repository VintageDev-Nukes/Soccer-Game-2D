using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

	public MyTeam goalType;
	public Transform ball;

	// Use this for initialization
	void Start () {
		if(ball == null) {
			ball = GameObject.Find("Ball").transform;
		}
	}
	
	// Update is called once per frame
	void Update () {

		Collider2D c2d = Physics2D.OverlapArea(transform.position, transform.position+transform.localScale, ~((1 << 8) | (1 << 9)));
		if(c2d != null && c2d.transform != null && c2d.transform.name == "Ball" && !Score.Scored) {
			Score.Scored = true;
			if(goalType == MyTeam.Local) {
				Score.Visitor++;
			} else {
				Score.Local++;
			}
		}
	
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, .1f);
		Gizmos.DrawSphere(transform.position+transform.localScale, .1f);
	}

}
