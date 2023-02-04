using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField]
    private TextMeshProUGUI textDescription;
    private Sc_Altar.AltarType currentAltarType;
    private Sc_Card cardSelected;
    private Sc_Card cardSelected2;

    public void OpenPanelAltar(Sc_Altar.AltarType altarType)
    {
        this.gameObject.SetActive(true);
        Sc_PlayerController.Instance.SetCanInteract(false);
        Sc_PlayerController.Instance.ReloadMagazine();
        currentAltarType = altarType;
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
        textDescription.text = "Choissiez une carte à supprimer";
        foreach (Sc_Card card in Sc_PlayerController.Instance.list_cards)
        {
            card.ShowButton(true);
        }
    }

    private void BuffAltar()
    {
        ClosePanelAltar();
    }

    private void SwitchAltar()
    {
        textDescription.text = "Choisissez deux carte à inverser";
    }

    private void CopyAltar()
    {
        textDescription.text = "Choisissez une carte à copier";
    }

    public void OnChoiceCard(Sc_Card card)
    {
        if (cardSelected == null) cardSelected = card;
        if (cardSelected != null && currentAltarType == Sc_Altar.AltarType.Switch) cardSelected2 = card;
    }


    public void ClosePanelAltar()
    {
        cardSelected = null;
        cardSelected2 = null;
        this.gameObject.SetActive(false);
        Sc_PlayerController.Instance.SetCanInteract(true);
    }
}
