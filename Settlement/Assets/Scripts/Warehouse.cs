using UnityEngine;
using System.Collections;

public class Warehouse : MonoBehaviour {

	private uint stockWood = 0;
	public void AddWood(uint amount) { this.stockWood += amount; }
	public uint TakeWood(uint reqAmount) {
		if (this.stockWood >= reqAmount) {
			this.stockWood -= reqAmount;
			return reqAmount;
		}
		else {
			uint back = this.stockWood;
			this.stockWood -= back;
			return back;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
