using System.Collections;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public bool isStunned = false;
    public float stunTime = 1f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Stun()
    {
        StopAllCoroutines();
        StartCoroutine(StunRoutine());
    }

    IEnumerator StunRoutine()
    {
        isStunned = true;

        // freeze movement instantly
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            rb.simulated = false;
        }

        yield return new WaitForSeconds(stunTime);

        // unfreeze
        if (rb != null)
        {
            rb.simulated = true;
        }

        isStunned = false;
    }
}