using UnityEngine;

public class Movement : MonoBehaviour
{
    // Hacemos referencia al componente Rigidbody 2D
    private Rigidbody2D rb2D;

    // Variables de movimiento
    [Header("Movement")]
    // Velocidad del personaje
    [SerializeField] private float velocidadDeMovimiento = 5f;
    // Suavizado de movimiento
    [Range(0, 0.3f)][SerializeField] private float suavizadoDeMovimiento = 0.05f;
    // Iniciamos la velocidad del eje z en cero por que no nos interesa movernos en ese eje
    private Vector3 velocidad = Vector3.zero;
    // Nos aseguramos de que el personaje inicie mirando al lado que queremos
    private bool mirandoDerecha = true;




[Header("Jump")]
//Fuerza con la que va a saltar el personaje
[SerializeField] private float fuerzaDeSalto;

// Detectar las superficies aptas para que el jugador salte
[SerializeField] private LayerMask queEsSuelo;


[SerializeField] private Transform controladorSuelo;

[SerializeField] private Vector3 dimensionesCaja;

[SerializeField] private bool enSuelo;

private bool salto = false;


[Header ("Animation")]
private Animator animator;




    // Iniciamos con la función Start que se ejecuta solo UNA vez al inicio
    private void Start()
    {
        // Seleccionamos el componente Rigidbody 2D que tiene el Player
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // La función Update se actualiza en cada ciclo una vez por frame
    private void Update()
    {
        // Tomamos la dirección del control. Izquierda (-1) derecha(1)
        float movimientoHorizontal = Input.GetAxisRaw("Horizontal");
        ////////////////////////
        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));

animator.SetFloat("VelocidadY", rb2D.velocity.y);
        ////////////////////////
        // Llamamos a la función Mover para actualizar el movimiento del personaje
        Mover(movimientoHorizontal * velocidadDeMovimiento * Time.fixedDeltaTime, salto);

        //Manejamos el salto
        if(Input.GetButtonDown("Jump")){
            salto = true;
        }
    }

    // La función FixedUpdate se ejecuta también por ciclos y se suele usar para controlar grupos de acciones y físicas.
    private void FixedUpdate()
    {
//Llamaremos a esta funcion para decir al juego cuando estamos en el suelo
enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);

//////////////
animator.SetBool("enSuelo", enSuelo);


        // La función Mover se llama en la función FixedUpdate para actualizar la física del movimiento del personaje
        // La velocidad que tenemos - la que tiene el Rigidbody (no altera la velocidad al caer o saltar)
        Vector3 velocidadObjetivo = new Vector2(rb2D.velocity.x, rb2D.velocity.y);
        // La función SmoothDamp nos permite hacer que haya un suavizado al acelerar o frenar el personaje
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);
    
    salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        // Establecemos la velocidad de movimiento del personaje
        Vector3 velocidadDeMovimiento = new Vector3(mover, rb2D.velocity.y, 0);
        // Actualizamos la velocidad del Rigidbody del personaje con la velocidad de movimiento y el suavizado
rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadDeMovimiento, ref velocidad, suavizadoDeMovimiento);
        if (mover > 0 && !mirandoDerecha)
        {
            // Girar
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            // Girar
            Girar();
        }

        if(enSuelo && saltar){
            enSuelo = false;
            rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));
        }
    }

   private void Girar()
{
    // Invertimos el valor de la variable mirandoDerecha para indicar la nueva dirección de mirada
    mirandoDerecha = !mirandoDerecha;
    // Invertimos la escala en el eje X para dar el efecto de giro
    Vector3 escala = transform.localScale;
    escala.x *= -1;
    transform.localScale = escala;
}


//OnDrawGizmos se usa para dibujar cajas que normalmente serian invisibles ingame
private void OnDrawGizmos() {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
}

}
