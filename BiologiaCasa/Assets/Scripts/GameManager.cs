using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	
	private GameObject prefabPlayer;
	private Player player;

	void Start()
	{
		this.prefabPlayer = (GameObject)Resources.Load ("Prefabs/Player");
		GameObject playerObj = (GameObject)Instantiate (this.prefabPlayer);
		playerObj.AddComponent<Player>();
	}


}
