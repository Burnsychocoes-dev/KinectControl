using UnityEngine;
using System.Collections;

public class avance : MonoBehaviour {
    public KinectModelControllerV2 kmc;
    [Tooltip("Epaule droite trackée par kmc.")]
    public GameObject Right_Shoulder;
    [Tooltip("Epaule gauche trackée par kmc.")]
    public GameObject Left_Shoulder;
    [Tooltip("Interval de valeurs de la composante en y de la rotation de l'épaule gauche impliquant un mouvement vers l'arrière.")]
    public Vector2 ep_gauche_interv;
    [Tooltip("Interval de valeurs de la composante en y de la rotation de l'épaule droite impliquant un mouvement vers l'avant.")]
    public Vector2 ep_droite_interv;
    private Vector3 right_shoulder_rot;
    private Vector3 left_shoulder_rot;
    private Vector3 inc_avance;
    private Vector3 inc_recule;

	// Use this for initialization
	void Start () {
        right_shoulder_rot = new Vector3();
        left_shoulder_rot = new Vector3();
        inc_avance = new Vector3(0.0F, 0.0F, -0.1F);
        inc_recule = new Vector3(0.0F, 0.0F, 0.1F);
        ep_droite_interv.x = 50.0F;
        ep_droite_interv.y = 180.0F;
        ep_gauche_interv.x = 50.0F;
        ep_gauche_interv.y = 180.0F;
	}
	
	// Update is called once per frame
	void Update () {
        if (kmc.isTracked == true)
        {
            right_shoulder_rot = Right_Shoulder.transform.eulerAngles;
            if (right_shoulder_rot.y > ep_droite_interv.x && right_shoulder_rot.y < ep_droite_interv.y)
            {
                this.transform.Translate(inc_avance);
                Debug.Log("Vers l'avant!");
            }
            else { }
            
            left_shoulder_rot = Left_Shoulder.transform.eulerAngles;
            if (left_shoulder_rot.y > ep_gauche_interv.x & left_shoulder_rot.y < ep_gauche_interv.y)
            {
                this.transform.Translate(inc_recule);
                Debug.Log("Vers l'arrière!");
            }
            else { }
        }
	}
}
