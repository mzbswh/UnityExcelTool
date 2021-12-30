# Unity导表工具

## 概述
&nbsp;&nbsp;&nbsp;&nbsp;此工具是一个高性能的Unity读表工具，将excel转为字节文件并在Unity内读取数据。支持多种c#基本数据类型以及List，Vector和Dictionary。最大的特点是可直接读取任一Excel对应单元格的数据而不必先缓存所有数据。

## 用法
1. Excel格式<br/>
   ![excel示例]("C:\Users\mzbswh\Desktop")
   * 默认保留第一行第一列
   * 第一个单元格为[Sheet表关键字](#Sheet关键字)，用于控制这个sheet表的导出设置。
   * 第一行为[列标签](#列标签)，用于定义此列的一些属性，为空代表数据列。如果列标签为空且此列的类型为空，则认为前一列为最后
  一列并忽略此列及之后所有列。因此如果最后一个有效列前如果有空列必须标记为注释列
   * 第一列为[行标签](#行标签)，用于定义此行的一些属性，为空代表数据列。如果行标签为空且主列对应的单元格为空，则认为前一行行为最后一行并忽略此行及之后所有行。因此如果最后一个有效行前如果有空行必须标记为注释行。
2. Unity数据读取
   * 核心代码位于UnityProject/Assets/Scripts/ExcelTool。
   * ExcelDataMgr用于控制所有的字节数据读取，可通过此类的一些静态方法直接读取数据，Init()方法进行数据初始化。
   * 如果有设置为需要缓存的sheet表，会生成一个ExcelDataCacheMgr，一定要先执行ExcelDataMgr.Init()，再执行ExcelDataCacheMgr.CacheData()缓存数据。

## Sheet关键字
&nbsp;&nbsp;&nbsp;&nbsp;sheet关键字用于控制sheet表的整体导出设置，只针对当前sheet表生效。所有关键字不区分大小写并忽略空格。

关键字有两种类型写法
1. bool类型：此类型关键字可通过在前面加 ! 取反，类似c#bool写法，也可使用第二种类型写法。
2. key-value类型，写法为key=value形式。

关键字定义：

+ Export(default: true)：决定此sheet表是否需要导出。
+ ExportName(default: 空)：如果为空，导出名称为ExcelName_SheetName(一个excel有多个需要导出的sheet)(注意此时sheet名字不能重复)， ExcelName(只有一个sheet需要导出)
+ Cache(default: false)：决定是否缓存此sheet所有数据。
+ Optimize(default: true)：是否优化数据加载，具体介绍查看[数据加载优化](#数据加载优化)。
+ [Extra]：此关键字下可添加任意key-value对，并且这些内容将原封不动的导出为Dictionary<string, string>数据，可在unity内获取这些数据信息。

## 行标签
&nbsp;&nbsp;&nbsp;&nbsp;所有行标签不区分大小写并忽略空格
+ None：最后一个有效行前代表是有效数据行
+ Type：[元素类型](#元素类型)行，必须存在。
+ Name：必须存在，定义变量名称，单个sheet内不可重复。
+ Comment：可选，生成的代码中将此行用作变量注释。
+ #/Note：代表此行为注释行，生成工具会自动忽略此行。

&nbsp;&nbsp;&nbsp;&nbsp;※&nbsp;由于解析数据前必须先遍历获取类型及名称行，为了减少遍历次数，此工具限定如果Type，Name或Comment存在，必须位于此sheet前20行(不包括默认保留的第一行)。

## 列标签
&nbsp;&nbsp;&nbsp;&nbsp;所有列标签不区分大小写并忽略空格
+ None：最后一个有效列前代表是有效数据列
+ Primary/Key：主列，必须存在，获取数据是通过主列值获取的。
+ #/Note：代表此列为注释列，生成工具会自动忽略此列。

## 数据加载优化
&nbsp;&nbsp;&nbsp;&nbsp;由于数据的获取是通过在字节文件的偏移值获取，所以在初始化加载数据时必须先读取所有主列数据值以及主列对应的偏移值，以此确定每一个元素的偏移值。如果sheet的优化选项开启并符合优化条件，如sheet的主列值是连续的数值，则此时只需要记录第一个主列值及偏移值，还有连续数值的步长，即可得到所有元素的偏移值，此时就不必先获取所有的主列值，数据的初始化也会用时更短。