using UnityEngine;
using System.Collections.Generic;
using System;

public class BuildingPlacer : MonoBehaviour {

	private Type buildingType;
	public void SetBuildingType<T>() { this.buildingType = typeof(T); }

	// Use this for initialization
	void Start () {
		Vector3 placement = this.TerrainMousePoint();
		this.transform.position = placement.Equals(new Vector3(-1, -1, -1)) ? this.transform.position : placement;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 placement = this.TerrainMousePoint();
		this.transform.position = placement.Equals(new Vector3(-1, -1, -1)) ? this.transform.position : placement;

		// LMB clicked
		if (Input.GetMouseButtonDown(0)){
			MonoBehaviour[] scripts = this.GetComponents<MonoBehaviour>();
			for (int i = 0; i < scripts.Length; i++)
			{
				if (scripts[i].GetType() == this.buildingType){
					scripts[i].enabled = true;
					this.enabled = false;
				}
			}
		}
	}

	/// <summary>
	/// Finds the point on the terrain/ground surface at which the mouse cursor is pointing.
	/// </summary>
	/// <returns>Return the point on the terrain being pointed at.</returns>
	private Vector3 TerrainMousePoint() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		int mask = 1 << LayerMask.NameToLayer("Terrain");
		if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, mask)){
			return hitInfo.point;
		}
		return new Vector3(-1, -1, -1);
	}
}
