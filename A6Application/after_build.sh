target_dir="Libraries"

# 删除 libs 文件夹
rm -rf $target_dir

# 创建一个新的 libs 文件夹
mkdir -p $target_dir/avalonia
mkdir -p $target_dir/a6toolkits
mkdir -p $target_dir/third-party

# 移动 Avalonia 的 dll 文件到 libs/avalonia 文件夹
mv ./Avalonia.* ./$target_dir/avalonia

# 移动 A6ToolKits 的 dll 文件到 libs/A6ToolKits 文件夹
mv ./A6ToolKits.*.dll ./$target_dir/a6ToolKits
mv ./A6ToolKits.*.pdb ./$target_dir/a6ToolKits

# 移动其他的 dll 文件到 libs/ThirdParty 文件夹 除了 A6ToolKits.dll
mv ./*.dll ./$target_dir/third-party

# 必要的 dll 文件需要移动到根目录

dlls=(
    '/third-party/A6ToolKits.dll'
    '/third-party/A6Application.dll'
    '/avalonia/Avalonia.Base.dll'
    '/avalonia/Avalonia.Controls.dll'
    '/third-party/Serilog.dll'
    '/third-party/Serilog.Sinks.Console.dll')

for dll in "${dlls[@]}"; do
    cp $target_dir"$dll" ./
done
