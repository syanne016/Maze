using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySampleNAV : MonoBehaviour {
	[SerializeField]private Vector3 target;
	[SerializeField]private NavMeshAgent agent;
	[SerializeField]private bool objectNear;
	[SerializeField]private float destinationReach;
	[SerializeField]private float distanceToTarget;
	[SerializeField]private Vector3 direction;
	[SerializeField]private float rotationY;
	[SerializeField]private bool rotating;


	public enum State{
		ROAM,
		CHASE
	}
	[SerializeField]private State state;

	void OnTriggerEnter(Collider other){
		if (other.gameObject.name.Equals("Dude")){

		}
		else{
			objectNear = true;
		}
	}

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		objectNear = false;
		rotating = true;
		Roam ();
	}

	
	// Update is called once per frame
	void Update () {
		rotationY = Vector3.Angle (target-this.transform.position, this.transform.forward);
		distanceToTarget = Vector3.Distance(transform.position, target);
		if (distanceToTarget<0.5) {
			Roam ();
		} else if (objectNear == true) {
			objectNear = false;
			Roam ();
		}
		else {
			rotating = true;
			if (rotationY < 3f) {
				transform.LookAt (new Vector3(target.x, 0, target.z));
				this.transform.localEulerAngles=new Vector3(0, this.transform.localEulerAngles.y,0);
				agent.updateRotation = false;
				rotating = false;
				agent.SetDestination (target);
			} else if(rotating == true) {
				direction = (target - transform.position).normalized;
				Quaternion qDir = Quaternion.LookRotation (direction);
				transform.rotation = Quaternion.Slerp (transform.rotation, qDir, Time.deltaTime * 3);

			}

		}
	}

	void Roam(){
		target = new Vector3(Random.Range(-10f, 10f), this.transform.position.y, Random.Range(-5f, 5f));
	}
}
