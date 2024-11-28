using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class InitPlugin : MonoBehaviour
{
    [SerializeField] private string sceneName = "LanguageScene";

    private void OnEnable() => YG2.onGetSDKData += OnGetData;

    private void OnGetData()
    {
        SceneManager.LoadScene(sceneName);
    }
}
