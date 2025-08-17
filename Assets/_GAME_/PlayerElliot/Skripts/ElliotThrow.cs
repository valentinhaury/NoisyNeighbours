using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    public GameObject projectilePrefab;   // Dein Wurfobjekt-Prefab
    public float maxThrowDistance = 3f;

    public Animator animator;       // Animator des Spielers
    public PlayerMovement movement; // Dein Movement-Skript, um Bewegung zu blockieren

    private Vector2 throwTarget;

    private bool isThrowing = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isThrowing)
        {
            PrepareThrow();
        }
    }

    void PrepareThrow()
    {
        isThrowing = true;

        // Mausposition in Weltkoordinaten holen
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        // Richtung vom Spieler zur Maus
        Vector2 startPos = transform.position;
        Vector2 dir = (mouseWorld - transform.position);

        if (dir.magnitude > maxThrowDistance)
            dir = dir.normalized * maxThrowDistance;

        throwTarget = startPos + dir;

        // Bewegung stoppen
        if (movement != null)
            movement.enabled = false;

        // Wurf-Animation abspielen
        if (animator != null)
            animator.SetTrigger("Throw");
    }

    public void ReleaseThrow()
    {
        // Projektil instanziieren
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        ThrowProjectile throwScript = proj.GetComponent<ThrowProjectile>();
        if (throwScript != null)
            throwScript.Launch(throwTarget);

        // Bewegung wieder freigeben
        if (movement != null)
            movement.enabled = true;

        isThrowing = false;
    }

    public void EndThrow()
    {
        if (animator != null)
            animator.SetTrigger("Idle");
    }

}
