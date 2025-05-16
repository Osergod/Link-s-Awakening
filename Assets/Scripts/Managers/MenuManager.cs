using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private StatsManager stats;
    [SerializeField] private TMP_InputField PlayerName;
    [SerializeField] private LinkDatabase db;

    private bool DBSave;
    int total;

    [SerializeField] private TMP_Text nombreMostrado;
    [SerializeField] private TMP_Text killsMostradas;
    [SerializeField] private TMP_Text tiempoMostrado;

    void Start()
    {
        stats = FindObjectOfType<StatsManager>();

        DBSave = false;

        //TimeTransform();
    }

    public void OnEnterPlayerName()
    {
        if (DBSave == false)
        {
            stats.namePlayer = PlayerName.text;
            Debug.Log("Nombre guardado: " + stats.namePlayer);
            db.UpdateDatabase();

            //stats.playTime = 3600;

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
            string kills = resultado["KillsNumber"].AsString;
            string time = resultado["TimePlay"].AsString;

            nombreMostrado.text = "Nombre Jugador: " + nombre;
            killsMostradas.text = "Numero de kills: " + kills;
            tiempoMostrado.text = "Tiempo: " + time + " segundos";

            Debug.Log("Nombre leído: " + nombre);
        }
        else
        {
            nombreMostrado.text = "Nombre no encontrado";
            Debug.LogWarning("No se encontró el nombre en la base de datos.");
        }
    }

    /*public void TimeTransform()
    {
        float hores;
        float minuts;
        float segons;

        float total = stats.playTime;

        segons = total % 60;
        total = total / 60;

        minuts = total % 60;
        total = total / 60;

        hores = total;

        stats.playTime = total;
    }*/
}
