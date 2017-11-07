/*
 Avance dans la direction des Z positifs si le booléen b1 est vrai.
 - En entrée, booléen b1 issu du script bouge_bras.cs, le GameObject qui va être éventuellement translaté et l'incrément élémentaire de translation
 - En sortie, translation éventuelle du GameObject body dans la direction du GameObject portant ce script
  */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avance_regard : MonoBehaviour {
    [Tooltip("GameObject à translater dans la direction du GameObject courant")]
    public GameObject body;
    [Tooltip("Méthode servant au contrôle d'application pour avancer ou non")]
    public bouge_bras ca;
    [Tooltip("Incrément de translation")]
    public float inc;
	
    // Use this for initialization
	void Start () {
        inc = 0.005F;
	}
	
	// Update is called once per frame
	void Update () {
        if (ca.b1 == true)
        {
            Vector3 move= inc*transform.forward;
            move.y = 0.0F; // On élimine la partie de translation suivant l'axe Y (hauteurs)
            body.transform.position += move;
        }
	}
}
