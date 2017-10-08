using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrainGeneration : MonoBehaviour {

	public GameObject board;
	public GameObject [] tiles;

	Vector3 offset;
	List<Transform> squares = new List<Transform>();

	void Start() {
		offset = new Vector3( 5f, 5f, 0f );

		foreach( Transform square in board.GetComponentsInChildren<Transform>() ) {
			if( square.gameObject.tag == "Square" )
				squares.Add( square );
		}

		GenerateTiles();
	}

	void GenerateTiles() {
		foreach( Transform square in squares ) {
			GameObject tile = Instantiate( RandomTile(), square.position + offset, square.rotation ) as GameObject;
			tile.transform.parent = square.transform;
		}
	}

	GameObject RandomTile() {
		int randomIndex = Random.Range( 0, tiles.Length );
		return tiles[ randomIndex ];
	}

}
