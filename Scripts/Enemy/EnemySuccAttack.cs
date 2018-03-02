using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuccAttack : MonoBehaviour {

	public EnemyAttack attack;
	[SerializeField] private Movement lifesDude;
	[SerializeField] private Animator dudeContr;
	void OnTriggerEnter(Collider other){
		if (other.gameObject.name.Equals("Dude") && attack) {
			lifesDude = other.GetComponent<Movement>();
			dudeContr = GameObject.Find ("Hands").GetComponent<Animator> ();
			if (lifesDude.st != Movement.State.STOP) {
				StartCoroutine (Wait ());
			}

		}
	}
	void Start(){

	}
	IEnumerator Wait(){
		lifesDude.pj.AddForce (new Vector3 (0, 2, -2), ForceMode.Impulse);
		dudeContr.SetBool ("Hit", true);
		lifesDude.st = Movement.State.STOP;
		yield return new WaitForSeconds(0.4f);
		dudeContr.SetBool ("Hit", false);
		lifesDude.lifes--;
		lifesDude.st = Movement.State.MOVE;

	}
}
