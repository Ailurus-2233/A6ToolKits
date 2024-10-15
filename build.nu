# build script for A6Toolkits
cd Platform

# build all projects
dotnet build -c Release --no-restore
dotnet build -c Debug --no-restore

# Define target directories
let root_dir = pwd
let target_dir = $root_dir + "/../Output"
let release_dir = $target_dir + "/release"
let debug_dir = $target_dir + "/debug"

# Remove existing dll directories
rm -rf ($release_dir + "/dll")
rm -rf ($debug_dir + "/dll")

# Create new dll directories
mkdir ($release_dir + "/dll")
mkdir ($debug_dir + "/dll")

# Create nupkg directories if they don't exist
if not (echo ($release_dir + "/nupkg") | path exists) {
    mkdir ($release_dir + "/nupkg")
}

if not (echo ($debug_dir + "/nupkg") | path exists) {
    mkdir ($debug_dir + "/nupkg")
}

# Define projects array
let projects = [
    "/A6Toolkits",
    "/A6Toolkits.Database",
    "/A6Toolkits.Layout",
    "/A6Toolkits.MVVM",
    "/A6Toolkits.System",
    "/A6ToolKits.UIPackage"
]

# Iterate over each project
for project in $projects {
    let p_debug_dir = ($root_dir + $project + "/bin/Debug/")
    let p_release_dir = ($root_dir + $project + "/bin/Release/")

    # Copy Release files
    if (echo ($p_release_dir) | path exists) {
        cd $p_release_dir
        cp *.nupkg ($release_dir + "/nupkg/")

        if (echo ($p_release_dir + "/net8.0") | path exists) {
            cd ($p_release_dir + "/net8.0")
            cp *.dll ($release_dir + "/dll/")
            cp *.pdb ($release_dir + "/dll/")
        }
    }

    # Copy Debug files
    if (echo ($p_debug_dir) | path exists) {
        cd $p_debug_dir
        cp *.nupkg ($debug_dir + "/nupkg/")

        if (echo ($p_debug_dir + "/net8.0") | path exists) {
            cd ($p_debug_dir + "/net8.0")
            cp *.dll ($debug_dir + "/dll/")
            cp *.pdb ($debug_dir + "/dll/")
            cp *.xml ($debug_dir + "/dll/")
        }
    }
}

