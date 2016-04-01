using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeNode : MonoBehaviour {
	public GameObject parent;
	public Transform [] positions;
	private Transform latestParentPosition;
	private Vector3 formerPosition = Vector3.zero;
	public List<Vector3> formerPositions = new List<Vector3>();
	public int frameDelay=0;
	public void Start(){
		parent = transform.parent.gameObject;
		positions = parent.GetComponentsInChildren<Transform>();
	}
	
	public void Update(){
		
		if(positions[positions.Length-2].position != formerPosition){
			formerPositions.Add(positions[positions.Length-2].position);
		}
		if(((int)(transform.position - positions[positions.Length-2].position).magnitude) >= 2){
			MoveFollower(transform);
		}
		formerPosition = transform.position;
	}
	public void MoveFollower(Transform target, float maxSpeed = 10f)
	{
		if (formerPositions.Count >= 10){
			var frame = formerPositions[formerPositions.Count - 5];
			target.position = Vector3.MoveTowards(target.position, frame, maxSpeed * Time.deltaTime);
		}
    }
	
	public void MoveBack(Transform target, float maxSpeed = 10f)
	{
		if (formerPositions.Count >= 10){
			var frame = formerPositions[formerPositions.Count - 5];
			target.position = Vector3.MoveTowards(frame, target.position,maxSpeed * Time.deltaTime);
		}
    }
	
}
