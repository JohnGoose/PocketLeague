using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Count : MonoBehaviour
{
	public float timeValue = 300;
	public TextMeshProUGUI mText;
	public TextMeshProUGUI scoreBlue;
	public TextMeshProUGUI scoreRed;

	public TextMeshProUGUI winner;
	
	//timerText;


	void Update() 
	{
		if (timeValue > 0)
		{
			timeValue -= Time.deltaTime;
		}

		else
		{
			timeValue = 0;
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
		DisplayTime(timeValue);

	}
# region GUI
	private void DisplayTime(float timeToDisplay)
	{
		if (timeToDisplay < 0)
			timeToDisplay = 0;

		float minutes = Mathf.FloorToInt(timeToDisplay / 60);
		float seconds = Mathf.FloorToInt(timeToDisplay % 60);

		mText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}
	#endregion
}
