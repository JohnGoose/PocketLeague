using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Goal : MonoBehaviour
{
	public GameObject spawner;
	public GameObject ball;
	
	public GameObject goalBlue;
	public GameObject explode;
	
	public TextMeshProUGUI scoreBlue;

	void OnTriggerEnter(Collider other) 
	{
		int score;
		if (other.CompareTag("Ball"))
		{
			if (Resources.FindObjectsOfTypeAll<PlayerParticleSystem>()[0] != null)
            Resources.FindObjectsOfTypeAll<PlayerParticleSystem>()[0].gameObject.SetActive(true);
			ball.transform.position = spawner.transform.position;
			ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
			ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

			int.TryParse(scoreBlue.text, out score);
			score++;
			scoreBlue.text = score.ToString();
		}
	}
}
