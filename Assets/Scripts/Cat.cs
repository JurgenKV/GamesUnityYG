using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    public int CatID = -1;
    public bool WasFound = false;
    public Animator animator;
    public Button _catButton;
    public bool IsHelpActive = false;
    private GameController _gameController;
    
    private static readonly int Found = Animator.StringToHash("Found");
    private static readonly int Help = Animator.StringToHash("Help");

    
    private void Start()
    {
        _gameController = GameObject.FindAnyObjectByType<GameController>();
    }

    public void OnClickCat()
    {
        IsHelpActive = false;
        _gameController.CatWasFound(CatID);
    }

    public void CheckCatState()
    {
        animator.SetBool(Found , WasFound);
        _catButton.interactable = !WasFound;
    }

    public void SetHelpTrigger()
    {
        IsHelpActive = true;
        animator.SetTrigger(Help);
    }
    
    
}
