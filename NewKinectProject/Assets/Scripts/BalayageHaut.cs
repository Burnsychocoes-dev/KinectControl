/*
 Contrôle d'application créé à partir de la distance de la main gauche
 - En entrée, le tracker de la main gauche, ainsi que les seuils d'activation des états permettant de déterminer le booléen b1
 - En sortie, la valeur du booléen b1
 On passe à l'état 1 si la main gauche est en position basse et qu'on est dans l'état 0. b1 vaut vrai si état 1 et position haute détectée.
 */

using UnityEngine;
using System.Collections;

public class BalayageHaut : MonoBehaviour
{
    [Tooltip("Kinect Point Controller associé à l'upperbody.")]
    public KinectPointController kpc;
    [Tooltip("Seuil de position haute du bras droit (éloigné du corps).")]
    public float distance_threshold_up = 1.1F;
    [Tooltip("Seuil de position basse du bras droit (collé au corps).")]
    public float distance_threshold_down = 0.7F;
    //[Tooltip("Seuil de position middle du bras droit (mi-distance).")]
    //public float distance_threshold_middle = 0.9F;
    public int index_state = 0;
    //movement détecté
    public bool b1 = false;

    private Vector3 left_hand_position;
    private Vector3 right_hand_position; // Pour le controle d'application b1
    private Vector3 left_elbow_position;
    private Vector3 right_elbow_position;
    private float distanceToBodyRight;
    private float distanceToBodyLeft;
    private enum hand_states { HANDS_NEUTRAL = 0, HANDS_LOW, HANDS_HIGH, HANDS_MIDDLE };
    private hand_states[] state;

    // Use this for initialization
    void Start()
    {
        b1 = false;
        state = new hand_states[4];
        state[0] = hand_states.HANDS_NEUTRAL;
        state[1] = hand_states.HANDS_NEUTRAL;
        state[2] = hand_states.HANDS_NEUTRAL;
        state[3] = hand_states.HANDS_NEUTRAL;
        index_state = 0;
        distance_threshold_down = (kpc.Elbow_Right.transform.position.y - kpc.Hip_Right.transform.position.y) / 3;
        distance_threshold_up = 2 * (kpc.Elbow_Right.transform.position.y - kpc.Hip_Right.transform.position.y) / 3;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateThreshold();
        hand_states new_state = hand_states.HANDS_NEUTRAL;
        if (kpc.isTracked)
        {
            right_hand_position = kpc.Hand_Right.transform.position;
            left_hand_position = kpc.Hand_Left.transform.position;
            right_elbow_position = kpc.Elbow_Right.transform.position;
            left_elbow_position = kpc.Elbow_Left.transform.position;
            distanceToBodyRight = right_hand_position.y - right_elbow_position.y;
            distanceToBodyLeft = left_hand_position.y - left_elbow_position.y;
            //à affiner selon le transform que l'on mettra
            //right_hand_position = kpc.transform.position;
            if (distanceToBodyRight > distance_threshold_up && distanceToBodyLeft > distance_threshold_up)
                new_state = hand_states.HANDS_HIGH;
            else if (distanceToBodyRight < distance_threshold_down && distanceToBodyLeft < distance_threshold_down)
                new_state = hand_states.HANDS_LOW;
            else if (distanceToBodyRight < distance_threshold_up && distanceToBodyRight > distance_threshold_down && distanceToBodyLeft < distance_threshold_up && distanceToBodyLeft > distance_threshold_down)
                new_state = hand_states.HANDS_MIDDLE;
            else { }

            if (index_state == 0 && new_state == hand_states.HANDS_LOW)
            {
                index_state++;
                b1 = false;
            }
            else if (index_state == 1 )
            {
                if(new_state == hand_states.HANDS_LOW)
                {

                }else if(new_state == hand_states.HANDS_MIDDLE)
                {
                    index_state++;
                }
                
                //b1 = true;
            }
            else if (index_state == 2)
            {
                if(new_state == hand_states.HANDS_MIDDLE)
                {

                }else if ( new_state == hand_states.HANDS_HIGH)
                {
                    index_state++;
                    b1 = true;
                }
                
            }
            else if (index_state == 3)
            {
                if(new_state == hand_states.HANDS_HIGH)
                {
                    b1 = false;
                }
                else
                {
                    index_state = 0;
                    b1 = false;
                }
                
            }
            else { }
        }
        else
            b1 = false;
    }
    private void UpdateThreshold()
    {
        if ((kpc.Elbow_Right.transform.position.y - kpc.Hip_Right.transform.position.y) / 3 > distance_threshold_down)
        {
            distance_threshold_down = (kpc.Elbow_Right.transform.position.y - kpc.Hip_Right.transform.position.y) / 3;
        }
        if (2 * (kpc.Elbow_Right.transform.position.y - kpc.Hip_Right.transform.position.y) / 3 > distance_threshold_up)
        {
            distance_threshold_up = 2 * (kpc.Elbow_Right.transform.position.y - kpc.Hip_Right.transform.position.y) / 3;
        }
    }
}


