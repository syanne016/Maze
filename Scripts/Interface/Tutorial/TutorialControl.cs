using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialControl : MonoBehaviour {

	[SerializeField]private Animator canvasController;
	public Game state;	
	[SerializeField]private Image wasd;
	[SerializeField]private Text wasdText;
	[SerializeField]private Image space;
	[SerializeField]private Text spaceText;
	[SerializeField]private Text attackText;
	[SerializeField]private Image attack;
	[SerializeField]private Image attack2;
	[SerializeField]private Text asset3D;
	[SerializeField]private Text rotCamera;
	[SerializeField]private Text rise;
	[SerializeField]private Image ad;
	[SerializeField]private Text descend;
	[SerializeField]private EnemyMove enemy;
	[SerializeField]private GameObject portal;
	[SerializeField]private Animator portalController;


	public enum State{
		DO,
		space,
		PAUSE
	}
	private State states;

	public enum Canvas{
		WASD,
		Space, 
		Attack,
		Default
	}
	public Canvas controller;

	// Use this for initialization
	void Start () {
		canvasController = this.GetComponent<Animator> ();
		state = GameObject.Find ("GAME").GetComponent<Game>();
		controller = Canvas.WASD;
	}

	// Update is called once per frame
	void Update () {		
		if (states == State.DO) 
			{

			} 
		else if (states == State.PAUSE) 
			{

			}
		if (controller == Canvas.WASD) {	
			wasd.enabled = true;
			wasdText.enabled = true;
			canvasController.SetBool ("WASD", true);
			StartCoroutine (WaitWASD ());
		} else if (controller == Canvas.Space) {
			space.enabled = true;
			spaceText.enabled = true;
			canvasController.SetBool ("Space", true);
			StartCoroutine (WaitSpace ());
		} else if (controller == Canvas.Attack) {
				
			StartCoroutine (WaitAttack ());
		} else if (controller == Canvas.Default) {
			
		}



		if (enemy.lifes <= 0) 
			{
				portal.SetActive (true);
				portalController.SetTrigger("Open");
			}
	}
	void Pause(){

	}

	void Do()
		{
			wasd.gameObject.SetActive(true);
		}


	IEnumerator Wait()
		{
			yield return new WaitForSeconds(0.5f);
		}

	IEnumerator WaitWASD()
		{
			yield return new WaitForSeconds(5f);
			controller = Canvas.Space;
			canvasController.SetBool ("WASD", false);
			wasd.enabled = false;
			wasdText.enabled = false;
		}

	IEnumerator WaitSpace()
		{
			yield return new WaitForSeconds(5f);
			controller = Canvas.Attack;
			canvasController.SetBool ("Space", false);
			space.enabled = false;
			spaceText.enabled = false;
		}
	IEnumerator WaitAttack()
		{	
			attackText.enabled = true;
			attack.enabled = true;
			attack2.enabled = true;
			canvasController.SetBool ("Attack", true);
			yield return new WaitForSeconds(5f);
			controller = Canvas.Default;
			canvasController.SetBool ("Attack", false);
			attackText.enabled = false;
			attack.enabled = false;
			attack2.enabled = false;
		}


}
