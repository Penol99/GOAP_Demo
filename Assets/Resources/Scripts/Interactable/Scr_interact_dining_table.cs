using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_interact_dining_table : Scr_interactable
{
    public Transform m_foodSlot;

    public override bool Interact(Scr_goap_agent_bert m_interacter)
    {
        Transform food = Scr_food.GetActiveFood();
        food.parent = m_foodSlot;
        food.localPosition = Vector3.zero;
        return DelayedResponse(2f);
    }
}
