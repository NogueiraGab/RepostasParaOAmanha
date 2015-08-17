using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DIRECTION { RIGHT = 1 , LEFT = -1 };

public class CameraControler : MonoBehaviour 
{
	private GameObject camera;
	private Vector3 finalWorldPos;
	private Vector3 startWorldPos;
	private Vector3 WorldPos;
	private Vector3 newFinal;
	private int currentStage = 0;
	private float totalDistance;
	private bool cameraMoving;

	[SerializeField]
	int speedCamera = 10;


	void Start()
	{
		this.camera = GameObject.Find ("Main Camera");
		this.startWorldPos = Camera.main.ScreenToWorldPoint (new Vector3(0, 0, 0));
		this.finalWorldPos = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0));
		this.WorldPos = new Vector3 (this.finalWorldPos.x - this.startWorldPos.x, this.finalWorldPos.y - this.startWorldPos.y, 0);

	}

	void Update()
	{
		this.LimitsPassed ();
	}

	IEnumerator MoveNextStage(DIRECTION direction)
	{
		yield return new WaitForFixedUpdate ();
		//Define a posiçao no centro da proxima tela e move a camera
		Vector3 newPos = new Vector3 ((this.WorldPos.x * this.currentStage) + (this.WorldPos.x * (int)direction) , 
									  this.camera.transform.position.y, 
									  this.camera.transform.position.z);  
		this.camera.transform.position = Vector3.MoveTowards (this.camera.transform.position, newPos, this.speedCamera * Time.deltaTime);

		//Confere a distancia para parar o movimento da camera.
		float currentDistance = Vector3.Distance (this.camera.transform.position, newPos);
		if (currentDistance > this.totalDistance/100) 
		{
			StartCoroutine (MoveNextStage (direction));
		} 
		else
		{
			this.cameraMoving = false;
			this.ResetWorldPos();
			this.currentStage += (int)direction;
		//	StopCoroutine(this.MoveNextStage(direction));
		}
	}

	private void SetTotalDistance(DIRECTION direction)
	{
		Vector3 newPos = new Vector3 ((this.WorldPos.x * this.currentStage) + (this.WorldPos.x * (int)direction) , 
		                              this.camera.transform.position.y, 
		                              this.camera.transform.position.z); 
		this.totalDistance = Vector3.Distance(this.camera.transform.position, newPos);
		this.cameraMoving = true;
	}

	private void LimitsPassed()
	{
		if (this.cameraMoving)
			return;

		if (this.transform.position.x > this.finalWorldPos.x)
		{
			this.SetTotalDistance (DIRECTION.RIGHT);
			StartCoroutine (MoveNextStage (DIRECTION.RIGHT));
		} 
		else if (this.transform.position.x < this.startWorldPos.x) 
		{
			this.SetTotalDistance (DIRECTION.LEFT);
			StartCoroutine (MoveNextStage (DIRECTION.LEFT));
		}
	}

	private void ResetWorldPos()
	{
		this.startWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0));
		this.finalWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
	}
}
