using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//전역 함수

public static partial class Function
{
    public static GameObject CreateGameObject(string a_oName, GameObject a_oParent, bool a_bIsStayWorldState = false)
    {
        var oGameObject = new GameObject(a_oName);
        // SetParent의 월드속성 유지 매개 변수가 true로 설정이 되면 월드상에 존재했을때의 속성을 그대로 유지하기 때문에
        // 특정 객체의 하위에 종속이 될 경우 부모 객체의 크기에 따라서 자식 객체의 스케일 값에 영향을 준다
        oGameObject?.transform.SetParent(a_oParent?.transform.transform, a_bIsStayWorldState);

        return oGameObject;
    }

    public static T CreateGameObject<T>(string a_oName, GameObject a_oParent, bool a_bIsStayWorldState = false)
        where T : Component
    {
        var oGameObject = Function.CreateGameObject(a_oName, a_oParent, a_bIsStayWorldState);
        return Function.AddComponent<T>(oGameObject);
    }

    public static T FindComponent<T>(string a_oName) where T : Component
    {
        var oGameObject = GameObject.Find(a_oName);
        return oGameObject?.GetComponentInChildren<T>();
    }

    public static T AddComponent<T>(GameObject a_oGameObject) where T : Component
    {
        var oComponent = a_oGameObject.GetComponent<T>();
        if (oComponent == null)
        {
            oComponent = a_oGameObject.AddComponent<T>();
        }

        return oComponent;
    }

    // 비도기 작업을 수행한다
    public static IEnumerator WaitAsyncOperation(AsyncOperation a_oAsyncOperation,
        System.Action<AsyncOperation> a_oCallBack)
    {
        while (!a_oAsyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
            a_oCallBack?.Invoke(a_oAsyncOperation);
        }
    }
}