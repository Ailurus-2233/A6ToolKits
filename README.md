# A6ToolKits

一个基于 Avalonia 框架的桌面端应用程序快速开发工具，由多个模块构成，每个模块都是一个独立的工具，可以单独使用，也可以组合使用。

## 项目预览

## 模块说明

1. `A6ToolKits.Core` 是整个项目的核心模块，提供了一些基础的功能和服务，包括：
   - 启动引导 Bootstrapper
   - 基本定义 Common（Action、Attribute、Behavior 等）
   - 模块加载器 ModuleLoader
   - 其他帮助类 Helper
     - 程序集加载器 AssemblyHelper
     - 配置文件管理器 ConfigHelper
     - 日志记录器 LoggerHelper
     - 资源管理器 ResourceHelper
     - 实例管理器 InstanceHelper
2. `A6ToolKits.Layout` 提供了一个简单的布局方案，可以用于快速构建界面布局。
   - 定义者 Definer
     - 布局定义器 LayoutDefiner
     - 菜单定义器 MenuDefiner
     - 状态栏定义器 StatusBarDefiner
     - 工具栏定义器 ToolBarDefiner
     - 页面定义器 PageDefiner
   - 生成器 Generator
3. `A6ToolKits.MVVM` 基于 CommunityToolkit.Mvvm 的 MVVM 框架，提供了一些基础的 MVVM 功能和服务。
   - 基本定义 Common（ViewModelBase 等）
   - IoC 服务
4. `A6ToolKits.UIPackage` 开发过程中，所需要的一些简单控件与样式，后续会补充该模块内容
5. `A6ToolKits.Database` 数据库操作模块，提供了一些基础的数据库操作功能和服务。
6. `A6ToolKits.System` 系统操作模块，提供了一些基础的系统操作功能和服务。

## 如何使用


## 开发状态

本项目目前处于开发阶段，尚未发布第一个稳定版本。如果您对本项目感兴趣，欢迎您参与项目的开发和测试，或者提出宝贵的意见和建议。

**模块开发状态** 

- [x] `A6ToolKits.Core`
- [x] `A6ToolKits.Layout`
- [x] `A6ToolKits.MVVM`
- [ ] `A6ToolKits.UIPackage`
- [ ] `A6ToolKits.Database`
- [ ] `A6ToolKits.System`

**项目开发状态**
- [ ] UIDemo
- [ ] A6Application
- [ ] A6SpendLog

## 开源许可证

本项目使用 GPL 3.0 许可证进行授权，请查看 [这里](https://www.gnu.org/licenses/gpl-3.0.html) 获取 GPL 3.0 许可证的详细信息。

> 使用须知
>
> 根据 GPL 3.0 许可证的要求，您在使用、修改或分发本项目时必须遵守许可证的规定。请确保您已阅读并理解许可证的条款和条件，并遵守以下要求：
>
> - 在**任何衍生创作中都必须保留 GPL 3.0 许可证**，并提供源代码和相应的授权文件。
> - 如果您对本项目进行了修改或衍生创作，并进行了分发，您需要在相应的授权文件中明确说明您对原始软件所做的修改，并将修改后的软件以
    GPL 3.0 许可证进行分发。
> - 如果您使用本项目创建了其他软件，并且不以 GPL 3.0 许可证进行分发，您不能要求其他人仅仅因为使用您的软件而受到 GPL 3.0
    许可证的限制。
>
> 如果您对项目或许可证有任何疑问或需要更多信息，请联系作者（邮箱：[Ailurus2233@outlook.com](mailto:ailusu2233@outlook.com)）。
