using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class TraceDataHeadRot : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        File.AppendAllText("DataHeadRot.txt", "" + transform.eulerAngles + "\r\n");
    }
}
