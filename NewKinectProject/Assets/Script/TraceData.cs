using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class TraceData : MonoBehaviour {


    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        File.AppendAllText("Data.txt", "" + transform.position + "\r\n");
    }
}
