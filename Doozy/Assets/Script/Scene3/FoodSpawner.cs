using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{

    public bool hasFoodSpawned = false;
    private void Start()
    {
        hasFoodSpawned = true;
    }
    public void setHasFoodSpawned(bool status)
    {
        hasFoodSpawned = status;
    }
}
