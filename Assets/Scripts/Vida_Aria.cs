using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Aria : MonoBehaviour
{
    
    [SerializeField] private float vida;
    [SerializeField] private float maximaVida;
    private Barra_Vida barraVida; // Referencia a la barra de vida
    private Aria_Script aria; // Referencia al script del jugador
    private Animator animator; // Referencia al Animator del jugador

    void Start()
    {

        // Buscar y asignar la barra de vida desde el mismo objeto o desde el inspector
        barraVida = FindObjectOfType<Barra_Vida>();
        aria = GetComponent<Aria_Script>();
        animator = GetComponent<Animator>();

        BarraVida();
    }


    public void BarraVida()
    {
        vida = maximaVida;

        
        if (barraVida != null)
        {
            barraVida.IniciarBarraVida(vida);
        }
        else
        {
            Debug.LogError("No se encontró el componente Barra_Vida.");
        }
    }

    // Método que se llama cuando Aria recibe daño
    public void TomarDano(float dano)
    {
        vida -= dano; // Reducir la vida en la cantidad de daño recibida

        // Ejecutar la animación de golpe
        animator.SetTrigger("golpeTrigger");

        // Actualizar la barra de vida
        if (barraVida != null)
        {
            barraVida.CambiarVidaActual(vida);
        }

        // Verificar si la vida llegó a cero
        if (vida <= 0)
        {
            aria.Muerte();
        }
    }

    // Método sobrecargado para tomar daño con un punto de contacto (con rebote)
    public void TomarDano(float dano, Vector2 puntoDeContacto)
    {
        vida -= dano; // Reducir la vida

        // Actualizar la barra de vida
        if (barraVida != null)
        {
            barraVida.CambiarVidaActual(vida);
        }

        animator.SetTrigger("golpeTrigger");
        if (aria != null)
        {
            aria.Empujar(puntoDeContacto);
        }

        // Verificar si la vida llegó a cero
        if (vida <= 0)
        {
            // Lógica de muerte
            Destroy(gameObject);
        }
    }

    // Método para curar al jugador
    public void Curar(float cantidadCura)
    {
        // Verificamos si la vida del jugador ya está al máximo
        if (vida >= maximaVida)
        {
            Debug.Log("La vida ya está al máximo, no se puede recoger el corazón.");
            return; // Si la vida está al máximo, salimos del método sin curar
        }

        // Si la vida no está al máximo, curamos al jugador
        vida += cantidadCura; // Aumenta la vida del jugador
        vida = Mathf.Clamp(vida, 0, maximaVida); // Asegúrate de que no supere la vida máxima

        barraVida.CambiarVidaActual(vida); // Actualiza la barra de vida con la nueva cantidad de vida
    }

    // Método para verificar si la vida no está al máximo
    public bool VidaNoMaxima()
    {
        return vida < maximaVida; // Retorna true si la vida es menor que la máxima
    }
    
}