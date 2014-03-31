using UnityEngine;
using System.Collections;

public class WoodcutterVillager : MonoBehaviour {

	private bool isCollecting = false;

	private Transform targetTree = null;

	private WoodcutterBuilding myBuilding = null;
	public void SetMyBuilding(WoodcutterBuilding building){ this.myBuilding = building; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.targetTree == null){
			this.FindTree();
		}
		else if (isCollecting == false) {
			this.MoveToTargetTree();
		}
	}

	private void FindTree() {
		this.targetTree = this.myBuilding.GetClosestTree();
	}

	private void MoveToTargetTree() {
		this.transform.position = Vector3.MoveTowards(this.transform.position, this.targetTree.position, 2.0f * Time.deltaTime);
		if ((this.transform.position - this.targetTree.position).magnitude <= 2.0f) {
			this.isCollecting = true;
		}
	}
}
