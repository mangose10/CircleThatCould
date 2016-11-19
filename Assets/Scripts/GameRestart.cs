using UnityEngine;
using UnityEngine.SceneManagement;
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
        Debug.Log(other);
        if(other == player.GetComponent<PolygonCollider2D>() || other == player.GetComponent<CircleCollider2D>())  
            SceneManager.LoadScene(SceneName);

        if (other == enemyT.GetComponent<Collider2D>())
            Destroy(enemyT);
            

        
        other = null;
    }

    /*void OnTriggerExit2D(Collider2D player)
    {
        Application.LoadLevel(SceneName);
    }
       */
}
