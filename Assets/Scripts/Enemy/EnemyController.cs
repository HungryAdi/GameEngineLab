﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	// Movement
	public float speed;
	public bool patrol = true;

	public Transform[] waypoints;
	public int curWaypoint;
	public Vector3 target;
	public Vector3 moveDirection;
	public Vector3 velocity;

	// Health
	public GameObject[] players;
	public GameObject EnemyHealthManager;
	private EnemyHealthManager ehm;

	void Awake() {
		ehm = EnemyHealthManager.GetComponent<EnemyHealthManager>();
	}

	void Update() {
		// Movement
		if (curWaypoint < waypoints.Length) {
			target = waypoints [curWaypoint].position;
			moveDirection = target - transform.position;
			velocity = GetComponent<Rigidbody> ().velocity;

			if (moveDirection.magnitude < 1) {
				curWaypoint++;
			}
			else {
				velocity = moveDirection.normalized * speed;
			}
		}
		else {
			if (patrol) {
				curWaypoint = 0;
			}
			else {
				velocity = Vector3.zero;
			}
		}

		GetComponent<Rigidbody> ().velocity = velocity;

		// Rotation
		transform.Rotate (new Vector3 (0, 0, 300) * Time.deltaTime);
	}

	void OnTriggerEnter(Collider col) {
		if (col.CompareTag("p1") || col.CompareTag("p2")) {
			Debug.Log ("do this");
		}
		else if (col.CompareTag("playerBullet")) {
			// Need to add in player strength at some point
			ehm.gotHit (5);
		}
	}

	void Death() {
		Destroy (this.gameObject);
	}
}
