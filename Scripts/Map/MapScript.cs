using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript: MonoBehaviour{

	[SerializeField]private bool clicked;				//Check if the section map is being clicked
	[SerializeField]private bool moving;				//Check if the section map is moving
	[SerializeField]private bool rotatRight;			//Check if the section map is rotating right
	[SerializeField]private bool rotatLeft;				//Check if the section map is rotating right 
	[SerializeField]private float time;					//Time that takes the map section to move
	[SerializeField]private int rot;					//Degrees we rotate in time

	[SerializeField]private Camera cam;
	[SerializeField]private Camera cam2;

	public ConstructorMap move;							//Public variable to check if we can move
	public Game state;									//Public variable to check which map is the one that is active
	public Movement name;								//Public variable to store the name of the section map you are in

	[SerializeField] private GameObject[] map;

	[SerializeField] private Material mouseOverFloor;
	[SerializeField] private Material origMaterialFloor;
	[SerializeField] private Material mouseOverWall;
	[SerializeField] private Material origMaterialWall;
	[SerializeField] private MeshRenderer render1;
	[SerializeField] private MeshRenderer render2;
	[SerializeField]private float positionY;


	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("FP_Camera").GetComponent<Camera>();
		cam2 = GameObject.Find ("3D_Camera").GetComponent<Camera>();
		state = GameObject.Find ("GAME").GetComponent<Game>();
		move = GameObject.Find ("3D_Map").GetComponent<ConstructorMap>();
		name = GameObject.Find ("Dude").GetComponent<Movement> ();
		clicked = false;
		moving = false;
		rotatRight = false;
		rotatLeft = false;
		rot = 0;
		map = new GameObject[2];
		render1 = this.gameObject.transform.GetChild (0).GetComponent<MeshRenderer> ();
		render2 = this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer> ();
		positionY= this.gameObject.transform.GetChild (0).transform.position.y;
	}

	// Store in a list all the objects with the same tag
	GameObject[] searchTag(string name){
		GameObject[] mapa =GameObject.FindGameObjectsWithTag(name);
		return mapa;
	}


	void OnMouseOver(){
		if (!clicked && !moving && !rotatRight && !rotatLeft) {
			render1.material = mouseOverFloor;
			render2.material = mouseOverWall;
		}
	}

	void OnMouseExit(){
		if (!clicked && !moving && !rotatRight && !rotatLeft) {
			render1.material = origMaterialFloor;
			render2.material = origMaterialWall;
		}
	}

	// Update is called once per frame
	void Update () {
		//Check if the map that is active is the second one
		if (state.active.Equals ("MAP2")) {
			cam.enabled = false;
			cam2.enabled = true;
			Cursor.lockState = CursorLockMode.None;

			Ray ray = cam2.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			//When you click the left button and the section of the map isn't the one you are in 
			if (Input.GetMouseButtonDown (0) && name.name != this.transform.tag) {
				if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
					if (hit.collider.tag == this.transform.tag) {
						//We call the method searchTag to store all the sections of both maps that have the same tag in an array
						map = searchTag (this.transform.tag);

						if (moving == false && move.canMove == false) {
							//To start moving the section up
							clicked = true;
							moving = true;
							move.canMove = true;
						} else if (moving == true && rotatLeft == false && rotatRight == false && clicked == false) {
							//To start moving the section down
							moving = false;
							clicked = true;
						}
					} 
				}
			//Change to the other map when esc is pressed
			} else if (Input.GetKey (KeyCode.Escape) && moving==false && clicked==false) {
				state.active = "MAP1";
			}

			//Starts moving the section up
			if (clicked == true && moving == true) {
				time = time + Time.deltaTime;
				if (time <= 1) {
					//Takes a second to move both sections with the same tag up
					map[0].transform.GetChild(0).position += Vector3.up * Time.deltaTime * 15;
					map[1].transform.GetChild(0).position += Vector3.up * Time.deltaTime * 15;
				} else {
					//Once a second has passed, the time is reset and clicked turned back to false
					map [0].transform.GetChild(0).position =new Vector3(map[0].transform.GetChild(0).position.x,map[0].transform.GetChild(0).position.y,map[0].transform.GetChild(0).position.z);
					map [1].transform.GetChild(0).position = new Vector3(map[1].transform.GetChild(0).position.x,map[1].transform.GetChild(0).position.y,map[1].transform.GetChild(0).position.z);
					clicked = false;
					time = 0;
				}

			}

			//Starts moving the section down
			if (clicked == true && moving == false) {
				time = time + Time.deltaTime;
				if (time <= 1) {
					map[0].transform.GetChild(0).position -= Vector3.up * Time.deltaTime * 15;
					map[1].transform.GetChild(0).position -= Vector3.up * Time.deltaTime * 15;
				} else {
					map[0].transform.GetChild(0).position = new Vector3 (map[0].transform.GetChild(0).position.x, positionY, map[0].transform.GetChild(0).position.z);
					map[1].transform.GetChild(0).position = new Vector3 (map[1].transform.GetChild(0).position.x, positionY, map[1].transform.GetChild(0).position.z);
					clicked = false;
					move.canMove = false;
					time = 0;
				}
			}
			//Rotates the section
			if (clicked == false && moving == true) {
				if (Input.GetKeyDown (KeyCode.A) && rotatLeft == false && rotatRight == false) {
					rotatRight = true;
				} else if (Input.GetKeyDown (KeyCode.D) && rotatLeft == false && rotatRight == false) {
					rotatLeft = true;
				}

				if (rotatRight == true) {
					map[0].transform.GetChild(0).Rotate (Vector3.up * 5);
					map[1].transform.GetChild(0).Rotate (Vector3.up * 5);
					rot += 5;
					if (rot == 90) {
						rotatRight = false;
						rot = 0;
					}
				} else if (rotatLeft == true) {
					map[0].transform.GetChild(0).Rotate (-1 * Vector3.up * 5);
					map[1].transform.GetChild(0).Rotate (-1 * Vector3.up * 5);
					rot += 5;
					if (rot == 90) {
						rotatLeft = false;
						rot = 0;
					}
				}
			}
		}
	}
}