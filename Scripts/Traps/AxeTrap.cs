using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeTrap : MonoBehaviour {

	[SerializeField] private float time;
	[SerializeField] private float timeHide;
	[SerializeField] private bool hide;
	[SerializeField] private bool moveRight;
	[SerializeField] private bool moveLeft;
	[SerializeField] private float rotation;

	// Use this for initialization
	void Start () {
		this.transform.localEulerAngles = new Vector3(30,0,0);
		hide=true;
		moveLeft =false;
		moveRight=false;
		timeHide=1f;
		rotation =30;
	}
	
	// Update is called once per frame
	void Update () {
		if(rotation==30 || rotation==-30){
			hide =true;
			moveRight=false;
			moveLeft=false;
		}
		if(hide){
			time += Time.deltaTime;
			if(time>timeHide && (rotation==30 || rotation==-30)){
				if(rotation==30){
					moveLeft=true;
					moveRight=false;
					hide=false;
					this.transform.localEulerAngles=new Vector3(this.transform.localEulerAngles.x-3, 0,0);
					rotation-=3;
					time=0;
				}
				else if(rotation==-30){
					moveRight=true;
					moveLeft = false;
					hide=false;
					this.transform.localEulerAngles=new Vector3(this.transform.localEulerAngles.x+3,0,0);
					rotation+=3;
					time=0;
				}
			}
		}
		else if(moveRight){
			this.transform.localEulerAngles=new Vector3(this.transform.localEulerAngles.x+3,0,0);
			rotation+=3;
		}
		else if(moveLeft){
			this.transform.localEulerAngles=new Vector3(this.transform.localEulerAngles.x-3, 0,0);
			rotation-=3;
		}

	}
}
