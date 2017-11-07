/*
Mise en oeuvre de la technique Main Virtuelle Simple
- En entrée, le tracker qui récupère des infos 3D via KinectPointController, la matrice de passage du réel vers le monde virtuel ainsi que l'offset
- en sortie, la position du GameObject qui porte le scripts mvs.cs est modifiée
 */

using UnityEngine;
using System.Collections;

public class mvs : MonoBehaviour {
    [Tooltip("IN: Tracker est un EmptyObject qui est associé à la position 3D d'une articulation de la Kinectv1 via KinectPointController")]
    public GameObject Tracker; // Game Object qui contient le script KinectPointController
    [Tooltip("IN: A règler suivant la position et l'orientation de la kinect dans le monde réel.")]
    public Matrix4x4 Real_to_RV=Matrix4x4.identity;
    [Tooltip("IN: Offset afin de règler le 0 de la Kinect par rapport au 0 du monde virtuel.")]
    public Vector3 offset;

    private Vector3 raw_position;
    private Vector3 rv_position;

	// Use this for initialization
	void Start () {
        offset = new Vector3(0.0F, -1.0F, 1.5F); // Offset de règlage de la Kinect pour le passage données brutes vers position dans le monde virtuel
	}
	
	// Update is called once per frame
	void Update () {
        raw_position = Tracker.transform.position; // Récupération de la position 3D de la main droite dans le repère lié à la Kinect
        rv_position = (Vector3)(Real_to_RV * raw_position) + offset; // Passage dans le repère lié au monde virtuel, GAIN = 1 dans le cas de la MVS.
 
        this.transform.position = rv_position; // Mise à jour de la position de l'avatar qui porte le script mvs.cs
	}
}
