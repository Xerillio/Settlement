using UnityEngine;
using System.Collections;

public class WoodcutterBuilding : MonoBehaviour {

	public Transform myVillager;

	// Use this for initialization
	void Start () {
		Instantiate(myVillager, this.transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
