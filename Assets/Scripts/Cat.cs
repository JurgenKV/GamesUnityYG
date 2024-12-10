using System;
using NUnit.Framework;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public int CatID;
    public bool WasFound = false;
    public Animator animator;
    private GameController _gameController;
    
    private void Start()
    {
        _gameController = GameObject.FindAnyObjectByType<GameController>();
    }
    
    
}
