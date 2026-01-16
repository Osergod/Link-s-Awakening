using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class linkMovement : MonoBehaviour
{
    public InputActionAsset map;
    private InputAction horizontal_ia, vertical_ia;
    [SerializeField] private float velocidad;
    private Rigidbody2D rig;
    public Animator anim;
    private SpriteRenderer SpritePlayer;
    private float posColX;
    private float posColY;
    void Start()
    {
        map.Enable();
        horizontal_ia = map.FindActionMap("Movement").FindAction("horizonatl");
        vertical_ia = map.FindActionMap("Movement").FindAction("Vertical");
    }

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        SpritePlayer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float mx = horizontal_ia.ReadValue<float>();
        rig.velocity = new Vector2(mx * velocidad, rig.velocity.y);
        float my = vertical_ia.ReadValue<float>();
        rig.velocity = new Vector2(rig.velocity.x, my * velocidad);
        TransformP(mx);

        if (my < 0)
        {
            anim.SetFloat("walk_down", Mathf.Abs(rig.velocity.magnitude));
            anim.SetFloat("walk_up", 0);
        }
        if (my > 0)
        {
            anim.SetFloat("walk_up", Mathf.Abs(rig.velocity.magnitude));
            anim.SetFloat("walk_down", 0);
        }
        if (mx < 0)
        {
            anim.SetFloat("walk_right", Mathf.Abs(rig.velocity.magnitude));
        }
        if (mx > 0)
        {
            anim.SetFloat("walk_right", Mathf.Abs(rig.velocity.magnitude));
        }

        if (my == 0)
        {
            anim.SetFloat("walk_up", 0);
            anim.SetFloat("walk_down", 0);
        }
        if (mx == 0)
        {
            anim.SetFloat("walk_right", 0);
        }
    }

    private void TransformP(float mx)
    {
        if (mx >= 0)
        {
            gameObject.transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        }

        if (mx <= 0)
        {
            gameObject.transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        }
    }
}
