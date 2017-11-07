/*
 Implémentation de la méthode de navigation par Direction de la main [reprise du raycasting_pos]
 -En entrée: Le tracker récupérant la position 3D de la main, le tracker récupérant la position 3D de l'épaule, une matrice de rotation entre données réelles et virtuelles, une permutation éventuelle des données liées aux axes X et Y ou Z et Y
 - L'orientation du GameObject portant le RayCasting est modifiée en conséquence.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class direction_main : MonoBehaviour
{
    [Tooltip("IN: KinectPointController de la main")]
    public KinectPointController kpc_hand;
    [Tooltip("IN: Tracker qui récupère la position 3D de la main")]
    public GameObject Tracker_hand;
    [Tooltip("IN: KinectPointController de l'épaule")]
    public KinectPointController kpc_shoulder;
    [Tooltip("IN: Tracker qui récupère la position 3D de l'épaule")]
    public GameObject Tracker_shoulder;
    [Tooltip("IN: Matrice de rotation entre les données réelles et virtuelles")]
    public Matrix4x4 Real_to_RV = Matrix4x4.identity;
    [Tooltip("IN: Inverse les données suivant l'axe X")]
    public bool flip_x;
    [Tooltip("IN: Inverse les données suivant l'axe Z")]
    public bool flip_z;

    private Vector3 raw_ray_direction;
    private Vector3 rv_ray_direction;

    // Use this for initialization
    void Start()
    {
        /* A modifier suivant le positionnement de la kinect */
        flip_x = true;
        flip_z = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (kpc_hand.isTracked && kpc_shoulder.isTracked)
        {

            raw_ray_direction = Tracker_hand.transform.position - Tracker_shoulder.transform.position;
            rv_ray_direction = Real_to_RV * raw_ray_direction;
            if (flip_x)
                rv_ray_direction.x = -rv_ray_direction.x;

            transform.right = rv_ray_direction;
        }
    }
}
