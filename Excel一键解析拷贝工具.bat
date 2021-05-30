@echo off
cd %~dp0
::chcp 65001 能解决中文乱码，但有些系统文字会变为英文，因此使用转ANSI编码解决中文乱码

set toolExe=C:\Users\mzbswh\Desktop\Pub\ExcelToByteFile.exe

:: 第一个参数,用于生成字节文件 0:生成的文件不包含偏移信息 1:有每行行首的偏移数据 2:有每个单元格数据的偏移数据（相对行首的偏移）

start %toolExe% -e D:\Unity\Projects\Pasture\Tools\police\data\data -o .\out -c .\out
