using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using Serilog;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable AllUnderscoreLocalParameterName

[GitHubActions(
    "continuous",
    GitHubActionsImage.UbuntuLatest,
    FetchDepth = 0,
    On = [GitHubActionsTrigger.Push],
    PublishArtifacts = true,
    InvokedTargets = [nameof(Compile), nameof(Publish)])]
class Build : NukeBuild
{
    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    bool RunFormatAnalyzers => false;
    string[] Platforms => ["osx-x64", "linux-x64", "win-x64"];
    
    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    AbsolutePath PackagesDirectory => ArtifactsDirectory / "packages";
    
    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("*/bin", "*/obj").DeleteDirectories();
            TestsDirectory.GlobDirectories("*/bin", "*/obj").DeleteDirectories();
            ArtifactsDirectory.CreateOrCleanDirectory();
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s.SetProjectFile(Solution));
        });

    Target VerifyFormat => _ => _
        .After(Restore)
        .Description("Verify code formatting for the solution.")
        .Executes(() =>
        {
            DotNet($"format whitespace {Solution.Path} --verify-no-changes ");
            DotNet($"format style {Solution.Path} --verify-no-changes ");

            if (RunFormatAnalyzers)
            {
                DotNet($"format analyzers {Solution.Path} --verify-no-changes ");
            }
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });
    
    Target Publish => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            var salesPitch = Solution.AllProjects.First(x => x.Name.Contains("SalesPitch"));

            Platforms.ForEach(runtime =>
            {
                DotNetPublish(s => s
                    .SetProject(salesPitch)
                    .SetConfiguration(Configuration)
                    .SetRuntime(runtime)
                    .SetSelfContained(true)
                    .EnablePublishSingleFile()
                    .SetOutput(ArtifactsDirectory / "publish" / runtime));
            });
        });
}
