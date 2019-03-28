using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
	 
	Rigidbody body;
	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody>();
		
		Invoke("AddF", 3);
	}

	void AddF(){
		body.AddForce(Vector3.right * 100, ForceMode.Acceleration);
		Debug.Log("ppppppppppp");
	}
	
	// Update is called once per frame
	void Update () {
	 
	}
}
