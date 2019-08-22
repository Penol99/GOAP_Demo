using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_item_pickup : Scr_interactable
{
    bool m_itemFound = false;
    GameObject m_item;

    public override bool Interact(Scr_goap_agent_bert m_interacter)
    {
        if (!m_itemFound)
        {
            for (int i = 0; i < m_interacter.m_items.Count; i++)
            {
                if (m_interacter.m_items[i].name.Equals(gameObject.name))
                {
                    m_item = m_interacter.m_items[i];
                    m_itemFound = true;
                    break;
                }
            }
        } else
        {
            if (DelayedResponse(1f))
            {
                m_item.SetActive(true);
                gameObject.SetActive(false);
                return true;
            }
        }
        return false;
    }

    public void OnDrawGizmos()
    {
        
    }

}
