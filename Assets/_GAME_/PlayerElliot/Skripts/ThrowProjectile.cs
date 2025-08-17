using UnityEngine;
using System.Collections;

public class ThrowProjectile : MonoBehaviour
{
    [Header("References")]
    public Animator animator;      // Animator mit Aufprallanimation

    [Header("Flight Settings")]
    public float flightDuration = 1f;  // Zeit für den Wurf
    public float arcHeight = 2f;       // maximale Höhe des Bogens

    private Vector2 startPos;
    private Vector2 endPos;
    private bool isFlying = false;

    public void Launch(Vector2 targetPos)
    {
        startPos = transform.position;
        endPos = targetPos;
        StartCoroutine(ThrowArc());
    }

    private IEnumerator ThrowArc()
    {
        FindObjectOfType<YSortManager>().Register(GetComponent<SpriteRenderer>());
        isFlying = true;
        float time = 0f;

        while (time < flightDuration)
        {
            float t = time / flightDuration;

            // 1. Lineare Bewegung von Start zu Ziel
            Vector2 pos = Vector2.Lerp(startPos, endPos, t);

            // 2. Parabelhöhe (Fake-Z)
            float height = arcHeight * 4 * (t - t * t);

            // 3. Projektil-Position anpassen
            transform.position = new Vector3(pos.x, pos.y + height, 0);

            time += Time.deltaTime;
            yield return null;
        }

        // Endposition setzen
        transform.position = new Vector3(endPos.x, endPos.y, 0);

        isFlying = false;

        

        // Aufprall-Animation triggern
        if (animator != null)
            animator.SetTrigger("Impact");
    }

    public void DestroyItSelf()
    {
        YSortManager manager = FindObjectOfType<YSortManager>();
        if (manager != null)
            manager.Deregister(GetComponent<SpriteRenderer>());



        Destroy(gameObject);
    }

}
