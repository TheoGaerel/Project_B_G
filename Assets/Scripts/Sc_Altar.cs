using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sc_Altar : MonoBehaviour
{
    public enum AltarType
    {
        Delete,
        Buff,
        Switch,
        Copy
    }

    private const float F_DISTANCE_INTERACT = 5;

    [SerializeField]
    private AltarType altarType;

    [SerializeField]
    private GameObject fxAltarUsable;
    private bool b_usable = true;
    private void Update()
    {
        if (Vector3.Distance(Sc_PlayerController.Instance.transform.position, this.transform.position) < F_DISTANCE_INTERACT && b_usable)
        {
            if (fxAltarUsable) fxAltarUsable.gameObject.SetActive(true);
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                b_usable = false;
                OnInteract();
            }
        }
        else if (fxAltarUsable) fxAltarUsable.gameObject.SetActive(false);
    }
    public void OnInteract()
    {

    }
}
