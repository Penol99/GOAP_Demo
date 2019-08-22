using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_interact_quail_fodder : Scr_interactable
{

    public override bool Interact(Scr_goap_agent_bert m_interacter)
    {
        bool gotSeed = DelayedResponse(.5f);
        if (gotSeed)
            m_interacter.m_seed.SetActive(true);

        return gotSeed;
    }
}
