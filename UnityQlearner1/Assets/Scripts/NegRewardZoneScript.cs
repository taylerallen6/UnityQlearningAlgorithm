using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegRewardZoneScript : MonoBehaviour
{
	Collider myCollider;

	void Awake()
	{
		myCollider = GetComponent<Collider>();
		myCollider.isTrigger = true;
	}

	// detect overlap with object
	void OnTriggerEnter(Collider col)
	{
		if(col.GetComponent<PlayerScript>() != null)
		{
			Debug.Log(gameObject.name + " gave Negative reward to " + col.gameObject.name);
		}
	}
}
