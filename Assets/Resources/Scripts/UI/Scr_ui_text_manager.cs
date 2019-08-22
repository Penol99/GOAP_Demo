using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scr_ui_text_manager : MonoBehaviour
{
    public Animator m_menuPanel;
    public float m_typeTime = .5f;
    public TMPro.TextMeshProUGUI m_uiText;
    [TextArea(4, 10)]
    public string m_dialogue;

    private bool m_addNextChar;
    private bool m_isTyping;
    private bool m_writeChar;
    private bool m_hasStarted;
    private string m_textWritten = "";
    private int m_charIndex;
    

    // Start is called before the first frame update
    void Start()
    {
        StartDialogue();
    }

    private void StartDialogue()
    {
        if (!m_isTyping)
        {
            m_hasStarted = true;
            WriteDialogue(m_dialogue);
        }

    }

    private void WriteDialogue(string text)
    {

        int textSize = text.Length;

        if (!m_writeChar)
        {
            m_writeChar = true;
            m_charIndex++;
            StartCoroutine(AddNextChar(text, m_typeTime));
        }
    }

    private IEnumerator AddNextChar(string text, float time)
    {
        yield return new WaitForSeconds(time);
        m_hasStarted = false;
        m_writeChar = false;
        m_textWritten += text[m_charIndex - 1];
        m_uiText.text = m_textWritten;


        m_isTyping = !m_textWritten.Equals(text);

        if (m_charIndex < text.Length)
        {
            WriteDialogue(text);
        }
        else
        {
            m_charIndex = 0;
            m_menuPanel.SetTrigger("trigger_show");
        }

    }
}
