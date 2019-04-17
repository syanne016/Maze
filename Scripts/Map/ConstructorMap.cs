using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructorMap : MonoBehaviour {

	public bool canMove;								//Public variable to check if we can move
	[SerializeField]private float moveX = 0f;			//Variable to store where the map is moving around the X axis
	[SerializeField]private bool rotateR;				//Variable for the camera to rotate right
	[SerializeField]private bool rotateL;				//Variable for the camera to rotate right
	[SerializeField]private int rot;					//Degrees we rotate in time
	[SerializeField]private bool rotating;				//Check if camera is rotating

	[SerializeField]private Transform camRot;
	[SerializeField]private Camera cam;
	[SerializeField]private Camera cam2;

	//[SerializeField]private bool rolling;				//Check if we are getting closer or away
	//[SerializeField]private bool maximize;				//Check if you can maximize
	//[SerializeField]private bool minimize;				//Check if you can minimize
	//[SerializeField]private Vector3 camDir;				//Direction from the cam to a gameobject
	//[SerializeField]private GameObject dis;				//The gameobject to check the direction

	public Game state;									//Public variable to check which map is the one that is active

	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("FP_Camera").GetComponent<Camera>();
		cam2 = GameObject.Find ("3D_Camera").GetComponent<Camera>();
		state = GameObject.Find ("GAME").GetComponent<Game>();
		camRot = GameObject.Find ("CamRotation").GetComponent<Transform> ();
		canMove = false;
		rotateR = false;
		rotateL = false;
		rot = 0;
		rotating = false;
		//rolling = false;
		//camDir = new Vector3 (0, -0.5f, 1);
	}


	void Update(){
		//Check if the map that is active is the second one
		if (state.active.Equals ("MAP2")) {
			cam.enabled = false;
			cam2.enabled = true;

			moveX += Input.GetAxis ("Mouse X");
			//If the right click is pressed, we check in which part of the screen is clicked and we rotate the camara to see the map from another angle
			if (Input.GetMouseButtonDown (1) && moveX < 0 && rotating == false) {
				rotateR = true;
				rotating = true;
			} else if (Input.GetMouseButtonDown (1) && moveX > 0 && rotating == false) {
				rotateL = true;
				rotating = true;
			}

			if (rotateR == true && rotating == true) {
				camRot.transform.Rotate (Vector3.up * 5);
				rot += 5;
				if (rot == 90) {
					rotateR = false;
					rotating = false;
					rot = 0;
				}
			} else if (rotateL == true && rotating == true) {
				camRot.transform.Rotate (-1 * Vector3.up * 5);
				rot += 5;
				if (rot == 90) {
					rotateL = false;
					rotating = false;
					rot = 0;
				}
			}
		}
	}
}
