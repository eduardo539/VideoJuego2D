using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groot_Script : MonoBehaviour
{
    [SerializeField] private float velocidad = 1f;
    [SerializeField] private Transform contradorSuelo;
    [SerializeField] private float distancia = 0.6f;
    private bool movimientoDerecha;


    public LayerMask capaJugador;
    public Transform transformJugador;
    [SerializeField] private float radioBusqueda = 0.7f;
    private bool estaDetenido = false; // Indica si el personaje está detenido
    private bool puedeAtacar = true; // Controla si el goblin puede atacar

    

    private Ataque_Enemigo ataqueEnemigo; // Referencia al script de ataque
    private Rigidbody2D rb;
    private Animator animator; // Referencia al componente Animator

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtener el componente Animator
        ataqueEnemigo = GetComponent<Ataque_Enemigo>(); // Referencia al script de ataque del goblin
    }

    private void Update()
    {
        detectarJugador();
    }

    private void FixedUpdate()
    {
        if(!estaDetenido)
        {
            RaycastHit2D informacionSuelo = Physics2D.Raycast(contradorSuelo.position, Vector2.down, distancia);

            // Actualizar la velocidad del personaje
            rb.velocity = new Vector2(velocidad, rb.velocity.y);

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

            if(transformJugador.position.x > transform.position.x && movimientoDerecha)
            {
                Girar();
            }
            else if(transformJugador.position.x < transform.position.x && !movimientoDerecha)
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
        yield return new WaitForSeconds(2f); // Espera 2 segundos antes de poder atacar de nuevo
        puedeAtacar = true;
    }

    private void Girar()
    {
        movimientoDerecha = !movimientoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(contradorSuelo.transform.position, contradorSuelo.transform.position + Vector3.down * distancia);
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
    }
}
