using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_interact_carrot_garden : Scr_interactable
{

    public List<GameObject> m_carrotsToPick = new List<GameObject>();
    private bool m_shoveled;


    public override bool Interact(Scr_goap_agent_bert m_interacter)
    {
        bool shovelDone = DelayedResponse(1f);
        if (!m_shoveled)
        {
            m_interacter.Anim.SetTrigger(m_interacter.m_aStrings.m_anim_shovel);
            m_shoveled = true;
        }

        if (shovelDone)
        {
            for (int i = 0; i < m_carrotsToPick.Count; i++)
            {
                m_carrotsToPick[i].SetActive(false);
            }
            m_shoveled = false;
        }
        return shovelDone;
    }
}
