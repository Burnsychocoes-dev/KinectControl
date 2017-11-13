/*
 Contrôle d'application créé à partir de la distance de la main gauche
 - En entrée, le tracker de la main gauche, ainsi que les seuils d'activation des états permettant de déterminer le booléen b1
 - En sortie, la valeur du booléen b1
 On passe à l'état 1 si la main gauche est en position basse et qu'on est dans l'état 0. b1 vaut vrai si état 1 et position haute détectée.
 */

using UnityEngine;
using System.Collections;

public class CoupDePoingDroitAvant : Movement
{
    

    private Vector3 right_hand_position_1; // Pour le controle d'application b1
    private Vector3 right_hand_position_2;
    private Vector3 left_shoulder_position;
    private float distanceToBody;
    private float distanceHandShoulderY;
    private float punchSpeed;
    //public float right_hand_X_speed;
    //public float right_hand_Y_speed;
    public float punchSpeedMarge = 10f;
    public float marge = 3f;
    private enum Right_hand_states { RIGHT_HAND_NEUTRAL = 0, BOXING_STATE, RIGHT_HAND_PUNCH_STATE};
    private Right_hand_states[] state;
    private Right_hand_states new_state;

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
        right_hand_position_1 = kmc.Hand_Right.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateThreshold();
        new_state = Right_hand_states.RIGHT_HAND_NEUTRAL;
        if (kmc.isTracked)
        {
            right_hand_position_2 = right_hand_position_1;
            right_hand_position_1 = kmc.Hand_Right.transform.position;
            left_shoulder_position = kmc.Shoulder_Right.transform.position;
            distanceToBody = Mathf.Abs(right_hand_position_1.z - left_shoulder_position.z);
   
            //Debug.Log(punchSpeed);
            //Debug.Log(distanceToBody);
            distanceHandShoulderY = Mathf.Abs(right_hand_position_1.y- left_shoulder_position.y);
            //à affiner selon le transform que l'on mettra
            //right_hand_position = kmc.transform.position;



        }
        else
            b1 = false;
    }

    private void FixedUpdate()
    {
        punchSpeed = (right_hand_position_2.z - right_hand_position_1.z) / Time.fixedDeltaTime;
        //right_hand_X_speed = Mathf.Abs(right_hand_position_2.x - right_hand_position_1.x) / Time.fixedDeltaTime;
        //right_hand_Y_speed = Mathf.Abs(right_hand_position_2.y - right_hand_position_1.y) / Time.fixedDeltaTime;

        NewStateUpdate();
        StateTransition();
    }

    override protected void NewStateUpdate()
    {
        if (distanceHandShoulderY < marge)
            new_state = Right_hand_states.BOXING_STATE;
        //if (distanceHandShoulderY > marge)
        //    Debug.Log("Mauvaise position de punch !");
        else if (punchSpeed > punchSpeedMarge && distanceHandShoulderY < marge)
            new_state = Right_hand_states.RIGHT_HAND_PUNCH_STATE;
        //if (distanceToBody > distance_threshold_up && distanceHandShoulderY < distance_threshold_down / 2)
        //    new_state = Right_hand_states.RIGHT_HAND_HIGH;
        //else if (distanceToBody < distance_threshold_down * 0.75 && distanceHandShoulderY < distance_threshold_down / 2)
        //    new_state = Right_hand_states.RIGHT_HAND_LOW;
        //else if (distanceToBody < distance_threshold_up && distanceToBody > distance_threshold_down * 0.75 && distanceHandShoulderY < distance_threshold_down / 2)
        //    new_state = Right_hand_states.RIGHT_HAND_MIDDLE;
        else { }

        b1 = false;
    }

    override protected void StateTransition()
    {
        if (index_state == 0 && new_state == Right_hand_states.BOXING_STATE)
        {
            Debug.Log("Position boxing");
            index_state++;
            //b1 = false;
        }
        else if (index_state == 1)
        {
            if (new_state == Right_hand_states.BOXING_STATE)
            {

            }
            else if (new_state == Right_hand_states.RIGHT_HAND_PUNCH_STATE)
            {
                Debug.Log("Punch !");
                index_state = 0;
                b1 = true;
            }
            //else
            //{
            //    if (distanceHandShoulderY > marge)
            //    {
            //        index_state = 0;
            //    }
            //}
        }
        //else if (index_state == 2)
        //{
        //    if (new_state == Right_hand_states.RIGHT_HAND_MIDDLE)
        //    {

        //    }
        //    else if (new_state == Right_hand_states.RIGHT_HAND_HIGH)
        //    {
        //        Debug.Log("Coup de poing high");
        //        index_state++;
        //        //b1 = true;
        //    }
        //    //else
        //    //{
        //    //    index_state = 0;
        //    //}

        //}
        //else if (index_state == 3)
        //{
        //    //if (new_state == Right_hand_states.RIGHT_HAND_HIGH)
        //    //{
        //    //    //b1 = false;
        //    //}
        //    //else
        //    //{
        //        index_state = 0;
        //        b1 = true;
        //    //}

        //}
        else { }
    }
}

