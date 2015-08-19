﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	float speed = 10;

	private CameraControler cameraControler;
	private DoorController doorController;
	private LightManager lightManager;
	private Inventory inventory;

	void Start()
	{
		this.cameraControler = this.gameObject.AddComponent<CameraControler> ();
		this.doorController = this.gameObject.AddComponent<DoorController> ();
		this.inventory = this.gameObject.AddComponent<Inventory> ();
		this.lightManager = this.gameObject.AddComponent<LightManager> ();

		this.cameraControler.lightManager = this.lightManager;
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
