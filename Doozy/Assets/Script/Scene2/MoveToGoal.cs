using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MoveToGoal : Agent
{
    [SerializeField] Transform goal;
    public float moveSpeed =5f;
    [SerializeField] Material win, lose;
    [SerializeField] MeshRenderer mesh;
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition.x);
        sensor.AddObservation(transform.localPosition.z);
        sensor.AddObservation(goal.localPosition.x);
        sensor.AddObservation(goal.localPosition.z);

    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuouActions = actionsOut.ContinuousActions;
        continuouActions[0] = Input.GetAxisRaw("Horizontal");
        continuouActions[1] = Input.GetAxisRaw("Vertical");

    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-6f,-4f),0,Random.Range(-2f,2f));
        goal.localPosition = new Vector3(Random.Range(4f, 6f), 0, Random.Range(-2f, 2f)) ;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];

        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;

    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Goal>(out Goal goal)){
            SetReward(1f);
            mesh.material = win;
            EndEpisode();
        }
        if( other.TryGetComponent<Wall>(out Wall wall))
        {
            SetReward(-1f);
            mesh.material = lose;
            EndEpisode();
        }
    }
}
