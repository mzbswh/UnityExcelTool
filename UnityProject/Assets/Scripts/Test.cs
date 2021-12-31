using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Numerics;
using UnityEngine.Rendering;
using ExcelTool;
using System.IO;
using System.Text;

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


        // 测试直接通过流读取数据
        byte[] cache = new byte[100];
        using (FileStream fs = File.Open(Application.dataPath + "/Resources/ByteData/Test_s_ttt.bytes", FileMode.Open))
        {
            fs.Seek(4, SeekOrigin.Begin);
            fs.Read(cache, 0, 4);
            var addr = ByteReader.Read<int>(cache, 0);
            Debug.LogError("addr = " + addr);
            fs.Seek(addr, SeekOrigin.Begin);
            cache = new byte[20];
            fs.Read(cache, 0, 20);
        }
        var ls = ByteReader.ReadListInt(cache, 0, false);
        foreach (var i in ls)
        {
            Debug.LogError(i);
        }
    }
}
