using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
  [SerializeField] private float velocidad;
  [SerializeField] private Transform groundController;

   [SerializeField] private float distancia;
    [SerializeField] private bool moviendoDerecha;

private Rigidbody2D rb;
    private void Start() {

        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate(){
        RaycastHit2D informacionSuelo = Physics2D.Raycast(groundController.position, Vector2.down, distancia);

        rb.velocity = new Vector2(velocidad, rb.velocity.y);

        if(informacionSuelo == false){
            //Girar
            Girar();
        }
    }


    private void Girar(){
        moviendoDerecha = !moviendoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundController.transform.position, groundController.transform.position + Vector3.down * distancia);
    }
}