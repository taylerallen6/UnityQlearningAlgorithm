using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{

	void Awake()
	{
		gameObject.AddComponent<Rigidbody>();
	}

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	void OnCollisionEnter(Collision col)
	{
		Debug.Log(gameObject.name + " has collided with " + col.gameObject.name);
	}

	void OnCollisionExit(Collision col)
	{
		Debug.Log(gameObject.name + " is no longer colliding with " + col.gameObject.name);
	}
}
