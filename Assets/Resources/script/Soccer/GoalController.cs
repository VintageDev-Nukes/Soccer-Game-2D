using UnityEngine;
using System.Collections;

public class GoalController : MonoBehaviour {

	public Vector2 ballResetPos;
	public float TimeToReset = 5;
	public PlayerPositions LocalPositions, VisitorPositions;

	float t;

	// Use this for initialization
	void Start () {
		t = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Score.Scored) {
			if(t >= TimeToReset) {
				Transform ball = GameObject.Find("Ball").transform;
				ball.position = ballResetPos;
				ball.rigidbody2D.velocity = Vector2.zero;
				ball.rigidbody2D.angularVelocity = 0;
				ball.rigidbody2D.AddForce(Vector2.right*Random.Range(-3, 3), ForceMode2D.Impulse);
				PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
				player.transform.position = ((player.mTeam == MyTeam.Local) ? LocalPositions.releaseArea : VisitorPositions.releaseArea);
				PlayerController bot = GameObject.Find("Bot").GetComponent<PlayerController>();
				bot.transform.position = ((bot.mTeam == MyTeam.Local) ? LocalPositions.releaseArea : VisitorPositions.releaseArea);
				t = 0;
				Score.Scored = false;
			}
			t += Time.deltaTime;
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(ballResetPos, .1f);
		Gizmos.DrawSphere(LocalPositions.releaseArea, .1f);
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(LocalPositions.defenseArea, .1f);
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(LocalPositions.midArea, .1f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(LocalPositions.attackArea, .1f);
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(VisitorPositions.releaseArea, .1f);
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere(VisitorPositions.defenseArea, .1f);
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(VisitorPositions.midArea, .1f);
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(VisitorPositions.attackArea, .1f);
	}
}
