using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_fjert : Scr_interactable
{
    public GameObject m_rightArm, m_rightArmDisconnected;
    public ParticleSystem m_bloodParticle;
    public GameObject m_bandaid;
    public float m_deathTimeLimit;

    private float m_deathTimer;
    private bool m_deathCountStart;
    
   


    private Animator m_anim;

    public float DeathTimer { get => m_deathTimer; set => m_deathTimer = value; }

    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();   
    }

    private void Update()
    {
        if (m_deathCountStart)
            DeathCountdown();
        else
            m_deathTimer = 0;
    }

    private void DeathCountdown()
    {
        m_deathTimer += Time.deltaTime;
        if (m_deathTimer >= m_deathTimeLimit)
        {
            Die();
        }
    }

    private bool m_armCutAnim;
    public bool CutOffArm(Scr_goap_agent_bert m_interacter)
    {
        bool armCut = DelayedResponse(1.05f);
        if (armCut)
        {
            if (!m_bloodParticle.isPlaying)
                m_bloodParticle.Play();
            m_deathCountStart = true;
            m_rightArm.SetActive(false);
            m_rightArmDisconnected.SetActive(true);
            m_armCutAnim = false;
            return true;
        } else if (!m_armCutAnim)
        {
            m_interacter.Anim.SetTrigger(m_interacter.m_aStrings.m_anim_slice);
            m_armCutAnim = true;
        }

        return false;
    }

    public bool GetArm()
    {
        bool gotArm = DelayedResponse(1f);
        if (gotArm)
            m_rightArmDisconnected.SetActive(false);
        return  gotArm;
    }

    public bool AddBandaid(Scr_goap_agent_bert m_interacter)
    {
        bool bandaidAdded = DelayedResponse(1.25f);
        m_deathCountStart = false;
        
        if (bandaidAdded)
        {
            m_bloodParticle.Stop();
            m_bandaid.SetActive(true);
        }
        return bandaidAdded;
    }

    private void Die()
    {
        SceneManager.LoadScene("scn_end_fjert_died");
    }

    public override bool Interact(Scr_goap_agent_bert m_interacter)
    {
        return false;
    }

}
