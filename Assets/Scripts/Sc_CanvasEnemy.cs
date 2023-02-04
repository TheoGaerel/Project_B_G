using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sc_CanvasEnemy : MonoBehaviour
{
    public List<Image> list_hearts;
    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.LookAt(Camera.main.transform.position);
    }
}
