using UnityEngine;
using System.Collections;

public enum ORIENTATION { UP = 1, DOWN = -1};

public class Door : MonoBehaviour
{
	[SerializeField]
	public ORIENTATION orientation;
	public GameObject pair;

}
