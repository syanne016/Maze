using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {

	[SerializeField]private float maxZ;					//Max value of the Z axis
	[SerializeField]private float friction;				//Friction value
	[SerializeField]private float accX;					//Initial acceleration in X axis
	[SerializeField]private float accY;					//Initial acceleration in Y axis
	[SerializeField]private float maxSpeedZPositive;	//Max speed in the Z+ axis
	[SerializeField]private float maxSpeedZNegative;	//Max speed in the Z- axis
	[SerializeField]private float maxSpeedX;			//Max value of the velocity in the X axis
	[SerializeField]private float maxSpeedY;			//Max value of the velocity in the y axis
    [SerializeField]public Rigidbody pj;				//Rigidbody of the character
	[SerializeField]private bool grounded;				//Check if we are on the ground
	[SerializeField]private bool nearMap;				//Check if the access to the 3Dmap is near 

    [SerializeField]private float minimumX;				//Minimum camara angle around the X axis
	[SerializeField]private float maximumX;				//Maximum camara angle around the X axis

    [SerializeField]private Camera cam;
	[SerializeField]private Camera cam2;
	[SerializeField]private Transform camRot;

    [SerializeField]private float rotationY = 0f;		//Rotation of the character around Y axis
    [SerializeField]private float rotationX = 0f;		//Rotation of the main camera around the X axis

	[SerializeField]private int first;					//Variable that checks if you want to change the map to wait 0.2 secs

	public Game state;									//Public variable to check which map is the one that is active
	public string name;									//Public variable to store the name of the section map you are in

	[SerializeField]private bool trap;
	[SerializeField]private float time;

	[SerializeField]private GameObject potion;
	[SerializeField]private GameObject key1;
	[SerializeField]private GameObject key2;
	[SerializeField]private GameObject swordSp;
	[SerializeField]private bool full;
	[SerializeField]private float angle;
	[SerializeField]private GameObject potionUI;
	[SerializeField]private GameObject key1UI;
	[SerializeField]private GameObject key2UI;
	[SerializeField]private GameObject sword;
	[SerializeField]private GameObject swordSpecial;
	[SerializeField] public int lifes;
	[SerializeField]private bool jumping;


	public enum State{
		MOVE,
		ATTACK,
		STOP,
		SUCCATTACK
	}
	[SerializeField]public State st = State.MOVE;
	public bool attack;

	public enum Inventory{
		NOTHING,
		KEY1,
		KEY2,
		SPECIAL,
		POTION
	}
	[SerializeField]private Inventory inv;
	[SerializeField]private Animator dudeController;

	[SerializeField]private Animator canvasController;
	[SerializeField]private Animator panel;
	[SerializeField]private Animator lifesController;
	[SerializeField]private GameObject lives;
	[SerializeField]private bool portalTr;

	//Store the tag of the map section you are in or access to the map
	void OnTriggerEnter (Collider other) {		
		if (other.gameObject.tag.Equals ("mapAccess")) {
			if(other.gameObject.transform.parent.parent.name.Equals("Section1") || other.gameObject.transform.parent.parent.name.Equals("Section3") && state.active.Equals ("MAP1")){  //Start
				camRot.position=new Vector3(-1478, 74, 104);
				camRot.GetChild(0).position = new Vector3 (-1478, 99, 145.3f);
				if(other.gameObject.transform.GetChild (3).GetComponent<ParticleSystem>().tag.Equals("3D")){

				}
				nearMap = true;
			}
			else if(other.gameObject.transform.parent.parent.name.Equals("Section7") || other.gameObject.transform.parent.parent.name.Equals("Section8") && state.active.Equals ("MAP1")){ //Middle
				camRot.position=new Vector3(-1668.5f,53,95);
				camRot.GetChild(0).position = new Vector3 (-1668.5f, 77.8f, 130.7f);
				if(other.gameObject.transform.GetChild (3).GetComponent<ParticleSystem>().tag.Equals("3D")){
					other.gameObject.transform.GetChild (3).GetComponent<GameObject> ().SetActive (true);
				}
				nearMap = true;
			}
			else if(other.gameObject.transform.parent.parent.name.Equals("Section11") && state.active.Equals ("MAP1")){ //Barracones
				camRot.position=new Vector3(-1581.5f, 75, -118);
				camRot.GetChild(0).position = new Vector3 (-1581.5f,99.8f, -82.3f);
				if(other.gameObject.transform.GetChild (3).GetComponent<ParticleSystem>().tag.Equals("3D")){
					other.gameObject.transform.GetChild (3).GetComponent<GameObject> ().SetActive (true);
				}
				nearMap = true;
			}
			else if(other.gameObject.transform.parent.parent.name.Equals("Section12") && state.active.Equals ("MAP1")){ //Zona Agua
				camRot.position=new Vector3(-1720.5f, 66, -140);
				camRot.GetChild(0).position = new Vector3 (-1720.5f, 76.3f, -87.9f);
				if(other.gameObject.transform.GetChild (3).GetComponent<ParticleSystem>().tag.Equals("3D")){
					other.gameObject.transform.GetChild (3).GetComponent<GameObject> ().SetActive (true);
				}
				nearMap = true;
			}
			else if(other.gameObject.transform.parent.parent.name.Equals("Section9")){ //Torre
				camRot.position=new Vector3(-1860,66,-14);
				camRot.GetChild(0).position = new Vector3 (-1860, 90.8f, 21.7f);
				/*if(other.gameObject.transform.GetChild (3).GetComponent<ParticleSystem>().tag.Equals("3D")){
					other.gameObject.transform.GetChild (3).GetComponent<GameObject> ().SetActive (true);
				}*/
				nearMap = true;
			}
			else if(other.gameObject.transform.parent.parent.parent.name.Equals("TutoralSecction2")){
				if(other.gameObject.transform.GetChild (3).GetComponent<ParticleSystem>().tag.Equals("3D")){
					//other.gameObject.transform.GetChild (3).GetComponent<GameObject> ().SetActive (true);
				}
				canvasController.SetBool ("Asset3D", true);
				nearMap = true;
			}


		} /*else if(other.gameObject.tag.Equals("MAP")) {
			name = other.gameObject.transform.GetChild (0).tag;
			this.transform.parent = other.gameObject.transform;
		}*/
		Debug.Log (other.gameObject.transform.name);
		if (other.gameObject.tag.Equals ("portal")) {
			portalTr = true;
		}

		if(other.gameObject.tag.Equals("Trap")){
			trap=true;
		}
		if (other.gameObject.tag.Equals ("Potion") && Input.GetKey (KeyCode.E) && inv == Inventory.NOTHING) {
			panel.SetBool ("Potion", true);
			potion.SetActive (true);
			inv = Inventory.POTION;
			potion.gameObject.SetActive (true);
			key1.gameObject.SetActive (false);
			key2.gameObject.SetActive (false);
			potion.SetActive (true);
		} else if (other.gameObject.tag.Equals ("Key1") && Input.GetKey (KeyCode.E) && inv == Inventory.NOTHING) {
			panel.SetBool ("Key1", true);
			key1.SetActive(true);
			inv = Inventory.KEY1;
			potion.gameObject.SetActive (false);
			key1.gameObject.SetActive (true);
			key2.gameObject.SetActive (false);
			key1.SetActive (true);
		} else if (other.gameObject.tag.Equals ("Key2") && Input.GetKey (KeyCode.E) && inv == Inventory.NOTHING) {
			panel.SetBool ("Key2", true);
			key2.SetActive(true);
			inv = Inventory.KEY2;
			potion.gameObject.SetActive (false);
			key1.gameObject.SetActive (false);
			key2.gameObject.SetActive (true);
			key2.SetActive (true);
		} else if (other.gameObject.tag.Equals ("SwordSpecial") && Input.GetKey (KeyCode.E) && inv == Inventory.NOTHING) {
			panel.SetBool ("SwordSpecial", true);
			swordSp.SetActive (true);
			inv = Inventory.SPECIAL;
			sword.gameObject.SetActive (false);
			swordSpecial.gameObject.SetActive(true);

		}

	}
	void OnTriggerExit (Collider other) {
		if (other.gameObject.tag.Equals ("mapAccess")) {
			nearMap = false;
		}
		if (other.gameObject.tag.Equals ("portal")) {
			portalTr = false;

		}
	}
   // Use this for initialization
	void Start () {
		cam = GameObject.Find ("FP_Camera").GetComponent<Camera>();
		cam2 = GameObject.Find ("3D_Camera").GetComponent<Camera>();
		camRot = GameObject.Find ("CamRotation").GetComponent<Transform> ();
		state = GameObject.Find ("GAME").GetComponent<Game>();
        pj = this.GetComponent<Rigidbody>();
		minimumX = -30f;
		maximumX = 30f;
		accY = 8;
		grounded = true;
		nearMap = false;
		trap = false;
		inv = Inventory.NOTHING;
		sword.gameObject.SetActive (true);
		lifes = 3;
		attack = false;
		jumping = false;
		st = State.MOVE;
    }

	//Coroutine for the 3D map to not be affected inmediatly
	IEnumerator Wait(){
		canvasController.SetTrigger ("Fade");
		canvasController.SetBool ("Asset2D", false);
		yield return new WaitForSeconds(0.5f);	
		state.active = "MAP2";
		nearMap = false;
	}

    // Update is called once per frame
    void Update () {
		//Check if the map that is active is the first one
		if (state.active.Equals ("MAP1")) {
			if (st == State.MOVE) {
				//Cursor.lockState = CursorLockMode.Locked;
				cam.enabled = true;
				cam2.enabled = false;

				Cursor.lockState = CursorLockMode.None;

				//Check if the object you are near is the one that access the map
				if (Input.GetMouseButtonDown (1) && nearMap == true) {
					StartCoroutine (Wait ());
				}

				rotationY += Input.GetAxis ("Mouse X");
				rotationX += Input.GetAxis ("Mouse Y");

				//Rotation capped
				rotationX = Mathf.Clamp (rotationX, minimumX, maximumX);

				//Rotation of the camara and the character
				cam.transform.localEulerAngles = new Vector3 (-rotationX, 0, 0);
				this.transform.localEulerAngles = new Vector3 (0, rotationY*2f, 0);

				if (lifes == 2) {
					lifesController.SetInteger ("lifes", lifes);
					lives.transform.GetChild (2).transform.GetChild (0).GetComponent<Image> ().enabled = false;
				} else if (lifes == 1) {
					lifesController.SetInteger ("lifes", lifes);
					lives.transform.GetChild (1).transform.GetChild (0).GetComponent<Image> ().enabled = false;
				}else if (lifes == 0) {
					lifesController.SetInteger ("lifes", lifes);
					lives.transform.GetChild (0).transform.GetChild (0).GetComponent<Image> ().enabled = false;
					canvasController.SetTrigger ("Fade");
					SceneManager.LoadScene( "GameOver", LoadSceneMode.Single );
				}
				Move ();
			} else if (st == State.ATTACK) {
				Attack ();
			} else if (st == State.STOP) {
				Stop ();
			}


			if (!Input.anyKeyDown) {			
				if (inv == Inventory.NOTHING || inv == Inventory.SPECIAL) {
					dudeController.SetBool ("Move", false);
				} else {
					dudeController.SetBool ("MoveObj", false);
				}
			}
			if (portalTr) {
				if (Input.GetMouseButton (0)) {
					SceneManager.LoadScene("PJEI_Examen", LoadSceneMode.Single);
				}
			}
		} 
    }

	void Move(){
		//FORWARD
		if (Input.GetKey (KeyCode.W) && !trap) {
			pj.velocity += this.transform.forward * Time.deltaTime * accX;
			if (inv == Inventory.NOTHING || inv == Inventory.SPECIAL) {
				dudeController.SetBool ("Move", true);
			} else {
				dudeController.SetBool ("MoveObj", true);
			}
		}	
		//BACKWARD
		if (Input.GetKey (KeyCode.S) && !trap) {
			pj.velocity -= this.transform.forward * Time.deltaTime * accX;
			if (inv == Inventory.NOTHING || inv == Inventory.SPECIAL) {
				dudeController.SetBool ("Move", true);
			} else {
				dudeController.SetBool ("MoveObj", true);
			}
		}
		//RIGHT
		if (Input.GetKey (KeyCode.D) && !trap) {
			pj.velocity += this.transform.right * Time.deltaTime * accX;
			if (inv == Inventory.NOTHING || inv == Inventory.SPECIAL) {
				dudeController.SetBool ("Move", true);
			} else {
				dudeController.SetBool ("MoveObj", true);
			}
		}
		//LEFT
		if (Input.GetKey (KeyCode.A) && !trap) {
			pj.velocity -= this.transform.right * Time.deltaTime * accX;
			if (inv == Inventory.NOTHING || inv == Inventory.SPECIAL) {
				dudeController.SetBool ("Move", true);
			} else {
				dudeController.SetBool ("MoveObj", true);
			}
		}
	
		if (Input.GetKeyDown(KeyCode.Space) && grounded == true && !trap) {
			dudeController.SetBool ("Jump", true);
			jumping = true;
			pj.AddForce(new Vector3(0, accY, 0),ForceMode.Impulse);
			grounded = false;

		}

		if (pj.velocity.y < 0) {
			dudeController.SetBool ("Falling", true);
		} 
		/*if (grounded) {
			pj.velocity = new Vector3 (pj.velocity.x, 0, pj.velocity.z);
		}*/
		if(trap && grounded == true){		
			if (inv == Inventory.NOTHING || inv == Inventory.SPECIAL) {
			}
			pj.AddForce(new Vector3(0, 2, 0),ForceMode.Impulse);
			pj.AddForce(new Vector3(0, 0, -2),ForceMode.Impulse);
			lifes--;
			trap = false;
		}
			

		//Friction (to stop)
		pj.velocity = Vector3.ProjectOnPlane (pj.velocity, Vector3.up) * (1 - friction * Time.deltaTime) + Vector3.Project (pj.velocity, Vector3.up);
		//Max Speed
		maxZ = maxSpeedZPositive;
		if (Vector3.Angle (Vector3.Project (pj.velocity, transform.forward), transform.forward) > 90) {
			maxZ = maxSpeedZNegative;
		}
		pj.velocity = Vector3.ClampMagnitude (Vector3.Project (pj.velocity, transform.forward), maxZ) + Vector3.ClampMagnitude (Vector3.Project (pj.velocity, transform.right), maxSpeedX) + Vector3.Project (pj.velocity, transform.up);
		if (this.transform.position.y < -17) {
			canvasController.SetTrigger ("Fade");
			SceneManager.LoadScene( "GameOver", LoadSceneMode.Single );
		}

		if (Input.GetMouseButtonDown (0)) {
			if (inv == Inventory.NOTHING || inv == Inventory.SPECIAL) {
				dudeController.SetBool ("Move", false);
			} else {
				dudeController.SetBool ("MoveObj", false);
			}
			StartCoroutine(WaitAttack ());
			st = State.ATTACK;
		}
		if (inv != Inventory.NOTHING || inv != Inventory.SPECIAL) {
			if (inv == Inventory.POTION && Input.GetKeyDown(KeyCode.E)) {
				panel.SetBool ("Potion", false);
				potion.SetActive (false);
			}else if (inv == Inventory.KEY1 && Input.GetKeyDown(KeyCode.E)) {
				panel.SetBool ("Key1", false);
			}else if (inv == Inventory.KEY2 && Input.GetKeyDown(KeyCode.E)) {
				panel.SetBool ("Key2", false);
			}
		}
	}

	void Jump(){

	}

	IEnumerator WaitAttack(){
		attack = true;
		dudeController.SetBool ("Attack", true);
		yield return new WaitForSeconds(1.05f);
		dudeController.SetBool ("Attack", false);
		yield return new WaitForSeconds (0.1f);
		attack = false;
		st = State.MOVE;

	}
	void Attack(){

	}

	void Stop(){
		StartCoroutine(WaitStop ());
	}

	IEnumerator WaitStop(){
		yield return new WaitForSeconds (0.8f);
		attack = false;
		st = State.MOVE;

	}


	/*void OnCollisionStay(Collision coll){
		if (coll.collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			grounded = true;
			jumping = false;
		}

	}
	void OnCollisionEnter(Collision coll){
		if (coll.collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			grounded = true;
			jumping = false;
			dudeController.SetBool ("Jump", false);
			dudeController.SetBool ("Falling", false);
		}

	}
	void OnCollisionExit(Collision coll){
		if (coll.collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			grounded = false;
			jumping = true;
			if (jumping) {
			}
		

		}
	}*/
	void OnCollisionStay(Collision coll){
		if (coll.collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			grounded = true;
			jumping = false;
		}

	}
	void OnCollisionExit(Collision coll){
		if (coll.collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			grounded = false;
			if (jumping) {
				
			}

		}
	}
}
