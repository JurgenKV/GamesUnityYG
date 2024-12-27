using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public enum ColorType
{
    Yellow,
    Red,
    Green,
    Blue
}

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject crosshair; 
    [SerializeField] private TMP_Text helpText;
    public List<GameObject> bulletPrefabs; 
    public Transform spawnPoint; 
    public float bulletSpeed = 10f;

    private InputSystem_Actions controls; 
    private GameObject bullet;

    private GameController _gameController;

    private Vector3 lastCrosshairPosition; 
    private bool _firstShoot = true;
    private AudioSource _audioSource;
    void Awake()
    {

        controls = new InputSystem_Actions();


        controls.Player.Attack.performed += ctx => Attack();
    }

    void OnEnable()
    {

        controls.Enable();
    }

    void OnDisable()
    {
       
        controls.Disable();
    }

    private void Start()
    {
        _gameController = FindAnyObjectByType<GameController>();
        _audioSource = GetComponent<AudioSource>();
        CreateBullet(); 
    }

    void Attack()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return; 
        
        if (_gameController.IsGamePaused || !_gameController.IsGameRunning || bullet == null)
            return;
        
        lastCrosshairPosition = crosshair.transform.position;
        _audioSource.Play();
        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
           
            Vector2 direction = (lastCrosshairPosition - spawnPoint.position).normalized;
            Debug.Log(direction);
            CheckFirstShoot();
            rb.AddForce(direction * bulletSpeed, ForceMode2D.Force); 
        }

        bullet = null; 
    }

    public void CreateBullet()
    {
        bullet = Instantiate(bulletPrefabs[Random.Range(0, bulletPrefabs.Count)], spawnPoint.position, spawnPoint.rotation);

        bullet.GetComponent<Bullet>().CurrentPlayer = this;
    }

    private void CheckFirstShoot()
    {
        if(!_firstShoot)
            return;
        _firstShoot = false;
        StartCoroutine(FadeOutCor());
    }

    private IEnumerator FadeOutCor()
    {
        while (helpText.color.a > 0)
        {

            helpText.color = new Color(helpText.color.r, helpText.color.g, helpText.color.b, Mathf.Max(helpText.color.a - 1f * Time.deltaTime, 0f));

            yield return null; 
        }
        helpText.gameObject.SetActive(false);
    }
}
