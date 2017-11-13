using UnityEngine;
using System.Collections;


public class Special : MonoBehaviour {
    public GameObject[] cubes;
    public int selection = 0;
    public int selected = -1;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        cubes[selection].GetComponent<Renderer>().material.color = Color.blue;
        if(selected != -1)
        {
            cubes[selected].GetComponent<Renderer>().material.color = Color.red;
        }

        //selectionné
        if (Input.GetKeyUp(KeyCode.A))
        {
            selected = selection;
            cubes[selected].GetComponent<Renderer>().material.color = Color.red;
        }
        //deselection
        if (Input.GetKeyUp(KeyCode.Z))
        {
            cubes[selected].GetComponent<Renderer>().material.color = Color.white;
            selected = -1;

        }
        //faire voler
        if (Input.GetKeyDown(KeyCode.Space) && selected != -1)
        {
            cubes[selected].transform.Translate(new Vector3(0,1,0));
        }
        //selection
        if(Input.GetKeyUp(KeyCode.B) && selected == -1)
        {
            cubes[selection].GetComponent<Renderer>().material.color = Color.white;
            selection++;
            if(selection == cubes.Length)
            {
                selection = 0;
                
            }
            cubes[selection].GetComponent<Renderer>().material.color = Color.blue;
        }

	}
}
