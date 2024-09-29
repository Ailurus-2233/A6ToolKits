target_dir='./Output'
relase_dir=$target_dir/release
debug_dir=$target_dir/debug

rm -rf $relase_dir/dll
rm -rf $debug_dir/dll
mkdir -p $relase_dir/dll
mkdir -p $debug_dir/dll



projects=(
    './A6Toolkits'
    './A6Toolkits.Database'
    './A6Toolkits.Layout'
    './A6Toolkits.MVVM'
    './A6Toolkits.System'
    './A6ToolKits.UIPackage'
)

for project in ${projects[@]}; do
    if [ -d $project/bin/Debug/net8.0 ]; then
        cp $project/bin/Debug/net8.0/*.dll $debug_dir/dll
        cp $project/bin/Debug/net8.0/*.pdb $debug_dir/dll
    fi

    if [ -d $project/bin/Release/net8.0 ]; then
        cp $project/bin/Release/net8.0/*.dll $relase_dir/dll
        cp $project/bin/Release/net8.0/*.pdb $relase_dir/dll
    fi
    
    cp $project/bin/Debug/*.nupkg $debug_dir/nupkg
    cp $project/bin/Release/*.nupkg $relase_dir/nupkg
done