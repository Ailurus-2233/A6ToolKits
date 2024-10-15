let target_dir = "Libraries"

# Remove Libraries Folder
rm -rf $target_dir

# Create new Libraries Folder
mkdir ($target_dir + "/avalonia")
mkdir ($target_dir + "/a6toolkits")
mkdir ($target_dir + "/third-party")

# Copy Avalonia
mv Avalonia*.dll ($target_dir + "/avalonia")

# Copy A6Toolkits
mv A6ToolKits*.dll ($target_dir + "/a6toolkits");
mv A6ToolKits*.pdb ($target_dir + "/a6toolkits");
try { 
    mv A6ToolKits*.xml ($target_dir + "/a6toolkits"); 
} catch { 
    echo "No XML files found";     
}


# Copy Third Party
mv *.dll ($target_dir + "/third-party")

# The necessary DLL files need to be moved to the root directory
let dlls = [
    "/a6toolkits/A6ToolKits.dll",
    "/avalonia/Avalonia.Base.dll",
    "/avalonia/Avalonia.Controls.dll",
    "/third-party/A6Application.dll",
    "/third-party/Serilog.dll",
    "/third-party/Serilog.Sinks.Console.dll",
]

for dll in $dlls {
    cp ($target_dir + $dll) .
}

rm -f after_build.nu