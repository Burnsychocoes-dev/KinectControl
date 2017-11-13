using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    [Tooltip("Kinect mouvement Controller associé à l'upperbody.")]
    public KinectModelControllerV2 kmc;
    [Tooltip("Seuil de position haute du bras droit (éloigné du corps).")]
    public float distance_threshold_up = 1.1F;
    [Tooltip("Seuil de position basse du bras droit (collé au corps).")]
    public float distance_threshold_down = 0.7F;
    //[Tooltip("Seuil de position middle du bras droit (mi-distance).")]
    //public float distance_threshold_middle = 0.9F;
    public int index_state = 0;
    //movement détecté
    public bool b1 = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected void UpdateThreshold()
    {
        if ((kmc.Shoulder_Right.transform.position.y - kmc.Hip_Right.transform.position.y) / 3 > distance_threshold_down)
        {
            distance_threshold_down = (kmc.Shoulder_Right.transform.position.y - kmc.Hip_Right.transform.position.y) / 3;
        }
        if (2 * (kmc.Shoulder_Right.transform.position.y - kmc.Hip_Right.transform.position.y) / 3 > distance_threshold_up)
        {
            distance_threshold_up = 2 * (kmc.Shoulder_Right.transform.position.y - kmc.Hip_Right.transform.position.y) / 3;
        }
    }

    protected virtual void NewStateUpdate()
    {

    }

    protected virtual void StateTransition()
    {

    }
}
