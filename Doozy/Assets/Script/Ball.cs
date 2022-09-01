using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] PongPlayer pongPlayer;
	private void OnTriggerEnter(Collider other)
	{
		pongPlayer.Penalty();
	}
}
