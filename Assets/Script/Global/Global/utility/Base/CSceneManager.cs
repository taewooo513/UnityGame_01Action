using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSceneManager : CComponent
{
    #region Public 변수

    public float m_fPlaneDistance = Kdefine.DEFAULT_PLANE_DISTANCE;

    #endregion

    public static Camera UICamera
    {
        get { return Function.FindComponent<Camera>(Kdefine.NAME_UI_CAMERA); }
    }

    public static Camera MainCamera
    {
        get { return Function.FindComponent<Camera>(Kdefine.NAME_MAIN_CAMERA); }
    }

    public static GameObject UIRoot
    {
        get { return GameObject.Find(Kdefine.NAME_UI_ROOT); }
    }

    public static GameObject ObjectRoot
    {
        get { return GameObject.Find(Kdefine.NAME_OBJECT_ROOT); }
    }

    public static GameObject CurrentSceneManager
    {
        get { return GameObject.Find(Kdefine.NAME_SCENE_MANAGER); }
    }

    public override void Awake()
    {
        base.Awake();
        this.SetUpUICamera();
        this.SetUpMainCamera();

        //vSync 모니터 주사율 
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Screen.SetResolution(Kdefine.SCREEN_WIDTH, Kdefine.SCREEN_HEIGHT, Kdefine.SCREEN_IS_FULLSCREEN);
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CSceneLoader.Instance.LoadSceneAsync(0,
                (a_oAsyncOperation) => { Debug.LogFormat("Percent: {0}", a_oAsyncOperation.progress); });
        }
    }

    protected void SetUpUICamera()
    {
        if (CSceneManager.UICamera != null)
        {
            CSceneManager.UICamera.orthographic = true;
        }
    }

    protected void SetUpMainCamera()
    {
        if (CSceneManager.MainCamera != null)
        {
            // FOV 
            //float fPlaneHeight = (Kdefine.SCREEN_HEIGHT / 2.0f) * Kdefine.UNIT_SCALE;
            //float fFieldOfView = Mathf.Atan(fPlaneHeight / m_fPlaneDistance);
            //
            //CSceneManager.MainCamera.orthographic = false;
            //CSceneManager.MainCamera.fieldOfView = (fFieldOfView * 2.0f) * Mathf.Rad2Deg;
        }
    }
}