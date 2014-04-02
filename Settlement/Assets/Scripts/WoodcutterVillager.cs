using UnityEngine;
using System.Collections;

public class WoodcutterVillager : Villager {

	/// <summary>
	/// True if the woodcutter is collecting wood.
	/// </summary>
	private bool isCollecting = false;
	/// <summary>
	/// True if the woodcutter just cut down a tree.
	/// </summary>
	private int heldWood = 0;
	/// <summary>
	/// The speed at which the woodcutter is collecting wood.
	/// </summary>
	private float collectionSpeed = 20.0f;
	/// <summary>
	/// The tree (script) currently being chopped down.
	/// </summary>
	private WoodCollector activeTree = null;
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
			if (!this.MoveToPosition(this.myBuilding.transform.position, 0.0f))
				this.heldWood = 0;
		}
		else if (this.targetTree == null){
			this.targetTree = this.myBuilding.GetClosestTree();						// If the tree breakes but heldWood != 5, what then??
		}
		else if (!this.isCollecting) {
			this.isCollecting = this.MoveToPosition(this.targetTree.position, 1.0f);
		}
		else {
			this.Collect();
		}
	}

	private void Collect() {
		if (this.activeTree == null)
			this.activeTree = this.GetTreeScript();

		if (this.activeTree.GetWoodLeft() == 0) {
			Destroy(this.activeTree.gameObject);
			return;
		}
		if ((this.heldWood += this.activeTree.Chop(this)) == 5)
			this.isCollecting = false;
	}

	private WoodCollector GetTreeScript() {
		WoodCollector script = this.targetTree.GetComponent<WoodCollector>();
		if (script == null)
			script = this.targetTree.gameObject.AddComponent<WoodCollector>();
		return script;
	}
}
