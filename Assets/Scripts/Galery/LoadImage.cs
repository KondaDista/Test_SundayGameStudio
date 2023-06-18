using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Update = UnityEngine.PlayerLoop.Update;

public class LoadImage : MonoBehaviour
{
    [SerializeField] private RawImage imagePrefab;
    [SerializeField] private List<RawImage> _images;
    [SerializeField] float _nextPos;
    private string urlAddres = "http://data.ikppbb.com/test-task-unity-data/pics/";

    private void Start()
    {
        for (int i = 0; i < 12; i++)
        {
            CreateStartImage();
        }
    }

    private void Update()
    {
        if (transform.localPosition.y >= _nextPos && _images.Count <= 64)
        {
            //Debug.Log("localPosition: " + transform.localPosition.y);
            _nextPos +=_images.Count + 500;
            CreateStartImage();
            CreateStartImage();
        }
    }

    void CreateStartImage()
    {
        RawImage currentImage = Instantiate(imagePrefab, transform);
        _images.Add(currentImage);

        currentImage.name = $"{_images.Count}.jpg";
        string currentURLAddress = urlAddres + $"{_images.Count}.jpg";

        StartCoroutine(LoadImageFromServer(currentURLAddress, _images.Count));
    }

    IEnumerator LoadImageFromServer(string url, int numberImage)
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
            _images[numberImage - 1].texture = texture;
        }
        request.Dispose();
    }

}
