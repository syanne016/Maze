using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalFly1 : MonoBehaviour {
	public float tim;
	public Vector3 position;
	// Use this for initialization
	void Start () {
		position = this.transform.position;
	}

	// Update is called once per frame
	void Update () {
		tim += Time.deltaTime;
		if (tim <= 2.5) {
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + 0.08f, this.transform.position.z);
		} else if (tim <= 5) {
			this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y - 0.08f, this.transform.position.z);

		} else {

			tim = 0;
		}


	}
}
