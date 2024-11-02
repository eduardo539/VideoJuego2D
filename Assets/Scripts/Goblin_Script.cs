using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Script : MonoBehaviour
{

    [SerializeField] private float velocidad = 1f;
    [SerializeField] private Transform contradorSuelo;
    [SerializeField] private float distancia = 0.5f;
    private float stopDurationMin = 1f; // Tiempo mínimo de parada en segundos
    private float stopDurationMax = 4f; // Tiempo máximo de parada en segundos
    public LayerMask capaJugador;
    public Transform transformJugador;
    [SerializeField] private float radioBusqueda = 0.7f;
    //private bool estaAtacando = false;
    private bool movimientoDerecha;
    private bool puedeAtacar = true; // Controla si el goblin puede atacar
    

    private Ataque_Enemigo ataqueEnemigo; // Referencia al script de ataque
    private Rigidbody2D rb;
    private Animator animator; // Referencia al componente Animator
    private bool estaDetenido = false; // Indica si el personaje está detenido

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtener el componente Animator
        ataqueEnemigo = GetComponent<Ataque_Enemigo>(); // Referencia al script de ataque del goblin

        // Iniciar la corrutina que controla las detenciones aleatorias
        StartCoroutine(DetenerAleatoriamente());
    }

    private void Update()
    {
        DetectarJugador();
    }

    private void DetectarJugador()
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
        yield return new WaitForSeconds(1.5f); // Espera 1.5 segundos antes de poder atacar de nuevo
        puedeAtacar = true;
    }

    private void FixedUpdate()
    {

        if (transformJugador == null && !estaDetenido) // Solo se mueve si no está detenido y no persigue al jugador
        {
            RaycastHit2D informacionSuelo = Physics2D.Raycast(contradorSuelo.position, Vector2.down, distancia);

            rb.velocity = new Vector2(velocidad, rb.velocity.y);

            animator.SetBool("running", Mathf.Abs(rb.velocity.x) > 0);

            if (!informacionSuelo)
            {
                Girar();
            }
        }
        else if (estaDetenido)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("running", false); // Desactivar la animación de correr
        }
    }

    private void Girar()
    {
        movimientoDerecha = !movimientoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

    private IEnumerator DetenerAleatoriamente()
    {
        while (true) // Bucle infinito para detener aleatoriamente al personaje
        {
            if (transformJugador == null) // Solo detenerse si no está persiguiendo al jugador
            {
                yield return new WaitForSeconds(Random.Range(2f, 5f));

                estaDetenido = true;

                yield return new WaitForSeconds(Random.Range(stopDurationMin, stopDurationMax));

                estaDetenido = false;
            }
            else
            {
                // Si está persiguiendo al jugador, no se detiene
                yield return null; // Espera al siguiente frame
            }
        }
    }


    // Visualización de Raycast en el editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(contradorSuelo.position, contradorSuelo.position + Vector3.down * distancia);
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
    }
}
