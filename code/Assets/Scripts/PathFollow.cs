using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour {

	public Color lineColor;
	private List<Transform> nodes = new List<Transform>();

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//transform.LookAt(nodes[1].position);
	}

	void OnDrawGizmos() {
		Gizmos.color = lineColor;

		Transform[] pathTransforms = GetComponentsInChildren<Transform>();
		nodes = new List<Transform>();

		for (int i = 0; i < pathTransforms.Length; i++) {
			if (pathTransforms[i] != transform) {
				nodes.Add(pathTransforms[i]);
			}
		}

		for (int i = 0; i < nodes.Count; i++) {
			Vector3 currentNode = nodes[i].position;
			Vector3 prevNode = Vector3.zero;

			if (i>0) {
				prevNode = nodes[i-1].position;
			} else if (i==0 && nodes.Count >1) {
				prevNode = nodes[nodes.Count-1].position;
			}

			Gizmos.DrawLine(prevNode, currentNode);
			Gizmos.DrawSphere(currentNode, 1f);
		}
	}
}
