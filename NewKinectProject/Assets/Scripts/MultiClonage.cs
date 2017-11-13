/*
 Contrôle d'application créé à partir de la distance de la main gauche
 - En entrée, le tracker de la main gauche, ainsi que les seuils d'activation des états permettant de déterminer le booléen b1
 - En sortie, la valeur du booléen b1
 On passe à l'état 1 si la main gauche est en position basse et qu'on est dans l'état 0. b1 vaut vrai si état 1 et position haute détectée.
 */

using UnityEngine;
using System.Collections;

public class MultiClonage : Movement
{
    
    private Vector3 right_hand_position; // Pour le controle d'application b1
    private Vector3 left_hand_position;
    private Vector3 right_elbow_position;
    private Vector3 left_elbow_position;
    private float x_DistanceBetweenRightElbowToRightHand;
    private float y_DistanceBetweenLeftElbowToLeftHand;
    private float x_DistanceBetweenRightHandToLeftHand;
    private float y_DistanceBetweenRightHandToLeftHand;
    public float marge = 1f;
    private enum MultiClonageState { NEUTRAL_STATE = 0, KAGE_BUNCHIN_STATE };
    private MultiClonageState[] state;
    private MultiClonageState new_state;

    // Use this for initialization
    void Start()
    {
        b1 = false;
        state = new MultiClonageState[2];
        state[0] = MultiClonageState.NEUTRAL_STATE;
        state[1] = MultiClonageState.NEUTRAL_STATE;
        index_state = 0;
        distance_threshold_down = (kmc.Shoulder_Right.transform.position.y - kmc.Hip_Right.transform.position.y) / 3;
        distance_threshold_up = 2*(kmc.Shoulder_Right.transform.position.y - kmc.Hip_Right.transform.position.y) / 3;
        Debug.Log(distance_threshold_down);
        Debug.Log(distance_threshold_up);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateThreshold();
        new_state = MultiClonageState.NEUTRAL_STATE;
        if (kmc.isTracked)
        {
            right_hand_position = kmc.Hand_Right.transform.position;
            right_elbow_position = kmc.Elbow_Right.transform.position;
            left_hand_position = kmc.Hand_Left.transform.position;
            left_elbow_position = kmc.Elbow_Left.transform.position;

            //Update des distances
            x_DistanceBetweenRightElbowToRightHand = Mathf.Abs(right_elbow_position.x - right_hand_position.x);
            y_DistanceBetweenLeftElbowToLeftHand = Mathf.Abs(left_elbow_position.y - left_hand_position.y);
            x_DistanceBetweenRightHandToLeftHand = Mathf.Abs(right_hand_position.x - left_hand_position.x);
            y_DistanceBetweenRightHandToLeftHand = Mathf.Abs(right_hand_position.y - left_hand_position.y);

            //à affiner selon le transform que l'on mettra
            //right_hand_position = kmc.transform.position;
            NewStateUpdate();
            StateTransition();


        }
        else
            b1 = false;
    }


    override protected void NewStateUpdate()
    {
        if (x_DistanceBetweenRightElbowToRightHand < marge &&
            y_DistanceBetweenLeftElbowToLeftHand < marge &&
            x_DistanceBetweenRightHandToLeftHand < marge &&
            y_DistanceBetweenRightHandToLeftHand < marge &&
            left_hand_position.z < right_hand_position.z)
        { 
            new_state = MultiClonageState.KAGE_BUNCHIN_STATE;
        }   
        else { }
        b1 = false;
    }

    override protected void StateTransition()
    {
            if (index_state == 0 && new_state == MultiClonageState.KAGE_BUNCHIN_STATE)
            {
                Debug.Log("Multi clonage");
                index_state++;
                //b1 = false;
            }
            else if (index_state == 1)
            {
                if (new_state == MultiClonageState.KAGE_BUNCHIN_STATE)
                {
                    //b1 = false;
                }
                else
                {
                    Debug.Log("Multi clonage !");
                    index_state = 0;
                    b1 = true;
                }

            }
            else { }               
    }
}

