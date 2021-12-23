using System.Collections.Generic;
using UnityEngine;

#pragma warning disable

public class EDC_sceneObj_cache
{
    static bool cached = false;
    static ByteFileInfo<int> byteFileInfo;
    static Dictionary<int, EDC_sceneObj_cache> cacheDict = new Dictionary<int, EDC_sceneObj_cache>();

    /// <summary>字典id</summary>
    public int id { get; }
    /// <summary>场景id</summary>
    public int sceneid { get; }
    /// <summary>name</summary>
    public string name { get; }
    /// <summary>类型</summary>
    public int type { get; }
    /// <summary>预制体路径</summary>
    public string path { get; }
    /// <summary>显示状态</summary>
    public int display { get; }
    /// <summary>加载等级</summary>
    public int loadpriority { get; }
    /// <summary>Grass位置</summary>
    public int xzpos { get; }
    /// <summary>X坐标</summary>
    public float x { get; }
    /// <summary>Y坐标</summary>
    public float y { get; }
    /// <summary>Z坐标</summary>
    public float z { get; }
    /// <summary>四元数X</summary>
    public float quaterx { get; }
    /// <summary>四元数Y</summary>
    public float quatery { get; }
    /// <summary>四元数Z</summary>
    public float quaterz { get; }
    /// <summary>四元数W</summary>
    public float quaterw { get; }
    /// <summary>X缩放</summary>
    public float scalex { get; }
    /// <summary>Y缩放</summary>
    public float scaley { get; }
    /// <summary>Z缩放</summary>
    public float scalez { get; }
    /// <summary>layer</summary>
    public int layer { get; }
    /// <summary>box当lightType</summary>
    public int box { get; }
    /// <summary>triger当shadow</summary>
    public int triger { get; }
    /// <summary>是否是convex</summary>
    public int convex { get; }
    /// <summary>rigi当cull</summary>
    public int rigi { get; }
    /// <summary>boxX当r</summary>
    public float boxx { get; }
    /// <summary>boxY当g</summary>
    public float boxy { get; }
    /// <summary>boxZ当b</summary>
    public float boxz { get; }
    /// <summary>boxCenterX</summary>
    public float boxcenterx { get; }
    /// <summary>boxCenterY</summary>
    public float boxcentery { get; }
    /// <summary>boxCenterZ</summary>
    public float boxcenterz { get; }
    /// <summary>coliType当mode</summary>
    public int colitype { get; }
    /// <summary>coliValue</summary>
    public int colivalue { get; }
    /// <summary>coliCondition</summary>
    public int colicondition { get; }
    /// <summary>nvX当intensity</summary>
    public float nvx { get; }
    /// <summary>nvY</summary>
    public float nvy { get; }
    /// <summary>nvZ</summary>
    public float nvz { get; }
    /// <summary>lod</summary>
    public int lod { get; }
    /// <summary>场景等级标记</summary>
    public int scenelv { get; }
    /// <summary>光照贴图Index</summary>
    public string lightmapindex { get; }
    /// <summary>光照贴图Child</summary>
    public string lightmapchild { get; }
    /// <summary>光照贴图偏移X</summary>
    public string lightmapscaleoffsetx { get; }
    /// <summary>光照贴图偏移Y</summary>
    public string lightmapscaleoffsety { get; }
    /// <summary>光照贴图偏移Z</summary>
    public string lightmapscaleoffsetz { get; }
    /// <summary>光照贴图偏移W</summary>
    public string lightmapscaleoffsetw { get; }
    /// <summary>灯光路径动画</summary>
    public string lightanipath { get; }
    /// <summary>物品的类型</summary>
    public int itemtype { get; }

