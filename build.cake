#tool "nuget:?package=xunit.runner.console"

var target = Argument("target", "Default");

var solution = File("./bf3.sln");
var artifactsDirectory = Directory("./artifacts");
var configuration = "Release";

Task("Clean").Does(() =>
{
	CleanDirectory(artifactsDirectory);
});

Task("Restore").IsDependentOn("Clean").Does(() =>
{
	DotNetCoreRestore();
	//NuGetRestore(solution);
});

Task("Build").IsDependentOn("Restore").Does(() =>
{
	var projects = GetFiles("./src/**/*.xproj");
	foreach(var project in projects)
	{
		DotNetCoreBuild(
			project.GetDirectory().FullPath,
			new DotNetCoreBuildSettings()
			{
				Configuration = configuration
			}
		);
	}
});

Task("Test").IsDependentOn("Build").Does(() =>
{
	var projects = GetFiles("./tests/**/*.xproj");
	foreach(var project in projects)
	{
		Information("PROJECT: " + project.GetDirectory().FullPath);
	}
	//XUnit2("./tests/Blogifier.UnitTests/bin/Debug/netcoreapp1.0/Blogifier.UnitTests.dll",
	//	new XUnit2Settings {
	//		ToolPath = "./tools/xunit.runner.console/tools/"
	//	});

	//XUnit2("./tests/Blogifier.UnitTests/bin/Debug/netcoreapp1.0/Blogifier.UnitTests.dll");
});

Task("Pack").IsDependentOn("Test").Does(() =>
{
	Information("Pack complete");
});

Task("Default").IsDependentOn("Pack").Does(() =>
{
	Information("All done!");
});

RunTarget(target);