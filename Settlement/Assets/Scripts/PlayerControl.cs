using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public Transform woodcutter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Instantiate a woodcutter building on Q pressed
		if (Input.GetKeyDown(KeyCode.Q)){
			Object instance = Instantiate(woodcutter, new Vector3(0,0,0), Quaternion.identity);
			((Transform)instance).GetComponent<BuildingPlacer>().SetBuildingType<WoodcutterBuilding>();
		}
	}
}
