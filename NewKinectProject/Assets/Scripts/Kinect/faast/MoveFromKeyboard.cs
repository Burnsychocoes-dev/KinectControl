using UnityEngine;
using System.Collections;

public class MoveFromKeyboard : MonoBehaviour {
    private Vector3 increment;
	// Use this for initialization
	void Start () {
        increment = new Vector3(0.1F, 0.0F, 0.0F);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.A) == true)
        {
            this.transform.Translate(increment);
        }
	}
}
