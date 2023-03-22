using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeatherData
{
    public float temperature;
    public float windspeed;
    public float winddirection;
    public int weathercode;

}
[System.Serializable]
public class APIResponseData
{
    public WeatherData current_weather;
}