    public EDC_sceneObj_cache(int id, int row)
    {
       this.id = id;
       this.sceneid = byteFileInfo.GetByRowAndIndex<int>(row, 1);
       this.name = byteFileInfo.GetByRowAndIndex<string>(row, 2);
       this.type = byteFileInfo.GetByRowAndIndex<int>(row, 3);
       this.path = byteFileInfo.GetByRowAndIndex<string>(row, 4);
       this.display = byteFileInfo.GetByRowAndIndex<int>(row, 5);
       this.loadpriority = byteFileInfo.GetByRowAndIndex<int>(row, 6);
       this.xzpos = byteFileInfo.GetByRowAndIndex<int>(row, 7);
       this.x = byteFileInfo.GetByRowAndIndex<float>(row, 8);
       this.y = byteFileInfo.GetByRowAndIndex<float>(row, 9);
       this.z = byteFileInfo.GetByRowAndIndex<float>(row, 10);
       this.quaterx = byteFileInfo.GetByRowAndIndex<float>(row, 11);
       this.quatery = byteFileInfo.GetByRowAndIndex<float>(row, 12);
       this.quaterz = byteFileInfo.GetByRowAndIndex<float>(row, 13);
       this.quaterw = byteFileInfo.GetByRowAndIndex<float>(row, 14);
       this.scalex = byteFileInfo.GetByRowAndIndex<float>(row, 15);
       this.scaley = byteFileInfo.GetByRowAndIndex<float>(row, 16);
       this.scalez = byteFileInfo.GetByRowAndIndex<float>(row, 17);
       this.layer = byteFileInfo.GetByRowAndIndex<int>(row, 18);
       this.box = byteFileInfo.GetByRowAndIndex<int>(row, 19);
       this.triger = byteFileInfo.GetByRowAndIndex<int>(row, 20);
       this.convex = byteFileInfo.GetByRowAndIndex<int>(row, 21);
       this.rigi = byteFileInfo.GetByRowAndIndex<int>(row, 22);
       this.boxx = byteFileInfo.GetByRowAndIndex<float>(row, 23);
       this.boxy = byteFileInfo.GetByRowAndIndex<float>(row, 24);
       this.boxz = byteFileInfo.GetByRowAndIndex<float>(row, 25);
       this.boxcenterx = byteFileInfo.GetByRowAndIndex<float>(row, 26);
       this.boxcentery = byteFileInfo.GetByRowAndIndex<float>(row, 27);
       this.boxcenterz = byteFileInfo.GetByRowAndIndex<float>(row, 28);
       this.colitype = byteFileInfo.GetByRowAndIndex<int>(row, 29);
       this.colivalue = byteFileInfo.GetByRowAndIndex<int>(row, 30);
       this.colicondition = byteFileInfo.GetByRowAndIndex<int>(row, 31);
       this.nvx = byteFileInfo.GetByRowAndIndex<float>(row, 32);
       this.nvy = byteFileInfo.GetByRowAndIndex<float>(row, 33);
       this.nvz = byteFileInfo.GetByRowAndIndex<float>(row, 34);
       this.lod = byteFileInfo.GetByRowAndIndex<int>(row, 35);
       this.scenelv = byteFileInfo.GetByRowAndIndex<int>(row, 36);
       this.lightmapindex = byteFileInfo.GetByRowAndIndex<string>(row, 37);
       this.lightmapchild = byteFileInfo.GetByRowAndIndex<string>(row, 38);
       this.lightmapscaleoffsetx = byteFileInfo.GetByRowAndIndex<string>(row, 39);
       this.lightmapscaleoffsety = byteFileInfo.GetByRowAndIndex<string>(row, 40);
       this.lightmapscaleoffsetz = byteFileInfo.GetByRowAndIndex<string>(row, 41);
       this.lightmapscaleoffsetw = byteFileInfo.GetByRowAndIndex<string>(row, 42);
       this.lightanipath = byteFileInfo.GetByRowAndIndex<string>(row, 43);
       this.itemtype = byteFileInfo.GetByRowAndIndex<int>(row, 44);

    }

    public static void CacheData()
    {
        if (cached) return;
        if (byteFileInfo == null)
        {
            byteFileInfo = ExcelDataMgr.GetByteFileInfo<int>(ExcelName.sceneObj_cache);
        }
        if (!byteFileInfo.ByteDataLoaded) byteFileInfo.LoadByteData();
        for (int i = 0; i < byteFileInfo.RowCount; i++)
        {
            int id = byteFileInfo.GetKey(i);
            EDC_sceneObj_cache cache = new EDC_sceneObj_cache(id, i);
            cacheDict.Add(id, cache);
        }
    }

    public static EDC_sceneObj_cache Get(int id)
    {
        if (cacheDict.TryGetValue(id, out var cache)) return cache;
        Debug.LogError($"{typeof(EDC_sceneObj_cache).Name}不存在主列值{id.ToString()}");
        return null;
    }
}
