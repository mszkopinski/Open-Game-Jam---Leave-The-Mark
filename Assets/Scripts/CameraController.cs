using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Vector2 offset;
	public Transform target;
	public float smoothSpeed = .125f;

	void LateUpdate() {
		Vector2 desiredPosition = new Vector2( target.position.x, target.position.y ) + offset;
		Vector2 smoothedPosition = Vector2.Lerp( transform.position, desiredPosition, smoothSpeed );

		transform.position = new Vector3( smoothedPosition.x, smoothedPosition.y, transform.position.z );
	}
}
