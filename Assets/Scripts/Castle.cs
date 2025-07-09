using UnityEngine;

public class Castle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); 
        }
    }
}
