using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QLearner;

public class PlayerScript : MonoBehaviour
{
	public float moveDistance = 1f;
	public float xStartPos = -4f;
	public float yStartPos = 1.5f;
	public float zStartPos = -4f;

	Rigidbody rb;

	private bool posTrigger = false;
	private bool negTrigger = false;
	private bool restartPostion = false;

	QLearnerScript QL = new QLearnerScript(5);

	public float posRewardVal = 10f;
	public float negRewardVal = -10f;
	public float timeCostVal = -1f;
	private float reward = 0f;


	float myX;
	float myZ;

	void Awake()
	{
		gameObject.AddComponent<Rigidbody>();
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
		rb.freezeRotation = true;
	}

	// Update is called once per frame
	void Update ()
	{

		int action = QL.main(new float[] {transform.position.x, transform.position.z}, reward);
		reward = 0f;

		MoveAround(action);
		
		reward += timeCostVal;

		if(posTrigger)
		{
			reward += posRewardVal;
		}
		if(negTrigger)
		{
			reward += negRewardVal;
		}

		rb.velocity = Vector3.zero;

		if(restartPostion)
		{
			transform.position = new Vector3(xStartPos, yStartPos, zStartPos);
			restartPostion = false;
			Debug.Log("restarting position");
		}

		// transform.position = new Vector3(transform.position.x, yStartPos, transform.position.z);
	}


	// detect overlap with object
	void OnTriggerEnter(Collider col)
	{
		// reaction to POS and NEG reward objects
		if(col.GetComponent<RewardObjectScript>() != null)
		{
			posTrigger = true;
			// Debug.Log(gameObject.name + " recieved POSITIVE reward from " + col.gameObject.name);
		}
		if(col.GetComponent<NegRewardObjectScript>() != null)
		{
			negTrigger = true;
			// Debug.Log(gameObject.name + " recieved NEGATIVE reward from " + col.gameObject.name);
		}

		// reaction to POS and NEG reward zones
		if(col.GetComponent<RewardZoneScript>() != null)
		{
			posTrigger = true;
			restartPostion = true;
			// Debug.Log(gameObject.name + " recieved POSITIVE reward from " + col.gameObject.name);
		}
		if(col.GetComponent<NegRewardZoneScript>() != null)
		{
			negTrigger = true;
			restartPostion = true;
			// Debug.Log(gameObject.name + " recieved NEGATIVE reward from " + col.gameObject.name);
		}
	}

	// detect overlap with object
	void OnTriggerExit(Collider col)
	{
		// reaction to POS and NEG reward objects
		if(col.GetComponent<RewardObjectScript>() != null)
		{
			posTrigger = false;
		}
		if(col.GetComponent<NegRewardObjectScript>() != null)
		{
			negTrigger = false;
		}

		// reaction to POS and NEG reward zones
		if(col.GetComponent<RewardZoneScript>() != null)
		{
			posTrigger = false;
		}
		if(col.GetComponent<NegRewardZoneScript>() != null)
		{
			negTrigger = false;
		}
	}



//	|-------------------------------------------|
//	|	MY CUSTOM FUNCTIONS						|
//	|-------------------------------------------|

	// move around using keys
	void MoveAround()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			rb.MovePosition(transform.position + transform.forward * moveDistance);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			rb.MovePosition(transform.position + transform.forward * -moveDistance);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			rb.MovePosition(transform.position + transform.right * moveDistance);
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			rb.MovePosition(transform.position + transform.right * -moveDistance);
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			return;
		}
	}

	// move around overload for AI
	void MoveAround(int inputVal)
	{

		if(inputVal == 1)
		{
			rb.MovePosition(transform.position + transform.forward * moveDistance);
		}
		if(inputVal == 2)
		{
			rb.MovePosition(transform.position + transform.forward * -moveDistance);
		}
		if(inputVal == 3)
		{
			rb.MovePosition(transform.position + transform.right * moveDistance);
		}
		if(inputVal == 4)
		{
			rb.MovePosition(transform.position + transform.right * -moveDistance);
		}

		if(inputVal == 0)
		{
			return;
		}
	}
}
