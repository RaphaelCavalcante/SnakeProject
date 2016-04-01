using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeGroup : MonoBehaviour {
	public GameObject nodePrefab;
	public List <Transform> nodes = new List <Transform>();
	public List <GameObject> GOs = new List <GameObject>();
	
	public void CreateNode(Vector3 parentPosition, Quaternion parentRotation){
		if(GOs.Count > 0){
			GOs[GOs.Count - 1].tag="SnakeNode";
		}
		GameObject node = Instantiate(nodePrefab,parentPosition,parentRotation) as GameObject;
		node.transform.parent = transform;
		node.tag="Tail";
		GOs.Add(node);
		nodes.Add(node.transform);
	}
	
	public int Damage(GameObject damagedNode){
		int nodeIndex = GOs.LastIndexOf(damagedNode);
		int damagePoints = GOs.Count - nodeIndex;
		//Debug.Log(toRemove.Count + " " + end);
		DestroyNodesInRange(nodeIndex, GOs.Count-1);
		
		return damagePoints;
	}
	public int RemoveTail(GameObject tail){
		GOs.Remove(tail);
		tail.transform.parent = null;
		Destroy(tail);
		
		return 1;
	}
	private void DestroyNodesInRange(int start, int end){
		List<GameObject> toRemove= GOs.GetRange(start, end);
		Debug.Log(toRemove.Count + " " + end);
		/*for(int i = end ;i > end ;i--){
			GameObject defunct = toRemove[i];
			Destroy(defunct);
		}*/
		GOs.RemoveRange(start, end);
		
	}
}

