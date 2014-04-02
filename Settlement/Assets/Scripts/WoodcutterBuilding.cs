using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WoodcutterBuilding : MonoBehaviour {

	public Transform myVillager;

	private float checkRadius = 30.0f;

	private List<Transform> treesInRange = new List<Transform>();

	// Use this for initialization
	void Start () {
		// Find trees in range of the building and save a reference to them in "treesInRange"
		int mask = 1 << LayerMask.NameToLayer("Terrain");
		Collider[] hits = Physics.OverlapSphere(this.transform.position, this.checkRadius, mask);
		for (int i = 0; i < hits.Length; i++)
		{
			if (hits[i].tag == "Wood") {
				this.treesInRange.Add(hits[i].transform);
			}
		}
		this.myVillager = Instantiate(myVillager, this.transform.position, Quaternion.identity) as Transform;
		this.myVillager.GetComponent<WoodcutterVillager>().SetMyBuilding(this);
	}

	/// <summary>
	/// Finds the tree that is closest to this building.
	/// </summary>
	/// <returns>Returns that tree.</returns>
	public Transform GetClosestTree() {
		// If there are no trees in range
		if (this.treesInRange.Count == 0 || (this.treesInRange.Count == 1 && this.treesInRange[0] == null))
			return null;

		Transform closest = this.treesInRange[0];
		float dist = 1000.0f;
		for (int i = 0; i < this.treesInRange.Count; i++) {
			if (this.treesInRange[i] == null) {
				this.treesInRange.RemoveAt(i);
			}
			else {
				float tmpDist = (this.transform.position - this.treesInRange[i].position).magnitude;
				if (tmpDist < dist) {
					dist = tmpDist;
					closest = this.treesInRange[i];
				}
			}
		}
		return closest;
	}
}
