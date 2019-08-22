using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_interact_quail_gate : Scr_interactable
{
    private Animator m_anim;
    private bool m_unlocked;
    private bool m_keyUsed;
    public override bool Interact(Scr_goap_agent_bert m_interacter)
    {
        m_unlocked = DelayedResponse(1.5f);
        if (m_unlocked)
        {
            m_keyUsed = false;
            m_anim = GetComponent<Animator>();
            m_anim.SetTrigger("trigger_open_gate"); // Gate animation
        } else if (!m_keyUsed)
        {
            m_interacter.Anim.SetTrigger(m_interacter.m_aStrings.m_anim_open_gate); // Key Animation
            m_keyUsed = true;

        }
        

        return m_unlocked;
    }
}
