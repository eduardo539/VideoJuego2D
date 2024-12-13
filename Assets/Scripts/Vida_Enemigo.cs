using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vida_Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    private Animator animator;
    private Salida_Level1 salida1;
    private bool estaMuerto = false; // Para evitar que se ejecute varias veces la muerte

    private Drops_Aleatorios dropsAleatorios;


    public TextMeshPro vidaTexto; // Referencia al TextMeshPro 3D sobre la cabeza del enemigo


    private void Start()
    {
        animator = GetComponent<Animator>();
        salida1 = FindObjectOfType<Salida_Level1>();
        dropsAleatorios = GetComponent<Drops_Aleatorios>();
        ActualizarTextoVida();
    }


    private void Update()
    {
        MantenerTextoVisible(); // Actualiza la orientación del texto en cada frame
    }


    public void TomarDano(float dano)
    {
        if (estaMuerto) return; // No hacer nada si el enemigo ya está muerto

        vida -= dano;
        // Ejecutar la animación de golpe
        animator.SetTrigger("golpeTrigger");
        ActualizarTextoVida();

        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void ActualizarTextoVida()
    {
        if (vidaTexto != null)
        {
            if(vida > 0)
            {
                vidaTexto.text = "Vida: " + Mathf.Max(vida, 0); // Asegura que no se muestre negativo
            }
            else
            {
                vidaTexto.text = "Vida: " + + Mathf.Max(0, 0);
            }
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

    private void MantenerTextoVisible()
    {
        if (vidaTexto != null && Camera.main != null)
        {
            // Hacemos que el texto siempre mire hacia la cámara
            vidaTexto.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }
}
