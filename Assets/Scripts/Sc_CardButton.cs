using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_CardButton : MonoBehaviour
{
    public void OnCardButtonClick()
    {
        Debug.Log("OnCardButtonClick");
        GetComponentInParent<Sc_Card>().OnClickButtonAltar();
    }
}
