using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public enum Agent_State
{
    IDLE,
    ACTION
}

public abstract class Scr_goap_agent : MonoBehaviour
{
    public float m_minRange, m_maxRange;
    public Dictionary<G_Actions,bool> m_goalState = new Dictionary<G_Actions, bool>();
    public Dictionary<G_Actions, bool> m_worldState = new Dictionary<G_Actions, bool>();
    public List<Scr_goap_action> m_agentActions;
    public Queue<Scr_goap_action> m_plan = new Queue<Scr_goap_action>();
    public Agent_State m_agentState;


    private Scr_goap_agent m_agent;
    private bool m_planStarted;
    private bool m_noMoreGoals = false;


    protected void InitializeAgent()
    {
        m_agent = GetComponent<Scr_goap_agent>();
        m_goalState = m_agent.CreateGoalState();
        m_worldState = m_agent.CurrentWorldState();
        m_agentActions = GetAgentActions();
    }

    public void ChangeToActionState()
    {
        StartCoroutine(ChangeToActionStateEnumerated());
    }

    private IEnumerator ChangeToActionStateEnumerated()
    {
        yield return new WaitForSeconds(0.3f);
        m_agentState = Agent_State.ACTION;

    } 


    protected void StateMachine(Agent_State state, Queue<Scr_goap_action> plan,
                            Dictionary<G_Actions, bool> currentWorldState,
                            Dictionary<G_Actions, bool> goalState)
    {
        switch (state)
        {
            case Agent_State.IDLE:
                m_planStarted = false;
                State_Idle();
                break;
            case Agent_State.ACTION:
                State_Action(currentWorldState,goalState);
                break;
            default:
                break;
        }
    }

    //When agent is not trying to achieve a goal
    protected abstract void State_Idle();
    // When agent is trying to achieve a goal
    private void State_Action(Dictionary<G_Actions, bool> currentWorldState,
                              Dictionary<G_Actions, bool> goalState)
    {
        if (!m_planStarted)
        {
            m_plan = CreatePlan(m_agentActions, m_worldState, m_goalState);
            /* Just to check if the generated list is as it should be. I write this alot so im just leaving it here.
            foreach (var item in m_plan)
            {
                Debug.Log(item);
            }
            */
            m_planStarted = true;
        }

        RunPlan(m_plan, currentWorldState, goalState);

    }

    protected void RunPlan(Queue<Scr_goap_action> plan, Dictionary<G_Actions, bool> currentWorldState,
                           Dictionary<G_Actions, bool> goalState)
    {
        if (plan.Count > 0) // if we have a plan
        {
            Scr_goap_action currentAction = plan.Peek();
            if (currentAction.ActionFailed())
            {
                PlanUnsuccessful();
                return;
            }

            if (currentAction.PerformAction())
            {
                Parallel.ForEach(currentAction.m_effects, effect =>
                {
                    currentWorldState.Remove(effect.Key);
                    currentWorldState.Add(effect.Key, effect.Value);
                });
                plan.Dequeue();
            }
        } else
        {
            PlanSuccessful(currentWorldState,goalState);
        }
    }

    private void PlanSuccessful(Dictionary<G_Actions, bool> currentWorldState, Dictionary<G_Actions, bool> goalState)
    {
        
        if (!m_noMoreGoals)
        {
            bool hasState = false;
            bool noState = false;
            Parallel.ForEach(goalState, state =>
            {
                hasState = currentWorldState.Contains(state);
                noState = !hasState;
            });
            if (hasState && !noState) // all goals are achieved
            {
                m_noMoreGoals = true;
                m_agentState = Agent_State.IDLE;
            }

        }
    }

    private void PlanUnsuccessful()
    {
        Debug.Log("Plan Failed");
    }

    public abstract Dictionary<G_Actions, bool> CreateGoalState();
    public abstract Dictionary<G_Actions, bool> CurrentWorldState();

    private List<Scr_goap_action> GetAgentActions()
    {
        List<Scr_goap_action> actions = new List<Scr_goap_action>();
        Scr_goap_action[] actionsInAgent = GetComponents<Scr_goap_action>();

        for (int i = 0; i < actionsInAgent.Length; i++)
        {
            actions.Add(actionsInAgent[i]);
        }
        return actions;
    }

