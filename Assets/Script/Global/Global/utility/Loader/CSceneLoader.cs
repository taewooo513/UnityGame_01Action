using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CSceneLoader : CSingleton<CSceneLoader>
{
    // 1. 씬을 변경한다.
    public void LoadScene(int a_nIndex)
    {
        // 씬 리스트의 빌드 인덱스를 가진 씬을 컬렉션 등에 담을 수 있는 메서드
        string oScenePath = SceneUtility.GetScenePathByBuildIndex(a_nIndex);
        LoadScene(oScenePath);
    }

    public void LoadScene(string a_oName)
    {
        SceneManager.LoadScene(a_oName, LoadSceneMode.Single);
    }

    // 씬 로드 한다.
    public void LoadSceneAsync(int a_nIndex, System.Action<AsyncOperation> a_oCallBack, float a_Delay = 0.0f,
        LoadSceneMode a_eLoadSceneMode = LoadSceneMode.Single)
    {
        string oScenePath = SceneUtility.GetScenePathByBuildIndex(a_nIndex);
        this.LoadSceneAsync(oScenePath, a_oCallBack, a_Delay, a_eLoadSceneMode);
    }

    public void LoadSceneAsync(string a_oName, System.Action<AsyncOperation> a_oCallBack, float a_Delay = 0.0f,
        LoadSceneMode a_eLoadSceneMode = LoadSceneMode.Single)
    {
        StartCoroutine(this.DoLoadSceneAsync(a_oName, a_oCallBack, a_Delay));
    }

    private IEnumerator DoLoadSceneAsync(string a_oName, System.Action<AsyncOperation> a_oCallBack,
        float aDelay , LoadSceneMode a_eLoadSceneMode = LoadSceneMode.Single)
    {
        yield return new WaitForSeconds(aDelay);
        var oAsyncOperation = SceneManager.LoadSceneAsync(a_oName, a_eLoadSceneMode);
        yield return Function.WaitAsyncOperation(oAsyncOperation, a_oCallBack);
    }
}