using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    private int r, g, b;
    [SerializeField] int increment = 10;


    private void Awake()
    {
        r = 0;
        g = 250;
        b = 250;
    }


    public void ChangeColor(Material mat)
    {
        if (r != 250 && g != 0 && b == 250)
        {
            r += increment;
            g -= increment;
        }
        if (r == 250 && g != 250 && b != 0)
        {
            g += increment;
            b -= increment;
        }
        if (r != 0 && g == 250 && b != 250)
        {
            r -= increment;
            b += increment;
        }

        mat.color = new Color(r / 255.0f, g / 255.0f, b / 255.0f);
    }

    public void KeepColor(Material mat)
    {
        mat.color = new Color(r / 255.0f, g / 255.0f, b / 255.0f);
    }
}
