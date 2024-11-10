using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input_Manager : MonoBehaviour
{
    private string inputTexto;


    private Manager_Records records;

    
    void Start()
    {
        records = FindObjectOfType<Manager_Records>();
    }

    // Método para obtener el texto ingresado
    public void LeerTexto( string texto)
    {
        inputTexto = texto;
        records.DatosTablaNombre(inputTexto);
        Debug.Log("Texto ingresado: " + inputTexto);
        
    }

    
    
}
