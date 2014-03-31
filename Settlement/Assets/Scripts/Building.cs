﻿using UnityEngine;
using System.Collections.Generic;
using System;

public class Building : MonoBehaviour {

	/// <summary>
	/// True when the building has been placed.
	/// </summary>
	private bool isStatic = false;

	private string buildingType;
	public void SetBuildingType(string type) { this.buildingType = type; }

	// Use this for initialization
	void Start () {
		Vector3 placement = this.TerrainMousePoint();
		this.transform.position = placement.Equals(new Vector3(-1, -1, -1)) ? this.transform.position : placement;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.isStatic)
			return;

		Vector3 placement = this.TerrainMousePoint();
		this.transform.position = placement.Equals(new Vector3(-1, -1, -1)) ? this.transform.position : placement;

		// LMB clicked
		if (Input.GetMouseButtonDown(0)){
			this.isStatic = true;
			MonoBehaviour[] scripts = this.GetComponents<MonoBehaviour>();
			for (int i = 0; i < scripts.Length; i++)
			{
				if (scripts[i].GetType().ToString() == this.buildingType){
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
