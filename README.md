# The Worst Engine(TWE) Document

## 本文档可以帮您快速上手我的TWE引擎

### Twe是什么？

Twe集成了很多关于游戏`Undertale`战斗系统的功能，可以帮您制作自己喜欢的AU

### Twe怎么用？

这是一段玩家移动的代码，非常的简单

TestEngine项目里有详细的代码，包括玩家移动、攻击检测、血条等系统

```C#
Twe Heart = Twe();
Heart.picturebox = heartp;
Heart.NewImage(".\\img\\red.png");
Heart.KeySet(Keys.Up, Keys.Down, Keys.Left, Keys.Right); // 设置移动按键 顺序是上,下,左,右
Heart.CanMove(true, 5); // 5 代表速度 默认就为5 有函数重载 可选
```
### 如何让不会编程的人制作自己的AU？
在目录 `TheWorstEngine\TestEngine\bin\Debug\scripts` 下

您可以看到一个名为`example.script`文件

这个文件可以用记事本编辑，里面的代码是可以被我制作的Scripter处理的

注意：.script里不可以用 `//` 来写注释，这里只是方便对脚本的解释而使用 `//` 符号代替注释

```
// 生成一个敌人，图片路径为.\img\gb\GB1.png，不需要带引号，但是必须带.\ 代表的路径是TestEngine.exe所在的路径
// 50 50 为图片的Height和Width
// 10 10 为图片初始所在坐标（由于是panel渲染，所以坐标轴是以玩家活动区域的左上角为(0, 0)来画的
Enemy = InitEnemy(.\img\gb\GB1.png, 50, 50, 10, 10)

// 让敌人移动到坐标(200, 200)处
// 别问龙骨炮该怎么办，以后再说(((
Enemy.MoveTo(200, 200)

// 等待5秒，差不多移动就会完成
Wait(5)

// 同上，生成一个敌人并且让它移动到(50, 50)处
Enemy2 = InitEnemy(.\img\gb\GB3.png, 50, 50, 10, 10)
Enemy2.MoveTo(50, 50)

// 等待5秒
Wait(5)

// 再Call第一个敌人，让它回到(100, 100)处
Enemy.MoveTo(100, 100)
```