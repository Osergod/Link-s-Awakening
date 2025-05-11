using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class LinkController : MonoBehaviour
{
    [SerializeField] public float velocidad = 5f;
    public bool IsOnStairs = false;

    public float speedYModifier = 1;
    public bool HasFeather = false;

    public InputActionAsset map;
    public InputAction horizontal_ia, vertical_ia, atack_ia, jump_ia;

    public Rigidbody2D rig;
    public Transform trans;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public IPlayerState currentState;

    private float lastHorizontalMovementValue;
    private float lastVerticalMovementValue;

    private void Awake()
    {
        map.Enable();
        horizontal_ia = map.FindActionMap("Movement").FindAction("Horizontal");
        vertical_ia = map.FindActionMap("Movement").FindAction("Vertical");
        atack_ia = map.FindActionMap("Atack").FindAction("Atack");
        jump_ia = map.FindActionMap("Movement").FindAction("Jump");

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


        // prova de "FallState"

        float mj = jump_ia.ReadValue<float>();

        if (mj != 0 && HasFeather == false)
        {
            ChangeState(new FallState());
            return;
        }

        // fi de la prova
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
        if (other.CompareTag("Stairs"))
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

    public IEnumerator ReloadSceneAfterFall()
    {
        yield return new WaitForSeconds(0.6f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

}
