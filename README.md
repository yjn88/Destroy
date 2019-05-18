# Destroy

### GreatDestroyerCharlie Presents

[English]("https://github.com/GreatDestroyerCharlie/Destroy/blob/master/Docs/README.md")

欢迎来到`Destroy`的储存库! 

这是一个主要使用`C#`打造的游戏引擎, 专为`Windows Console`(conhost.exe)设计

## 立即上手:

* 首先你需要创建一个C#控制台项目(DotNet Framework)
* 接下来编译DestroyKernel与Destroy或下载[Release]("")以获得动态链接库(dll), 并将它们放在控制台项目的bin文件夹里
* 在控制台项目中添加对Destroy.dll的引用

`构造控制台`:
``` cs
namespace Example
{
    using Destroy;
    using Destroy.Kernel;

    public class Program
    {
        private static void Main()
        {
            //构造控制台
            RuntimeEngine.Construct
            (
                consoleType: ConsoleType.Default,
                maximum: false,
                width: 60,
                height: 30,
                title: "Destroy"
            );
            CONSOLE.ReadKey();
        }
    }
}
```
`开始游戏的生命周期`:
``` cs
RuntimeEngine.Start(onStart: null, onUpdate: null, onDestroy: null, fps: 60);
```
`输入与输出`:
``` cs
        private static void Start()
        {
            Graphics graphics = new Graphics(30, 30, CharWidth.Double);
            graphics.SetGridByString(Vector2.Zero, "你好世界!", ConsoleColor.Red, ConsoleColor.Black);
            graphics.Render();
        }

        private static void Update()
        {
            if (Input.GetKeyDown(ConsoleKey.Escape))
            {
                RuntimeEngine.Exit();
            }
        }
```
`还有更多!` 阅读[wiki]("https://github.com/GreatDestroyerCharlie/Destroy/wiki")以获取更多API的简介

## 链接:
    1.Roguelike:
        http://pre-sence.com/archives/roguelike-dossier

    2.Semantic Versioning:
        https://semver.org/

    3.Windows Console:
        https://docs.microsoft.com/en-us/windows/console/

    4.Windows Console's Introducing:
        https://devblogs.microsoft.com/commandline/
    
    5.System.Console:
        https://docs.microsoft.com/en-us/dotnet/api/system.console?redirectedfrom=MSDN&view=netframework-4.7.2

    6.System.Console's Source Code:
        https://referencesource.microsoft.com/#mscorlib/system/console.cs