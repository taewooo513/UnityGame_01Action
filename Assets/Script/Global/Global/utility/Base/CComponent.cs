using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CComponent : MonoBehaviour
{
    #region public 변수

    [HideInInspector] public Transform m_oTransform = null;
    [HideInInspector] public Rigidbody m_oRigidBody = null;
    [HideInInspector] public Rigidbody2D m_oRigidBody2D = null;

    #endregion

    public virtual void Awake()
    {
        m_oTransform = this.transform;
        m_oRigidBody = this.transform.GetComponentInChildren<Rigidbody>();
        m_oRigidBody2D = this.transform.GetComponentInChildren<Rigidbody2D>();
    }

    public virtual void Start()
    {
        // Do Nothing
    }

    public virtual void Update()
    {
    }

    public virtual void LateUpdate()
    {
    }
}