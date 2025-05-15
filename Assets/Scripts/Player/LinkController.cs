using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class LinkController : MonoBehaviour
{
    [SerializeField] public float velocidad = 5f;
    public bool IsOnStairs = false;
    [SerializeField] int keys;

    public float speedYModifier = 1;
    public bool HasFeather = false;

    public InputActionAsset map;
    public InputAction horizontal_ia, vertical_ia, atack_ia, jump_ia, shield_ia;

    public Rigidbody2D rig;
    public Transform trans;
    public Transform shadow;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public IPlayerState currentState;

    private float lastHorizontalMovementValue;
    private float lastVerticalMovementValue;

    public bool OnJumping;

    public Vector2 shieldDirection { get; private set; }
    public ShieldCollider shieldCollider;

    private void Awake()
    {
        map.Enable();
        horizontal_ia = map.FindActionMap("Movement").FindAction("Horizontal");
        vertical_ia = map.FindActionMap("Movement").FindAction("Vertical");
        atack_ia = map.FindActionMap("Atack").FindAction("Atack");
        jump_ia = map.FindActionMap("Movement").FindAction("Jump");
        shield_ia = map.FindActionMap("Defense").FindAction("Shield");

        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        ChangeState(new IdleState());
    }

    void Update()
    {
        if (currentState == null) return;

        currentState.HandleInput();
        currentState.Update();

        float mx = horizontal_ia.ReadValue<float>();
        float my = vertical_ia.ReadValue<float>();
        Vector2 dir = new Vector2(mx, my);
        if (dir != Vector2.zero)
        {
            SetShieldDirection(dir);
        }

        float mj = jump_ia.ReadValue<float>();
    }

    public void ChangeState(IPlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter(this);
    }

    public void NotAtack()
    {
        float atk = atack_ia.ReadValue<float>();
        atk = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetLastHorizontalInputValue(float v)
    {
        lastHorizontalMovementValue = v;

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

    public float GetLastHorizontalMovementValue()
    {
        return lastHorizontalMovementValue;
    }

    public void SetLastVerticalInputValue(float v)
    {
        lastVerticalMovementValue = v;
        anim.SetFloat("LastMoveY", v);
    }

    public float GetLastVerticalMovementValue()
    {
        return lastVerticalMovementValue;
    }

    public void SetSpeedYModifier(float speedYModifier)
    {
        this.speedYModifier = speedYModifier;
    }

    public void ResetSpeedYModifier()
    {
        speedYModifier = 1;
    }

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

    public int GetKeys()
    {
        return keys;
    }

    public void DecrementKeys()
    {
        keys--;
    }

    public float GetHorizontalMovement()
    {
        return horizontal_ia.ReadValue<float>();
    }

    public void Death()
    {
        ChangeState(new DeadControl());
    }

    public void SetShieldDirection(Vector2 dir)
    {
        shieldDirection = dir.normalized;
    }
}
