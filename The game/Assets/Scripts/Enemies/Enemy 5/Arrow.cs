using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float launchForce = 10f;
    public int damage = 5;
	private Rigidbody2D rb2D;

    private void Awake() => rb2D = GetComponent<Rigidbody2D>();

    private void Start() => rb2D.velocity = transform.right * launchForce;

    private void Update()
    {
        float angle = Mathf.Atan2(rb2D.velocity.y, rb2D.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
		if (trigger.gameObject.CompareTag("Player"))
		{
            trigger.GetComponent<IDamagable>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}