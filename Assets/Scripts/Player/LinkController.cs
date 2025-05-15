using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class LinkController : MonoBehaviour
{
    // Configuración de movimiento
    [SerializeField] public float velocidad = 5f;
    public bool IsOnStairs = false;
    [SerializeField] int keys;  // Cantidad de llaves recolectadas

    // Modificadores de movimiento
    public float speedYModifier = 1;
    public bool HasFeather = false;  // Habilidad especial

    // Sistema de input
    public InputActionAsset map;
    public InputAction horizontal_ia, vertical_ia, atack_ia, jump_ia, shield_ia;

    // Componentes y referencias
    public Rigidbody2D rig;
    public Transform trans;
    public Transform shadow;  // Sombra del personaje
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public IPlayerState currentState;  // Estado actual del jugador

    // Variables de movimiento
    private float lastHorizontalMovementValue;
    private float lastVerticalMovementValue;
    public bool OnJumping;  // Indica si está saltando

    // Sistema de defensa
    public Vector2 shieldDirection { get; private set; }
    public ShieldCollider shieldCollider;

    private void Awake()
    {
        // Configuración inicial del sistema de input
        map.Enable();
        horizontal_ia = map.FindActionMap("Movement").FindAction("Horizontal");
        vertical_ia = map.FindActionMap("Movement").FindAction("Vertical");
        atack_ia = map.FindActionMap("Atack").FindAction("Atack");
        jump_ia = map.FindActionMap("Movement").FindAction("Jump");
        shield_ia = map.FindActionMap("Defense").FindAction("Shield");

        // Obtención de componentes
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        // Estado inicial del personaje
        ChangeState(new IdleState());
    }

    void Update()
    {
        // Actualización del estado actual
        if (currentState == null) return;

        currentState.HandleInput();
        currentState.Update();

        // Manejo de dirección del escudo
        float mx = horizontal_ia.ReadValue<float>();
        float my = vertical_ia.ReadValue<float>();
        Vector2 dir = new Vector2(mx, my);
        if (dir != Vector2.zero)
        {
            SetShieldDirection(dir);
        }

        // Lectura del input de salto
        float mj = jump_ia.ReadValue<float>();
    }

    // Cambia el estado del jugador
    public void ChangeState(IPlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    // Resetea el estado de ataque
    public void NotAtack()
    {
        float atk = atack_ia.ReadValue<float>();
        atk = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Manejo de la última dirección horizontal
    public void SetLastHorizontalInputValue(float v)
    {
        lastHorizontalMovementValue = v;

        // Orientación del sprite
        if (lastHorizontalMovementValue > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (lastHorizontalMovementValue != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        anim.SetFloat("LastMoveX", v);
    }

    public float GetLastHorizontalMovementValue() => lastHorizontalMovementValue;

    // Manejo de la última dirección vertical
    public void SetLastVerticalInputValue(float v)
    {
        lastVerticalMovementValue = v;
        anim.SetFloat("LastMoveY", v);
    }

    public float GetLastVerticalMovementValue() => lastVerticalMovementValue;

    // Modificadores de velocidad vertical
    public void SetSpeedYModifier(float speedYModifier) => this.speedYModifier = speedYModifier;
    public void ResetSpeedYModifier() => speedYModifier = 1;

    // Detección de escaleras
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Stairs") && OnJumping == false)
        {
            IsOnStairs = true;
            ChangeState(new StairsState());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Stairs"))
        {
            IsOnStairs = false;
        }
    }

    // Corrutinas para acciones especiales
    public IEnumerator DelayJump()
    {
        yield return new WaitForSeconds(0.1f);
        ChangeState(new JumpState());
    }

    public IEnumerator Fall_ReloadSceneAfterFall()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator Dead_ReloadSceneAfterFall()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Manejo de llaves
    public int GetKeys() => keys;
    public void DecrementKeys() => keys--;

    // Movimiento y estado
    public float GetHorizontalMovement() => horizontal_ia.ReadValue<float>();
    public void Death() => ChangeState(new DeadControl());
    public void SetShieldDirection(Vector2 dir) => shieldDirection = dir.normalized;
}