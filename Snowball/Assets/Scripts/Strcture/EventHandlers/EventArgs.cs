using UnityEngine;
using System.Collections;

//Can be stored in backend ?
public class GameObjectEventArgs : System.EventArgs
{
	public GameObject GameObject { get; set; }
	public GameObjectEventArgs(GameObject gameObject)
	{
		GameObject = gameObject;
	}
}
