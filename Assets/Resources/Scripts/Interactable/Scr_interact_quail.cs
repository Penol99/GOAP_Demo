using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_interact_quail : Scr_interactable
{
    public GameObject m_egg;
    public GameObject m_aliveQuail, m_deadQuail;
    public ParticleSystem m_bloodParticles;

    private Animator m_anim;
    private bool m_layEgg;
    private bool m_quailShot;

    private void Start()
    {
        m_anim = GetComponent<Animator>();
    }

    public override bool Interact(Scr_goap_agent_bert m_interacter)
    {

        return true;
    }

    public bool GetEgg(Scr_goap_agent_bert m_interacter)
    {
        
        bool gotEgg = DelayedResponse(2.2f);
        if (!m_layEgg)
        {
            m_interacter.m_seed.SetActive(false);
            m_anim.SetTrigger("trigger_lay_egg");
            m_layEgg = true;
        }
        if (gotEgg)
        {
            m_layEgg = false;
            m_egg.SetActive(false);          
        }

        return gotEgg;

    }

    public bool KillQuail(Scr_goap_agent_bert m_interacter)
    {
        bool quailKilled = DelayedResponse(1f);
        if (!m_quailShot)
        {
            m_interacter.Anim.SetTrigger(m_interacter.m_aStrings.m_anim_shoot);
            m_quailShot = true;
        }
        else if (quailKilled)
        {
            if (!m_bloodParticles.isPlaying)
                m_bloodParticles.Play();
            m_aliveQuail.SetActive(false);
            m_deadQuail.SetActive(true);
            m_quailShot = false;
        }  
        
        return quailKilled;
    }

    public bool GetDeadQuail()
    {
        bool getDeadQuail = DelayedResponse(1.5f);

        if (getDeadQuail)
        {
            m_deadQuail.SetActive(false);
            m_bloodParticles.Stop();
        }
        return getDeadQuail;
    }
}
