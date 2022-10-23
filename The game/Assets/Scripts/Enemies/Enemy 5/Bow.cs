using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab;

    public void CreateArrow()
    {
        Instantiate(arrowPrefab, transform.position, transform.rotation);
    }
}
