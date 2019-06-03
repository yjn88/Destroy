# Destroy

### GreatDestroyerCharlie Presents (开发者还包括kyasever)

## 欢迎来到Destroy的储存库!

Destroy是一个使用C/C++，C#打造的控制台游戏引擎, 专为[Windows Console](https://github.com/microsoft/terminal)设计。
它提供了C#编写的API，并使用C/C++来控制Windows的conhost.exe(创建窗体界面并进行字体的渲染等)。它拥有其他控制台游戏引擎(玩具)不具备的特性。

## 示例

![Computer](https://github.com/GreatDestroyerCharlie/Destroy/blob/master/Docs/computer.gif)

![Logo](https://github.com/GreatDestroyerCharlie/Destroy/blob/master/Docs/logo.gif)

## 特性

* 自定义调色盘
* 自定义字体以及字体大小
* 键盘 & 鼠标支持
* 高精度计时器
* 高性能CONSOLE类
* 支持播放.mp3与.wav格式的音频
* 简易且容易扩展的的网络模块
* 简易的UI库
* 寻路系统
* 十分强大的图形API
* 良好且扁平化的架构
* 中文注释

## 立即上手体验

* 首先你需要创建一个C#控制台项目(.Net Framework)
* 接下来编译DestroyKernel与Destroy或下载可用的[Release](https://github.com/GreatDestroyerCharlie/Destroy/releases)以获得动态链接库(dll), 并将Destroy.dll与DestroyKernel.dll一同放在控制台项目的bin文件夹里
* 在控制台项目中添加对Destroy.dll的引用

    构造控制台:
    ``` cs
    //构造控制台
    Graphics graphics = RuntimeEngine.Construct2
    (
        consoleType: ConsoleType.Default,
        bold: true,
        maximum: false,
        width: 50,
        height: 20,
        charWidth: CharWidth.Double,
        title: "Hello World"
    );
    ```

    开始游戏的生命周期:
    ``` cs
    //只被调用一次用于初始化游戏逻辑
    void Start()
    {
    }
    //每帧都会被调用
    void Update()
    {
    }
    //退出游戏循环时调用一次
    void Destroy()
    {
    }
    //开启生命周期
    RuntimeEngine.Start
    (
        onStart: Start, 
        onUpdate: Update, 
        onDestroy: Destroy, 
        fps: 60
    );
    ```

    输入与输出:
    ``` cs
    void Start()
    {
        //播放Logo
        Assets.MadeWithDestroy(true, 0, 0);
        //创建渲染网格
        graphics.CreatGridByString(Vector2.Zero, "你好世界", Colour.Red, Colour.Black);
    }

    void Update()
    {
        //进行渲染
        graphics.PreRender();
        graphics.Render();
        //用于检测按键输入
        if (Input.GetKeyDown(ConsoleKey.Escape))
        {
            RuntimeEngine.Exit();
        }
    }
    ```

## 其他文档

[前期版本](https://github.com/GreatDestroyerCharlie/Destroy/blob/master/Docs/README.md)

[Dev文档](https://github.com/GreatDestroyerCharlie/Destroy/blob/master/Docs/Dev.md)

## 文档

阅读[Wiki](https://github.com/GreatDestroyerCharlie/Destroy/wiki)以获取更多API的简介

## 证书

该项目根据[MIT License](https://github.com/GreatDestroyerCharlie/Destroy/blob/master/LICENSE)授权

## 特别感谢

感谢[kyasever](https://github.com/kyasever)在合作时提供的技术支持

如果想支持一下本项目的发展, 别忘了给一个星🌟哦