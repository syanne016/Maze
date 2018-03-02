using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {
	public bool attack;
	void OnTriggerStay(Collider other){
		if(other.gameObject.name.Equals("Dude")){
			attack=true;
		}
	}
	// Use this for initialization
	void Start () {
		attack = false;
	}
	

}
