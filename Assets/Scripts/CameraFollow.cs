using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float smoothing;

    private Transform player;

    private Vector3 offset;
    
	
	private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //Debug.Log(player);
        offset = transform.position - player.transform.position;
	}
	
	
	void LateUpdate()
    {
        transform.position = player.transform.position + offset * smoothing;
	}
}
