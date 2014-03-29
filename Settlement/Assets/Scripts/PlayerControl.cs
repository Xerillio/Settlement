using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public Transform building;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Q)){
			Instantiate(building, new Vector3(0,0,0), Quaternion.identity);
		}

	}
}
