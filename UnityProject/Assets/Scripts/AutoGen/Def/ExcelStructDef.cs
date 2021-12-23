using System.Collections.Generic;
using UnityEngine;

#pragma warning disable

public struct ESD_sceneObj_noCache
{
    int primaryColVal;
    readonly ByteFileInfo<int> byteFileInfo;
    public ESD_sceneObj_noCache(int val)
    {
        this.primaryColVal = val;
        this.byteFileInfo = ExcelDataMgr.GetByteFileInfo<int>(ExcelName.sceneObj_noCache);
    }
    public void SetPrimary(int id) { this.primaryColVal = id; } 
    /// <summary>字典id</summary>
    public int id => byteFileInfo.Get<int>(primaryColVal, 0);
    /// <summary>场景id</summary>
    public int sceneid => byteFileInfo.Get<int>(primaryColVal, 65540);
    /// <summary>name</summary>
    public string name => byteFileInfo.Get<string>(primaryColVal, 131080);
    /// <summary>类型</summary>
    public int type => byteFileInfo.Get<int>(primaryColVal, 196620);
    /// <summary>预制体路径</summary>
    public string path => byteFileInfo.Get<string>(primaryColVal, 262160);
    /// <summary>显示状态</summary>
    public int display => byteFileInfo.Get<int>(primaryColVal, 327700);
    /// <summary>加载等级</summary>
    public int loadpriority => byteFileInfo.Get<int>(primaryColVal, 393240);
    /// <summary>Grass位置</summary>
    public int xzpos => byteFileInfo.Get<int>(primaryColVal, 458780);
    /// <summary>X坐标</summary>
    public float x => byteFileInfo.Get<float>(primaryColVal, 524320);
    /// <summary>Y坐标</summary>
    public float y => byteFileInfo.Get<float>(primaryColVal, 589860);
    /// <summary>Z坐标</summary>
    public float z => byteFileInfo.Get<float>(primaryColVal, 655400);
    /// <summary>四元数X</summary>
    public float quaterx => byteFileInfo.Get<float>(primaryColVal, 720940);
    /// <summary>四元数Y</summary>
    public float quatery => byteFileInfo.Get<float>(primaryColVal, 786480);
    /// <summary>四元数Z</summary>
    public float quaterz => byteFileInfo.Get<float>(primaryColVal, 852020);
    /// <summary>四元数W</summary>
    public float quaterw => byteFileInfo.Get<float>(primaryColVal, 917560);
    /// <summary>X缩放</summary>
    public float scalex => byteFileInfo.Get<float>(primaryColVal, 983100);
    /// <summary>Y缩放</summary>
    public float scaley => byteFileInfo.Get<float>(primaryColVal, 1048640);
    /// <summary>Z缩放</summary>
    public float scalez => byteFileInfo.Get<float>(primaryColVal, 1114180);
    /// <summary>layer</summary>
    public int layer => byteFileInfo.Get<int>(primaryColVal, 1179720);
    /// <summary>box当lightType</summary>
    public int box => byteFileInfo.Get<int>(primaryColVal, 1245260);
    /// <summary>triger当shadow</summary>
    public int triger => byteFileInfo.Get<int>(primaryColVal, 1310800);
    /// <summary>是否是convex</summary>
    public int convex => byteFileInfo.Get<int>(primaryColVal, 1376340);
    /// <summary>rigi当cull</summary>
    public int rigi => byteFileInfo.Get<int>(primaryColVal, 1441880);
    /// <summary>boxX当r</summary>
    public float boxx => byteFileInfo.Get<float>(primaryColVal, 1507420);
    /// <summary>boxY当g</summary>
    public float boxy => byteFileInfo.Get<float>(primaryColVal, 1572960);
    /// <summary>boxZ当b</summary>
    public float boxz => byteFileInfo.Get<float>(primaryColVal, 1638500);
    /// <summary>boxCenterX</summary>
    public float boxcenterx => byteFileInfo.Get<float>(primaryColVal, 1704040);
    /// <summary>boxCenterY</summary>
    public float boxcentery => byteFileInfo.Get<float>(primaryColVal, 1769580);
    /// <summary>boxCenterZ</summary>
    public float boxcenterz => byteFileInfo.Get<float>(primaryColVal, 1835120);
    /// <summary>coliType当mode</summary>
    public int colitype => byteFileInfo.Get<int>(primaryColVal, 1900660);
    /// <summary>coliValue</summary>
    public int colivalue => byteFileInfo.Get<int>(primaryColVal, 1966200);
    /// <summary>coliCondition</summary>
    public int colicondition => byteFileInfo.Get<int>(primaryColVal, 2031740);
    /// <summary>nvX当intensity</summary>
    public float nvx => byteFileInfo.Get<float>(primaryColVal, 2097280);
    /// <summary>nvY</summary>
    public float nvy => byteFileInfo.Get<float>(primaryColVal, 2162820);
    /// <summary>nvZ</summary>
    public float nvz => byteFileInfo.Get<float>(primaryColVal, 2228360);
    /// <summary>lod</summary>
    public int lod => byteFileInfo.Get<int>(primaryColVal, 2293900);
    /// <summary>场景等级标记</summary>
    public int scenelv => byteFileInfo.Get<int>(primaryColVal, 2359440);
    /// <summary>光照贴图Index</summary>
    public string lightmapindex => byteFileInfo.Get<string>(primaryColVal, 2424980);
    /// <summary>光照贴图Child</summary>
    public string lightmapchild => byteFileInfo.Get<string>(primaryColVal, 2490520);
    /// <summary>光照贴图偏移X</summary>
    public string lightmapscaleoffsetx => byteFileInfo.Get<string>(primaryColVal, 2556060);
    /// <summary>光照贴图偏移Y</summary>
    public string lightmapscaleoffsety => byteFileInfo.Get<string>(primaryColVal, 2621600);
    /// <summary>光照贴图偏移Z</summary>
    public string lightmapscaleoffsetz => byteFileInfo.Get<string>(primaryColVal, 2687140);
    /// <summary>光照贴图偏移W</summary>
    public string lightmapscaleoffsetw => byteFileInfo.Get<string>(primaryColVal, 2752680);
    /// <summary>灯光路径动画</summary>
    public string lightanipath => byteFileInfo.Get<string>(primaryColVal, 2818220);
    /// <summary>物品的类型</summary>
    public int itemtype => byteFileInfo.Get<int>(primaryColVal, 2883760);
}
public struct ESD_sceneObj_cache
{
    int primaryColVal;
    readonly ByteFileInfo<int> byteFileInfo;
    public ESD_sceneObj_cache(int val)
    {
        this.primaryColVal = val;
        this.byteFileInfo = ExcelDataMgr.GetByteFileInfo<int>(ExcelName.sceneObj_cache);
    }
    public void SetPrimary(int id) { this.primaryColVal = id; } 
    /// <summary>字典id</summary>
    public int id => byteFileInfo.Get<int>(primaryColVal, 0);
    /// <summary>场景id</summary>
    public int sceneid => byteFileInfo.Get<int>(primaryColVal, 65540);
    /// <summary>name</summary>
    public string name => byteFileInfo.Get<string>(primaryColVal, 131080);
    /// <summary>类型</summary>
    public int type => byteFileInfo.Get<int>(primaryColVal, 196620);
    /// <summary>预制体路径</summary>
    public string path => byteFileInfo.Get<string>(primaryColVal, 262160);
    /// <summary>显示状态</summary>
    public int display => byteFileInfo.Get<int>(primaryColVal, 327700);
    /// <summary>加载等级</summary>
    public int loadpriority => byteFileInfo.Get<int>(primaryColVal, 393240);
    /// <summary>Grass位置</summary>
    public int xzpos => byteFileInfo.Get<int>(primaryColVal, 458780);
    /// <summary>X坐标</summary>
    public float x => byteFileInfo.Get<float>(primaryColVal, 524320);
    /// <summary>Y坐标</summary>
    public float y => byteFileInfo.Get<float>(primaryColVal, 589860);
    /// <summary>Z坐标</summary>
    public float z => byteFileInfo.Get<float>(primaryColVal, 655400);
    /// <summary>四元数X</summary>
    public float quaterx => byteFileInfo.Get<float>(primaryColVal, 720940);
    /// <summary>四元数Y</summary>
    public float quatery => byteFileInfo.Get<float>(primaryColVal, 786480);
    /// <summary>四元数Z</summary>
    public float quaterz => byteFileInfo.Get<float>(primaryColVal, 852020);
    /// <summary>四元数W</summary>
    public float quaterw => byteFileInfo.Get<float>(primaryColVal, 917560);
    /// <summary>X缩放</summary>
    public float scalex => byteFileInfo.Get<float>(primaryColVal, 983100);
    /// <summary>Y缩放</summary>
    public float scaley => byteFileInfo.Get<float>(primaryColVal, 1048640);
    /// <summary>Z缩放</summary>
    public float scalez => byteFileInfo.Get<float>(primaryColVal, 1114180);
    /// <summary>layer</summary>
    public int layer => byteFileInfo.Get<int>(primaryColVal, 1179720);
    /// <summary>box当lightType</summary>
    public int box => byteFileInfo.Get<int>(primaryColVal, 1245260);
    /// <summary>triger当shadow</summary>
    public int triger => byteFileInfo.Get<int>(primaryColVal, 1310800);
    /// <summary>是否是convex</summary>
    public int convex => byteFileInfo.Get<int>(primaryColVal, 1376340);
    /// <summary>rigi当cull</summary>
    public int rigi => byteFileInfo.Get<int>(primaryColVal, 1441880);
    /// <summary>boxX当r</summary>
    public float boxx => byteFileInfo.Get<float>(primaryColVal, 1507420);
    /// <summary>boxY当g</summary>
    public float boxy => byteFileInfo.Get<float>(primaryColVal, 1572960);
    /// <summary>boxZ当b</summary>
    public float boxz => byteFileInfo.Get<float>(primaryColVal, 1638500);
    /// <summary>boxCenterX</summary>
    public float boxcenterx => byteFileInfo.Get<float>(primaryColVal, 1704040);
    /// <summary>boxCenterY</summary>
    public float boxcentery => byteFileInfo.Get<float>(primaryColVal, 1769580);
    /// <summary>boxCenterZ</summary>
    public float boxcenterz => byteFileInfo.Get<float>(primaryColVal, 1835120);
    /// <summary>coliType当mode</summary>
    public int colitype => byteFileInfo.Get<int>(primaryColVal, 1900660);
    /// <summary>coliValue</summary>
    public int colivalue => byteFileInfo.Get<int>(primaryColVal, 1966200);
    /// <summary>coliCondition</summary>
    public int colicondition => byteFileInfo.Get<int>(primaryColVal, 2031740);
    /// <summary>nvX当intensity</summary>
    public float nvx => byteFileInfo.Get<float>(primaryColVal, 2097280);
    /// <summary>nvY</summary>
    public float nvy => byteFileInfo.Get<float>(primaryColVal, 2162820);
    /// <summary>nvZ</summary>
    public float nvz => byteFileInfo.Get<float>(primaryColVal, 2228360);
    /// <summary>lod</summary>
    public int lod => byteFileInfo.Get<int>(primaryColVal, 2293900);
    /// <summary>场景等级标记</summary>
    public int scenelv => byteFileInfo.Get<int>(primaryColVal, 2359440);
    /// <summary>光照贴图Index</summary>
    public string lightmapindex => byteFileInfo.Get<string>(primaryColVal, 2424980);
    /// <summary>光照贴图Child</summary>
    public string lightmapchild => byteFileInfo.Get<string>(primaryColVal, 2490520);
    /// <summary>光照贴图偏移X</summary>
    public string lightmapscaleoffsetx => byteFileInfo.Get<string>(primaryColVal, 2556060);
    /// <summary>光照贴图偏移Y</summary>
    public string lightmapscaleoffsety => byteFileInfo.Get<string>(primaryColVal, 2621600);
    /// <summary>光照贴图偏移Z</summary>
    public string lightmapscaleoffsetz => byteFileInfo.Get<string>(primaryColVal, 2687140);
    /// <summary>光照贴图偏移W</summary>
    public string lightmapscaleoffsetw => byteFileInfo.Get<string>(primaryColVal, 2752680);
    /// <summary>灯光路径动画</summary>
    public string lightanipath => byteFileInfo.Get<string>(primaryColVal, 2818220);
    /// <summary>物品的类型</summary>
    public int itemtype => byteFileInfo.Get<int>(primaryColVal, 2883760);
}
