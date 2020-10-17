using UnityEngine;

public class Fog : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Foggable"))
        {
            Debug.Log(collision.gameObject.name);
            Destroy(collision.gameObject);
        }
    }
}
