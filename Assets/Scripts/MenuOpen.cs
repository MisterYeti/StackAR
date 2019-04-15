using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpen : MonoBehaviour
{
    public GameObject Buttons;

    public void SetOnOff()
    {
        Buttons.SetActive(!Buttons.activeSelf);
    }
}
