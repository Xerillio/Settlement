using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WoodCollector : MonoBehaviour {

	/// <summary>
	/// Keeps track of which woodcutter is collecting wood from this tree, and how long he has been chopping since he last got a piece.
	/// </summary>
	private class Collector {
		public WoodcutterVillager villager;
		public float collectionSpeed;
		public float lastCollectionTime;

		public Collector(WoodcutterVillager villager, float collectionSpeed) {
			this.villager = villager;
			this.collectionSpeed = collectionSpeed;
			this.lastCollectionTime = Time.realtimeSinceStartup;
		}
	}

	private int woodLeft = 1;
	public int GetWoodLeft() { return this.woodLeft; }
	List<Collector> collectors = new List<Collector>();

	/// <summary>
	/// Start the collection of wood from this tree.
	/// </summary>
	/// <param name="villager">The villager starting the collection.</param>
	public void StartCollection(WoodcutterVillager villager, float collectionSpeed) {
		this.collectors.Add(new Collector(villager, collectionSpeed));
	}

	public void StopCollection(WoodcutterVillager villager) {
		for (int i = 0; i < this.collectors.Count; i++) {
			Collector elem = this.collectors[i];
			if (elem.villager.Equals(villager))
				this.collectors.RemoveAt(i);
		}
	}

	public uint Chop(WoodcutterVillager villager) {
		for (int i = 0; i < this.collectors.Count; i++) {
			Collector elem = this.collectors[i];
			if (elem.villager.Equals(villager)) {
				if ((Time.realtimeSinceStartup - elem.lastCollectionTime) * elem.collectionSpeed >= 100.0f) {
					elem.lastCollectionTime += 100.0f / elem.collectionSpeed;
					this.woodLeft--;
					if (this.woodLeft == 0)
						Destroy(this.gameObject);
					return 1;
				}
				return 0;
			}
		}
		return 0;
	}
}
