using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampa_explosivo : MonoBehaviour
{
    public LayerMask capaJugador;
    public Transform transformJugador;
    [SerializeField] private float radioBusqueda;
    [SerializeField] private Transform controlador_Explosion;
    [SerializeField] private float radioExplosion;
    [SerializeField] private float danoExplosion;

    private Rigidbody2D rb;
    private Animator animator; // Referencia al componente Animator
    private bool haExplotado = false; // Para evitar que la explosión se repita

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Obtener el componente Animator
    }

    void Update()
    {
        if (!haExplotado) // Solo detectar jugador si no ha explotado aún
        {
            detectarJugador();
        }
    }

    private void detectarJugador()
    {
        // Usamos un OverlapCircle para detectar colisiones dentro de un círculo
        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaJugador);

        if (jugadorCollider != null)
        {
            transformJugador = jugadorCollider.transform; // Guardamos la posición del jugador

            // Inicia explosión si se detecta al jugador
            iniciarExplosion();
        }
        else
        {
            transformJugador = null; // Si no hay jugador, reseteamos la variable\
        }
    }

    private void iniciarExplosion()
    {
        if (!haExplotado) // Asegurarse de que la explosión ocurra solo una vez
        {
            haExplotado = true;
            animator.SetTrigger("explosionTrigger"); // Ejecutar animación de explosión
            StartCoroutine(ProcesarExplosion());
        }
    }

    private IEnumerator ProcesarExplosion()
    {
        yield return new WaitForSeconds(0.2f); // Esperar un pequeño tiempo para sincronizar con la animación

        // Buscar objetos en el radio de explosión
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controlador_Explosion.position, radioExplosion);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Player"))
            {
                colisionador.transform.GetComponent<Vida_Player>().TomarDano(danoExplosion); // Hacer daño al jugador
            }
        }

        yield return new WaitForSeconds(0.2f); // Esperar un tiempo adicional para que la animación termine

        Destroy(gameObject); // Destruir la trampa después de la explosión
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
        Gizmos.DrawWireSphere(controlador_Explosion.position, radioExplosion);
    }
}
