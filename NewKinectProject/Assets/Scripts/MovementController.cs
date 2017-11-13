using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//Cette classe va agir en fonction des differents mouvements que l'on a
public class MovementController : MonoBehaviour {
    //soit on met ça à la main, soit il doit aller les chercher sur l'objet dans le start, cela dépend de où les scripts se trouvent
    public BalayageDroit balayageDroit;
    public BalayageGauche balayageGauche;
    public BalayageHaut balayageHaut;
    public CoupDePoingDroitAvant coupDePoingDroitAvant;
    public Course course;
    public MultiClonage multiClonage;

    public Text text;

    public GameObject cubePrefab;
    public GameObject[] cubes;
    public int selection = 0;
    public int selected = -1;
	// Use this for initialization
	void Start () {
        balayageDroit = gameObject.GetComponent<BalayageDroit>();
        balayageGauche = gameObject.GetComponent<BalayageGauche>();
        balayageHaut = gameObject.GetComponent<BalayageHaut>();
        coupDePoingDroitAvant = gameObject.GetComponent<CoupDePoingDroitAvant>();
        course = gameObject.GetComponent<Course>();
        multiClonage = gameObject.GetComponent<MultiClonage>();
        //text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        //pas encore de selectionné
        //text.text = "";
        if (selected == -1)
        {
            cubes[selection].GetComponent<Renderer>().material.color = Color.blue;

            //augmente la selection de 1
            if (balayageDroit.b1)
            {
                text.text = "Balayage droit";
                Debug.Log("Balayage droit !");
                //do sth
                cubes[selection].GetComponent<Renderer>().material.color = Color.white;
                selection++;
                if (selection == cubes.Length)
                {
                    selection = 0;

                }
                cubes[selection].GetComponent<Renderer>().material.color = Color.blue;
            }
            //diminue la selection de 1
            if (balayageGauche.b1)
            {
                text.text = "Balayage gauche";
                Debug.Log("Balayage gauche !");
                //do sth
                cubes[selection].GetComponent<Renderer>().material.color = Color.white;
                selection--;
                if (selection == -1)
                {
                    selection = cubes.Length - 1;

                }
                cubes[selection].GetComponent<Renderer>().material.color = Color.blue;
            }

            //selectionne le cube
            if (balayageHaut.b1)
            {
                text.text = "Balayage haut";
                Debug.Log("Balayage haut !");
                //do sth
                selected = selection;
                cubes[selected].GetComponent<Renderer>().material.color = Color.red;
            }

            if(coupDePoingDroitAvant.b1)
            {
                text.text = "Coup de poing avant";
                Debug.Log("Coup de poing avant");
            }

            if (course.b1)
            {
                text.text = "Course";
                Debug.Log("Course");
            }

            if (multiClonage.b1)
            {
                text.text = "Multi clonage";
                Debug.Log("Multi Clonage");
                Application.LoadLevel("menu");
            }
        }
        //après selection
        else
        {
            cubes[selected].GetComponent<Renderer>().material.color = Color.red;

            //deselectionne le cube
            if (coupDePoingDroitAvant.b1)
            {
                text.text = "Coup de poing avant";
                Debug.Log("Coup de poing droit avant !");
                //do sth
                cubes[selected].GetComponent<Renderer>().material.color = Color.white;
                selected = -1;
            }

            //fais voler le cube
            if (course.b1)
            {
                text.text = "Course";
                Debug.Log("Course !");
                //do sth
                cubes[selected].transform.Translate(new Vector3(0, 1, 0));
            }

            ////duplique le cube
            //if(multiClonage.b1)
            //{
            //    Debug.Log("Multi Clonage !");
            //    //do sth
            //    Object cube = Instantiate(cubePrefab, new Vector3(cubes[cubes.Length - 1].transform.position.x+2, 0, 0), Quaternion.identity);
            //    cubes.SetValue(cube, cubes.Length);
            //}
        }       
        

        //dernier movement pour sortir du jeu

        if (Input.GetKeyUp(KeyCode.Escape) == true)
        {
            Debug.Log("Escape !");
            Application.LoadLevel("menu");
            //do sth
        }
	}
}
