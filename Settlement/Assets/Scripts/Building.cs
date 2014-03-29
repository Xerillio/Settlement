using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Vector3 placement = this.MouseTerrainPoint();
		this.transform.position = placement.Equals(new Vector3(-1, -1, -1)) ? this.transform.position : placement;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 placement = this.MouseTerrainPoint();
		this.transform.position = placement.Equals(new Vector3(-1, -1, -1)) ? this.transform.position : placement;
	}

	private Vector3 MouseTerrainPoint() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		int mask = 1 << LayerMask.NameToLayer("Terrain");
		if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, mask)){
			return hitInfo.point;
		}
		return new Vector3(-1, -1, -1);
	}
}
