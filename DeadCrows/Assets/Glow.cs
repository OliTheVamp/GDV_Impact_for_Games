using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glow : MonoBehaviour
{
    public GameObject GlowEffect;
    public Image NormalImage;

    public void GlowMaker()
    {
        GlowEffect.SetActive(true);
        // Debug.Log("Active");
        //NormalImage.enabled = false;
    }
    public void GlowEnder()
    {
        GlowEffect.SetActive(false);
        // Debug.Log("Active");
       // NormalImage.enabled = (true);
    }
}
