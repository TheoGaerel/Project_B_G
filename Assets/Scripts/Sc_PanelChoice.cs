using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PanelChoice : MonoBehaviour
{
    private static Sc_PanelChoice sc_instance;
    [SerializeField]
    private Transform trsf_scalerLeft;
    [SerializeField]
    private Transform trsf_scalerRight;

    private Sc_Card cardLeft;
    private Sc_Card cardRight;

    public static Sc_PanelChoice Instance
    {
        get
        {
            if (sc_instance == null)
            {
                sc_instance = FindObjectOfType<Sc_PanelChoice>(true);
            }
            return sc_instance;
        }
    }


    public void ShowChoices(Sc_Card refCardLeft, Sc_Card refCardRight)
    {
        this.gameObject.SetActive(true);
        cardLeft = GameObject.Instantiate(refCardLeft);
        cardLeft.transform.SetParent(trsf_scalerLeft);
        cardLeft.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        cardLeft.transform.localScale = refCardLeft.transform.localScale;
        cardRight = GameObject.Instantiate(refCardRight);
        cardRight.transform.SetParent(trsf_scalerRight);
        cardRight.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        cardRight.transform.localScale = refCardRight.transform.localScale;
    }

    public void OnClickChoiceLeft()
    {
        if (cardLeft is Sc_Card_Heart)
        {
            Sc_PlayerController.Instance.GetComponent<Sc_Player>().OnHeal();
        }
        else
        {
            Sc_PlayerController.Instance.AddCard(cardLeft);
            Destroy(cardRight.gameObject);
        }

        Sc_PlayerController.Instance.SetCanInteract(true);
        this.gameObject.SetActive(false);
    }

    public void OnClickChoiceRight()
    {
        if (cardRight is Sc_Card_Heart)
        {
            Sc_PlayerController.Instance.GetComponent<Sc_Player>().OnHeal();
        }
        else
        {
            Sc_PlayerController.Instance.AddCard(cardRight);
            Destroy(cardLeft.gameObject);
        }

        Sc_PlayerController.Instance.SetCanInteract(true);
        this.gameObject.SetActive(false);
    }
}
