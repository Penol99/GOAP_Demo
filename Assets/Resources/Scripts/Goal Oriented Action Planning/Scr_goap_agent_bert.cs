using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Scr_goap_agent_bert : Scr_goap_agent
{
    public Scr_fjert m_fjert;
    public List<GameObject> m_items;
    public Dictionary<G_Actions, bool> m_foodStates = new Dictionary<G_Actions, bool>();
    public float m_foodPoints;
    public Transform m_foodSlot;
    public AnimStrings m_aStrings = new AnimStrings();
    public GameObject m_seed;

    private Animator m_anim;
    private float m_pointCarrotSoup = 5;
    private float m_pointSunnySides = 7;
    private float m_pointCarrotOmellete = 13;
    private float m_pointQuailRoast = 10;
    private float m_pointQuailStew = 20;

    

    public Animator Anim { get => m_anim;}

    private void Start()
    {
        InitializeAgent();
        m_anim = GetComponent<Animator>();
    }

    private void Update()
    {
        StateMachine(m_agentState, m_plan, m_worldState, m_goalState);
    }

    protected override void State_Idle()
    {

    }

    public override Dictionary<G_Actions, bool> CreateGoalState()
    {
        Dictionary<G_Actions, bool> goalState = new Dictionary<G_Actions, bool>();
        goalState.Add(G_Actions.STOP_HUNGER, true);
        goalState.Add(G_Actions.FJERT_BLEEDING, false);
        return goalState;
    }

    public override Dictionary<G_Actions, bool> CurrentWorldState()
    {
        Dictionary<G_Actions, bool> worldState = new Dictionary<G_Actions, bool>();
        // add all the kvp actions
        for (int i = 0; i < Enum.GetNames(typeof(G_Actions)).Length; i++)
        {
            worldState.Add((G_Actions)i, false);
        }     

        return worldState;
    }

    public Dictionary<G_Actions, bool> GetCurrentFoodState()
    {
        Dictionary<G_Actions, bool> foodTypes = new Dictionary<G_Actions, bool>();
        Dictionary<G_Actions, bool> currentFoodState = new Dictionary<G_Actions, bool>();

        for (int i = 0; i < Enum.GetNames(typeof(G_Actions)).Length; i++)
        {
            if (Enum.GetName(typeof(G_Actions),i).Contains("HAS_FOOD"))
            {
                foodTypes.Add((G_Actions)i,false);
            }
        }
        foreach (var item in m_worldState)
        {
            foreach (var s in foodTypes)
            {
                if (item.Key.Equals(s.Key))
                {
                    currentFoodState.Add(item.Key,item.Value);
                }
                    
            }
            
        }
        return currentFoodState;     
    }
    public void AddFoodPoints()
    {
        Dictionary<G_Actions, bool> foodState = GetCurrentFoodState();
        foreach (var food in foodState)
        {
            if (food.Value)
            {
                switch (food.Key)
                {
                    case G_Actions.HAS_FOOD_CARROT_SOUP:
                        m_foodPoints += m_pointCarrotSoup;
                        break;
                    case G_Actions.HAS_FOOD_SUNNY_SIDES:
                        m_foodPoints += m_pointSunnySides;
                        break;
                    case G_Actions.HAS_FOOD_CARROT_OMELLETE:
                        m_foodPoints += m_pointCarrotOmellete;
                        break;
                    case G_Actions.HAS_FOOD_QUAIL_STEW:
                        m_foodPoints += m_pointQuailStew;
                        break;
                    case G_Actions.HAS_FOOD_QUAIL_ROAST:
                        m_foodPoints += m_pointQuailRoast;
                        break;
                    default:
                        break;
                }
            }

        }
    }

    public void RotateTowardsDir(Transform target)
    {
        float rotSpeed = 13f;
        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        lookRot.x = 0f;
        lookRot.z = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * rotSpeed);
    }

}

public class AnimStrings
{
    public readonly string
        m_anim_open_gate = "trigger_open_gate",
        m_anim_shovel = "trigger_shovel",
        m_anim_slice = "trigger_slice",
        m_anim_shoot = "trigger_shoot";
}
