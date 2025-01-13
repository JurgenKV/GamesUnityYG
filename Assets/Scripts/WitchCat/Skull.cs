using System;
using System.Collections;
using UnityEngine;

public class Skull : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float fadeDuration = 2f;
    [HideInInspector] public GameController GameController;
    [HideInInspector] public Vector2 moveDirection;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AudioSource audioSource;
    void Start()
    {
        Invoke(nameof(DeleteObject), 10);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameController.IsGamePaused || !GameController.IsGameRunning)
            return;
        
        transform.Translate(moveDirection * Time.fixedDeltaTime * speed, Space.World);
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        
        if(player ==null)
            return;
        gameObject.GetComponent<Collider2D>().enabled = false;
        if (GameController.CurrentHealth > 0)
            GameController.CurrentHealth -= 1;
        particle.Play();
        audioSource.Play();
        GameController.witch.DamageWitch();
        StartCoroutine(FadeCoroutine(false));
        
    }
    
    private IEnumerator FadeCoroutine(bool fadeIn)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        
        float startAlpha = spriteRenderer.color.a;
        float targetAlpha = fadeIn ? 1f : 0f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);

            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);

            yield return null;
        }
        
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, targetAlpha);
    }

    private void DeleteObject()
    {
        if (GameController.IsGamePaused)
        {
            Invoke(nameof(DeleteObject), 10);
            return;
        }
        
        try
        {
            Destroy(gameObject);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}