using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Manager_Records : MonoBehaviour
{
    private string nombreGuardado;
    private float puntosGuardados;
    private List<Record> records = new List<Record>(); // Lista para almacenar los registros

    private string filePath; // Ruta del archivo donde se guardarán los registros

    // Clase para representar un registro con nombre y puntos
    [System.Serializable]
    private class Record
    {
        public string nombre;
        public float puntos;

        public Record(string nombre, float puntos)
        {
            this.nombre = nombre;
            this.puntos = puntos;
        }
    }

    void Start()
    {
        filePath = Application.persistentDataPath + "/records.json"; // Ruta donde se guardarán los registros
        CargarRegistros(); // Cargar los registros desde el archivo al iniciar
        ImprimirRecords();
    }

    // Método para guardar el nombre
    public void DatosTablaNombre(string nombre)
    {
        nombreGuardado = nombre;
        Debug.Log("Nombre recibido: " + nombreGuardado);

        // Si ya tenemos los puntos, agregamos el registro
        if (puntosGuardados != 0)
        {
            GuardarRecord();
        }
    }

    // Método para guardar los puntos
    public void DatosTablaPuntos(float puntos)
    {
        puntosGuardados = puntos;
        Debug.Log("Puntos recibidos: " + puntosGuardados);

        // Si ya tenemos el nombre, agregamos el registro
        if (!string.IsNullOrEmpty(nombreGuardado))
        {
            GuardarRecord();
        }
    }

    // Método para agregar el registro a la lista y resetear variables temporales
    private void GuardarRecord()
    {
        Record nuevoRecord = new Record(nombreGuardado, puntosGuardados);

        // Si la lista tiene 10 registros, verificamos si el nuevo registro es válido
        if (records.Count >= 10)
        {
            // Ordenamos los registros por puntos de menor a mayor
            records.Sort((x, y) => x.puntos.CompareTo(y.puntos));

            // Si el nuevo registro tiene más puntos que el registro con menos puntos, reemplazamos
            if (nuevoRecord.puntos > records[0].puntos)
            {
                records[0] = nuevoRecord; // Reemplaza el registro con menos puntos
            }
            else
            {
                Debug.Log("El nuevo registro no tiene más puntos que el mínimo, no se agrega.");
                return; // No se agrega el registro si no tiene más puntos que el mínimo
            }
        }
        else
        {
            // Si hay menos de 10 registros, simplemente lo agregamos
            records.Add(nuevoRecord);
        }

        // Guardar los registros en el archivo
        GuardarRegistros();

        // Imprime el registro agregado
        Debug.Log("Registro agregado: Nombre: " + nombreGuardado + ", Puntos: " + puntosGuardados);

        // Resetea los valores temporales para el próximo registro
        nombreGuardado = null;
        puntosGuardados = 0;
    }

    // Método para imprimir todos los registros
    public void ImprimirRecords()
    {
        for (int i = 0; i < records.Count; i++)
        {
            Record record = records[i];
            Debug.Log((i + 1) + ". Nombre: " + record.nombre + ", Puntos: " + record.puntos);
        }
    }

    // Método para guardar los registros en un archivo JSON
    private void GuardarRegistros()
    {
        string json = JsonUtility.ToJson(new RecordList { records = records });
        File.WriteAllText(filePath, json);
        Debug.Log("Registros guardados en archivo.");
    }

    // Método para cargar los registros desde un archivo JSON
    private void CargarRegistros()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            RecordList loadedData = JsonUtility.FromJson<RecordList>(json);
            records = loadedData.records;
            Debug.Log("Registros cargados desde archivo.");
        }
        else
        {
            Debug.Log("No se encontró archivo de registros, comenzando nuevo.");
        }
    }

    // Clase contenedora para los registros (utilizada para la serialización en JSON)
    [System.Serializable]
    private class RecordList
    {
        public List<Record> records;
    }
}









/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Records : MonoBehaviour
{

    private string nombreGuardado;

    private float puntosGuardados;

    
    
    public void DatosTablaNombre(string nombre)
    {
        nombreGuardado = nombre;
        Debug.Log("Nombre: " + nombreGuardado);
    }

    public void DatosTablaPuntos(float puntos)
    {
        puntosGuardados = puntos;
        Debug.Log("Puntos guardados: " + puntosGuardados);
    }

}
*/