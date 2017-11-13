/*
 Contrôle d'application créé à partir de la distance de la main gauche
 - En entrée, le tracker de la main gauche, ainsi que les seuils d'activation des états permettant de déterminer le booléen b1
 - En sortie, la valeur du booléen b1
 On passe à l'état 1 si la main gauche est en position basse et qu'on est dans l'état 0. b1 vaut vrai si état 1 et position haute détectée.
 */

using UnityEngine;
using System.Collections;

public class BalayageHaut1 : Movement
{

    private Vector3 left_hand_position;
    private Vector3 right_hand_position; // Pour le controle d'application b1
    private Vector3 left_shoulder_position;
    private Vector3 right_shoulder_position;
    private float distanceToBodyRight;
    private float distanceToBodyLeft;
    private enum hand_states { HANDS_NEUTRAL = 0, HANDS_LOW, HANDS_HIGH, HANDS_MIDDLE };
    private hand_states[] state;
    private hand_states new_state;

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
        distance_threshold_down = (kmc.Shoulder_Right.transform.position.y - kmc.Hip_Right.transform.position.y) / 3;
        distance_threshold_up = 2 * (kmc.Shoulder_Right.transform.position.y - kmc.Hip_Right.transform.position.y) / 3;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateThreshold();
        new_state = hand_states.HANDS_NEUTRAL;
        if (kmc.isTracked)
        {
            right_hand_position = kmc.Hand_Right.transform.position;
            left_hand_position = kmc.Hand_Left.transform.position;
            right_shoulder_position = kmc.Shoulder_Right.transform.position;
            left_shoulder_position = kmc.Shoulder_Left.transform.position;
            distanceToBodyRight = right_hand_position.y - right_shoulder_position.y;
            distanceToBodyLeft = left_hand_position.y - left_shoulder_position.y;
            //à affiner selon le transform que l'on mettra
            //right_hand_position = kmc.transform.position;
            NewStateUpdate();
            StateTransition();

            
        }
        else
            b1 = false;
    }
    protected void NewStateUpdate()
    {
        if (distanceToBodyRight > distance_threshold_up && distanceToBodyLeft > distance_threshold_up)
            new_state = hand_states.HANDS_HIGH;
        else if (distanceToBodyRight < distance_threshold_down && distanceToBodyLeft < distance_threshold_down)
            new_state = hand_states.HANDS_LOW;
        else if (distanceToBodyRight < distance_threshold_up && distanceToBodyRight > distance_threshold_down && distanceToBodyLeft < distance_threshold_up && distanceToBodyLeft > distance_threshold_down)
            new_state = hand_states.HANDS_MIDDLE;
        else { }

        b1 = false;
    }

    protected void StateTransition()
    {
        if (index_state == 0 && new_state == hand_states.HANDS_LOW)
        {
            index_state++;
            //b1 = false;
        }
        else if (index_state == 1)
        {
            if (new_state == hand_states.HANDS_LOW)
            {

            }
            else if (new_state == hand_states.HANDS_MIDDLE)
            {
                index_state++;
            }

            //b1 = true;
        }
        else if (index_state == 2)
        {
            if (new_state == hand_states.HANDS_MIDDLE)
            {

            }
            else if (new_state == hand_states.HANDS_HIGH)
            {
                index_state++;
                //b1 = true;
            }

        }
        else if (index_state == 3)
        {
            if (new_state == hand_states.HANDS_HIGH)
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
}


