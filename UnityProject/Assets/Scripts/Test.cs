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
        Stopwatch sw = new Stopwatch();

        sw.Start();
        ExcelDataMgr.Init();
        sw.Stop();
        Debug.LogError($"初始化={sw.ElapsedMilliseconds}ms.");

        sw.Restart();
        ExcelDataCacheMgr.CacheData();
        sw.Stop();
        Debug.LogError($"缓存={sw.ElapsedMilliseconds}ms.");

        //var cache = new ESD_sceneObj_noCache(2640); // EDC_sceneObj_cache.Get(2640);
        ByteFileInfo<int> fileInfo = ExcelDataMgr.GetByteFileInfo<int>((short)ExcelName.sceneObj_noCache);

        Debug.LogError(fileInfo.Get<string>(3986, EVD_sceneObj_noCache.path));

        //string s = string.Empty;
        //int a = 0;
        //sw.Restart();
        //for (int c = 0; c < 100; c++)
        //{
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        //a += EDC_sceneObj_cache.Get(i).id; 
        //        //a += fileInfo.Get<int>(i, EVD_sceneObj_noCache.id);
        //        //s = EDC_sceneObj_cache.Get(i).path;
        //        //s = fileInfo.Get<string>(i, EVD_sceneObj_noCache.path);
        //    }
        //}
        //sw.Stop();
        //Debug.LogError($"val = {s}  time = {sw.ElapsedMilliseconds} ms.");

        //Debug.LogError(cache.path);
        //Debug.LogError(cache.xzpos);
        //Debug.LogError(cache.x);
        //foreach (var i in cache.ds)
        //{
        //    //Debug.LogError(i);
        //    Debug.LogError($"key {i.Key} val {i.Value}");
        //}
        //Debug.LogError($"{cache.v4.x}, {cache.v4.y}, {cache.v4.z}, {cache.v4.w}");
        //Debug.LogError(cache.v3i);
        //Debug.LogError(cache.v4);
    }

}
