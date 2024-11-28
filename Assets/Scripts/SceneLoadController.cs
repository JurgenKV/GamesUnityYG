using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadController : MonoBehaviour
{
    private static readonly int FadeIn = Animator.StringToHash("FadeIn");
    
    [SerializeField] private string sceneToLoad = "";
    [SerializeField] private Animator fadeOutAnimator = null;

    private void Start()
    {
        fadeOutAnimator = GetComponent<Animator>();
    }

    public void LoadSceneWithAnim(string sceneToLoad)
    {
        this.sceneToLoad = sceneToLoad;
        fadeOutAnimator.SetTrigger(FadeIn);
    }

    public void LoadSceneEvent()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
    
    private void LoadSceneEventPrivate()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
