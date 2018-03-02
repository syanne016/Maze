using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class MenuControl : MonoBehaviour {

	public Button start;
	public Button tutorial;
	public Button exit;	
	[SerializeField]private GameObject charge;
	[SerializeField]private GameObject fondo;
	[SerializeField]private GameObject title;
	
	// Update is called once per frame
	void Update () {
		exit.onClick.AddListener (exitClick);
		start.onClick.AddListener (startClick);
		tutorial.onClick.AddListener (tutorialClick);
	}

	public void exitClick(){
		Application.Quit ();
	}
	public void startClick(){
		StartCoroutine (WaitStart());


	}
	public void tutorialClick(){		
		StartCoroutine (WaitTutorial());
	}

	IEnumerator WaitStart(){
		charge.SetActive (true);
		fondo.SetActive (false);
		title.SetActive (false);
		yield return new WaitForSeconds (5f);
		SceneManager.LoadScene("PJEI_Examen", LoadSceneMode.Single);
	}
	IEnumerator WaitTutorial(){
		charge.SetActive (true);
		fondo.SetActive (false);
		title.SetActive (false);
		yield return new WaitForSeconds (5f);
		SceneManager.LoadScene("Tutorial", LoadSceneMode.Single);
	}
}
