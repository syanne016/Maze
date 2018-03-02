using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameOver : MonoBehaviour {

	public Button exit;
	public Button restart;
	[SerializeField]private GameObject charge;
	[SerializeField]private GameObject over;

	void Update(){
		exit.onClick.AddListener (exitClick);
		restart.onClick.AddListener (restartClick);
	}
	public void exitClick(){
		StartCoroutine (WaitExit ());
	}
	public void restartClick(){
		StartCoroutine (WaitRestart ());
	}

	IEnumerator WaitRestart(){
		charge.SetActive (true);
		over.SetActive (false);
		yield return new WaitForSeconds (5f);
		SceneManager.LoadScene("PJEI_Examen", LoadSceneMode.Single);
	}
	IEnumerator WaitExit(){
		charge.SetActive (true);
		over.SetActive (false);
		yield return new WaitForSeconds (5f);
		SceneManager.LoadScene("Menu", LoadSceneMode.Single);
	}
}
