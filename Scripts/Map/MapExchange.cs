using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapExchange : MonoBehaviour {
	[SerializeField]private bool clicked;				//Check if the section map is being clicked
	[SerializeField]private bool moving;				//Check if the section map is moving
	[SerializeField]private bool exchange;			//Check if the section map is rotating right
	[SerializeField]private float time;					//Time that takes the map section to move
	[SerializeField]private int rot;

	[SerializeField]private Camera cam;
	[SerializeField]private Camera cam2;

	public ConstructorMap move;							//Public variable to check if we can move
	public Game state;									//Public variable to check which map is the one that is active
	public Movement name;								//Public variable to store the name of the section map you are in

	[SerializeField] private GameObject[] map;
	[SerializeField] private GameObject[] map2;

	[SerializeField] private Material mouseOverFloor;
	[SerializeField] private Material origMaterialFloor;
	[SerializeField] private MeshRenderer render1;
	[SerializeField]private float positionY;
	[SerializeField]private float positionYEx;
	[SerializeField]private GameObject excSection;
	[SerializeField]private GameObject excParent;


	// Use this for initialization
	void Start () {
		cam = GameObject.Find ("FP_Camera").GetComponent<Camera>();
		cam2 = GameObject.Find ("3D_Camera").GetComponent<Camera>();
		state = GameObject.Find ("GAME").GetComponent<Game>();
		move = GameObject.Find ("3D_Map").GetComponent<ConstructorMap>();
		name = GameObject.Find ("Dude").GetComponent<Movement> ();
		clicked = false;
		moving = false;
		exchange = false;
		excParent = this.transform.parent.gameObject;
		if (this.gameObject == excParent.transform.GetChild (1).gameObject) {
			excSection = excParent.transform.GetChild (0).gameObject;

		} else {
			excSection = excParent.transform.GetChild (1).gameObject;
		}

		map = new GameObject[2];
		map2 = new GameObject[2];
		render1 = this.gameObject.transform.GetComponent<MeshRenderer> ();
		positionY= this.gameObject.transform.position.y;
		positionYEx = excSection.gameObject.transform.position.y;
	}

	// Store in a list all the objects with the same tag
	GameObject[] searchTag(string name){
		GameObject[] mapa =GameObject.FindGameObjectsWithTag(name);
		return mapa;
	}


	void OnMouseOver(){
		if (!clicked && !moving && !exchange) {
			render1.material = mouseOverFloor;
		}
	}

	void OnMouseExit(){
		if (!clicked && !moving && !exchange) {
			render1.material = origMaterialFloor;
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
						map2 = searchTag (excSection.transform.tag);

						if (moving == false && move.canMove == false) {
							//To start moving the section up
							clicked = true;
							moving = true;
							move.canMove = true;
						} else if (moving == true && exchange == false && clicked == false) {
							//To start moving the section down
							moving = false;
							clicked = true;
						}
					} 
				}
				//Change to the other map when esc is pressed
			} else if (Input.GetKey (KeyCode.Escape) && moving==false && clicked==false && exchange == false) {
				state.active = "MAP1";
			}

			//Starts moving the section up
			if (clicked == true && moving == true) {
				time = time + Time.deltaTime;
				if (time <= 1) {
					//Takes a second to move both sections with the same tag up
					map[0].transform.position += Vector3.up * Time.deltaTime * 15;
					map[1].transform.position += Vector3.up * Time.deltaTime * 15;
					map2 [0].transform.position += Vector3.up * Time.deltaTime * 15;
					map2 [1].transform.position += Vector3.up * Time.deltaTime * 15;
				} else {
					//Once a second has passed, the time is reset and clicked turned back to false
					map [0].transform.GetChild(0).position =new Vector3(map[0].transform.position.x,map[0].transform.position.y,map[0].transform.position.z);
					map [1].transform.GetChild(0).position = new Vector3(map[1].transform.position.x,map[1].transform.position.y,map[1].transform.position.z);
					map2 [0].transform.GetChild(0).position =new Vector3(map2[0].transform.position.x,map2[0].transform.position.y,map2[0].transform.position.z);
					map2 [1].transform.GetChild(0).position = new Vector3(map2[1].transform.position.x,map2[1].transform.position.y,map2[1].transform.position.z);
					clicked = false;
					time = 0;
				}

			}

			//Starts moving the section down
			if (clicked == true && moving == false) {
				time = time + Time.deltaTime;
				if (time <= 1) {
					map[0].transform.position -= Vector3.up * Time.deltaTime * 15;
					map[1].transform.position -= Vector3.up * Time.deltaTime * 15;
					map2 [0].transform.position -= Vector3.up * Time.deltaTime * 15;
					map2 [1].transform.position -= Vector3.up * Time.deltaTime * 15;
				} else {
					map[0].transform.position = new Vector3 (map[0].transform.position.x, positionY, map[0].transform.position.z);
					map[1].transform.position = new Vector3 (map[1].transform.position.x, positionY, map[1].transform.position.z);
					map2 [0].transform.position = new Vector3 (map2 [0].transform.position.x, positionYEx, map2 [0].transform.position.z);
					map2 [1].transform.position = new Vector3 (map2 [1].transform.position.x, positionYEx, map2 [1].transform.position.z);

					clicked = false;
					move.canMove = false;
					time = 0;
				}
			}
			//Rotates the section
			if (clicked == false && moving == true) {
				if (Input.GetKeyDown (KeyCode.W) && exchange == false) {
					exchange = true;
				}

				if (exchange == true) {
					rot += 5;
					if (rot <= 180) {
						map [0].transform.localEulerAngles = new Vector3 (map [0].transform.localEulerAngles.x, map [0].transform.localEulerAngles.y + 5, map [0].transform.localEulerAngles.z);
						map [1].transform.localEulerAngles = new Vector3 (map [1].transform.localEulerAngles.x, map [1].transform.localEulerAngles.y + 5, map [1].transform.localEulerAngles.z);
						map2 [0].transform.localEulerAngles = new Vector3 (map2 [0].transform.localEulerAngles.x, map2 [0].transform.localEulerAngles.y + 5, map2 [0].transform.localEulerAngles.z);
						map2 [1].transform.localEulerAngles = new Vector3 (map2 [1].transform.localEulerAngles.x, map2 [1].transform.localEulerAngles.y + 5, map2 [1].transform.localEulerAngles.z);
						excParent.transform.localEulerAngles = new Vector3 (excParent.transform.localEulerAngles.x, excParent.transform.localEulerAngles.y + 5, excParent.transform.localEulerAngles.z);	
					} else {
						rot = 0;
						exchange = false;
					}
				} 
			}
		}
	}
}
