using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour {
	[SerializeField] private float time;
	[SerializeField] private bool left;
	[SerializeField] private bool right;
	[SerializeField] private float accRight;
	[SerializeField] private float accLeft;
	[SerializeField] private float initLeft;
	[SerializeField] private float initRight;

	// Use this for initialization
	void Start () {
		left=false;
		right=true;
		accRight = 12f;
		accLeft = 3f;
		initLeft = this.transform.position.x;
		initRight = this.transform.position.x+4.5f;
	}
	
	// rightdate is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 0.8 && !left) {
			right =true;
		}
		if (right) {
			if (this.transform.position.x <= initRight) {
				this.transform.position += transform.right * Time.deltaTime * accRight;
			} else {
				right = false;
				time = 0;
				left = true;
			}
		} else if (left) {
			if(time>0.5){
				if (this.transform.position.x > initLeft) {
					this.transform.position -= transform.right * Time.deltaTime * accLeft;
				} else {
					left = false;
					time = 0;
				}
			}
			
		}
	}
}