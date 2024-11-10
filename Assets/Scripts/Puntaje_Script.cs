using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntaje_Script : MonoBehaviour
{

    private float puntos;              // Puntaje total guardado
    private float puntosTemporales;    // Puntaje temporal del nivel actual
    private TextMeshProUGUI textMesh;

    private Puntaje_Victoria puntajeVic;

    private Manager_Records recordsDatos;

    void Start()
    {
        // Cargar el puntaje guardado al inicio
        puntos = PlayerPrefs.GetFloat("PuntajeGuardado", 0f);

        puntajeVic = FindObjectOfType<Puntaje_Victoria>();
        recordsDatos = FindObjectOfType<Manager_Records>();
        textMesh = GetComponent<TextMeshProUGUI>();
        ActualizarTextoPuntos();
    }

    void Update()
    {
        // Actualiza el texto con el puntaje total más el temporal
        ActualizarTextoPuntos();
    }

    public void SumarPuntos(float puntosEntrada)
    {
        // Añadir puntos a los temporales
        puntosTemporales += puntosEntrada;
        ActualizarTextoPuntos();
    }

    // Método para guardar los puntos temporales en el puntaje total
    public void GuardarPuntosAlCompletarNivel()
    {
        puntos += puntosTemporales;
        puntosTemporales = 0f; // Reiniciar los puntos temporales
        PlayerPrefs.SetFloat("PuntajeGuardado", puntos);
        PlayerPrefs.Save(); // Asegura que se guarde inmediatamente
    }

    // Método para resetear los puntos temporales al morir o salir del nivel
    public void ResetearPuntosTemporales()
    {
        puntosTemporales = 0f;
        ActualizarTextoPuntos();
    }

    private void ActualizarTextoPuntos()
    {
        // Muestra el total de puntos (puntos guardados + puntos temporales)
        textMesh.text = (puntos + puntosTemporales).ToString("0");
    }


    public void ReiniciarPuntaje()
    {
        puntos = 0f;
        puntosTemporales = 0f;
        PlayerPrefs.SetFloat("PuntajeGuardado", 0f); // Reiniciar el puntaje en PlayerPrefs a cero
        PlayerPrefs.Save(); // Asegura que se guarde inmediatamente
        ActualizarTextoPuntos();
    }

    public void enviarPuntos()
    {
        puntajeVic.recibirPuntos(puntosTemporales);
    }

    public void enviarDatosyGuardar()
    {
        recordsDatos.DatosTablaPuntos(puntos);
    }



}







/*
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

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
*/