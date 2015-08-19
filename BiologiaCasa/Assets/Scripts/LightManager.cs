using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightManager : MonoBehaviour 
{
	private GameObject[] allLights;
	private List<float> lightsIntensity;
	private Light currentLight;
	private int currentStage;
	[SerializeField]
	private float maxIntensity = 0.9f;
	private float speedIntensity = 0.05f;
	private bool lightTurningOn;

	void Start()
	{
		this.allLights = new GameObject[2];
		this.lightsIntensity = new List<float> ();
		for (int i=0; i<allLights.Length; i++)
		{
			this.allLights[i] = GameObject.Find ("Light_" + i);
			this.lightsIntensity.Add(0);
		}

	}

	void OnTriggerStay2D(Collider2D c)
	{
		if(c.tag.Equals(Tags.anSwitch) && Input.GetKeyDown(KeyCode.Space) && lightTurningOn == false)
		{
			this.currentLight = c.GetComponentInChildren<Light>();
			this.lightTurningOn = true;

			if(this.currentLight.intensity == 0)
			StartCoroutine(TurnOn());
			else
				StartCoroutine(TurnOff());
		}
	}

	IEnumerator TurnOn()
	{
		yield return new WaitForFixedUpdate ();
		if (this.currentLight != null) 
		{
			this.currentLight.intensity += speedIntensity;

			if (this.currentLight.intensity < this.maxIntensity)
				StartCoroutine (TurnOn ());
			else
				this.lightTurningOn = false;
		}
	}

	IEnumerator TurnOff()
	{
		yield return new WaitForFixedUpdate ();
		if (this.currentLight != null) 
		{
			this.currentLight.intensity += -speedIntensity;
			
			if (this.currentLight.intensity > 0)
				StartCoroutine (TurnOff ());
			else
				this.lightTurningOn = false;
		}
	}

	public  void SetCurrentStage(int stage)
	{
		this.currentStage = stage;
		this.ChangeStage ();
	}

	public  void ChangeStage()
	{
		for (int i=0; i<this.allLights.Length; i++)
		{
			print ("c" + this.currentStage);
			if(this.allLights[i].name.Substring(this.allLights[i].name.Length-1) == this.currentStage.ToString())
			   {
				this.allLights[i].GetComponent<Light>().intensity = this.lightsIntensity[i];
				}
			else
			{
				if(this.lightsIntensity.Count-1 > i)
				this.lightsIntensity[i] = this.currentLight.intensity;

				this.allLights[i].GetComponent<Light>().intensity = 0;
			}
		}
	}
}
