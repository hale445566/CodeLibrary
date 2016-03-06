#API

[TOC]

##ChangeShader
高亮，改变物体shader。
拖到需要高亮的物体上使用，物体要有碰撞体。
###Variables
|变量|说明|
|-----|------|
|outLineShader|高亮的shader|
|defaultShader|默认shader|

##CoroutineUtil
协程工具类，当脚本不继承MonoBehaviour还想用协程时使用。
内部采用单例，当首次调用脚本时会在场景中生成“_CoroutineUtil”物体，并且添加此脚本。“_CoroutineUtil”物体跨场景时不会销毁。
注意：不要使用CoroutineUtil.Instance,直接使用静态方法即可。
###Static Functions
|方法|参数|返回值|说明|
|---|---|---|---|
|StartMonoCoroutine|**IEnumerator** enumerator|Coroutine|开启一个协程|
|WaitForRealSeconds|**float** seconds|Coroutine|真实时间等待函数，用于代替受timeScale影响的WaitForSeconds。seconds-等待的秒数|

##GetSystemTime
获得系统日期，获得系统时间。
###Static Functions
|方法|参数|返回值|说明|
|---|---|---|---|
|GetDate|null|string|获得系统日期，返回0000年00月00日|
|GetTime|null|string|获得系统时间，返回00时00分00秒|

##GZip
压缩解压byte[],dll在Plugins文件夹下ICSharpCode.SharpZipLib.dll
###Static Functions
|方法|参数|返回值|说明|
|---|---|---|---|
|Compress|**byte[]** data|byte[]|将字节数组进行压缩后返回压缩的字节数组。data-需要压缩的数组,返回压缩后的数组。|
|Decompress|**byte[]** data|byte[]|将字节数组进行压缩后返回压缩的字节数组。data-需要压缩的数组,返回压缩后的数组。|

##LoadAssetBundle
解压assetbundle
###Public Functions
|方法|参数|返回值|说明|
|---|---|---|---|
|LoadAssetBundle|**string[]** name,**System.Action`<object[]>`** callBack|void|解压AssetBundle，name-解压到文件名,callBack-回调。**注意：路径前要加file://**|

##Record
记录玩家位置旋转，保存成byte[]本低文件，没有经过压缩。
无需拖拽到场景内。
由PlayerRecord类播放。
###Public Functions
|方法|参数|返回值|说明|
|---|---|---|---|
|RecordCurrentFrame|**RecordCommond_Type** type, **Vector3** value|void|记录当点帧的动作。type-命令枚举,value-值。位置和旋转为分开记录。|
|SaveRecord|**string** fileName|void|保存记录到的所有数据。默认存在streamingassets下。fileName-文件名|

##PlayRecord
播放Record记录并保存的文件。
无需挂载脚本，首次调用会自动在场景中创建"PlayRecord"物体，并挂载脚本。
###Static Functions
|方法|参数|返回值|说明|
|---|---|---|---|
|Instance|null|PlayRecord|单例|

###Public Functions
|方法|参数|返回值|说明|
|---|---|---|---|
|Play|**Queue`<RecordTransform>`** record|void|采用协程播放记录的数据|

##RandomSortArray
随机排列数组,采用泛型。
###Static Functions
|方法|参数|返回值|说明|
|---|---|---|---|
|RandomSortArray|**T[]** array|void|对数组内元素进行随机排列|

##SortString
对string进行排列，采用冒泡排序算法。
###Static Functions
|方法|参数|返回值|说明|
|---|---|---|---|
|BubbleSort|**string** s|string|对string按字母顺序排列，返回排列好的string。|

##VoiceButton
发送语音，通过Microphone接收语音，通过rpc发送。
将语音片段转成byte[]。
###Public Functions
|方法|参数|返回值|说明|
|---|---|---|---|
|OnPress|**bool** isPress|void|点击按钮使用，会记录点击后到松开的语音数据。|