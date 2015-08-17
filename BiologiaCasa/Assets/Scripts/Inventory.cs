using UnityEngine;
using System.Collections;

public class Inventory : MonoBehaviour 
{
	private bool mousePressioned;
	private GameObject selectedObject;

	void Update()
	{
		this.ClickOnObjects ();
		this.MoveObjects ();

	}

	private void ClickOnObjects()
	{
		if(Input.GetMouseButtonDown(0))
		{
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			if(hit != null)
			{
				if(hit.collider.tag == Tags.anObject)
				{
					Debug.Log ("Target Position: " + hit.collider.gameObject.transform.position);
					this.mousePressioned = true;
					this.selectedObject = hit.collider.gameObject;
				}
			}
		}

		if (Input.GetMouseButtonUp (0)) 
		{
			this.mousePressioned = false;
		}
	}

	private void MoveObjects()
	{
		if(mousePressioned)
		{
			selectedObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
	}
}
