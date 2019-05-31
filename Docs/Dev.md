Dev
==========
## 动机

最开始创建这个库的目的是为了脱离Unity引擎与艺术家，在程序员专属的控制台上制作游戏。

## 注意

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