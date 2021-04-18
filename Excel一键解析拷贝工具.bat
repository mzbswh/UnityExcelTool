@echo off
cd %~dp0
::chcp 65001 能解决中文乱码，但有些系统文字会变为英文，因此使用转ANSI编码解决中文乱码

set toolExe=.\police\data\tools\ExcelParseTool\ExcelParseTool\bin\Release\ExcelParseTool.exe
set sourcePath=.\police\data\excelParseOutput\csCode
set byteFilePath=.\police\data\excelParseOutput\byteFiles

:: 第一个参数,用于生成字节文件 0:生成的文件不包含偏移信息 1:有每行行首的偏移数据 2:有每个单元格数据的偏移数据（相对行首的偏移）

del /s/Q %sourcePath%>null
del /s/Q %byteFilePath%>null
del /s/Q ..\pasture\Assets\Resources\data>null

:: 第二个参数,用于生成代码文件 
:: 0:生成的代码文件会读取所有数据并存在数组里，数组个数=列长，数组长度=有效数据行长
:: 1:只会解析数据里的行首偏移数据，因此读取时必须一次取一整行数据，即一个Model结构体 
:: 2:解析数据里的行首偏移数据，及行内元素相对行首的偏移，同时会生成每个变量的Get方法，
::	 因此支持取任意的一个数据，如dictSceneObj.Get_ScaleY(id) 可以只取出此id对应的ScaleY数据而不必取得完整的模型Model
:: *注意*：第一个参数一定是 >= 第二个参数
start /WAIT /B %toolExe% 0 0

xcopy /s/e/q/y %sourcePath%\*.* ..\pasture\Assets\hotfixScripts\AutoDict  
echo 已将所有的字典类和一个字典管理器拷贝到项目
xcopy /s/e/q/y .\police\data\excelParseOutput\byteFiles\*.* ..\pasture\Assets\Resources\data
echo 已将所有的字节数据文件拷贝到项目

del .\null

::xcopy /s/e/q/y %sourcePath%\*.* C:\Users\Administrator\Desktop\UnityTest\Assets\Scripts\AutoDict
::xcopy /s/e/q/y .\police\data\excelParseOutput\byteFiles\*.* C:\Users\Administrator\Desktop\UnityTest\Assets\Resources\data

pause