using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingingBoid : MonoBehaviour
{
    public AudioSource boidsource;

    public Rigidbody boidbody;

    private Spawner spawnerRef;

    private Attractor attractorRef;

    private void Start()
    {
        spawnerRef = GameObject.FindObjectOfType<Spawner>();
        attractorRef = GameObject.FindObjectOfType<Attractor>();
    }
    void Update()
    {
        float pitchCalc = (Mathf.Sin((boidbody.transform.position - attractorRef.transform.position).magnitude / Mathf.Pow(spawnerRef.collDist, 3)) + 1);
        boidsource.pitch = pitchCalc / (pitchCalc - 0.01f);
    }
    public void Activate()
    {
        boidsource.Play();
    }
}
