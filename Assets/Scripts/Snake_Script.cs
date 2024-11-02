using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_Script : MonoBehaviour
{
    [SerializeField] private float speed = 1f; // Velocidad de movimiento de la serpiente
    [SerializeField] private Transform controlerSuelo;
    [SerializeField] private float distancia = 0.5f; // Distancia del Raycast para detectar colisiones con paredes


    public LayerMask capaJugador;
    public Transform transformJugador;
    [SerializeField] private float radioBusqueda = 0.7f;
    private bool moviendoDerecha; // Control para el movimiento
    private bool estaDetenido = false; // Indica si el personaje está detenido
    private bool puedeAtacar = true; // Controla si el goblin puede atacar


    private Ataque_Enemigo ataqueEnemigo; // Referencia al script de ataque
    private Rigidbody2D rb;
    private Animator animator; // Referencia al componente Animator

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtener el componente Animator
        ataqueEnemigo = GetComponent<Ataque_Enemigo>(); // Referencia al script de ataque del goblin
    }

    private void Update()
    {
        detectarJugador();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!estaDetenido)
        {
            RaycastHit2D informacionSuelo = Physics2D.Raycast(controlerSuelo.position, Vector2.down, distancia);

            // Actualizar la velocidad del personaje
            rb.velocity = new Vector2(speed, rb.velocity.y);

            // Cambiar el estado de la animación en función de la velocidad
            if (Mathf.Abs(rb.velocity.x) > 0)
            {
                animator.SetBool("running", true); // Activar la animación de correr si hay movimiento horizontal
            }
            else
            {
                animator.SetBool("running", false); // Desactivar la animación de correr si no hay movimiento horizontal
            }

            if (informacionSuelo == false)
            {
                Girar();
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("running", false);
        }
    }

    private void detectarJugador()
    {
        // Usamos un OverlapCircle para detectar colisiones dentro de un círculo
        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaJugador);

        if(jugadorCollider != null)
        {
            transformJugador = jugadorCollider.transform; // Guardamos la posición del jugador

            if(transformJugador.position.x > transform.position.x && moviendoDerecha)
            {
                Girar();
            }
            else if(transformJugador.position.x < transform.position.x && !moviendoDerecha)
            {
                Girar();
            }
            
            estaDetenido = true;
            // Iniciar ataque tras girar
            iniciarAtaque();

        }
        else
        {
            transformJugador = null; // Si no hay jugador, reseteamos la variable\
            estaDetenido = false;
        }
    }

    private void iniciarAtaque()
    {
        if(puedeAtacar)
        {
            animator.SetTrigger("attackTrigger");
            ataqueEnemigo.Golpe(); // Llama al método Golpe() del script de ataque
            StartCoroutine(EsperarParaAtacar()); // Inicia la espera para el próximo ataque
        }
    }

    private IEnumerator EsperarParaAtacar()
    {
        puedeAtacar = false;
        yield return new WaitForSeconds(1.5f); // Espera 1.5 segundos antes de poder atacar de nuevo
        puedeAtacar = true;
    }


    void Girar()
    {
        moviendoDerecha = !moviendoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        speed *= -1;
    }

    // Para visualizar el Raycast en el editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controlerSuelo.transform.position, controlerSuelo.transform.position + Vector3.down * distancia);
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
    }
}
