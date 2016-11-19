using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {

    float speed = 4;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    void Movement()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right*speed*Time.deltaTime);
        }
    }
}
