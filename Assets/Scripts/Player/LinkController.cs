<<<<<<< HEAD
using JetBrains.Annotations;
=======
using System.Collections;
using System.Collections.Generic;
>>>>>>> test
using UnityEngine;
using UnityEngine.InputSystem;

public class LinkController : MonoBehaviour
{
<<<<<<< HEAD
    [SerializeField] public float velocidad = 5f;
    [SerializeField] public BoxCollider2D stairs;
    [SerializeField] public stairs stairs_code;

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
=======
    [SerializeField] private float velocidad;
    public InputActionAsset map;
    private InputAction horizontal_ia, vertical_ia;
    private Rigidbody2D rig;
    public Animator anim;
    private SpriteRenderer SpritePlayer;
>>>>>>> test

    private void Awake()
    {
        map.Enable();
        horizontal_ia = map.FindActionMap("Movement").FindAction("Horizontal");
        vertical_ia = map.FindActionMap("Movement").FindAction("Vertical");
<<<<<<< HEAD
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
    }

    public float GetLastHorizontalMovementValue()
    {
        return lastHorizontalMovementValue;
    }

    public void SetSpeedYModifier(float speedYModifier)
    {
        this.speedYModifier = speedYModifier;
    }

    public void ResetSpeedYModifier()
    {
        speedYModifier = 1;
    }


=======

        rig = GetComponent<Rigidbody2D>();
        SpritePlayer = GetComponent<SpriteRenderer>();
    }

    enum STATES
    {
        IDLE, ON_WALK_DOWN, ON_WALK_UP, ON_WALK_HORIZONTAL, ONFLOOR
    }

    STATES current_state;

    // Start is called before the first frame update
    void Start()
    {
        current_state = STATES.ONFLOOR;
    }

    private void Update()
    {
        switch (current_state)
        {
            case STATES.ONFLOOR:
                OnFloor();
                break;
        }
    }

    void OnFloor()
    {
        float mx = horizontal_ia.ReadValue<float>();
        float my = vertical_ia.ReadValue<float>();

        rig.velocity = new Vector2(velocidad * mx, rig.velocity.y);
        rig.velocity = new Vector2(rig.velocity.x, velocidad * my);

        RotateCharacter(mx);
    }

    void StateTransition()
    {
        float mx = horizontal_ia.ReadValue<float>();
        float my = vertical_ia.ReadValue<float>();

        if (mx == 0 && my == 0)
        {
            current_state = STATES.IDLE;
        }
        if (my < 0)
        {
            current_state = STATES.ON_WALK_DOWN;
        }
        else if (my > 0)
        {
            current_state = STATES.ON_WALK_UP;
        }
        if (mx != 0)
        {
            current_state = STATES.ON_WALK_HORIZONTAL;
        }
    }

    // Update is called once per frame
    /*void Update()
    {
        StateTransition();

        switch (current_state)
        {
            case STATES.IDLE:
                //Still();
                break;
            case STATES.ON_WALK_DOWN:
                OnWalkDown();
                break;
            case STATES.ON_WALK_UP:
                OnWalkUp();
                break;
            case STATES.ON_WALK_HORIZONTAL:
                OnWalkHorizontal();
                break;
        }
    }

    void Still()
    {
        anim.SetFloat("walk_down", 0);
        anim.SetFloat("walk_up", 0);
        anim.SetFloat("walk_right", 0);
        //rig.velocity = new Vector2(0, 0);
    }
    void OnWalkDown()
    {
        anim.SetFloat("walk_down", Mathf.Abs(rig.velocity.magnitude));
        anim.SetFloat("walk_up", 0);
        Movement();
    }
    void OnWalkUp()
    {
        anim.SetFloat("walk_up", Mathf.Abs(rig.velocity.magnitude));
        anim.SetFloat("walk_down", 0);
        Movement();
    }
    void OnWalkHorizontal()
    {
        anim.SetFloat("walk_right", Mathf.Abs(rig.velocity.magnitude));
        //Movement();
        float mx = horizontal_ia.ReadValue<float>();

        if (mx == 0)
        {
            current_state = STATES.IDLE;
        }

        
        rig.velocity = new Vector2(mx * velocidad, rig.velocity.y);
    }

    private void Movement()
    {
        float mx = horizontal_ia.ReadValue<float>();
        float my = vertical_ia.ReadValue<float>();

        rig.velocity = new Vector2(mx * velocidad, rig.velocity.y);
        rig.velocity = new Vector2(rig.velocity.x, my * velocidad);
        TransformP(mx);
    }*/

    private void RotateCharacter(float mx)
    {
        if (mx < 0)
        {
            gameObject.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }

        if (mx > 0)
        {
            gameObject.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
    }
>>>>>>> test
}