    protected Queue<Scr_goap_action> CreatePlan(List<Scr_goap_action> agentActions, Dictionary<G_Actions, bool> currentWorldState,
                                                Dictionary<G_Actions, bool> goalState)
    {
        List<ActionNode> openList = new List<ActionNode>();
        Queue<Scr_goap_action> closedQueue = new Queue<Scr_goap_action>();
        List<Scr_goap_action> applicableActions = new List<Scr_goap_action>();

        for (int i = 0; i < agentActions.Count; i++)
        {
            var action = agentActions[i];
            action.Reset();
            action.InitializeAction();
            if (action.PreinitializationCondition())
            {
                applicableActions.Add(action);
            }
        }

        ActionNode startNode = new ActionNode(null, null, 0, currentWorldState);
        bool planFound = ConstructTable(startNode, openList, applicableActions, goalState);
        if (!planFound)
        {

            return null;
        }

        ActionNode lowestCostNode = null;
        Parallel.ForEach(openList, actionNode =>
        {
            if (lowestCostNode == null)
                lowestCostNode = actionNode;
            else if (actionNode.m_cost < lowestCostNode.m_cost)
                lowestCostNode = actionNode;
        });

        List<Scr_goap_action> completedNodes = new List<Scr_goap_action>();
        ActionNode node = lowestCostNode;
        while (node != null)
        {
            if (node.m_action != null)
            {
                completedNodes.Insert(0, node.m_action);
            }
            node = node.m_parentNode;
        }

        for (int i = 0; i < completedNodes.Count; i++)
        {
            closedQueue.Enqueue(completedNodes[i]);
        }

        return closedQueue;
    }
    private bool ConstructTable(ActionNode parent, List<ActionNode> openList, List<Scr_goap_action> availableActions,
                             Dictionary<G_Actions, bool> goalState)
    {
        bool hasGoalEffect = false;
        for (int i = 0; i < availableActions.Count; i++)
        {
            var action = availableActions[i];
            if (ExistsInState(action.m_preconditions, parent.m_goalStates))
            {
                Dictionary<G_Actions, bool> currentState = ApplyStateChange(parent.m_goalStates, action.m_effects);
                ActionNode node = new ActionNode(parent, action, parent.m_cost + action.m_cost, currentState);

                if (ExistsInState(goalState, currentState))
                {
                    openList.Add(node);
                    hasGoalEffect = true;
                }
                else
                {
                    List<Scr_goap_action> subplan = ActionSubplan(availableActions, action);
                    bool found = ConstructTable(node,openList, subplan,goalState);
                    if (found)
                        hasGoalEffect = true;
                }
            }

        }
        return hasGoalEffect;
    }
    private bool ExistsInState(Dictionary<G_Actions, bool> states, Dictionary<G_Actions, bool> goalStates)
    {

        foreach (var state in states)
        {
            if (goalStates.ContainsKey(state.Key))
            {
                if (goalStates[state.Key] != state.Value)
                {
                    return false;
                }
            }
        }
        return true;
        
    }

    Dictionary<G_Actions, bool> m_tempGoalState = new Dictionary<G_Actions, bool>();
    private Dictionary<G_Actions, bool> ApplyStateChange(Dictionary<G_Actions, bool> currentGoalStates, Dictionary<G_Actions, bool> goalStateChange)
    {
        m_tempGoalState = new Dictionary<G_Actions, bool>(currentGoalStates);

        foreach (var change in goalStateChange)
        {
            
            if (goalStateChange.ContainsKey(change.Key))
            {
                m_tempGoalState[change.Key] = change.Value;
            }
            else
            {
                m_tempGoalState.Add(change.Key, change.Value);
            }
        }
        return m_tempGoalState;
    }
    private List<Scr_goap_action> ActionSubplan(List<Scr_goap_action> actions, Scr_goap_action actionToRemove)
    {
        List<Scr_goap_action> subplan = new List<Scr_goap_action>();
        for (int i = 0; i < actions.Count; i++)
        {
            var action = actions[i];
            if (!action.Equals(actionToRemove))
                subplan.Add(action);
        }
        return subplan;
    }
}




public class ActionNode
{
    public ActionNode m_parentNode;
    public int m_cost;
    public Scr_goap_action m_action;
    public Dictionary<G_Actions, bool> m_goalStates;

    public ActionNode(ActionNode parentNode, Scr_goap_action action,int cost, Dictionary<G_Actions, bool> goalStates )
    {
        m_parentNode = parentNode;
        m_action = action;
        m_cost = cost;
        m_goalStates = goalStates;
    }
}




