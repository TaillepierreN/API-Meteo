using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
struct Parameter
{
    public string name;
    public string value;
}

public class APIManager : MonoBehaviour
{
    [SerializeField] string _endpoint;
    [SerializeField] List<Parameter> _parameters;
    Color lerpedColor;
    Color newColor;
    [SerializeField] MeshRenderer sphereMat;
    [SerializeField] Image button;
    [SerializeField] Light lumiere;
    [SerializeField] ParticleSystem particle;
    [SerializeField] List<Material> skyboxes;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {

    }

    public void CallURL()
    {
        // foreach( Parameter param in _parameters){
        //     queryString += param.name + "=" + param.value + "&";
        // }
        // if(queryString.EndsWith('&'))
        // {
        //     queryString.Remove(queryString.Length-1);
        // }
        string queryString = "";

        for (int i = 0; i < _parameters.Count; i++)
        {
            if (i == 0)
            {
                queryString += "?";
            }
            else
            {
                queryString += "&";
            }
            queryString += _parameters[i].name + "=" + _parameters[i].value;

        }
        string requestURL = _endpoint + queryString;
        Debug.Log(requestURL);
        StartCoroutine(GetRequest(requestURL));
    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Il y a un problème de connexion");
            }
            else
            {
                HandleMeteoResponse(webRequest);
            }
        }
    }
    void HandleResponse(UnityWebRequest webRequest)
    {
        Debug.Log(webRequest.downloadHandler.text);
    }
    void HandleColorResponse(UnityWebRequest webRequest)
    {
        ColorUtility.TryParseHtmlString(webRequest.downloadHandler.text, out newColor);
        sphereMat.material.color = newColor;
        button.color = newColor;
    }
    void HandleMeteoResponse(UnityWebRequest webRequest)
    {
        var apiresponsedata = JsonUtility.FromJson<APIResponseData>(webRequest.downloadHandler.text);
        Debug.Log("la température est de : " + apiresponsedata.current_weather.temperature + "C");
        lerpedColor = Color.Lerp(Color.cyan, Color.red, (apiresponsedata.current_weather.temperature * 100 / 40) / 100);
        lumiere.color = lerpedColor;
        particle.startSpeed = apiresponsedata.current_weather.windspeed;
        RenderSettings.skybox = skyboxes[apiresponsedata.current_weather.weathercode];
    }
}
