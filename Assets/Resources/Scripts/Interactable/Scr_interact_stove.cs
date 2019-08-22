using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_interact_stove : Scr_interactable
{
    public GameObject m_pot;
    public GameObject m_bertFoodSlot;
    public ParticleSystem m_smokeParticle;


    public override bool Interact(Scr_goap_agent_bert m_interacter)
    {
        bool isDone = DelayedResponse(4f);

        if (isDone)
            m_smokeParticle.Stop();
        else if (!m_smokeParticle.isPlaying)
            m_smokeParticle.Play();

        return isDone;
    }
}
