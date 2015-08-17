using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	float speed = 10;

	private CameraControler cameraControler;
	private DoorController doorController;

	void Start()
	{
		this.gameObject.AddComponent<CameraControler> ();
		this.gameObject.AddComponent<DoorController> ();
		this.gameObject.AddComponent<Inventory> ();
		this.gameObject.name = "Player";
	}

	void Update()
	{
		this.Movement ();
	}
	
	private void Movement()
	{
		this.transform.Translate (Input.GetAxis ("Horizontal") * speed * Time.deltaTime, 0, 0);
	}

	public GameObject GetPlayerGameObj()
	{
		return this.gameObject;
	}

}
