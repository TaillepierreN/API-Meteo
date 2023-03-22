using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable] struct Parameter
{
    public string name;
    public string value;
}

public class APIManager : MonoBehaviour
{
    [SerializeField]string _endpoint;
    [SerializeField]List<Parameter> _parameters;
    string queryString;
    Color newColor;
    [SerializeField] MeshRenderer sphereMat;
    [SerializeField] Image button;
    // Start is called before the first frame update
    void Start()
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
        for (int i = 0; i < _parameters.Count; i++)
        {
            if (i == 0)
            {
                queryString += "?";
            } else 
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
            if(webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Il y a un problème de connexion");
            } else
            {
                HandleResponse(webRequest);
            }
        }
    }
    void HandleResponse(UnityWebRequest webRequest){
        Debug.Log(webRequest.downloadHandler.text);
        ColorUtility.TryParseHtmlString(webRequest.downloadHandler.text, out newColor);
        sphereMat.material.color = newColor;
        button.color = newColor;
    }
}
