/*
 Contrôle d'application créé à partir de la distance de la main gauche
 - En entrée, le tracker de la main gauche, ainsi que les seuils d'activation des états permettant de déterminer le booléen b1
 - En sortie, la valeur du booléen b1
 On passe à l'état 1 si la main gauche est en position basse et qu'on est dans l'état 0. b1 vaut vrai si état 1 et position haute détectée.
 */

using UnityEngine;
using System.Collections;

public class BalayageDroit : MonoBehaviour
{
    [Tooltip("Kinect Point Controller associé à l'upperbody.")]
    public KinectPointController kpc;
    //threshold à faire en proportion avec le corps
    [Tooltip("Seuil de position haute du bras droit (éloigné du corps).")]
    public float distance_threshold_up = 1.1F;
    [Tooltip("Seuil de position basse du bras droit (collé au corps).")]
    public float distance_threshold_down = 0.7F;
    //[Tooltip("Seuil de position middle du bras droit (mi-distance).")]
    //public float distance_threshold_middle = 0.9F;
    public int index_state = 0;
    //movement détecté
    public bool b1 = false;

    private Vector3 right_hand_position; // Pour le controle d'application b1
    private Vector3 right_elbow_position;
    private float distanceToBody;
    private enum Right_hand_states { RIGHT_HAND_NEUTRAL = 0, RIGHT_HAND_LOW, RIGHT_HAND_HIGH, RIGHT_HAND_MIDDLE };
    private Right_hand_states[] state;

    // Use this for initialization
    void Start()
    {
        b1 = false;
        state = new Right_hand_states[4];
        state[0] = Right_hand_states.RIGHT_HAND_NEUTRAL;
        state[1] = Right_hand_states.RIGHT_HAND_NEUTRAL;
        state[2] = Right_hand_states.RIGHT_HAND_NEUTRAL;
        state[3] = Right_hand_states.RIGHT_HAND_NEUTRAL;
        index_state = 0;
        distance_threshold_down = (kpc.Elbow_Right.transform.position.y - kpc.Hip_Right.transform.position.y) / 3;
        distance_threshold_up = 2*(kpc.Elbow_Right.transform.position.y - kpc.Hip_Right.transform.position.y) / 3;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateThreshold();
        Right_hand_states new_state = Right_hand_states.RIGHT_HAND_NEUTRAL;
        if (kpc.isTracked)
        {
            right_hand_position = kpc.Hand_Right.transform.position;
            right_elbow_position = kpc.Elbow_Right.transform.position;
            distanceToBody = right_hand_position.x - right_elbow_position.x;
            //à affiner selon le transform que l'on mettra
            //right_hand_position = kpc.transform.position;
            if (distanceToBody > distance_threshold_up)
                new_state = Right_hand_states.RIGHT_HAND_HIGH;
            else if (distanceToBody < distance_threshold_down)
                new_state = Right_hand_states.RIGHT_HAND_LOW;
            else if (distanceToBody < distance_threshold_up && distanceToBody > distance_threshold_down)
                new_state = Right_hand_states.RIGHT_HAND_MIDDLE;
            else { }

            if (index_state == 0 && new_state == Right_hand_states.RIGHT_HAND_LOW)
            {
                index_state++;
                b1 = false;
            }
            else if (index_state == 1)
            {
                if(new_state == Right_hand_states.RIGHT_HAND_LOW)
                {

                }else if ( new_state == Right_hand_states.RIGHT_HAND_MIDDLE)
                {
                    index_state++;
                }
                
                //b1 = true;
            }
            else if (index_state == 2 )
            {
                if(new_state == Right_hand_states.RIGHT_HAND_MIDDLE)
                {

                }else if(new_state == Right_hand_states.RIGHT_HAND_HIGH)
                {
                    index_state++;
                    b1 = true;
                }
                
            }
            else if (index_state == 3 )
            {
                if(new_state == Right_hand_states.RIGHT_HAND_HIGH)
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

