/*
 Contrôle d'application créé à partir de la hauteur de la main gauche
 - En entrée, le tracker de la main gauche, ainsi que les seuils d'activation des états permettant de déterminer le booléen b1
 - En sortie, la valeur du booléen b1
 On passe à l'état 1 si la main gauche est en position basse et qu'on est dans l'état 0. b1 vaut vrai si état 1 et position haute détectée.
 */

using UnityEngine;
using System.Collections;

public class bouge_bras : MonoBehaviour {
    [Tooltip("Kinect Point Controller associé à la main gauche.")]
    public KinectPointController kpc;
    [Tooltip("Seuil de position haute du bras gauche.")]
    public float hauteur_threshold_up = 1.1F;
    [Tooltip("Seuil de position basse du bras gauche.")]
    public float hauteur_threshold_down = 0.7F;
    public int index_state=0;
    public bool b1=false;
   
    private Vector3 left_hand_position; // Pour le controle d'application b1
    private enum Left_hand_states { LEFT_HAND_NEUTRAL=0, LEFT_HAND_LOW, LEFT_HAND_HIGH };
    private Left_hand_states[] state;
    
	// Use this for initialization
	void Start () {
        b1 = false;
        state = new Left_hand_states[3];
        state[0] = Left_hand_states.LEFT_HAND_NEUTRAL;
        state[1] = Left_hand_states.LEFT_HAND_NEUTRAL;
        state[2] = Left_hand_states.LEFT_HAND_NEUTRAL;
        index_state = 0;
	}
	
	// Update is called once per frame
	void Update () {
        Left_hand_states new_state=Left_hand_states.LEFT_HAND_NEUTRAL;
        if (kpc.isTracked)
        {
            left_hand_position = kpc.transform.position;
            if (left_hand_position.y > hauteur_threshold_up)
                new_state = Left_hand_states.LEFT_HAND_HIGH;
            else if (left_hand_position.y < hauteur_threshold_down)
                new_state = Left_hand_states.LEFT_HAND_LOW;
            else { }

            if (index_state == 0 & new_state == Left_hand_states.LEFT_HAND_LOW)
            {
                index_state++;
                b1 = false;
            }
            else if (index_state == 1 & new_state == Left_hand_states.LEFT_HAND_HIGH)
            {
                index_state++;
                b1 = true;
            }
            else if (index_state == 2 & new_state == Left_hand_states.LEFT_HAND_LOW)
            {
                index_state = 0;
                b1 = false;
            }
            else { }
        }
        else
            b1 = false;
	}
}
