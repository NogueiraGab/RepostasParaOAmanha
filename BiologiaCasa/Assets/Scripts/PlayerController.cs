using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public Animator anim;

	void Start()
	{
		this.anim = this.gameObject.GetComponent<Animator> ();
	}

	void Update()
	{
		this.UpdateAnimations ();
	}

	void UpdateAnimations()
	{
		if (Input.GetAxis ("Horizontal") > 0 && this.anim.GetBool("moving") == false) 
		{
			this.anim.SetBool("moving", true);
			this.transform.localScale = new Vector3(1,1,1);
		}
		else if(Input.GetAxis ("Horizontal") < 0 && this.anim.GetBool("moving") == false)
		{
			this.anim.SetBool("moving", true) ;
			this.transform.localScale = new Vector3(-1,1,1);
		}
		/*
		else
			this.anim.SetBool("moving", false);*/
	}
}
