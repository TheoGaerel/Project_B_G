using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_PlayerController : MonoBehaviour
{
    private static Sc_PlayerController sc_instance;

    public static Sc_PlayerController Instance
    {
        get
        {
            if (sc_instance == null)
            {
                sc_instance = FindObjectOfType<Sc_PlayerController>();
            }
            return sc_instance;
        }
    }

    private const float F_MAX_DASH_DELAY = 0.2f; // 1f
    private const float F_DASH_TIME = 0.175f; // 1f
    private const float F_DASH_FORCE = 35f; // 1f
    private CharacterController controller;
    private Rigidbody rbBody;
    private PlayerInput playerInput;
    private PlayerControls playerControls;
    public Transform trsf_launchPosition;
    [Header("Variables")]
    [SerializeField] private float f_playerSpeed = 5f;

    private bool b_inDash = false;
    [Header("Infos Only")]
    [SerializeField]
    private Vector2 v2_movement;
    [SerializeField]
    private Vector3 v3_aim;
    [SerializeField]
    private float f_dashDelay;
    private bool b_shotLock = false;

    [Header("Cards")]
    public List<Sc_Card> list_cards = new List<Sc_Card>();


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        rbBody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        HandleInputs();
        HandleDash();
        HandleMovements();
        HandleRotation();
        HandleShoot();

    }

    private void HandleInputs()
    {
        v2_movement = playerControls.Controls.Movement.ReadValue<Vector2>();
        v3_aim = playerControls.Controls.Aim.ReadValue<Vector2>();
    }
    private void HandleMovements()
    {
        if (b_inDash) return;
        Vector3 move = new Vector3(v2_movement.x, 0, v2_movement.y);
        controller.Move(move * Time.deltaTime * f_playerSpeed);
    }
    #region aim
    private void HandleRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(v3_aim);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            transform.LookAt(new Vector3(point.x, transform.position.y, point.z));
        }
    }
    #endregion //aim

    #region dash
    private void HandleDash()
    {
        if (f_dashDelay > 0) f_dashDelay -= Time.deltaTime;
        if (f_dashDelay <= 0 && Mouse.current.rightButton.wasPressedThisFrame)
        {
            f_dashDelay = F_MAX_DASH_DELAY;
            b_inDash = true;
            StartCoroutine(DashCoroutine());
        }
    }
    private IEnumerator DashCoroutine()
    {
        float startTime = Time.time; // need to remember this to know how long to dash
        Vector3 forward = transform.forward;
        while (Time.time < startTime + F_DASH_TIME)
        {
            controller.Move(forward * F_DASH_FORCE * Time.deltaTime);
            yield return null;
        }
        b_inDash = false;
    }
    #endregion // dash

    private void HandleShoot()
    {
        if (b_shotLock || list_cards.Count == 0) return;
        if (Mouse.current.leftButton.isPressed)
        {
            SetShotLock(true);
            Sc_Card card = list_cards[0];
            list_cards.RemoveAt(0);
            list_cards.Add(card);

            card.OnUse();
        }
    }


    public void SetShotLock(bool isLocked)
    {
        b_shotLock = isLocked;
    }
}
