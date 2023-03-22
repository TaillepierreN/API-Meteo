using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CallURL()
    {
        StartCoroutine(GetRequest("https://webhook.site/11805f45-e44d-4230-b0db-5eab8dda93f7?question=combien%20de%20giga&objet=forfait"));
    }
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
        }
    }
}
