using UnityEngine;

public class BreakableBarrel : MonoBehaviour
{
    public float breakForce = 5f;
    public float upwardForce = 2f;

    private bool isBroken = false;

    void OnCollisionEnter(Collision collision)
    {
        if (isBroken) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Break();
        }
    }

    void Break()
    {
        isBroken = true;

        foreach (Transform plank in transform)
        {
            Rigidbody rb = plank.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;

                Vector3 forceDir = (plank.position - transform.position).normalized;
                rb.AddForce(forceDir * breakForce + Vector3.up * upwardForce, ForceMode.Impulse);
            }
        }

        // Optional: disable barrel collider
        GetComponent<Collider>().enabled = false;

        // Optional: destroy parent after planks fall
        Destroy(gameObject, 5f);
    }
}
