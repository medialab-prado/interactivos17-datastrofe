using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

	public GameObject[] paths;
	private List<Transform> nodes = new List<Transform>();
	private Transform nodePos;
	private int i;
	public int startingNode;
	public float speed;
	public float actualSpeed;
	private GameObject path;
	private Transform startingPoint;

	void Awake() {
		speed = Random.Range(18f, 25f);
		actualSpeed = speed;
		path = paths[Random.Range(0, paths.Length)];

		startingNode = Random.Range(0, path.transform.childCount);
		startingPoint = path.transform.GetChild(startingNode).transform;
		transform.position = startingPoint.position;
	}

	// Use this for initialization
	void Start () {
		Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
		nodes = new List<Transform>();

		for (int i = 0; i < pathTransforms.Length; i++) {
			if (pathTransforms[i].gameObject.tag != "path") {
				nodes.Add(pathTransforms[i]);
			}
		}
		nodePos = nodes[startingNode];
		i = startingNode;
		StartCoroutine("MoveVehicle");
		//Invoke("ActivarCollider", 10f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void ActivarCollider() {
		//print("ACTIVANDO COLLIDER");
		//gameObject.GetComponent<BoxCollider>().enabled = true;
	}

	private IEnumerator MoveVehicle() {
		while(true) {
			if (transform.position == nodePos.position) {
				/*if (nodePos.tag == "semaforo") {
					yield return new WaitForSeconds(1.0f);
				}*/
				i++;
				if (i>=nodes.Count) i = 0;
				nodePos = nodes[i];
			} else {
				var targetRotation = Quaternion.LookRotation(nodePos.transform.position - transform.position);
				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);

				//transform.LookAt(nodePos);
				/*if (nodePos.tag == "semaforo") {
					actualSpeed -= 0.1f;
					if (actualSpeed<0) actualSpeed = 0;
				} else {
					if (actualSpeed<speed) {
						actualSpeed+=(speed/2)*Time.deltaTime;
					}
				}*/

				transform.position = Vector3.MoveTowards(transform.position, nodePos.position, actualSpeed*Time.deltaTime);
			}
			yield return null;
		}
	}


	/*
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "coche") {
			actualSpeed = other.gameObject.GetComponent<FollowPath>().actualSpeed - 20f;
			if (actualSpeed<0) actualSpeed = 0;
			//other.enabled = false;
			//StartCoroutine("Wait");
		}
	}


	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag=="coche") {
			if (actualSpeed>0) actualSpeed -= 50f;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "coche") {
			StartCoroutine("Wait");
			other.enabled = true;
		}
	}*/

	/************************************************************
	** Make sure to add rigidbodies to your objects.
	** Place this script on your object not object being hit
	** this will only work on a Cube being hit 
	** it does not consider the direction of the Cube being hit
	** remember to name your C# script "GetSideHit"
	************************************************************/
	/*
	void OnCollisionEnter( Collision collision ){

		if (collision.gameObject.tag == "coche") {
			print(ReturnDirection( collision.gameObject, this.gameObject ) );
			if( ReturnDirection( collision.gameObject, this.gameObject ) == HitDirection.None) {
				print("EN TOP");
				if (actualSpeed > collision.gameObject.GetComponent<FollowPath>().actualSpeed) {
					if (actualSpeed>0) actualSpeed = collision.gameObject.GetComponent<FollowPath>().actualSpeed - 20f;
				} else {
					if (actualSpeed>0) actualSpeed -= 20f;
				}
				print(actualSpeed + " VS " + collision.gameObject.GetComponent<FollowPath>().actualSpeed);

			}
			if (actualSpeed<0) actualSpeed = 0;
		}
	}

	void OnCollisionExit(Collision collision) {
		if (collision.gameObject.tag == "coche") actualSpeed = speed;
	}

	void OnCollisionStay(Collision collision) {
		if (collision.gameObject.tag == "coche") {
			if (actualSpeed > collision.gameObject.GetComponent<FollowPath>().actualSpeed) {
				if (actualSpeed>0) actualSpeed = collision.gameObject.GetComponent<FollowPath>().actualSpeed - 20f;
			} else {
				if (actualSpeed>0) actualSpeed -= 20f;
			}
		}
		if (actualSpeed<0) actualSpeed = 0;
	}

	private enum HitDirection { None, Top, Bottom, Forward, Back, Left, Right }
	private HitDirection ReturnDirection( GameObject Object, GameObject ObjectHit ){

		HitDirection hitDirection = HitDirection.None;
		RaycastHit MyRayHit;
		Vector3 direction = ( Object.transform.position - ObjectHit.transform.position ).normalized;
		Ray MyRay = new Ray( ObjectHit.transform.position, direction );

		if ( Physics.Raycast( MyRay, out MyRayHit ) ){

			if ( MyRayHit.collider != null ){

				Vector3 MyNormal = MyRayHit.normal;
				MyNormal = MyRayHit.transform.TransformDirection( MyNormal );

				if( MyNormal == MyRayHit.transform.up ){ hitDirection = HitDirection.Top; }
				if( MyNormal == -MyRayHit.transform.up ){ hitDirection = HitDirection.Bottom; }
				if( MyNormal == MyRayHit.transform.forward ){ hitDirection = HitDirection.Forward; }
				if( MyNormal == -MyRayHit.transform.forward ){ hitDirection = HitDirection.Back; }
				if( MyNormal == MyRayHit.transform.right ){ hitDirection = HitDirection.Right; }
				if( MyNormal == -MyRayHit.transform.right ){ hitDirection = HitDirection.Left; }
			}    
		}
		return hitDirection;
	}

	IEnumerator Wait() {
		gameObject.GetComponent<BoxCollider>().enabled = true;
		yield return new WaitForSeconds(2f);
	}*/
}
