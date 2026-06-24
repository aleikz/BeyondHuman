
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform fireballPos;
    public GameObject fireball;

    public float cooldown = 1f;
    private float nextFireTime = 0f;
    public Rigidbody2D playerRb;
    public float dashForce = 10f;
    public Transform burstPoint;
    public float burstRadius = 2f;
    public float burstForce = 15f;
    public PlayerController controller;
    public GameObject flamedash;
    public GameObject flameburst;
    public float psychicRadius = 4f;
    public float pushForce = 15f;
    public float pullForce = 15f;
    public float stunTime = 1f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && Time.time >= nextFireTime)
        {
            if (AbilityManager.instance == null) return;

            int element = AbilityManager.instance.selectedElement;

            if (element == 1)
            {
                ShootFireball();
            }
            else if (element == 2)
            {
                PsychicPush();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            int element = AbilityManager.instance.selectedElement;

            if (element == 1)
            {
                StartCoroutine(Dash());
            }
            else if (element == 2)
            {
                PsychicPull();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            int element = AbilityManager.instance.selectedElement;

            if (element == 1)
            {
                FlameBurst();
            }
            else if (element == 2)
            {
                PsychicStun();
            }
        }
    }

    void ShootFireball()
    {
        Instantiate(fireball, fireballPos.position, fireballPos.rotation);
    }
    void PsychicPush()
    {
        Debug.Log("Push used");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, psychicRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Rigidbody2D rb = hit.attachedRigidbody;

                if (rb != null)
                {
                    Vector2 dir = (hit.transform.position - transform.position).normalized;
                    rb.AddForce(dir * pushForce, ForceMode2D.Impulse);
                }
            }
        }
    }
    void PsychicPull()
    {
        Debug.Log("Pull used");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, psychicRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Rigidbody2D rb = hit.attachedRigidbody;

                if (rb != null)
                {
                    Vector2 dir = (transform.position - hit.transform.position).normalized;
                    rb.AddForce(dir * pullForce, ForceMode2D.Impulse);
                }
            }
        }
    }
    void PsychicStun()
    {
        Debug.Log("Stun used");

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, psychicRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                EnemyStatus status = hit.GetComponent<EnemyStatus>();

                if (status != null)
                {
                    status.Stun();
                }
            }
        }
    }
    void FlameBurst()
    {
        Instantiate(flameburst, transform.position + transform.right * 0.5f, Quaternion.identity);

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            burstPoint.position,
            burstRadius
        );

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Rigidbody2D enemyRb = hit.GetComponent<Rigidbody2D>();

                if (enemyRb != null)
                {
                    Vector2 direction =
                        (hit.transform.position - transform.position).normalized;

                    enemyRb.AddForce(
                        direction * burstForce,
                        ForceMode2D.Impulse
                    );
                }
            }
        }
    }
    System.Collections.IEnumerator Dash()
    {
        controller.isDashing = true;

        Instantiate(flamedash, transform.position + Vector3.up * 0.2f, transform.rotation);

        playerRb.linearVelocity = new Vector2(15f, playerRb.linearVelocity.y);

        yield return new WaitForSeconds(0.2f);

        controller.isDashing = false;
        Debug.Log("Dash VFX spawned");
    }
    void OnDrawGizmosSelected()
    {
        if (burstPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(burstPoint.position, burstRadius);
    }
}
