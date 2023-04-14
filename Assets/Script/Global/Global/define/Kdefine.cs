using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    partial
    - partial클래스는 C# 2.0에 도입된 기능
        ㄴ 클래스를 여러 파일에 정의할 수 있다.
        
    - 클래스가 대상이 되기도 하지만 기본저긍로 구조체, 인터페이스에도 사용이 가능
    
    - 논리적으로 하나이고 분활된 클래스를 컴파일 타임때 하나로 병합을 해줌 
 */

public static partial class Kdefine
{
    #region 해상도

    public static readonly int SCREEN_WIDTH = 1280;
    public static readonly int SCREEN_HEIGHT = 720;
    public static readonly float UNIT_SCALE = 0.01f;
    public static readonly bool SCREEN_IS_FULLSCREEN = false;

    #endregion
}