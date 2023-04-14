using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWCube : CComponent
{
    public float m_fSpeed = 0.0f;
    public GameObject m_oCubeObject;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        float fVertical = Input.GetAxis("Vertical");
        float fHorizontal = Input.GetAxis("Horizontal");

        var stPosition = m_oCubeObject.transform.localPosition;
        stPosition.y = 0;
        stPosition.z += (fVertical * m_fSpeed) * Time.deltaTime;
        stPosition.x += (fHorizontal * m_fSpeed) * Time.deltaTime;

        m_oCubeObject.transform.localPosition = stPosition;
    }
}