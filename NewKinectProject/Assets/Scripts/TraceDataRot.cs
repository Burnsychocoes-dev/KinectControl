/*
 Trace les angles d'Euler issus de la Kinectassociés à une partie du corps 
 - En entrée, le tracker permettant de récupérer une position3D, Le nom du fichier de trace.
 Le format d'une ligne du fichier est <temps en secondes> <objet.euler_x> <objet.euler_y> <objet.euler_z>
 - En sortie, le temps en secondes depuis le début de la trace
 */

using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System;

public class TraceDataRot : MonoBehaviour
{
    [Tooltip("KMC récupérant une orientation 3D issue de la Kinect.")]
    public KinectModelControllerV2 tracker;
    [Tooltip("Nom du fichier de trace.")]
    public string trace_file = "TraceRot.txt";
    [Tooltip("Temps en secondes depuis le début de la trace.")]
    public double sec;

    private Stopwatch stopWatch;
    private Vector3 tracker_orientation;

    // Use this for initialization
    void Start()
    {
        stopWatch = new Stopwatch();
        stopWatch.Start();
        sec = 0.0;
        System.IO.File.WriteAllText(trace_file, "");
        tracker_orientation = tracker.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (tracker.isTracked == true ) // Trace dans le fichier si l'objet est tracké par la Kinect
        {
            tracker_orientation = tracker.transform.rotation.eulerAngles;
            sec = stopWatch.Elapsed.TotalSeconds;
            System.IO.File.AppendAllText(trace_file, sec.ToString() + " " + tracker_orientation.x.ToString() + " " + tracker_orientation.y.ToString() + " " + tracker_orientation.z.ToString() + "\r\n");
        }

    }

}
