using UnityEngine;
using System.Collections;

public class WoodcutterVillager : Villager {

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
	private int heldWood = 0;
	/// <summary>
	/// The speed at which the woodcutter is collecting wood.
	/// </summary>
	private float collectionSpeed = 40.0f;
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
			if (this.MoveToPosition(this.myBuilding.transform.position, 0.0f))
				this.heldWood = 0;
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
				this.activeTreeScript.StartCollection(this, this.collectionSpeed);
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
