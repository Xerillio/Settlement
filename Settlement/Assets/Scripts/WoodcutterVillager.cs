using UnityEngine;
using System.Collections;

public class WoodcutterVillager : Villager {

	/// <summary>
	/// True if the woodcutter is delivering wood to the storehouse.
	/// </summary>
	private bool isDelivering = false;
	/// <summary>
	/// True if the woodcutter is collecting wood.
	/// </summary>
	private bool isCollecting = false;
	/// <summary>
	/// True if the woocutter is near the tree he's collecting from.
	/// </summary>
	private bool isNearTree = false;
	/// <summary>
	/// True if the woodcutter just cut down a tree.
	/// </summary>
	private uint heldWood = 0;
	/// <summary>
	/// The speed at which the woodcutter is collecting wood.
	/// </summary>
	private static float collectionSpeed = 40.0f;
	/// <summary>
	/// The tree (script) currently being chopped down.
	/// </summary>
	private WoodCollector activeTreeScript = null;
	/// <summary>
	/// The target tree currently collecting from.
	/// </summary>
	private Transform targetTree = null;
	/// <summary>
	/// The building, which the woodcutter-villager belongs to.
	/// </summary>
	private WoodcutterBuilding myBuilding = null;
	public void SetMyBuilding(WoodcutterBuilding building){ this.myBuilding = building; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.heldWood > 0 && !this.isCollecting) {
			if (this.isDelivering) {
				// Move to the nearest storehouse
			}
			else if (this.MoveToPosition(this.myBuilding.transform.position, 0.0f)) {
				if (this.myBuilding.AddWood(this.heldWood))
					this.heldWood = 0;
				else
					this.isDelivering = true;
			}
		}
		else if (this.targetTree == null){
			if ((this.targetTree = this.myBuilding.GetClosestTree()) == null) {
				this.isCollecting = false;
				if (this.MoveToPosition(this.myBuilding.transform.position, 0.2f)) {
					this.myBuilding.AwaitNewTrees(this);
				}
			}
			else {
				this.isNearTree = false;
			}
		}
		else if (!this.isNearTree) {
			if (this.MoveToPosition(this.targetTree.position, 1.0f)) {
				this.isNearTree = true;
				this.isCollecting = true;
				if (this.activeTreeScript == null) {
					this.activeTreeScript = this.GetTreeScript();
				}
				this.activeTreeScript.StartCollection(this, WoodcutterVillager.collectionSpeed);
			}
		}
		else if (this.isCollecting) {
			this.Collect();
		}
	}

	private void Collect() {
		if ((this.heldWood += this.activeTreeScript.Chop(this)) == 2) {
			this.activeTreeScript.StopCollection(this);
			this.isCollecting = false;
		}
	}

	private WoodCollector GetTreeScript() {
		WoodCollector script = this.targetTree.GetComponent<WoodCollector>();
		if (script == null)
			script = this.targetTree.gameObject.AddComponent<WoodCollector>();
		return script;
	}
}
