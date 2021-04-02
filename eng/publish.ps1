function publishForRuntime([string] $runtime) {

    # Publish.
    dotnet publish -r $runtime `
        -p:PublishSingleFile=true `
        --self-contained true `
        /p:PublishTrimmed=true `
        -c Release `
        ..\src\Convent.Cli\Convent.Cli.csproj

    # Rename published executable.
    $publishDirectory = "..\src\Convent.Cli\bin\Release\net5.0\$runtime\publish"
    renameForRuntime $runtime $publishDirectory

    # Get version.
    $Version = (Select-String -Path ..\Directory.Build.props -Pattern '(?<=<Version>).*(?=<\/Version>)').Matches.Value

    # Create versioned archive.
    Compress-Archive -Path "$publishDirectory\*" `
        -DestinationPath "..\release\Convent.Cli-$runtime-$Version.zip" `
        -Force
}

function renameForRuntime([string] $runtime, [string] $directory) {

    switch ($runtime) {
        'win-x64' {
            if (Test-Path "$directory\convent-cli.exe") {
                Remove-Item "$directory\convent-cli.exe"
            }

            Rename-Item "$directory\Convent.Cli.exe" 'convent-cli.exe'
            return
        }
        Default {
            if (Test-Path "$directory\convent-cli") {
                Remove-Item "$directory\convent-cli"
            }
            Rename-Item "$directory\Convent.Cli" 'convent-cli'
            return
        }
    }
}

Push-Location
Set-Location $PSScriptRoot

$runtimes = @('win-x64', 'linux-x64', 'osx-x64')

try {
    foreach ($runtime in $runtimes) {
        publishForRuntime $runtime
    }

}
finally {
    Pop-Location
}
