using System;
using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class FoodAgent : Agent
{
    public event EventHandler OnAteFood;
    public event EventHandler OnEpisodeBeginEvent;

    [SerializeField] private FoodSpawner foodSpawner;
    [SerializeField] private FoodButton foodButton;

    private Rigidbody agentRb;

    private void Awake()
    {
        agentRb = GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(UnityEngine.Random.Range(-6f, -4f), 0, UnityEngine.Random.Range(-2f, 2f));

        OnEpisodeBeginEvent?.Invoke(this, EventArgs.Empty); 
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(foodButton.CanUseButton() ? 1 : 0);
        Vector3 dirToFoodButton = (foodButton.transform.localPosition - transform.localPosition).normalized;
        sensor.AddObservation(dirToFoodButton.x);
        sensor.AddObservation(dirToFoodButton.z);

        sensor.AddObservation(foodSpawner.hasFoodSpawned ? 1 : 0);
        if (foodSpawner.hasFoodSpawned)
        {
            //Vector3 dirToFood= (foodSpawner.getLastFoodTransform().local)
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        base.Heuristic(actionsOut);
    }


    public override void OnActionReceived(ActionBuffers actions)
    {
        int moveX = actions.DiscreteActions[0];//0 = Dont move ; 1 left ; 2 =right
        int moveZ = actions.DiscreteActions[1];//0 dont move ; 1 back, 2 forward

        Vector3 addForce = new Vector3(0, 0, 0);
        switch (moveX)
        {
            case 0: addForce.x = 0f;break;
            case 1: addForce.x = -1f;break;
            case 2: addForce.x = +1f;break;
        }
        switch (moveZ)
        {
            case 0: addForce.z = 0f; break;
            case 1: addForce.z = -1f; break;
            case 2: addForce.z = +1f; break;
        }

        float moveSpeed = 5f;
        agentRb.velocity = addForce * moveSpeed + new Vector3(0, agentRb.velocity.y, 0);

        bool isUseButtonDown = actions.DiscreteActions[2] == 1;
        if (isUseButtonDown)
        {
            //Use Action
            Collider[] colliderArray = Physics.OverlapBox(transform.position, Vector3.one * .5f);
        }
    }


}
