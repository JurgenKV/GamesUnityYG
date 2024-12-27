using PointShoot;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ColorType ColorType;
    public GameObject ParticleSystem;
    public Player CurrentPlayer;

    private bool IsDeleted = false;
    
    public void Delete(bool enableAnim = false)
    {
        if(IsDeleted)
            return;
        
        IsDeleted = true;
        GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        CurrentPlayer.CreateBullet();
        //ParticleSystem.Play();
        if(enableAnim)
            Destroy(Instantiate(ParticleSystem, transform.position, Quaternion.identity), 5);
        Destroy(gameObject, 1);
    }
    
}
