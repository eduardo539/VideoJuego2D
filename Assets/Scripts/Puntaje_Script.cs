using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntaje_Script : MonoBehaviour
{
    private float puntos;
    private TextMeshProUGUI textMesh;

    void Start()
    {
        // Cargar el puntaje guardado al inicio
        puntos = PlayerPrefs.GetFloat("PuntajeGuardado", 0f);

        textMesh = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Actualiza el texto con el puntaje actual
        textMesh.text = puntos.ToString("0");
    }

    public void SumarPuntos(float puntosEntrada)
    {
        puntos += puntosEntrada;

        // Guardar el puntaje después de sumarlo
        PlayerPrefs.SetFloat("PuntajeGuardado", puntos);
        PlayerPrefs.Save(); // Asegura que se guarde inmediatamente
    }
}
