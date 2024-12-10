using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadController : MonoBehaviour
{
    private static readonly int ChangeScene = Animator.StringToHash("ChangeScene");
    
    [SerializeField] private string sceneToLoad = "";
    [SerializeField] private Animator fadeOutAnimator = null;

    private void Start()
    {
        if(fadeOutAnimator == null)
            fadeOutAnimator = GetComponent<Animator>();
    }

    public void LoadSceneWithAnim(string sceneToLoad)
    {
        this.sceneToLoad = sceneToLoad;
        if (fadeOutAnimator != null)
            fadeOutAnimator.SetBool(ChangeScene, true);
        else
            LoadSceneByName(sceneToLoad);
    }
    
    public static void LoadSceneByName(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void LoadSceneEvent()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
