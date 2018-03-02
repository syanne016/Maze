using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour {
	[SerializeField] private float time;
	[SerializeField] private bool down;
	[SerializeField] private bool up;
	[SerializeField] private float accUP;
	[SerializeField] private float accDOWN;
	[SerializeField] private float iniPos;

	// Use this for initialization
	void Start () {
		down=true;
		up=false;
		accUP = 3;
		accDOWN = 1;
		iniPos = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time > 1 && !down) {
			up =true;
		}
		if (up) {
			if (this.transform.position.y <= (iniPos+0.6f)) {
				this.transform.position += transform.up * Time.deltaTime * accUP;
			} else {
				up = false;
				time = 0;
				down = true;
			}
		} else if (down) {
			if (time > 1) {
				if (this.transform.position.y > iniPos) {
					this.transform.position -= transform.up * Time.deltaTime * accDOWN;
				} else {
					down = false;
					time = 0;
				}
			}
		}
	}
}
