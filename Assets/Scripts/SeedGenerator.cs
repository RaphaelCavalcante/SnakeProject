using UnityEngine;
using System.Collections;

public class SeedGenerator : MonoBehaviour {
	public GameObject seedPrefab;
	private GameObject [] seeds;
	private Vector3 position;
	private float rangeX;
	private float rangeY=0.5f;
	private float rangeZ;
		
	public void Update(){
		if(GameObject.FindGameObjectsWithTag("Seed").Length == 0){
			createSeed();
		}
	}
	// Use this for initialization
	void createSeed () {
		
		position = getRandomPosition(7F, 7F);
		GameObject seed	= Instantiate(seedPrefab, position , transform.rotation) as GameObject;
		seed.transform.tag="Seed";
	}
	
	private Vector3 getRandomPosition(float areaRangeX, float areaRangeZ){
		rangeX = Random.Range(areaRangeX*(-1), areaRangeX);
		rangeZ = Random.Range(areaRangeZ*(-1), areaRangeZ);
		Vector3 position = new Vector3(rangeX, rangeY, rangeZ);
		return position;
	}
}
