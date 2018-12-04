﻿using UnityEngine;
using System.Collections;

public class BasicCameraFollow : MonoBehaviour 
{
	public GameObject followTarget;
	private Vector3 targetPos;
	public float moveSpeed, smoothTime;

    private void Awake()
    {
        followTarget = Player.instance.gameObject;
    }

    void Update () 
	{
		targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
		Vector3 velocity = targetPos - transform.position;
		transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref velocity, smoothTime, moveSpeed * Time.deltaTime);	
	}
}
