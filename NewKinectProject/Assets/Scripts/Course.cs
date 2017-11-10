/*
 Contrôle d'application créé à partir de la distance de la main gauche
 - En entrée, le tracker de la main gauche, ainsi que les seuils d'activation des états permettant de déterminer le booléen b1
 - En sortie, la valeur du booléen b1
 On passe à l'état 1 si la main gauche est en position basse et qu'on est dans l'état 0. b1 vaut vrai si état 1 et position haute détectée.
 */

using UnityEngine;
using System.Collections;

public class Course : MonoBehaviour
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
    private float distanceToBodyRightY;
    private float distanceToBodyLeftY;
    private enum hand_states { HANDS_NEUTRAL = 0, RIGHT_UP_LEFT_DOWN, HANDS_EQUAL, LEFT_UP_RIGHT_DOWN };
    private hand_states[] state;

    // Use this for initialization
    void Start()
    {
        b1 = false;
        state = new hand_states[3];
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
        hand_states new_state = hand_states.HANDS_NEUTRAL;
        if (kpc.isTracked)
        {
            right_hand_position = kpc.Hand_Right.transform.position;
            left_hand_position = kpc.Hand_Left.transform.position;
            right_elbow_position = kpc.Elbow_Right.transform.position;
            left_elbow_position = kpc.Elbow_Left.transform.position;
            distanceToBodyRightY = right_hand_position.y - right_elbow_position.y;
            distanceToBodyLeftY = left_hand_position.y - left_elbow_position.y;
            //on pourrait ajouter des conditions sur le x aussi
            //à affiner selon le transform que l'on mettra
            //right_hand_position = kpc.transform.position;

            //état initial
            //premier état distanceGauche>thresholdup et absdistanceDroite<thresholdown
            //deuxième état, mains même y ou bien dans la même tranche de y
            //3ème état, absdistanceGauche<thresholddown et absdistanceDroite>threhsoldup
            //4ème état, mains même y ou bien dans la même tranche de y
            //
            if (Mathf.Abs(distanceToBodyRightY) < distance_threshold_down && Mathf.Abs(distanceToBodyLeftY) > distance_threshold_up)
                new_state = hand_states.RIGHT_UP_LEFT_DOWN;
            else if (Mathf.Abs(distanceToBodyLeftY) < distance_threshold_down && Mathf.Abs(distanceToBodyRightY) > distance_threshold_up)
                new_state = hand_states.LEFT_UP_RIGHT_DOWN;
            else if (Mathf.Abs(distanceToBodyRightY) < distance_threshold_up && Mathf.Abs(distanceToBodyRightY) > distance_threshold_down && Mathf.Abs(distanceToBodyLeftY) < distance_threshold_up && Mathf.Abs(distanceToBodyLeftY) > distance_threshold_down)
                new_state = hand_states.HANDS_EQUAL;
            else { }

            if (index_state == 0 && new_state == hand_states.RIGHT_UP_LEFT_DOWN)
            {
                index_state++;
                b1 = false;
            }
            else if (index_state == 1 && new_state == hand_states.HANDS_EQUAL)
            {
                index_state++;
                //b1 = true;
            }
            else if (index_state == 2 && new_state == hand_states.LEFT_UP_RIGHT_DOWN)
            {
                index_state++;
                b1 = true;
            }
            else if (index_state == 3 && new_state == hand_states.HANDS_EQUAL)
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


