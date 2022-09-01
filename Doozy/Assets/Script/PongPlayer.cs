using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class PongPlayer : Agent
{
	[SerializeField]Rigidbody rbBall;


	[SerializeField] float moveSpeed=5f;
	[SerializeField] float power = 10f;

	[SerializeField] Material win, lose;
	[SerializeField] MeshRenderer meshRenderer;
	internal void Penalty()
	{
		//SetReward(-1);

		meshRenderer.material = lose;
		EndEpisode();
	}

	public override void OnEpisodeBegin()
	{
		//meshRenderer.material = win;

		rbBall.transform.localPosition = new Vector3(0, 1f, 0);
		rbBall.velocity = Vector3.zero;
		Vector3 dest = new Vector3(Random.Range(-4f, 4f), 7);

		Vector3 direction = (dest - rbBall.transform.localPosition).normalized;
		rbBall.AddForce(direction * power, ForceMode.VelocityChange);
		transform.localPosition = Vector3.zero;
	}
	public override void CollectObservations(VectorSensor sensor)
	{
		sensor.AddObservation(transform.localPosition.x);
		sensor.AddObservation(rbBall.transform.localPosition.x);
		sensor.AddObservation(rbBall.transform.localPosition.y);

	}

	public override void OnActionReceived(ActionBuffers actions)
	{
		int action = actions.DiscreteActions[0];
		print(action);
		int h = 0;
		switch (action)
		{
			case 0: h = 0;break;
			case 1: h = -1;break;
			case 2: h = 1;break;
		}
		float currentX = transform.localPosition.x;
		float nextX = currentX + h * Time.deltaTime * moveSpeed;
		nextX = Mathf.Clamp(nextX, -4f, 4f);
		transform.localPosition = new Vector3(nextX, 0, 0);
	}

	public override void Heuristic(in ActionBuffers actionsOut)
	{
		float h = Input.GetAxisRaw("Horizontal");
		h = h == 0 ? 0 : h < 0 ? 1 : 2;
		var discreteActionsOut = actionsOut.DiscreteActions;
		discreteActionsOut[0] = (int)h;
		
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("ball"))
		{
			//SetReward(1);
			AddReward(0.01f);
			meshRenderer.material = win;
			Vector3 dest = new Vector3(Random.Range(-4f, 4f), 7);
			Vector3 direction = (dest - rbBall.transform.localPosition).normalized;
			rbBall.AddForce(direction * power, ForceMode.VelocityChange);
			//EndEpisode();
		}	
	}

}
