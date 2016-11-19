using UnityEngine;
using System.Collections;

public class GameRestart : MonoBehaviour {

    public GameObject player;
    public GameObject enemyT;

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
     if(other == player.GetComponent<CircleCollider2D>() || player.GetComponent<PolygonCollider2D>())   
       Application.LoadLevel(SceneName);

        if (other == enemyT.GetComponent<Collider2D>())
        {
            Destroy(enemyT);
            other = null;
        }

        Debug.Log("Hit");
        
    }

    /*void OnTriggerExit2D(Collider2D player)
    {
        Application.LoadLevel(SceneName);
    }
       */
}
