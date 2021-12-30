using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Numerics;
using UnityEngine.Rendering;
using ExcelTool;

public class Test : MonoBehaviour
{
    private void Start()
    {
        ExcelDataMgr.Init();
        ExcelDataCacheMgr.CacheData();

        //var data = EDC_Test_Sheet1.Get(1);
        //Debug.LogError(data.lsi[2]);
        //Debug.LogError(ExcelDataMgr.Get<int, int>((short)ExcelName.Test_s_ttt, 1, EVD_Test_s_ttt.intType));

        foreach (var i in EDC_Test_Sheet1.Ids)
        {
            foreach (var j in i)
                Debug.LogError($"{j.Key}  {j.Value}");
        }
    }
}
