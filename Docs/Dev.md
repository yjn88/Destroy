Dev
==========
## 动机

最开始创建这个库的目的是为了脱离Unity引擎与艺术家，在程序员专属的控制台上制作游戏。

## 注意

* [语义化版本](https://semver.org/)

* [Windows Console](https://docs.microsoft.com/en-us/windows/console/)

* [Windows Console介绍](https://devblogs.microsoft.com/commandline/)

* [System.Console](https://docs.microsoft.com/en-us/dotnet/api/system.console?redirectedfrom=MSDN&view=netframework-4.7.2)

* [System.Console源代码](https://referencesource.microsoft.com/#mscorlib/system/console.cs)

* Media Control Interface教程:https://blog.csdn.net/wangqiulin123456/article/details/8231551

* 在线生成ascii字符画网站:https://www.jianshu.com/p/fca56d635091

* md的超链接不能加引号

* C/CPP项目最好使用多线程编译(MT)

* ┌ ┐ └ ┘ ─ │ ├ ┤ ┬ ┴ ┼

* 为什么不用属性
    ```cs
    public struct Score
    {
        public int Number;
    }
    public class Student
    {
        public Score Score { get; set; }
    }
    Student student = new Student();
    student.Score.Number = 1; //Error
    ```