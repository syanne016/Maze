using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	[SerializeField]private Vector3 direction;
	[SerializeField]private Vector3 target;
	[SerializeField]private Vector3 rotate;
	[SerializeField]private Vector3 checkRight;
	[SerializeField]private Vector3 checkLeft;
	[SerializeField]private Rigidbody enemy;
	[SerializeField]private float acc;
	[SerializeField]private float maxSpeed;			//Max value of the velocity in the X axis
	[SerializeField]private float rotAngle;
	[SerializeField]private float time;
	[SerializeField]private float timeMov;
	[SerializeField]private bool rotating;
	[SerializeField]private string move;
	[SerializeField] string[] go= { "Forward", "Right", "Left", "Back" };



	[SerializeField]private GameObject pj;
	[SerializeField]private float angleDetect;
	[SerializeField]private float distanceDetect;
	[SerializeField]private float chaseAcc;
	[SerializeField]private float distance;
	[SerializeField]private float angle;
	public EnemyAttack attack;
	public LayerMask layer;
	[SerializeField]private Animator enemyController;
	[SerializeField] private AnimationEvent attackEvent;
	[SerializeField] public int lifes;

	public enum State{
		ROAM,
		CHASE,
		ATTACK,
		DIE,
		STOP
	}
	[SerializeField]private State state;

	string RandomItem(string[] array){
		string name=array[Random.Range(0, array.Length)];
		return name;
	}

	// Use this for initialization
	void Start () {
		state = State.ROAM;
		timeMov = 1.5f;
		move=RandomItem(go);
		rotating = true;
		enemy = this.GetComponent<Rigidbody> ();
		acc = 5f;
		pj = GameObject.Find ("Dude");
		distanceDetect = 10f;
		angleDetect = 50f;
		chaseAcc = 7f;
		enemyController = this.GetComponent<Animator> ();
		lifes = 2;
	}


	void OnTriggerEnter(Collider other){
		
	}

	// Update is called once per frame
	void Update () {
		if (state == State.ROAM) {
			Roam ();
		} else if (state == State.CHASE) {
			Chase ();
		}else if (state == State.ATTACK) {
			Attack ();
		}
		else if (state == State.STOP) {
			StartCoroutine (Stop ());
		}
		if (lifes <= 0) {
			enemyController.SetBool ("Die", true);
			state = State.DIE;
		}

	}

	/*
	 * Roam calculates a new direction to go for the enemy
	 * and also checks if the main character is near.
	 */
	void Roam(){
		RaycastHit hit;
		//To move forward
		if (move.Equals ("Forward")) {
			//If there is an object in front of us we choose another direction to go to
			if (Physics.Raycast (this.transform.position+Vector3.up*0.5f, this.transform.forward, out hit, 1.2f, layer,QueryTriggerInteraction.Ignore)) {
				time = 0;
				move = RandomItem (go);
				rotating = true;
			} 
			//If there isn't a thing in fornt of us we move for 1s
			else {
				enemyController.SetBool ("Turn", false);
				rotating = false;
				this.transform.position += this.transform.forward * Time.deltaTime * acc;
				time += Time.deltaTime;
				if (time >= timeMov) {
					time = 0;
					move = RandomItem (go);
					enemyController.SetBool ("Turn", true);
					rotating = true;
				}
			}
		}
		//To move right
		else if (move.Equals ("Right")) {
			//First we rotate to the right
			if (rotating == true) {	
				this.transform.Rotate (Vector3.up * 5);
				rotAngle += 5;
				if (rotAngle == 90) {
					rotating = false;
					enemyController.SetBool ("Turn", false);
					rotAngle = 0;
				}
			}
			//If there is an object in front of us we choose another direction to go to
			else if (Physics.Raycast (this.transform.position+Vector3.up*0.5f, this.transform.forward, out hit, 1.2f, layer,QueryTriggerInteraction.Ignore)) {
				time = 0;
				move = RandomItem (go);
				enemyController.SetBool ("Turn", true);
				rotating = true;
			} 
			//If there isn't a thing in fornt of us we move for 1s
			else if (rotating == false) {
				this.transform.position += this.transform.forward * Time.deltaTime * acc;
				time += Time.deltaTime;
				if (time >= timeMov) {
					time = 0;
					move = RandomItem (go);
					enemyController.SetBool ("Turn", true);
					rotating = true;
				}
			}
			
		} 
		//To move left
		else if (move.Equals ("Left")) {
			//First we rotate to the left
			if (rotating == true) {	
				this.transform.Rotate (-1 * Vector3.up * 5);
				rotAngle += 5;
				if (rotAngle == 90) {
					enemyController.SetBool ("Turn", false);
					rotating = false;
					rotAngle = 0;
				}
			}
			//If there is an object in front of us we choose another direction to go to
			else if (Physics.Raycast (this.transform.position+Vector3.up*0.5f, this.transform.forward, out hit, 1.2f, layer,QueryTriggerInteraction.Ignore)) {
				time = 0;
				move = RandomItem (go);
				enemyController.SetBool ("Turn", true);
				rotating = true;
			}
			//If there isn't a thing in fornt of us we move for 1s
			else if (rotating == false) {
				this.transform.position += this.transform.forward * Time.deltaTime * acc;
				time += Time.deltaTime;
				if (time >= timeMov) {
					time = 0;
					move = RandomItem (go);
					enemyController.SetBool ("Turn", true);
					rotating = true;
				}
			}
		}
		//To move back
		else if (move.Equals ("Back")) {
			//First we rotate 180º
			if (rotating == true) {	
				this.transform.Rotate (Vector3.up * 5);
				rotAngle += 5;
				if (rotAngle == 180) {
					enemyController.SetBool ("Turn", false);
					rotating = false;
					rotAngle = 0;
				}
			}
			//If there is an object in front of us we choose another direction to go to
			else if (Physics.Raycast (this.transform.position+Vector3.up, this.transform.forward, out hit, 1.2f, layer,QueryTriggerInteraction.Ignore)) {
				time = 0;
				move = RandomItem (go);
				enemyController.SetBool ("Turn", true);
				rotating = true;
			}
			//If there isn't a thing in fornt of us we move for 1s
			else if (rotating == false) {
				this.transform.position += this.transform.forward * Time.deltaTime * acc;
				time += Time.deltaTime;
				if (time >= timeMov) {
					time = 0;
					move = RandomItem (go);
					enemyController.SetBool ("Turn", true);
					rotating = true;
				}
			}
		}
		//We check if the character is near
		distance = (this.transform.position - pj.transform.position).magnitude;
		angle = Vector3.Angle ((pj.transform.position-this.transform.position), this.transform.forward);
		if (distance <= distanceDetect && angle <= angleDetect) {
			StartCoroutine(Detect ());

		} 

	}	
	void Chase(){
		enemyController.SetBool ("Chase", true);
		distance = (this.transform.position - pj.transform.position).magnitude;
		this.transform.LookAt (pj.transform, new Vector3(0,1,0));
		//this.transform.rotation.x = 0;
		//this.transform.rotation.z = 0;
		this.transform.localEulerAngles= new Vector3 (0, this.transform.localEulerAngles.y, 0);
		if (attack.attack == true){
			state = State.ATTACK;
		}else if (distance >= 2.5f) {
			this.transform.position += this.transform.forward * Time.deltaTime * chaseAcc;
		}
	}
	void Attack(){
		if (attack.attack == true) {
			enemyController.SetBool ("Chase", false);
			enemyController.SetBool ("Attack", true);
			StartCoroutine(Wait ());
		}
	}
	IEnumerator Detect(){
		enemyController.SetBool ("Detect", true);
		state = State.STOP;
		yield return new WaitForSeconds(1.05f);
		enemyController.SetBool ("Detect", false);
		state = State.CHASE;
	}

	IEnumerator Wait(){
		enemyController.SetBool ("Attack", true);
		yield return new WaitForSeconds(1.5f);
		attack.attack = false;
		enemyController.SetBool ("Attack", false);
		state = State.STOP;
	}

	IEnumerator Stop(){
		yield return new WaitForSeconds(1f);
		enemyController.SetBool ("Chase", true);
		state = State.CHASE;
	}
}
