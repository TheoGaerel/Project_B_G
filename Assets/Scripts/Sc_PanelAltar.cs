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

        foreach (Sc_Card card in Sc_PlayerController.Instance.list_cards)
        {
            card.ShowButton(true);
        }

        switch (altarType)
        {
            case Sc_Altar.AltarType.Delete:
                textDescription.text = "Choissiez une carte à supprimer";
                break;
            case Sc_Altar.AltarType.Copy:
                textDescription.text = "Choisissez une carte à copier";
                break;
            case Sc_Altar.AltarType.Switch:
                textDescription.text = "Choisissez deux carte à permuter";
                break;
            case Sc_Altar.AltarType.Buff:
                textDescription.text = "Choisissez une carte à améliorer";
                break;
        }
    }

    private void Update()
    {
        if (cardSelected != null && currentAltarType != Sc_Altar.AltarType.Switch)
        {
            if (currentAltarType == Sc_Altar.AltarType.Delete)
            {
                Sc_PlayerController.Instance.DeleteCard(cardSelected);
            }
            if (currentAltarType == Sc_Altar.AltarType.Copy)
            {
                Sc_PlayerController.Instance.CopyCard(cardSelected);
            }
            if (currentAltarType == Sc_Altar.AltarType.Buff)
            {
                Sc_PlayerController.Instance.BuffCard(cardSelected);
            }
            ClosePanelAltar();
        }
        if (cardSelected2 != null && currentAltarType == Sc_Altar.AltarType.Switch)
        {
            Sc_PlayerController.Instance.SwitchCard(cardSelected, cardSelected2);
            ClosePanelAltar();
        }
    }

    public void OnChoiceCard(Sc_Card card)
    {
        Debug.Log("OnChoiceCard : " + card.gameObject.name);
        if (cardSelected == null)
        {
            cardSelected = card;
            card.ShowButton(false);
        }
        else if (cardSelected != null && currentAltarType == Sc_Altar.AltarType.Switch) cardSelected2 = card;
    }

    public void ClosePanelAltar()
    {
        cardSelected = null;
        cardSelected2 = null;
        this.gameObject.SetActive(false);
        Sc_PlayerController.Instance.SetCanInteract(true);
    }
}
