## XluaDemo
- 这是一个在unity中使用xlua的demo。
### 组成
- 在这个demo中有两个场景，分别是main和class1，class2没有意义。main中有两个按钮点击后分别加载class1和class2，因为功能一样因此只实现了class1按钮的功能。
- class1中的两个按钮可以控制场景中三个cube的旋转。

### 如何使用xlua
- 在main场景中，GameRoot下挂着GameManager脚本，负责加载当前场景中的UI prefab，这个prefab上的挂着LuaBehaviour脚本，这个脚本可以会从AssetBundle中加载当前GameObject同名的lua脚本。这块的思路是每个需要用到lua脚本的GameObject都挂一个LuaBehaviour脚本（这个脚本是xlua自带的，但是进行了修改），然后这个脚本负责加载GameObject同名的lua代码，这样做的原因是便于管理lua脚本。
- 在项目中AssetBundle的加载、场景的跳转这些基础功能还是使用C#写的，但是基本上这个功能是比较稳定的了，不需要进行修改。
- 按钮的事件绑定、基础功能函数的调用和场景中GameObject的控制都是在对应的lua代码中完成的。
