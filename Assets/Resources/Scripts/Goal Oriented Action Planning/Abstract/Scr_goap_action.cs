using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum G_Actions
{
    HAS_CARROT,
    HAS_QUAIL_FODDER,
    HAS_QUAIL_KEY,
    HAS_QUAIL_EGG,
    HAS_QUAIL_MEAT,
    HAS_FJERT_ARM,
    HAS_SHOVEL,
    HAS_GUN,
    QUAIL_GATE_OPEN,
    QUAIL_KILLED,
    HAS_LAST_RESORT_KNIFE,
    HAS_BAND_AID,
    AT_DINNER_TABLE,
    FJERT_ARM_IS_SLICED,
    FJERT_BLEEDING,
    HAS_FOOD,
    HAS_FOOD_CARROT_SOUP,
    HAS_FOOD_SUNNY_SIDES,
    HAS_FOOD_CARROT_OMELLETE,
    HAS_FOOD_QUAIL_STEW,
    HAS_FOOD_QUAIL_ROAST,
    HAS_FOOD_SEARED_ARM,
    STOP_HUNGER

}

public abstract class Scr_goap_action : MonoBehaviour
{
    public Dictionary<G_Actions, bool> m_preconditions;
    public Dictionary<G_Actions, bool> m_effects;
    public bool m_actionIsDone;
    public int m_cost;

    protected NavMeshAgent m_navAgent;
    protected Scr_goap_agent m_goapAgent;
    protected Scr_find_item m_findItem;
    protected Scr_interactable m_target;


    public Scr_goap_action()
    {       
        m_preconditions = new Dictionary<G_Actions, bool>();
        m_effects = new Dictionary<G_Actions, bool>();
    }

    public void InitializeAction()
    {
        m_findItem = GetComponent<Scr_find_item>();
        m_goapAgent = GetComponent<Scr_goap_agent>();
        m_navAgent = GetComponent<NavMeshAgent>();
    }

    protected void AddPrecondition(G_Actions action, bool value)
    {
        m_preconditions.Add(action,value);
    }
    protected void AddEffect(G_Actions action, bool value)
    {
        m_effects.Add(action,value);
    }

    // returns true if action should be added to plan
    public abstract bool PreinitializationCondition();
    // Return true when action is done and false while its in progress of achieving
    public abstract bool PerformAction();
    // Abort
    public abstract bool ActionFailed();

    public abstract void Reset();

   
    public bool DistanceCheckObject(Vector3 pos, float range)
    {
        if (Vector3.Distance(pos, transform.position) <= range)
        {
            m_navAgent.isStopped = true;
            return true;
        }
        else
        {
            m_navAgent.isStopped = false;
        }
        return false;
    }
    public bool DistanceCheckObject(Transform obj, float range)
    {
        return DistanceCheckObject(obj.position, range);
    }




}
