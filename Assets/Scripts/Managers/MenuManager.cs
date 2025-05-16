using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private StatsManager stats;
    [SerializeField] private TMP_InputField PlayerName;
    [SerializeField] private LinkDatabase db;
    [SerializeField] private TMP_Text nombreMostrado;
    private bool DBSave;

    void Start()
    {
        stats = FindObjectOfType<StatsManager>();

        DBSave = false;
    }

    public void OnEnterPlayerName()
    {
        if (DBSave == false)
        {
            stats.namePlayer = PlayerName.text;
            Debug.Log("Nombre guardado: " + stats.namePlayer);
            db.UpdateDatabase();

            DBSave = true;
        }
        else
        {
            OnChangedPlayerName();
        }
    }

    public void OnChangedPlayerName()
    {
        if (DBSave == true)
        {
            nombreMostrado.text = "No puedes guardar tus datos más de una vez";
            Debug.Log("No puedes guardar tus datos más de una vez");


        }
    }

    private void RemoveText()
    {
        
    }

    public async void MostrarNombreDesdeBaseDeDatos()
    {
        if (stats == null)
            stats = FindObjectOfType<StatsManager>();

        string connectionString = "mongodb+srv://Zelda:zelda69*@cluster0.tvouedw.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("Zelda");
        var usersCollection = database.GetCollection<BsonDocument>("Stats_Game");

        var filtro = Builders<BsonDocument>.Filter.Eq("PlayerName", stats.GetNamePlayer());
        var resultado = await usersCollection.Find(filtro).FirstOrDefaultAsync();

        if (resultado != null)
        {
            string nombre = resultado["PlayerName"].AsString;
            nombreMostrado.text = "Nombre Jugador: " + nombre;
            Debug.Log("Nombre leído: " + nombre);
        }
        else
        {
            nombreMostrado.text = "Nombre no encontrado";
            Debug.LogWarning("No se encontró el nombre en la base de datos.");
        }
    }



}
