using UnityEngine;
using System.Collections;

public class ScreenController : MonoBehaviour 
{
	protected GameObject camera;
	private Vector3 finalWorldPos;
	private Vector3 startWorldPos;
	private Vector3 WorldPos;
	private Vector3 newFinal;

	private static int currentStageX = 0;
	private static int currentStageY = 0;

	private bool cameraMoving;

	public ScreenController()
	{
		this.camera = GameObject.Find ("Main Camera");
		this.startWorldPos = Camera.main.ScreenToWorldPoint (new Vector3(0, 0, 0));
		this.finalWorldPos = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, Screen.height, 0));
		this.WorldPos = new Vector3 (this.finalWorldPos.x - this.startWorldPos.x, this.finalWorldPos.y - this.startWorldPos.y, 0);

	}

}
