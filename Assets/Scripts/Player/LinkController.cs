using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LinkController : MonoBehaviour
{
    [SerializeField] private float velocidad;
    public InputActionAsset map;
    private InputAction horizontal_ia, vertical_ia;
    private Rigidbody2D rig;
    public Animator anim;
    private SpriteRenderer SpritePlayer;

    private void Awake()
    {
        map.Enable();
        horizontal_ia = map.FindActionMap("Movement").FindAction("Horizontal");
        vertical_ia = map.FindActionMap("Movement").FindAction("Vertical");

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
}
