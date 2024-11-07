using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    private Animator animator;
    private Salida_Level1 salida1;
    private bool estaMuerto = false; // Para evitar que se ejecute varias veces la muerte

    private Drops_Aleatorios dropsAleatorios;

    private void Start()
    {
        animator = GetComponent<Animator>();
        salida1 = FindObjectOfType<Salida_Level1>();
        dropsAleatorios = GetComponent<Drops_Aleatorios>();
    }

    public void TomarDano(float dano)
    {
        if (estaMuerto) return; // No hacer nada si el enemigo ya está muerto

        vida -= dano;
        // Ejecutar la animación de golpe
        animator.SetTrigger("golpeTrigger");

        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        estaMuerto = true;
        animator.SetTrigger("muerteTrigger");
        salida1.EnemigoEliminado();
        StartCoroutine(DestruirDespuesDeTiempo());
    }

    // Corrutina para destruir el objeto después de un tiempo
    private IEnumerator DestruirDespuesDeTiempo()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        dropsAleatorios.SoltarDrop();  // Llama al método de drops
    }
}
