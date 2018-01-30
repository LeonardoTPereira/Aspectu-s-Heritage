﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public static Player instance = null;
	public List<int> keys = new List<int>();
	public int x { private set;  get;}
	public int y { private set;  get;}
	public Camera cam;

	void Awake(){
		if (instance == null){
			instance = this;
		} else if (instance != this){
			Destroy (gameObject);
		}
		DontDestroyOnLoad (gameObject);
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetKey(int keyID){
		keys.Add (keyID);
	}
		
	public void SetPosition(int x, int y){
		Transform roomTransf = GameManager.instance.roomBHVMap [x, y].transform;
		cam.transform.position = new Vector3 (roomTransf.position.x, roomTransf.position.y, -10f);
	}
}
