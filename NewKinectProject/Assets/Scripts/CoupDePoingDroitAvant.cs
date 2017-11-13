/*
 Contrôle d'application créé à partir de la distance de la main gauche
 - En entrée, le tracker de la main gauche, ainsi que les seuils d'activation des états permettant de déterminer le booléen b1
 - En sortie, la valeur du booléen b1
 On passe à l'état 1 si la main gauche est en position basse et qu'on est dans l'état 0. b1 vaut vrai si état 1 et position haute détectée.
 */

using UnityEngine;
using System.Collections;

public class CoupDePoingDroitAvant : MonoBehaviour
{
    [Tooltip("Kinect mouvement Controller associé à l'upperbody.")]
    public KinectModelControllerV2 kmc;
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
    private Vector3 right_shoulder_position;
    private float distanceToBody;
    private float distanceHandShoulderY;
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
        distance_threshold_down = (kmc.Shoulder_Right.transform.position.y - kmc.Hip_Right.transform.position.y) / 3;
        distance_threshold_up = 2 * (kmc.Shoulder_Right.transform.position.y - kmc.Hip_Right.transform.position.y) / 3;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateThreshold();
        Right_hand_states new_state = Right_hand_states.RIGHT_HAND_NEUTRAL;
        if (kmc.isTracked)
        {
            right_hand_position = kmc.Hand_Right.transform.position;
            right_shoulder_position = kmc.Shoulder_Right.transform.position;
            distanceToBody = Mathf.Abs(right_hand_position.z - right_shoulder_position.z);
            distanceHandShoulderY = Mathf.Abs(right_hand_position.y- right_shoulder_position.y);
            //à affiner selon le transform que l'on mettra
            //right_hand_position = kmc.transform.position;
            if (distanceToBody > distance_threshold_up && distanceHandShoulderY < distance_threshold_down)
                new_state = Right_hand_states.RIGHT_HAND_HIGH;
            else if (distanceToBody < distance_threshold_down && distanceHandShoulderY < distance_threshold_down)
                new_state = Right_hand_states.RIGHT_HAND_LOW;
            else if (distanceToBody < distance_threshold_up && distanceToBody > distance_threshold_down && distanceHandShoulderY < distance_threshold_down)
                new_state = Right_hand_states.RIGHT_HAND_MIDDLE;
            else { }

            b1 = false;

            if (index_state == 0 && new_state == Right_hand_states.RIGHT_HAND_LOW)
            {
                index_state++;
                //b1 = false;
            }
            else if (index_state == 1)
            {
                if(new_state == Right_hand_states.RIGHT_HAND_LOW)
                {

                }else if(new_state == Right_hand_states.RIGHT_HAND_MIDDLE)
                {
                    index_state++;
                    //b1 = true;
                }

            }
            else if (index_state == 2 )
            {
                if(new_state == Right_hand_states.RIGHT_HAND_MIDDLE)
                {

                }else if(new_state == Right_hand_states.RIGHT_HAND_HIGH)
                {
                    index_state++;
                    //b1 = true;
                }
                
            }
            else if (index_state == 3 )
            {
                if(new_state == Right_hand_states.RIGHT_HAND_HIGH)
                {
                    //b1 = false;
                }
                else
                {
                    index_state = 0;
                    b1 = true;
                }
                
            }
            else { }
        }
        else
            b1 = false;
    }

    private void UpdateThreshold()
    {
        if((kmc.Shoulder_Right.transform.position.y - kmc.Hip_Right.transform.position.y) / 3 > distance_threshold_down)
        {
            distance_threshold_down = (kmc.Shoulder_Right.transform.position.y - kmc.Hip_Right.transform.position.y) / 3;
        }
        if(2 * (kmc.Shoulder_Right.transform.position.y - kmc.Hip_Right.transform.position.y) / 3 > distance_threshold_up)
        {
            distance_threshold_up = 2 * (kmc.Shoulder_Right.transform.position.y - kmc.Hip_Right.transform.position.y) / 3;
        }
    }
}

