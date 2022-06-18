using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float vertical;
    public float horizontal;
    public float speed = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical"); // W = 1 || S = -1
        horizontal = Input.GetAxis("Horizontal"); // A = -1 || D = 1
    }
    void FixedUpdate()
    {
        //movimentação para frente
        Vector3 desiredVelocity = transform.forward * vertical * speed; // velocidade final / desejad
        Vector3 changedVelocity = desiredVelocity - rb.velocity; // velocidade final - inicial
        changedVelocity.y = 0; // faz com que a gravidade afete o jogador
        
        //movimentação para os lados
        Vector3 desiredVelocityX = transform.right * horizontal * speed; // velocidade final / desejad
        Vector3 changedVelocityX = desiredVelocityX - rb.velocity; // velocidade final - inicial
        changedVelocityX.y = 0;

        rb.AddForce(changedVelocity, ForceMode.VelocityChange);
        rb.AddForce(changedVelocityX, ForceMode.VelocityChange);
    }
}
