using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class succAttack : MonoBehaviour {
	public EnemyMove lifesEnemy;
	public Animator enemyController;
	public Movement pj;


	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag.Equals("enemy")  ) {
			lifesEnemy = other.GetComponent<EnemyMove>();
			enemyController = other.GetComponent<Animator> ();
			if (pj.st == Movement.State.ATTACK) {
				StartCoroutine (Wait ());
			}

		}
	}
	void Start(){
		
	}
	IEnumerator Wait(){
		enemyController.SetBool ("Hit", true);
		yield return new WaitForSeconds(1.3f);
		enemyController.SetBool ("Hit", false);
		lifesEnemy.lifes--;
	}
}
