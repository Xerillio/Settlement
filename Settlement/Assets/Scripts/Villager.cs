using UnityEngine;
using System.Collections;

public class Villager : MonoBehaviour {

	protected float movementSpeed = 3.0f;

	/// <summary>
	/// Moving this transform towards a point.
	/// </summary>
	/// <param name="target">Target point to move towards.</param>
	/// <param name="arrivalDistance">Max distance from target which will be considered as arrived.</param>
	/// <returns>True if this transform has arrived.</returns>
	protected bool MoveToPosition(Vector3 target, float arrivalDistance) {
		this.transform.position = Vector3.MoveTowards(this.transform.position, target, this.movementSpeed * Time.deltaTime);
		return (this.transform.position - target).magnitude <= arrivalDistance;
	}
}
