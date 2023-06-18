using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLoadLevel : MonoBehaviour
{ 
    [SerializeField] private int _sceneID;
    private string _urlAddresImage;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        LoadLevel.SwitchToGalery(_sceneID);
    }
    
    public void OnClickToImage()
    {
        _urlAddresImage = $"http://data.ikppbb.com/test-task-unity-data/pics/{name}";
        PlayerPrefs.SetString("URLAddresImage", _urlAddresImage);
    }
}
