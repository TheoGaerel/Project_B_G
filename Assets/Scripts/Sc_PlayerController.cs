using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    [SerializeField] private float F_MAX_DASH_DELAY = 0.5f; // 1f
    [SerializeField] private float F_DASH_TIME = 0.2f; // 1f
    [SerializeField] private float F_DASH_FORCE = 35f; // 1f

    [SerializeField] private float F_RECOIL_TIME = 0.1f; // 1f
    [SerializeField] private float F_RECOIL_FORCE = 0.05f; // 1f

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
    private bool b_canInteract = true;

    [Header("Cards")]
    public List<Sc_Card> list_cards = new List<Sc_Card>();
    public List<Sc_Card> list_magazine = new List<Sc_Card>();
    [SerializeField]
    private Transform trsf_Cards;
    [SerializeField]
    private Image imgCooldown;

    [Header("Sounds")]
    [SerializeField]
    private AudioSource musicExploration;
    [SerializeField]
    private AudioSource musicBattle;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerControls = new PlayerControls();
        playerInput = GetComponent<PlayerInput>();
        rbBody = GetComponent<Rigidbody>();
        imgCooldown.fillAmount = 0;

        if (musicExploration) musicExploration.Play();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        list_magazine.AddRange(list_cards);
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        if (!b_canInteract) return;
        HandleInputs();
        HandleDash();
        HandleMovements();
        HandleRotation();
        HandleShoot();
    }
    private void LateUpdate()
    {
        this.transform.position = new Vector3(this.transform.position.x, 1, this.transform.position.z);
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
        if (f_dashDelay <= 0 && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            f_dashDelay = F_MAX_DASH_DELAY;
            b_inDash = true;
            StartCoroutine(DashCoroutine());
        }
    }
    private IEnumerator DashCoroutine()
    {
        float startTime = Time.time; // need to remember this to know how long to dash
        while (Time.time < startTime + F_DASH_TIME)
        {
            controller.Move(new Vector3(v2_movement.x, 0, v2_movement.y) * F_DASH_FORCE * Time.deltaTime);
            yield return null;
        }
        b_inDash = false;
    }
    #endregion // dash

    public void OnRecoil(Sc_Projectile.Recoil recoilMult)
    {
        StartCoroutine(RoutineRecoil(recoilMult));
    }

    private IEnumerator RoutineRecoil(Sc_Projectile.Recoil recoilMult)
    {
        float startTime = Time.time; // need to remember this to know how long to dash
        Vector3 backward = -transform.forward;
        while (Time.time < startTime + F_RECOIL_TIME)
        {
            controller.Move(backward * F_RECOIL_FORCE * (int)recoilMult * Time.deltaTime);
            yield return null;
        }
    }

    private void HandleShoot()
    {
        if (b_shotLock || list_magazine.Count == 0) return;
        if (Mouse.current.leftButton.isPressed && !b_inDash && !EventSystem.current.IsPointerOverGameObject())
        {
            SetShotLock(true);
            Sc_Card card = list_magazine[0];
            list_magazine.RemoveAt(0);
            card.OnUse();
        }
    }

    public void SetShotLock(bool isLocked)
    {
        b_shotLock = isLocked;
        if (!isLocked && list_magazine.Count == 0)
        {
            ReloadMagazine();
        }
    }

    public void ReloadMagazine()
    {
        imgCooldown.fillAmount = 0;
        list_magazine.Clear();
        list_magazine.AddRange(list_cards);
        foreach (Sc_Card card in list_cards) card.gameObject.SetActive(true);
    }

    public void SetCanInteract(bool interact)
    {
        b_canInteract = interact;
    }

    public void StartDelayNextCard(Sc_Projectile.Reload reload)
    {
        switch (reload)
        {
            case Sc_Projectile.Reload.Fast_0_5:
                StartCoroutine(RoutineToNextCard(0.5f));
                break;
            case Sc_Projectile.Reload.Medium_1:
                StartCoroutine(RoutineToNextCard(1f));
                break;
            case Sc_Projectile.Reload.Slow_2:
                StartCoroutine(RoutineToNextCard(2f));
                break;
        }
    }
    private IEnumerator RoutineToNextCard(float reloadTime)
    {
        float maxreload = reloadTime;
        while (reloadTime > 0)
        {
            imgCooldown.fillAmount = reloadTime / maxreload;
            reloadTime -= Time.deltaTime;
            yield return null;
        }
        imgCooldown.fillAmount = 0;
        SetShotLock(false);
    }

    public void AddCard(Sc_Card card)
    {
        card.transform.SetParent(trsf_Cards);
        card.transform.localScale = Vector3.one;
        list_cards.Add(card);
        card.transform.SetAsLastSibling();
        ReloadMagazine();
    }

    public void DeleteCard(Sc_Card card)
    {
        foreach (Sc_Card c in list_cards)
        {
            c.ShowButton(false);
        }
        list_cards.Remove(card);
        list_magazine.Remove(card);
        Destroy(card.gameObject);
    }
    public void CopyCard(Sc_Card card)
    {
        foreach (Sc_Card c in list_cards)
        {
            c.ShowButton(false);
        }
        Sc_Card copyCard = Instantiate(card);
        copyCard.transform.SetParent(card.transform.parent);
        copyCard.transform.SetAsLastSibling();
        copyCard.transform.localScale = card.transform.localScale;
        list_cards.Add(copyCard);
        list_magazine.Add(copyCard);
    }
    public void BuffCard(Sc_Card card)
    {
        foreach (Sc_Card c in list_cards)
        {
            c.ShowButton(false);
        }
    }
    public void SwitchCard(Sc_Card card1, Sc_Card card2)
    {
        foreach (Sc_Card c in list_cards)
        {
            c.ShowButton(false);
        }

        int index1 = list_cards.IndexOf(card1);
        int index2 = list_cards.IndexOf(card2);
        list_cards[index1] = card2;
        list_cards[index2] = card1;

        if (index1 < index2)
        {
            card2.transform.SetSiblingIndex(index1);
            card1.transform.SetSiblingIndex(index2);
        }
        else
        {
            card1.transform.SetSiblingIndex(index2);
            card2.transform.SetSiblingIndex(index1);
        }
        ReloadMagazine();
    }

    public void PlayMusic(bool battle)
    {
        if (battle)
        {
            musicExploration.Stop();
            musicBattle.Play();
        }
        else
        {
            musicExploration.Play();
            musicBattle.Stop();
        }
    }
}
