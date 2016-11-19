﻿using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour {

    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject player;
    public Vector3 offset;
    Vector3 targetPos;
    
	// Use this for initialization
	void Start () {
        targetPos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (player)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = player.transform.position.z;

            Vector3 playerDirection = (player.transform.position - posNoZ);

            interpVelocity = playerDirection.magnitude * 5f;

            targetPos = transform.position + (playerDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);
        }
	}
}
