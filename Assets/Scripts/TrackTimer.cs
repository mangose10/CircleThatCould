using UnityEngine;
using System.Collections;

public class TrackTimer : MonoBehaviour {
    private string timedis;
    private float sec;
    private int min;
    private int hour;
    private TextMesh text;

    public bool start = false;
	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<TextMesh>();
        sec = 00;
        min = 00;
        hour = 00;
	}
	
	// Update is called once per frame
	void Update () {
        if (start == true)
        {
            sec += Time.deltaTime;

            if(Mathf.Floor(sec) >= 60)
            {
                sec -= 60;
                min += 1;
            }
            if(Mathf.Floor(min) >= 60)
            {
                min -= 60;
                hour += 1;
            }
            timedis = ((hour/10).ToString() + (hour%10).ToString() + ":" + (min/10).ToString() + (min%10).ToString() + ":" + ((Mathf.Floor(sec/10)%10).ToString() + (Mathf.Floor(sec)%10).ToString()));
            text.text = timedis;
        }
	
	}
}
