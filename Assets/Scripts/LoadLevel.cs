using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class LoadLevel : MonoBehaviour
{
    private static LoadLevel instance;
    private static bool _shouldPlayOpeningAnimation = false;

    public TextMeshProUGUI loadingPercentage;
    public Image loadingProgressBar;
    
    private AsyncOperation _loadingSceneOperation;

    [SerializeField] private static List<GameObject> _uiObjects = new List<GameObject>();
    public List<GameObject> UIObjects;
    private Tween _tween;

    private void Awake()
    {
        for (int i = 0; i < FindObjectsOfType<LoadLevel>().Length; i++)
        {
            if (FindObjectsOfType<LoadLevel>()[i] == this && FindObjectsOfType<LoadLevel>().Length >= 2)
                if (FindObjectsOfType<LoadLevel>()[i].name == name)
                    Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        instance = this;
        _uiObjects.AddRange(UIObjects);
    }

    /*public static void SwitchToScene(int sceneID)
    {
        ShowUILoader(true);
        instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneID);
        instance._loadingSceneOperation.allowSceneActivation = false;
    }*/
    
    public async static void SwitchToGalery(int sceneID)
    {
        ShowUILoader(true);
        await Task.Delay(TimeSpan.FromSeconds(0.4f));
        instance._loadingSceneOperation = SceneManager.LoadSceneAsync(sceneID, LoadSceneMode.Additive);
    }


    public static void SwitchToSceneBack()
    {
        if (SceneManager.GetActiveScene().buildIndex >= 1)
        {
            SwitchToGalery(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    private async static void ShowUILoader(bool isOpen)
    {
        switch (isOpen)
        {
            case true: 
                _uiObjects[0].GetComponent<Image>().DOFade(1, 0.35f);
                _uiObjects[0].GetComponent<Image>().raycastTarget = true;
                _uiObjects[1].GetComponent<Image>().DOFade(1, 0.35f);
                _uiObjects[2].GetComponent<TMP_Text>().DOFade(1, 0.35f);
                await Task.Delay(TimeSpan.FromSeconds(1.25f));
                break;
            case false: 
                await Task.Delay(TimeSpan.FromSeconds(1.25f));
                _uiObjects[0].GetComponent<Image>().DOFade(0, 0.25f);
                _uiObjects[0].GetComponent<Image>().raycastTarget = false;
                _uiObjects[1].GetComponent<Image>().DOFade(0, 0.25f);
                _uiObjects[2].GetComponent<TMP_Text>().DOFade(0, 0.25f);
                break;
        }
        
        _shouldPlayOpeningAnimation = true;
       // instance._loadingSceneOperation.allowSceneActivation = true;
    }

    private void Update()
    {
        if (_loadingSceneOperation != null)
        {
            loadingPercentage.text = "Loading: " + Mathf.RoundToInt(_loadingSceneOperation.progress * 100) + "%";
            loadingProgressBar.fillAmount = _loadingSceneOperation.progress;
            
            if (_shouldPlayOpeningAnimation && _loadingSceneOperation.progress == 1)
            {
                ShowUILoader(false);
                _loadingSceneOperation = null;
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SwitchToSceneBack();
            }
        }
    }
}