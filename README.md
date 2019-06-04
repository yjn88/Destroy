# Destroy

[![Build Status](https://img.shields.io/github/license/GreatDestroyerCharlie/Destroy.svg)](https://github.com/GreatDestroyerCharlie/Destroy/blob/master/LICENSE)
[![GitHub issues](https://img.shields.io/github/issues/GreatDestroyerCharlie/Destroy.svg)](https://github.com/GreatDestroyerCharlie/Destroy/issues)
[![Stars](https://img.shields.io/github/stars/GreatDestroyerCharlie/Destroy.svg?style=social)](https://github.com/GreatDestroyerCharlie/Destroy)

![logo](https://github.com/GreatDestroyerCharlie/Destroy/blob/master/Docs/logo.png)

### GreatDestroyerCharlie Presents (开发者还包括kyasever)

## 欢迎来到Destroy的储存库!

Destroy是一个使用C/C++，C#打造的控制台游戏引擎, 专为[Windows Console](https://github.com/microsoft/terminal)设计。
它提供了C#编写的API，并使用C/C++来控制Windows的conhost.exe(创建窗体界面并进行字体的渲染等)。它拥有其他控制台游戏引擎(玩具)不具备的特性

为什么我要创建这个库？
* 体验开发游戏引擎而不是局限于Unity
* 尝试C/C++与C#混合编程
* 帮助跟我一样的初学者学习编程
* 制作复古的ASCII风格游戏

你可以使用Destroy做出ASCII风格的游戏以及很酷的控制台应用程序，比如：音乐播放器，文本编辑器甚至是像素化的3D游戏

## 注意

* 项目已经通过了在64位Windows 10 1803与1809版本上的测试

* 项目依赖于 DotNet Framework 4.7.2

* 如果Destroy调用DestroyKernel时失败，请修改Destroy与DestroyKernel的目标平台，使它们保持一致并重新编译

* 为什么现在还没有可用的release发布？虽然引擎已经十分稳定，但是示例项目并不完善，所以1.0 release会跟随示例项目一起发布

## 示例

![Computer](https://github.com/GreatDestroyerCharlie/Destroy/blob/master/Docs/computer.gif)

![effect](https://github.com/GreatDestroyerCharlie/Destroy/blob/master/Docs/effect.gif)

## 特性

* 自定义调色盘
* 自定义字体以及字体大小
* 键盘 & 鼠标支持
* 高精度计时器
* 比Console.Write效率更高的打印方式
* 支持播放.mp3与.wav格式的音频
* 简易且容易扩展的的网络模块
* 简易的UI库
* 寻路系统
* 十分强大的CONSOLE类
* 十分强大的图形API
* 简单且扁平化的架构
* 中文注释

## 立即上手体验

* 首先你需要创建一个C#控制台项目(.Net Framework)
* 下载本项目的.zip文件并解压
* 编译Destroy.sln中的Destroy项目与DestroyKernel项目并拿到两个项目的.dll文件
* 也可以选择跳过上面两步，直接下载可用的[Release](https://github.com/GreatDestroyerCharlie/Destroy/releases)
* 将两个.dll文件放到C#控制台项目的bin文件夹里
* 在C#控制台项目中添加对Destroy.dll的引用(不需要添加对DestroyKernel.dll的引用)

    构造控制台
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

    游戏的生命周期
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

    输入与输出
    ``` cs
    void Start()
    {
        //窗口特效
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

## 文档

阅读[Wiki📖](https://github.com/GreatDestroyerCharlie/Destroy/wiki)以获取更多API的简介

## 更新蓝图

* 完善示例项目
* 编写README英文版
* 利用OpenGL实现各种格式的图像渲染

## 证书

该项目根据[MIT License](https://github.com/GreatDestroyerCharlie/Destroy/blob/master/LICENSE)授权

## 特别感谢

感谢[kyasever](https://github.com/kyasever)在合作时提供的技术支持

如果想贡献代码👍欢迎提交[Pull request](https://github.com/GreatDestroyerCharlie/Destroy/pulls)

如果想支持一下本项目的发展，别忘了给一个星🌟哦

## 推荐阅读

[ASCII Art](https://indienova.com/indie-game-news/ascii-art-games/)

[Roguelike](http://pre-sence.com/archives/roguelike-dossier)

[Dev文档](https://github.com/GreatDestroyerCharlie/Destroy/blob/master/Docs/Dev.md)