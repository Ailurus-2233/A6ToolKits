﻿namespace A6ToolKits.Modules;

public interface IModuleManager
{
    List<Type> GetModulesType();

    List<IModule> GetLoadedModules();
}