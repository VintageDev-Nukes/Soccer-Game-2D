using UnityEngine;
using System.Collections;

public class Largero : MonoBehaviour {

	public LargeroPosition myPos;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Collider2D c2d = Physics2D.OverlapArea(transform.position-new Vector3(transform.localScale.x/2+.75f, 0, 0), transform.position+transform.localScale+Vector3.up*.75f, ~((1 << 8) | (1 << 9)));
		//Debug.Log(((c2d != null && c2d.transform != null && c2d.transform.name == "Ball" && c2d.rigidbody2D != null) ? c2d.rigidbody2D.velocity.magnitude.ToString() : "Nothing"));
		if(c2d != null && c2d.transform != null && c2d.transform.name == "Ball" && c2d.rigidbody2D != null && c2d.rigidbody2D.velocity.magnitude < 1) {
			c2d.rigidbody2D.AddForce(Vector2.right*((myPos == LargeroPosition.Izquierda) ? 1 : -1), ForceMode2D.Impulse);
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position-new Vector3(transform.localScale.x/2+.75f, 0, 0), .1f);
		Gizmos.DrawSphere(transform.position+transform.localScale+Vector3.up*.75f, .1f);
	}

}
