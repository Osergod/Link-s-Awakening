using UnityEngine;
using UnityEngine.InputSystem;

public class LinkController : MonoBehaviour
{
    [SerializeField] public float velocidad = 5f;
    public InputActionAsset map;

    public InputAction horizontal_ia, vertical_ia, atack_ia;
    public Rigidbody2D rig;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    private IPlayerState currentState;

    private void Awake()
    {
        map.Enable();
        horizontal_ia = map.FindActionMap("Movement").FindAction("Horizontal");
        vertical_ia = map.FindActionMap("Movement").FindAction("Vertical");
        atack_ia = map.FindActionMap("Atack").FindAction("Atack");

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
}
