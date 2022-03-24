using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveSkybox : MonoBehaviour
{
    int targetIndex = 0;

    public int samplePower;

    public Material SkyboxMat;

    private Boid[] boids;

    private Boid boi;

    private float R;
    private float G;
    private float B;

    private void Start()
    {
        Invoke("ChangeTarget", 0.2f);
    }
    // Update is called once per frame
    void Update()
    {
        float[] spectrum = new float[(int)Mathf.Pow(2f, samplePower)];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        float totalpitch = 0f;

        float averagepitch;

        for (int i = 0; i < spectrum.Length; i++)
        {
            totalpitch += spectrum[i];
        }
        averagepitch = totalpitch / spectrum.Length;

        SkyboxMat.SetFloat("_WireThickness", 800 * (averagepitch) / (averagepitch + 0.001f));

        if (boi != null)
        {
            SyncColors();
        }
        RenderSettings.skybox = SkyboxMat;
    }
    void SyncColors()
    {
        int checkprecision = 100;

        bool RInRange = false;
        bool GInRange = false;
        bool BInRange = false;

        float dt = Time.deltaTime;

        R = R < boi.BoidColor.x ? R + dt / 10f : R - dt / 10f;
        G = G < boi.BoidColor.y ? G + dt / 10f : G - dt / 10f;
        B = B < boi.BoidColor.z ? B + dt / 10f : B - dt / 10f;

        SkyboxMat.SetColor("_WireColor", new Color(R, G, B));

        Vector3 ColorHolder = new Vector3(R, G, B);

        if ((int)(ColorHolder.x * checkprecision) == (int)(boi.BoidColor.x * checkprecision))
        {
            RInRange = true;
        }
        if ((int)(ColorHolder.y * checkprecision) == (int)(boi.BoidColor.y * checkprecision))
        {
            GInRange = true;
        }
        if ((int)(ColorHolder.z * checkprecision) == (int)(boi.BoidColor.z * checkprecision))
        {
            BInRange = true;
        }

        if (RInRange && BInRange && GInRange)
        {
            Debug.Log("TargetChange");
            ChangeTarget();
        }
    }
    void ChangeTarget()
    {
        targetIndex++;
        boids = GameObject.FindObjectsOfType<Boid>();
        boi = boids[targetIndex % boids.Length];
    }
}


