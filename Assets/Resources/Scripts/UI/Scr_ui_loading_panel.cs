using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scr_ui_loading_panel : MonoBehaviour
{
    private Color newColor, startColor;
    private Image m_image;
    private float m_fadeSpeed = 0.9f;
    
    private void Awake()
    {
        m_image = GetComponent<Image>();
        startColor = m_image.color;
        newColor = startColor;
    }

    private void Update()
    {
        m_image.color = newColor;
        newColor.a -= Time.deltaTime * m_fadeSpeed;

        if (newColor.a <= 0)
        {
            m_image.color = startColor;
            gameObject.SetActive(false);
        }
    }
}
