using UnityEngine;

public class PawRandomRotate : MonoBehaviour
{
    [SerializeField] private int minRotation = -30;
    [SerializeField] private int maxRotation = 30;
    void Start()
    {
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Random.Range(minRotation, maxRotation));
    }

}
