using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab;
    private float launchForce = 10f;

    public void Shoot()
    {
        GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        arrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }
}
