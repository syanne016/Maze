using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	[SerializeField]private Vector3 direction;
	[SerializeField]private Vector3 target;
	[SerializeField]private Vector3 rotate;
	[SerializeField]private Rigidbody enemy;
	[SerializeField]private float acc;
	[SerializeField]private Quaternion targetRotation;
	[SerializeField]private float rot;
	[SerializeField]private float time;
	[SerializeField]private float posNeg;
	[SerializeField]private bool objectNear;
	[SerializeField]private bool move;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name.Equals("Dude")){

		}
		else if(other.gameObject.tag.Equals("MAP")) {
			this.transform.parent = other.gameObject.transform.GetChild (0).GetChild (0);;
		}
		else{
			objectNear = true;
		}
	}

	// Use this for initialization
	void Start () {
		enemy = this.GetComponent<Rigidbody>();
		acc = 10f;
		move = false;

		Roam();

	}

	// Update is called once per frame
	void Update () {
		if (objectNear==true) {
			Roam();
			objectNear = false;
		} else if(move ==false) {	
			targetRotation = Quaternion.LookRotation (-target, Vector3.up);
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, Time.deltaTime * 2.0f);
			//transform.LookAt (new Vector3(target.x, 0, target.z));
			//this.transform.localEulerAngles=new Vector3(0, this.transform.localEulerAngles.y,0);
			move = true;
						             
		}else if (move == true) {
			time = time + Time.deltaTime;
			enemy.velocity += direction * Time.deltaTime * acc;
			if (time>=3) {
				time = 0;
				move = false;
				Roam();
			}   
		}       

		enemy.velocity = Vector3.ClampMagnitude (Vector3.Project (enemy.velocity, transform.forward), 1) + Vector3.ClampMagnitude (Vector3.Project (enemy.velocity, transform.right), 1) + Vector3.Project (enemy.velocity, transform.up);
	}

	void Roam(){
		target = new Vector3(Random.Range(-5f, 5f), -this.transform.position.y, Random.Range(-5f, 5f))+this.transform.position;     
		direction = target.normalized;
	}
}
