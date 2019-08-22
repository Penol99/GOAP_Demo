using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    NONE,
    QUAIL_GATE_KEY,
    SHOVEL,
    GUN,
    LAST_RESORT_KNIFE,
    BANDAID,
    FJERT_ARM
}

public enum WorldInteract
{
    NONE,
    QUAIL,
    QUAIL_FODDER,
    QUAIL_GATE,
    STOVE,
    DINING_TABLE,
    CARROT_GARDEN
}

public abstract class Scr_interactable : MonoBehaviour
{

    public Item m_itemType;
    public WorldInteract m_worldInteractType;
    public Vector3 m_agentInteractPos;

    private float m_timer;

    // Called when interacted
    public abstract bool Interact(Scr_goap_agent_bert m_interacter);

    public bool DelayedResponse(float delayTime)
    {
        m_timer += Time.deltaTime;
        if (m_timer >= delayTime)
        {
            m_timer = 0f;
            return true;
        }
        return false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(m_agentInteractPos, Vector3.one);
    }



}
