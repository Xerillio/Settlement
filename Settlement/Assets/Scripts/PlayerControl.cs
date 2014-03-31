using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public Transform woodcutter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Q)){
			Object instance = Instantiate(woodcutter, new Vector3(0,0,0), Quaternion.identity);
			((Transform)instance).GetComponent<Building>().SetBuildingType("WoodcutterBuilding");
		}

	}
}
