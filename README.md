# Anime-Draw-project
 动漫人物AI绘画
 
# 写在前面
博主喜欢二次元，想着在读研期间做点对自己有意义的事，因此选择了动漫人物生成方向(呜呜呜，太感谢导师理解和支持了)。目前项目还有待优化，项目之后会慢慢更新。

# 源码
[https://github.com/hahahappyboy/Anime-Draw-project](https://github.com/hahahappyboy/Anime-Draw-project)
 
# 演示视频

[![](https://img-blog.csdnimg.cn/5e9ad16fe78446a18b5019eec23fd765.png)](https://player.bilibili.com/player.html?aid=909214797&bvid=BV1gM4y1U79m&cid=1073822589&page=1)




# 部分界面展示
**左边是画板，右边是AI生成的**

五等分的新娘
![在这里插入图片描述](https://img-blog.csdnimg.cn/5e9ad16fe78446a18b5019eec23fd765.png)
国家队02
![在这里插入图片描述](https://img-blog.csdnimg.cn/3d633f4798d948f4bd2400b40042e86f.png)


# 使用教程

本项目客户端是用Unity开发的，后端使用的是FlaskWeb

Unity版本：2020.3.28

后端：Flask+Pytorch

**（1）在github上下载本项目**

**（2）导入后端模型** 使用python编辑器(如pycharm)打开项目下的AnimeDrawFlask，这是后端代码，并安装项目需要的库主要是Flask和Pytorch。然后在网盘上下载checkpoints文件并放在AnimeDrawFlask目录下。

网盘链接: [https://pan.baidu.com/s/1TTB8GPN474qk9R8jtPmNBw?pwd=iimp](https://pan.baidu.com/s/1TTB8GPN474qk9R8jtPmNBw?pwd=iimp)

提取码: iimp 

![在这里插入图片描述](https://img-blog.csdnimg.cn/e992242d94254dfa8925e2118652d258.png)

**（3）运行后端程序** 运行AnimeDrawFlask下的main文件，如果控制台出现以下提示，就说明运行成功了。记住下面显示的IP地址，这就是后端程序的IP地址，之后Unity访问的就是这个IP地址。我这里是`http://192.168.42.130:5000/`，你们显示的IP地址一定不会跟我的一样，可以访问一下这个IP地址，会出现面码。

**注意：如果你电脑的显存小于12G请将config.py文件的`opt.gpu_ids`值改为-1，表示将会使用CPU运算AI绘画，值不为1会使用GPU** 

![在这里插入图片描述](https://img-blog.csdnimg.cn/2af013bc75724b2ea4c21eb49f7974a5.png)

**（4）打开Unity** 用Unity打开整个Anime-Draw-project，找到Assets/Resources/Scripts/Config.cs文件的`string DOWNLOAD_HTTP_URL`，将值改为刚刚后端显示的IP地址，这样Unity就能找到后端程序了

![在这里插入图片描述](https://img-blog.csdnimg.cn/34171761a08a42978d1e91fde5e57b21.png)

**（5）运行Unity**  找打Assets/Scenes/MainScene，运行MainScene

# 敬请期待

四月是你的谎言、未闻花名

 <img src="https://img-blog.csdnimg.cn/ac8993b6132c4839b0ab6adcd5b74e35.png" width="80%">
 
# 其他

**（1）人像卡通化APP**

博客:[https://blog.csdn.net/iiiiiiimp/article/details/118701276](https://blog.csdn.net/iiiiiiimp/article/details/118701276)

源码:[https://github.com/hahahappyboy/GANForCartoon](https://github.com/hahahappyboy/GANForCartoon)

 <img src="https://img-blog.csdnimg.cn/20210713192738503.png" width=" 60%">
 


# 写在后面

Emmm，还没想要写啥，先附个图吧，这是梦开始的地方

 <img src="https://img-blog.csdnimg.cn/1269aab0378d40aabc88256a3ac3d81d.jpeg" width=" 60%">
