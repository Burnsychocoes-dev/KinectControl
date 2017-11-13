using UnityEngine;
using System.Collections;


//Cette classe va agir en fonction des differents mouvements que l'on a
public class MovementController : MonoBehaviour {
    //soit on met ça à la main, soit il doit aller les chercher sur l'objet dans le start, cela dépend de où les scripts se trouvent
    public BalayageDroit balayageDroit;
    public BalayageGauche balayageGauche;
    public BalayageHaut balayageHaut;
    public CoupDePoingDroitAvant coupDePoingDroitAvant;
    public Course course;
	// Use this for initialization
	void Start () {
        balayageDroit = gameObject.GetComponent<BalayageDroit>();
        balayageGauche = gameObject.GetComponent<BalayageGauche>();
        balayageHaut = gameObject.GetComponent<BalayageHaut>();
        coupDePoingDroitAvant = gameObject.GetComponent<CoupDePoingDroitAvant>();
        course = gameObject.GetComponent<Course>();
    }
	
	// Update is called once per frame
	void Update () {
        if (balayageDroit.b1)
        {
            Debug.Log("Balayage droit !");
            //do sth
        }
        if (balayageGauche.b1)
        {
            Debug.Log("Balayage gauche !");
            //do sth
        }
        if (balayageHaut.b1)
        {
            Debug.Log("Balayage haut !");
            //do sth
        }
        if (coupDePoingDroitAvant.b1)
        {
            Debug.Log("Coup de poing droit avant !");
            //do sth
        }
        if (course.b1)
        {
            Debug.Log("Course !");
            //do sth
        }
	}
}
