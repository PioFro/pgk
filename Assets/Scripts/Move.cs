using UnityEngine;

public class Move : MonoBehaviour
{
    public int Speed = 3;

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0) * (Time.deltaTime * Speed);
        transform.position += movement;
    }


}
