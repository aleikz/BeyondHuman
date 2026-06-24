using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float time = 0.15f;

    void Start()
    {
        Destroy(gameObject, time);
    }
}