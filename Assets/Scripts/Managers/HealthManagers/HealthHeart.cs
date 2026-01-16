using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeart : MonoBehaviour
{
    public Sprite fullHeart, halfHeart, emptyHeart;  // Sprites para los estados del corazón
    Image heartImage;  // Componente Image que muestra el sprite

    // Obtiene la referencia al componente Image al iniciar
    public void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    // Cambia la imagen del corazón según su estado (lleno, medio, vacío)
    public void SetHeartImage(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = emptyHeart;
                break;
            case HeartStatus.Half:
                heartImage.sprite = halfHeart;
                break;
            case HeartStatus.Full:
                heartImage.sprite = fullHeart;
                break;
        }
    }

    // Enum que define los posibles estados de los corazones
    public enum HeartStatus
    {
        Empty = 0,
        Half = 1,
        Full = 2
    }

    void Start()
    {

    }

    void Update()
    {

    }
}