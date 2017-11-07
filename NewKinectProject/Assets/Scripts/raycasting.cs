/* 
Implémentation du RayCasting utisisant les angles d'Euler associés à une articulation. Le script doit être appliqué au GameObject portant le rayon graphique
-En entrée: Le tracker récupérant une orientation 3D sous la forme d'angles d'Euler, un offset à éventuellement appliquer sur les angles d'Euler, une permutation éventuelle des données liées aux axes X et Y ou Z et Y
- L'orientation du GameObject portant le RayCasting est modifiée en conséquence.
 */

using UnityEngine;
using System.Collections;

public class raycasting : MonoBehaviour {
    [Tooltip("IN: GameObject sur lequel un Tracker récupère une orientation 3D sous la forme d'angles d'Euler")]
    public GameObject Tracker;
    [Tooltip("IN: offset à appliquer sur les angles d'Euler")]
    public Vector3 offset;
    [Tooltip("IN: Permtute les données suivant les axes X et Y, suivant le cas")]
    public bool flip_xy;
    [Tooltip("IN: Permtute les données suivant les axes Z et Y, suivant le cas")]
    public bool flip_zy;

    private Vector3 ray_direction;
    private Vector3 raw_euler_angles;
    private Vector3 rv_euler_angles;

    void Start()
    {
        /* A modifier suivant le positionnement de la kinect */
        offset = new Vector3(135.0F, 0.0F, 0.0F);
        flip_xy = true;
        flip_zy = true;
    }

    // Update is called once per frame

    void Update()
    {
        raw_euler_angles = Tracker.transform.rotation.eulerAngles;
        rv_euler_angles = raw_euler_angles + offset;
        if (flip_xy)
        {
            float aux = rv_euler_angles.y;
            rv_euler_angles.y = rv_euler_angles.x;
            rv_euler_angles.x = aux;
        }
        if (flip_zy)
        {
            float aux = rv_euler_angles.y;
            rv_euler_angles.y = rv_euler_angles.z;
            rv_euler_angles.z = aux;
        }
        // this.transform.eulerAngles = rv_euler_angles; // Mise à jour des angles d'Euler de l'avatar de la main droite
        this.transform.right = rv_euler_angles;
    }
}
