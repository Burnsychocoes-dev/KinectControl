/*
 Trace d'une position 3D issue de la Kinect
 - En entrée, le tracker permettant de récupérer une position3D, Le nom du fichier de trace.
 Le format d'une ligne du fichier est <temps en secondes> <objet.x> <objet.y> <objet.z>
 - En sortie, le temps en secondes depuis le début de la trace
 */

using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Text;
using System;

public class TraceData : MonoBehaviour
{
    [Tooltip("KPC récupérant une position 3D issue de la Kinect.")]
    public KinectPointController kpc;
    [Tooltip("Nom du fichier de trace.")]
    public string trace_file = "Trace.txt";
    [Tooltip("Temps en secondes depuis le début de la trace.")]
    public double sec;

    private Stopwatch stopWatch;
    private Vector3 tracker_position;

    // Use this for initialization
    void Start()
    {
        stopWatch = new Stopwatch();
        stopWatch.Start();
        sec = 0.0;
        System.IO.File.WriteAllText(trace_file, "");
        tracker_position = kpc.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (kpc.isTracked == true) // Trace dans le fichier si l'objet est tracké par la Kinect
        {
            tracker_position = kpc.transform.position;
            sec = stopWatch.Elapsed.TotalSeconds;
            System.IO.File.AppendAllText(trace_file, sec.ToString() + " " + tracker_position.x.ToString() + " " + tracker_position.y.ToString() + " " + tracker_position.z.ToString() + "\r\n");
        }

    }

}
