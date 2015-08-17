using UnityEngine;
using System.Collections;

enum SCALE { GROWUP = 1,  DECREASE = -1 }

public class DoorController : MonoBehaviour 
{
	private GameObject camera;
	private Vector3 finalWorldPos;
	private Vector3 startWorldPos;
	private Vector3 WorldPos;
	private GameObject pair;

	private int currentStageY;
	private int speedCamera = 10;
	private bool cameraMoving;
	private float totalDistance;

	void Start () 
	{
		this.camera = GameObject.Find ("Main Camera");
		this.startWorldPos = Camera.main.ScreenToWorldPoint (new Vector3(0, 0, 0));
		this.finalWorldPos = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0));
		this.WorldPos = new Vector3 (this.finalWorldPos.x - this.startWorldPos.x, this.finalWorldPos.y - this.startWorldPos.y, 0);
	}

	IEnumerator MoveNextStage(ORIENTATION orientation)
	{
		yield return new WaitForFixedUpdate ();
		//Define a posiçao no centro da proxima tela e move a camera
		Vector3 newPos = new Vector3 (this.camera.transform.position.x , 
		                              (this.WorldPos.y * this.currentStageY) + (this.WorldPos.y * (int)orientation), 
		                              this.camera.transform.position.z);  
		this.camera.transform.position = Vector3.MoveTowards (this.camera.transform.position, newPos, this.speedCamera * Time.deltaTime);
		
		//Confere a distancia para parar o movimento da camera.
		float currentDistance = Vector3.Distance (this.camera.transform.position, newPos);
		if (currentDistance > this.totalDistance/100) 
		{
			StartCoroutine (MoveNextStage (orientation));
		} 
		else
		{
			this.cameraMoving = false;
			this.ResetWorldPos();
			this.currentStageY += (int)orientation;
			print (this.currentStageY);
			//	StopCoroutine(this.MoveNextStage(direction));
		}
	}

	private void ResetWorldPos()
	{
		this.startWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (0, 0, 0));
		this.finalWorldPos = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width, Screen.height, 0));
	}

	private void SetTotalDistance(DIRECTION direction)
	{
		Vector3 newPos = new Vector3 ((this.WorldPos.x * this.currentStageY) + (this.WorldPos.x * (int)direction) , 
		                              this.camera.transform.position.y, 
		                              this.camera.transform.position.z); 
		this.totalDistance = Vector3.Distance(this.camera.transform.position, newPos);
		this.cameraMoving = true;
	}

	private IEnumerator MoveNextDoor(GameObject pair, SCALE scale)
	{
		yield return new WaitForFixedUpdate ();
		print ((int)scale);
		this.transform.localScale += new Vector3 (0.01f * (int)scale, 0.01f * (int)scale, 0.01f * (int)scale);
		if (this.transform.localScale.x < 0.5f && scale == SCALE.DECREASE) 
		{
			this.transform.position = pair.transform.position;
			this.StartCoroutine(MoveNextDoor(pair,SCALE.GROWUP));
		}
		else if(scale == SCALE.GROWUP && this.transform.localScale.x >= 1)
		{
			//bher
		}
		else
		{
			StartCoroutine(MoveNextDoor(pair, scale));
		}
	}



	void OnTriggerStay2D(Collider2D c)
	{
		if (c.gameObject.tag.Equals(Tags.door))
	 	{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				StartCoroutine(MoveNextStage(c.GetComponent<Door>().orientation));
				StartCoroutine(MoveNextDoor(c.gameObject.GetComponent<Door>().pair, SCALE.DECREASE));
			}
		}

	}
}
