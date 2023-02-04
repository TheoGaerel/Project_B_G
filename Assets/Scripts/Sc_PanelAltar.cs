using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PanelAltar : MonoBehaviour
{
    private static Sc_PanelAltar sc_instance;

    public static Sc_PanelAltar Instance
    {
        get
        {
            if (sc_instance == null)
            {
                sc_instance = FindObjectOfType<Sc_PanelAltar>(true);
            }
            return sc_instance;
        }
    }

    public void OpenPanelAltar(Sc_Altar.AltarType altarType)
    {
        this.gameObject.SetActive(true);
        Sc_PlayerController.Instance.SetCanInteract(false);

        switch (altarType)
        {
            case Sc_Altar.AltarType.Delete:
                DeleteAltar();
                break;
            case Sc_Altar.AltarType.Copy:
                CopyAltar();
                break;
            case Sc_Altar.AltarType.Switch:
                SwitchAltar();
                break;
            case Sc_Altar.AltarType.Buff:
                BuffAltar();
                break;
        }
    }

    private void DeleteAltar()
    {

    }

    private void BuffAltar()
    {
        ClosePanelAltar();
    }

    private void SwitchAltar()
    {

    }

    private void CopyAltar()
    {

    }

    public void ClosePanelAltar()
    {
        this.gameObject.SetActive(false);
        Sc_PlayerController.Instance.SetCanInteract(true);
    }
}
