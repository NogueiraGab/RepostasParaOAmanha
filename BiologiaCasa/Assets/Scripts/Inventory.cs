using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour 
{
	private bool mousePressioned;
	private GameObject selectedObject;

	private Vector3 posInventoryOn;
	private Vector3 posInventoryOff;
	private bool inventoryOn;
	private bool moveInventory;
	private bool inventoryFixed; //True when activate inventory using I key

	private GameObject inventory;
	private GameObject[] spaces;
	private GameObject[] objectsInSpaces;

	void Start()
	{
		this.inventory = GameObject.Find ("Inventory");
		this.spaces = new GameObject[3];
		this.objectsInSpaces = new GameObject[3];
		for (int i=0; i<3; i++) 
		{
			this.spaces[i] = GameObject.Find("Space_"+i);
		}

		this.inventory.transform.parent = Camera.main.transform;
	}

	void Update()
	{
		this.ClickOnObjects ();
		this.MoveObjects ();
		this.MoveInventory ();
	}

	private void ClickOnObjects()
	{
		if(Input.GetMouseButtonDown(0))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			if(hit.collider != null)
			{
				if(hit.collider.tag == Tags.anObject)
				{
					this.mousePressioned = true;
					this.selectedObject = hit.collider.gameObject;
					this.inventoryOn = true;
					this.moveInventory = true;
					this.DefineInventoryPos();
				}
			}
		}

		if (Input.GetMouseButtonUp (0)) 
		{
			this.mousePressioned = false;
			if(this.inventoryFixed == false)
			{
				this.inventoryOn = false;
				this.moveInventory = true;
				DefineInventoryPos();
			}
		}
	}

	private void MoveObjects()
	{
		if(mousePressioned)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = selectedObject.transform.position.z;
			this.selectedObject.transform.position = mousePos;
		}


		if (!mousePressioned && this.selectedObject != null) 
		{
			//Conferir se ele ja esta dentro de um espaço
			for(int i=0; i<this.spaces.Length;i++)
			{
				if(this.objectsInSpaces[i] != null)
				{
					if(this.selectedObject.transform.position == this.objectsInSpaces[i].transform.position)
					{
						this.selectedObject.transform.parent = null;
						this.objectsInSpaces[i] = null;
					}
				}
			}
			for(int i=0; i<this.spaces.Length;i++)
			{
				if(Vector3.Distance(this.selectedObject.transform.position,this.spaces[i].transform.position) < 0.5f)
				{
					this.selectedObject.transform.parent = this.spaces[i].transform;
					this.selectedObject.transform.position = this.spaces[i].transform.position;
					this.objectsInSpaces[i] = this.selectedObject;
					this.selectedObject = null;
					break;
				}
			}
		}
	}

	private void MoveInventory()
	{
		if (Input.GetKeyDown (KeyCode.I) && this.moveInventory == false)
		{
			this.inventoryOn = !this.inventoryOn;
			this.moveInventory = true;
			this.DefineInventoryPos();
			this.inventoryFixed = this.inventoryOn;
		}

		if (this.moveInventory)
		{
			this.inventory.transform.position = Vector3.Lerp(this.inventory.transform.position, this.posInventoryOn, Time.deltaTime * 5);

			if(Vector3.Distance(this.inventory.transform.position, this.posInventoryOn) < 0.1f)
			{
				this.moveInventory = false;
				return;
			}
		}
	}

	private void DefineInventoryPos()
	{
		if(this.inventoryOn == true)
			this.posInventoryOn = new Vector3(Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, 0, 0)).x - this.inventory.renderer.bounds.size.x,0,0);
		else
			this.posInventoryOn = new Vector3(Camera.main.ScreenToWorldPoint (new Vector3(Screen.width, 0, 0)).x + this.inventory.renderer.bounds.size.x,0,0);
		
		this.posInventoryOn.z = this.inventory.transform.position.z;
		this.posInventoryOn.y = this.inventory.transform.position.y;
	}
}
