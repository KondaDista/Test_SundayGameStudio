using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadImageFullSceen : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadImageFromServer(PlayerPrefs.GetString("URLAddresImage")));
    }
    
    IEnumerator LoadImageFromServer(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success) 
        {
            Debug.Log(request.error + "\nAddress: " + url);
        }
        else 
        {
            Texture texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            GetComponent<RawImage>().texture = texture;
        }
        request.Dispose();
    }
}
