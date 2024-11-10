using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Puntaje_Victoria : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    private float puntajeTemp;

    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        MostrarPuntosText();
    }

    void Update()
    {
        MostrarPuntosText();
    }

    public void MostrarPuntosText()
    {
        //textMesh.text = "Puntos Temporales: " + puntajeScript.PuntosTemporales.ToString("0");
        textMesh.text = puntajeTemp.ToString("0");
    }

    public void recibirPuntos(float puntos)
    {
        puntajeTemp += puntos;
    }


    
}
