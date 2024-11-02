using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Usar el UI para el Canvas

public class Barra_Vida : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        if (slider == null)
        {
            Debug.LogError("No se encontró un componente Slider en este objeto ni en sus hijos.");
        }
    }

    // Método para cambiar el valor máximo de vida
    public void CambiarVidaMaxima(float vidaMaxima)
    {
        if (vidaMaxima > 0) // Asegurarse de que la vida máxima sea positiva
        {
            slider.maxValue = vidaMaxima;
        }
        else
        {
            Debug.LogWarning("El valor máximo de vida debe ser mayor que 0.");
        }
    }

    // Método para actualizar la cantidad de vida actual
    public void CambiarVidaActual(float cantidadVida)
    {
        // Restringir los valores para que no se salgan del rango
        cantidadVida = Mathf.Clamp(cantidadVida, 0, slider.maxValue);
        slider.value = cantidadVida;
    }

    // Inicializar la barra de vida con la vida máxima y la vida actual
    public void IniciarBarraVida(float vida)
    {
        CambiarVidaMaxima(vida);
        CambiarVidaActual(vida);
    }
}
