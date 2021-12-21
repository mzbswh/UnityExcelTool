using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class Test : MonoBehaviour
{

    private void Start()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        ExcelDataMgr.Init();
        sw.Stop();

        //foreach (var i in t.lss)
        //{
        //    Debug.LogError(i);
        //    //Debug.LogError($"key {i.Key} val {i.Value}");
        //}
        //Debug.LogError($"{t.v4.x}, {t.v4.y}, {t.v4.z}, {t.v4.w}");
        //Debug.LogError(t.v3i);
        //Debug.LogError(t.v4);

    }

}
