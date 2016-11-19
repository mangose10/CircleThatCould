using UnityEngine;
using System.Collections;

public class GameRestart : MonoBehaviour {
    
    //Tesitng g

    string SceneName;
    // Use this for initialization
    void Start () {
        SceneName = Application.loadedLevelName;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    

    void OnTriggerEnter2D(Collider2D other)
    {
         Application.LoadLevel(SceneName);
        
        //Debug.Log("Hit!");
    }
    void OnTriggerExit2D(Collider2D other)
    {
        Application.LoadLevel(SceneName);
    }
}
