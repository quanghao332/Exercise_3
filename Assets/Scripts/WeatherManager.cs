using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [Serializable]
    public class Season
    {
        public float duration;
        public Color lightColor;
        public float intensity;
        public ParticleSystem particles;
    }

    [Header("Weather Settings")]

    public Season[] seasons;
    public int currentYear = 0;
    public Light sunLight;

    public Season currentSeason;
    public int currentSeasonIndex = 1;
    public float seasonTime;


    void Start()
    {
        currentSeason = seasons[currentSeasonIndex];
        seasonTime = currentSeason.duration;
    }

    void Update()
    {
        seasonTime -= Time.deltaTime;
        if (seasonTime <= 0.0f)
        {
            currentSeasonIndex++;
            if (currentSeasonIndex >= seasons.Length)
            {
                currentSeasonIndex = 0;
                currentYear++;
            }

            if (currentSeason.particles != null)
            {
                currentSeason.particles.Stop();
            }

            currentSeason = seasons[currentSeasonIndex];
            seasonTime = currentSeason.duration;

            if (currentSeason.particles != null)
            {
                currentSeason.particles.Play();
            }

        }
        LerpLight(sunLight, currentSeason.lightColor, currentSeason.intensity);
    }

    private void LerpLight(Light light, Color c, float intensity)
    {
        float t = Time.deltaTime * 0.2f;
        light.color = Color.Lerp(light.color, c, t);
        light.intensity = Mathf.Lerp(light.intensity, intensity, t);
    }
}
