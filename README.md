# TheWorstEngine
# 世界上最垃圾的游戏引擎
# 它甚至不能称为游戏引擎
# 做着玩的，不要挑刺
## 1.2大更新
### 现在可以进行移动的操作
### 只需要如下代码
```C#
Twe Heart = Twe();
Heart.picturebox = heartp;
Heart.NewImage(".\\img\\red.png");
Heart.KeySet(Keys.Up, Keys.Down, Keys.Left, Keys.Right); // 设置移动按键 顺序是上,下,左,右
Heart.CanMove(true, 5); // 5 代表速度 默认就为5 有函数重载 可选
```
