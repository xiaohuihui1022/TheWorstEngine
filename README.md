# TheWorstEngine
# 世界上最垃圾的游戏引擎
## 它甚至不能称为游戏引擎
## 做着玩的，不要挑刺
# 警告
在您启动项目的时候肯定会报错，因为img不想外漏，所以请替换成自己的img

一共需要5张图片，需要自己新建img文件夹，然后将如下图片放进去，如果没有就随便搞几张图片凑活

sans动画4张，命名为sans1-4.png

决心(UT里的红心)图片一张，命名为red.png

# 1.2大更新
### 现在可以进行移动的操作
### 只需要如下代码
```C#
Twe Heart = Twe();
Heart.picturebox = heartp;
Heart.NewImage(".\\img\\red.png");
Heart.KeySet(Keys.Up, Keys.Down, Keys.Left, Keys.Right); // 设置移动按键 顺序是上,下,左,右
Heart.CanMove(true, 5); // 5 代表速度 默认就为5 有函数重载 可选
```
# 1.3小更新
## 现在可以四面八方移动了
### 好耶（bushi）
#### 还有一个根本没写完的音乐sys(
##### 优化了一下代码结构
###### 没了(逃)
