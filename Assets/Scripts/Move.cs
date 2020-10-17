using UnityEngine;

public class Move : MonoBehaviour
{
    public int speed = 3;

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0) * (Time.deltaTime * speed);
        transform.position += movement;
    }
}
