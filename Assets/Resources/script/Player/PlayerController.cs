using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

	public float speed = 1, jump = 1, kickForce = 1, resetForce = 1, kickImpulse = 1, footProximity = 1.1f, velocity, botJumpTime = 1;
	public MyTeam mTeam;
	public PlayerType myType;
	public bool canMove = true;

	PlayerPositions myPos;
	bool kicking, goalRisk, jumping, startCount;
	Vector3 kickDelta, lastPosition;
	float t;

	Transform foot, ball;

	void Start() {
		//Physics2D.IgnoreLayerCollision(8, 8);
		GoalController gc = GameObject.Find("Scripts").GetComponent<GoalController>();
		if(mTeam == MyTeam.Local) {
			myPos = gc.LocalPositions;
		} else {
			myPos = gc.VisitorPositions;
		}
		if(foot == null) {
			foot = transform.FindChild("foot");
		}
		if(ball == null) {
			ball = GameObject.Find("Ball").transform;
		}
	}	
	
	void FixedUpdate() {
		if(canMove) {
			if(myType == PlayerType.Human) {
				Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical")*jump, 0);
				transform.position += move * speed * Time.deltaTime;
				if(Input.GetKey(KeyCode.Space) && !kicking) {
					kicking = true;
				}
				if(Input.GetKeyUp(KeyCode.Space)) {
					kicking = false;
				}
				float roundPos = (float)System.Math.Round(foot.localPosition.x, 1);
				if(kicking) {
					if(roundPos >= 0 && roundPos <= 0.6f) {
						foot.localPosition += Vector3.right*kickForce*Time.deltaTime;
						if(Vector3.Distance(foot.position, ball.position) <= footProximity) {
							Vector3 shootDir = (ball.position - foot.position).normalized;
							ball.rigidbody2D.AddForce(shootDir * kickImpulse, ForceMode2D.Impulse);
						}
					} else if(roundPos >= 0.6f && !Input.GetKeyDown(KeyCode.Space)) {
						kicking = false;
						foot.localPosition -= Vector3.right*resetForce*Time.deltaTime;
					}
				} else {
					if(roundPos > 0) {
						foot.localPosition -= Vector3.right*resetForce*Time.deltaTime;
					}
				}
			} else {
				float distance = Mathf.Abs(ball.position.x - GameObject.Find("Scene").transform.FindChild("Porteria2").position.x);
				//Debug.Log(distance);
				if(distance > 13.8f) { //Follow the ball
					goalRisk = false;
					rigidbody2D.velocity = new Vector2(Mathf.Round((ball.position - transform.position).x), 0).normalized*speed;
				} else if(distance < 13.8f && distance > 7.5f) {
					float ballVelocity = ball.rigidbody2D.velocity.magnitude;
					//Debug.Log((Mathf.Round((myPos.defenseArea.x+((mTeam == MyTeam.Local) ? -4.5f : 4.5f)+((mTeam == MyTeam.Local) ? ballVelocity/10 : -ballVelocity/10)))).ToString());
					goalRisk = true;
					rigidbody2D.velocity = new Vector2(Mathf.Round((new Vector3(myPos.defenseArea.x+((mTeam == MyTeam.Local) ? -4.5f : 4.5f)+((mTeam == MyTeam.Local) ? ballVelocity/5 : -ballVelocity/5), 0, 0) - transform.position).x), 0).normalized*speed;
				} else if(distance < 7.5f) {
					goalRisk = true;
					rigidbody2D.velocity = new Vector2(Mathf.Round((myPos.releaseArea - transform.position).x), 0).normalized*speed;
				}
				if(goalRisk && ball.position.y >= -2) {
					BotJump();
				}
			}
		}
		velocity = (transform.position - lastPosition).magnitude/Time.deltaTime;
		lastPosition = transform.position;
	}

	void BotJump() {
		if(!jumping) {
			startCount = true;
			jumping = true;
		} else {
			if(t <= botJumpTime) {
				transform.position += new Vector3(0, t/botJumpTime, 0)*speed*Time.deltaTime;
			} else {
				startCount = false;
				if(transform.position.y < -4.7f) {
					jumping = false;
					t = 0;
				} else {
					transform.position -= Vector3.up*speed*Time.deltaTime;
				}
			}
		}
		if(startCount) {
			t += Time.deltaTime;
		}
	}

	bool IsGrounded() {
		return Physics.Raycast(foot.position-new Vector3(0, 0.3f, 0), -Vector3.up, 0.1f);
	}

	void OnDrawGizmos() {
		//Gizmos.color = Color.cyan;
		//Gizmos.DrawSphere(foot.position-new Vector3(0, 0.3f, 0), .1f);
	}

}