﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WoodcutterBuilding : MonoBehaviour {
	/// <summary>
	/// A villager connected to this building. Keeps track of his working status.
	/// </summary>
	private class Worker {
		public WoodcutterVillager villager;
		public WorkingState state;

		public Worker(WoodcutterVillager villager){
			this.villager = villager;
			this.state = WorkingState.Collecting;
		}

		public Worker(WoodcutterVillager villager, WorkingState state) {
			this.villager = villager;
			this.state = state;
		}
	}
	/// <summary>
	/// States in which a worker can be.
	/// </summary>
	private enum WorkingState {
		Waiting,
		Collecting,
		Delivering
	}
	/// <summary>
	/// The distance the building is searching for trees.
	/// </summary>
	private static float checkRadius = 30.0f;
	/// <summary>
	/// List of all workers connected to this building.
	/// </summary>
	private List<Worker> myWorkers = new List<Worker>();
	/// <summary>
	/// TEMP. Used to instantiate a villager on creation of the building.
	/// </summary>
	public Transform myVillager;

	private float lastCheckTime = 0.0f;

	private float checkRate = 2.0f;

	private List<Transform> treesInRange = new List<Transform>();

	private static uint woodCap = 5;

	private uint stockedWood = 0;

	// Use this for initialization
	void Start () {
		this.enabled = false;
		this.CheckTreesInRange();

		WoodcutterVillager villager = (Instantiate(myVillager, this.transform.position, Quaternion.identity) as Transform).GetComponent<WoodcutterVillager>();
		villager.SetMyBuilding(this);
		this.myWorkers.Add(new Worker(villager, WorkingState.Collecting));
	}

	void Update() {
		float time = Time.realtimeSinceStartup;
		if (time - this.lastCheckTime > this.checkRate) {
			this.lastCheckTime = time + this.checkRate;
			this.CheckTreesInRange();
		}
	}

	private bool CheckTreesInRange() {
		// Find trees in range of the building and save a reference to them in "treesInRange"
		int mask = 1 << LayerMask.NameToLayer("Terrain");
		Collider[] hits = Physics.OverlapSphere(this.transform.position, WoodcutterBuilding.checkRadius, mask);
		bool foundSome = hits.Length > 0;
		for (int i = 0; i < hits.Length; i++) {
			if (hits[i].tag == "Wood") {
				this.treesInRange.Add(hits[i].transform);
			}
		}
		return foundSome;
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
			Transform elem = this.treesInRange[i];
			if (elem == null) {
				this.treesInRange.RemoveAt(i);
				i--;
			}
			else {
				float tmpDist = (this.transform.position - elem.position).magnitude;
				if (tmpDist < dist) {
					dist = tmpDist;
					closest = elem;
				}
			}
		}
		return closest;
	}

	public void AwaitNewTrees(WoodcutterVillager villager){
		for (int i = 0; i < this.myWorkers.Count; i++) {
			Worker elem = this.myWorkers[i];
			if (elem.villager.Equals(villager)) {
				elem.villager.enabled = false;
				elem.state = WorkingState.Waiting;
				this.enabled = true;
			}
		}
	}

	public bool AddWood(uint amount) {
		if (this.stockedWood + amount >= WoodcutterBuilding.woodCap) {
			return false;
		}
		else {
			this.stockedWood += amount;
			return true;
		}
	}
}
