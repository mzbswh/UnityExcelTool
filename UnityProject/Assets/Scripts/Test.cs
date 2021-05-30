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
        ExcelDataAccess.Init();
        sw.Stop();
        Debug.LogError("初始化时间 = " + sw.ElapsedMilliseconds + " ms");



        List<string> ls = new List<string>();
        sw.Restart();
        for (int i = 0; i < 8000; i++)
        {
            string s = ExcelDataAccess.Get<string>(ExcelName.sceneObj, i, ExcelVariableDef.sceneObj.path);
            ls.Add(s);
        }
        sw.Stop();
        Debug.LogError(ls[7264]);
        Debug.LogError("取8000字符串 = " + sw.ElapsedMilliseconds + " ms");



    }

    public string GetStr()
    {
        return 20.ToString();
    }

}
