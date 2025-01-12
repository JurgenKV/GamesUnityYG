using UnityEngine;

public class Witch : MonoBehaviour
{
    private static readonly int StartWitch1 = Animator.StringToHash("StartWitch");
    private static readonly int HealthWitch1 = Animator.StringToHash("HealthWitch");
    private static readonly int DamageWitch1 = Animator.StringToHash("DamageWitch");
    private static readonly int CommonWitch = Animator.StringToHash("CommonWitch");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private Animator _animator;
    [SerializeField] private GameController gameController;
    [SerializeField] private AudioSource startSound;
    [SerializeField] private AudioSource damageSound;
    [SerializeField] private AudioSource healthSound;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat(Speed, 1f * gameController.SpeedMultiplier);
    }

    public void StartWitch()
    {
        _animator.SetTrigger(StartWitch1);
    }

    public void HealthWitch()
    {
        _animator.SetTrigger(HealthWitch1);
    }

    public void DamageWitch()
    {
        _animator.SetTrigger(DamageWitch1);
    }
    
    public void PlayCommonWitch()
    {
        _animator.SetTrigger(CommonWitch);
    }

    public void PlayStartSound()
    {
        startSound.Play();
    }

    public void PlayDamageSound()
    {
        damageSound.Play();
    }

    public void PlayHealthSound()
    {
        healthSound.Play();
    }

    
}
