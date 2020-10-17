using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour
{

    public int speed = 3;
    private Rigidbody2D rb;
    private Character character;
   // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //character = new Character();
        //avatars[0].GetComponentInChildren<Text>().text = "HP : "+character.currentHP + "/"+character.HP;

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, vertical,0)*(Time.deltaTime*speed);
        transform.position += movement;
    }
}
