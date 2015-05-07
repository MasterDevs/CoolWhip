$assemblyInfoFile = "./AssemblyInfo.cs"

$body = (Get-Content $assemblyInfoFile)

#-- Turn a string array into 1 big string (this way we can catch multi-line regular expressions just in case)
$body = [string]::Join("
", $body)

$body = $body -replace '\[assembly: AssemblyVersion', '

//These values will be replaced by the custom MS Build Task (Whippet) 
//Please see https://github.com/MasterDevs/CoolWhip for more info.

//$&'
$body = $body -replace '\[assembly: AssemblyFileVersion', '//$&'

Set-Content $assemblyInfoFile $body

echo "Commented out AssemblyVersion & AssemblyFileVersion"

#Todos
# 1. Prompt user for GitHub Owner & Repo name for this project (maybe store in App Config?)
# 2. Verify that works?
# 3. Store all text in csproj file
# 4. Add nuspec files 
# 5. Create a readme for the user to see as soon as the open the project
# 6. Create AppVeyor build file (put in root directory of git Repo or CSProj directory if can't be found)


#Modify CSProj file. Add the following elements

# Inside of <Project> add   <UsingTask AssemblyFile="$(PackageDir)\Path\To\MasterDevs.CoolWhip.dll" TaskName="Whippet" />

# I think we need this tag as well. Again it's inside of <Project> But having a dependency on MSBuildTools should alleviate this
# We just need to verify it's in the project
# <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />

# This file needs to be included, but we're hiding it from solution explorer. Hence the <false>
# Note this name must match the name in TempAssemblyFile for the whippet task look up exact text
# Also this needs to be in  the Compile ItemGroup (or should be at least)
# Another option, the more I think about it, is to make it a dependent file on AssemblyInfo.cs
# That way you can change it inside of VS.
# Hopefully you can have dependent files that don't live in the same folder. If not, we'll have to modify the .git ignore
# <Compile Include="Obj\WhippetAssemblyInfo.cs">
#	<IncludeInSolution>false</IncludeInSolution>
# </Compile>

# This is the actual task that'll run
#<Target Name="BeforeBuild">
#	<Whippet TempAssemblyFile="Obj\WhippetAssemblyInfo.cs" Version="$(AppVeyorTag)" Owner="MasterDevs" Repo="CoolWhip" UseLatestGitRelease="false"	 />
#</Target>