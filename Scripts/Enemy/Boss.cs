using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
	[SerializeField]private GameObject pj;
	[SerializeField]private float angleDetect;
	[SerializeField]private float distanceDetect;
	[SerializeField]private float chaseAcc;
	[SerializeField]private float distance;
	[SerializeField]private float angle;
	public BossAttack attack;
	[SerializeField]private Animator bossController;


	public enum State{
		IDLE,
		CHASE,
		ATTACK
	}
	[SerializeField]private State state;

	// Use this for initialization
	void Start () {
		state = State.IDLE;
		pj = GameObject.Find ("Dude");
		distanceDetect = 18f;
		angleDetect = 60f;
		chaseAcc = 5f;
		bossController = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (state == State.IDLE) {
			distance = (this.transform.position - pj.transform.position).magnitude;
			angle = Vector3.Angle ((pj.transform.position-this.transform.position), this.transform.forward);
			if (distance <= distanceDetect && angle <= angleDetect) {
				bossController.SetBool ("near", true);
				StartCoroutine(Wait ());
			} 
		} else if (state == State.CHASE) {
			Chase ();
		}else if (state == State.ATTACK) {
			Attack ();
		}
	}
	void Chase(){
		bossController.SetBool ("attack", attack.attack);
		distance = (this.transform.position - pj.transform.position).magnitude;
		this.transform.LookAt (pj.transform, new Vector3(0,1,0));
		this.transform.localEulerAngles= new Vector3 (0, this.transform.localEulerAngles.y, 0);
		if (attack.attack == true){
			bossController.SetBool ("attack", attack.attack);
			state = State.ATTACK;
		}else if (distance >= 2f) {
			this.transform.position += this.transform.forward * Time.deltaTime * chaseAcc;
		}
	}
	void Attack(){
		if (attack.attack == true) {			
			StartCoroutine(WaitDetect ());
		}
	}
	IEnumerator Wait(){
		yield return new WaitForSeconds(2f);
		state = State.CHASE;

	}
	IEnumerator WaitDetect(){
		yield return new WaitForSeconds(1.5f);
		attack.attack = false;
		bossController.SetBool ("attack", false);
		state = State.CHASE;
	}
}
