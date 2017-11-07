/*
 Le script est attaché à un objet cible du monde virtuel qui peut être sélectionné
 - En entrée, le GameObject utilisé pour sélectionner l'objet cible, le script permettant le contrôle d'application permettant de passer de l'état sélectionnable à l'état sélectionné pour la cible, l'état courant de l'objet cible
 - En sortie, l'état du système de sélection associé à la cible, le changement éventuel de couleur de la cible suivant l'état du système

 */

using UnityEngine;
using System.Collections;

public class EstSelectionnableMVS : MonoBehaviour
{
    [Tooltip("GameObject servant d'avatar à la sélection de la cible.")]
    public GameObject avatar_selection;
    [Tooltip("GameObject servant de parent à la cible si celle-ci est sélectionné")]
    public bouge_bras ca;

    private enum Etats_cible { LIBRE = 0, SELECTIONNABLE, SELECTIONNE }; // états atteignables
    [Tooltip("Etat courant du système de sélection de la cible.")]
    private Etats_cible etat = Etats_cible.LIBRE;
    private Color old_color;


    // Use this for initialization
    void Start()
    {
        etat = Etats_cible.LIBRE;
        old_color = this.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == avatar_selection.name) // L'objet cible est-il intersecté par l'avatar de sélection?
        {
            UnityEngine.Debug.Log("Enter");
            etat = Etats_cible.SELECTIONNABLE; // oui, on passe à l'état SELECTIONNABLE
            old_color = this.GetComponent<Renderer>().material.color;
            this.GetComponent<Renderer>().material.color = new Color(1.0F, 0.0F, 0.0F); // .. et la cible devient rouge
        }
    }


    void Update()
    {

        if (etat == Etats_cible.SELECTIONNABLE & this.transform.parent == null & ca.b1 == true)
        { // On passe de l'état SELECTIONNABLE à l'état SELECTIONNE
            etat = Etats_cible.SELECTIONNE;
            this.transform.parent = avatar_selection.transform; // avatar_selection devient le père de la cible
        }
        if (etat == Etats_cible.SELECTIONNE & ca.b1 == false)
        {
            etat = Etats_cible.LIBRE;
            this.transform.parent = null; // La cible n'a plus de père dans la hiérarchie Unity
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == avatar_selection.name)
        {
            UnityEngine.Debug.Log("Exit");
            etat = Etats_cible.LIBRE;
            this.transform.parent = null; // La cible n'a plus de père dans la hiérarchie Unity
            this.GetComponent<Renderer>().material.color = old_color;
        }
    }
}
