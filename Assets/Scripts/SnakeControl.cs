using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SnakeControl : MonoBehaviour {
	enum Direction {UP, DOWN, LEFT, RIGHT, STOCK};
	public float speed;
	private int count;
	private int level;
	private int prevLevel;
	
	private Direction currDirection;
	private Direction preDirection;
		
	private Rigidbody snake;
	
	public NodeGroup group;
	
	private List <Vector3> prevPaths = new List <Vector3>();
	private Vector3 prevPosition= Vector3.zero;
	
	public Text scoreText;
	public Text levelText;
	
	public void Start(){
		snake = GetComponent <Rigidbody> ();
		count = 0;
		level = 0;
		prevLevel = 0;
		speed = 5f;
		currDirection = Direction.UP;
		preDirection = Direction.STOCK;
		snake.velocity = Vector3.zero;
		snake.angularVelocity = Vector3.zero; 
		
		SetLeveLText();
		SetScoreText();
	}
	public void movementState(){
		
		if(Input.GetKey("a") ){
			snake.velocity = new Vector3 (0,0,0);
			preDirection = currDirection;
			currDirection = Direction.LEFT;
		}else if(Input.GetKey("s")){
			snake.velocity = new Vector3 (0,0,0);
			preDirection = currDirection;
			currDirection = Direction.DOWN;
		}else if(Input.GetKey("w")){
			snake.velocity = new Vector3 (0,0,0);
			preDirection = currDirection;
			currDirection = Direction.UP;
		}else if(Input.GetKey("d")){
			snake.velocity = new Vector3 (0,0,0);
			preDirection = currDirection;
			currDirection = Direction.RIGHT;
		}
	}
	public void FixedUpdate(){
		movementState();
		
		if(currDirection == Direction.UP && preDirection != Direction.DOWN){
			
			snake.AddForce(Vector3.forward * speed);
			
		}else if(currDirection == Direction.DOWN && preDirection!= Direction.UP){ 
			
			snake.AddForce(Vector3.back * speed);
		}else if(currDirection == Direction.LEFT && preDirection != Direction.RIGHT){
			snake.AddForce(Vector3.left * speed);
		}else if(currDirection == Direction.RIGHT && preDirection!= Direction.LEFT){
			snake.AddForce(Vector3.right * speed);
		}
	}
	
	public void Update(){
		if(transform.position != prevPosition){
			prevPaths.Add(transform.position);
		}
		prevPosition = transform.position;
	}
		
	public void SetLeveLText(){
		levelText.text = "Level: "+level.ToString();
	}
	public void SetScoreText(){
		scoreText.text = "Score: "+count.ToString();
	}
	public void CalculateStage(){
		if(count < 4){
			count++;
		}
		if(count == 4){
			level++;
			count = 0;
		}
		if(prevLevel < level){
			speed+=level;
			prevLevel = level;
		}
		
		SetLeveLText();
		SetScoreText();
	}
	public void OnTriggerEnter(Collider pickup){
		if(pickup.CompareTag("Seed")){
			Destroy(pickup.gameObject);
			group.CreateNode(this.prevPaths[prevPaths.Count - 10], transform.rotation);
			CalculateStage();
		}else if(pickup.CompareTag("Tail") || pickup.CompareTag("SnakeNode")){
			count = count - group.RemoveTail(pickup.gameObject);
			CalculateStage();
		}
	}
}
