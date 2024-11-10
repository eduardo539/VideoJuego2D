using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class Tabla_Puntajes : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Asegúrate de asignar este campo en el inspector

    private string filePath; // Ruta donde se almacenan los registros JSON

    void Start()
    {
        filePath = Application.persistentDataPath + "/records.json"; // Ruta del archivo de registros
        MostrarPuntajes(); // Llamamos al método para mostrar los puntajes al iniciar
    }

    public void Salir()
    {
        SceneManager.LoadScene("Menu_Principal");
    }

    // Método para cargar y mostrar los puntajes
    private void MostrarPuntajes()
    {
        // Comprobamos si el archivo existe
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath); // Leemos el contenido del archivo
            RecordList loadedData = JsonUtility.FromJson<RecordList>(json); // Deserializamos el JSON a la lista de registros

            // Ordenamos los registros de mayor a menor según los puntos
            loadedData.records.Sort((x, y) => y.puntos.CompareTo(x.puntos)); // Comparador descendente

            // Creamos una cadena para mostrar en TextMeshPro
            string puntajesTexto = "";

            // Concatenamos los registros en un formato de fila por línea
            for (int i = 0; i < loadedData.records.Count; i++)
            {
                Record record = loadedData.records[i];
                puntajesTexto += (i + 1) + " ----- " + record.nombre + " ----- " + record.puntos + "\n"; // Formato de fila
            }

            // Asignamos el texto concatenado al TextMeshPro
            textMeshPro.text = puntajesTexto;
        }
        else
        {
            textMeshPro.text = "No se encontraron registros.";
        }
    }

    // Clases de serialización para los registros
    [System.Serializable]
    private class Record
    {
        public string nombre;
        public float puntos;
    }

    [System.Serializable]
    private class RecordList
    {
        public List<Record> records;
    }
}
