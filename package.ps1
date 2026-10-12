$ErrorActionPreference = 'Stop'
& (Join-Path $PSScriptRoot 'build.ps1')
$taskVersion = '1.0.0-beta.1'
$taskArtifacts = Join-Path $PSScriptRoot 'artifacts'
$taskBundle = Join-Path $taskArtifacts ("EldenSpotter-$taskVersion-windows-x64")
New-Item -ItemType Directory -Path $taskBundle -Force | Out-Null
Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'src/EldenSpotter.exe'), (Join-Path $PSScriptRoot 'src/enemy-names.json') -Destination $taskBundle -Force
foreach ($taskName in @('README.md', 'LICENSE', 'THIRD_PARTY_NOTICES.md', 'RELEASE_NOTES.md')) {
  Copy-Item -LiteralPath (Join-Path $PSScriptRoot $taskName) -Destination $taskBundle -Force
}
$taskSource = Join-Path $taskBundle 'source'
New-Item -ItemType Directory -Path (Join-Path $taskSource 'src') -Force | Out-Null
foreach ($taskName in @('build.ps1', 'package.ps1', 'README.md', 'LICENSE', 'THIRD_PARTY_NOTICES.md', 'RELEASE_NOTES.md', '.gitignore')) {
  Copy-Item -LiteralPath (Join-Path $PSScriptRoot $taskName) -Destination $taskSource -Force
}
Get-ChildItem -LiteralPath (Join-Path $PSScriptRoot 'src') -File | Where-Object { $_.Extension -in @('.cs', '.json') } | Copy-Item -Destination (Join-Path $taskSource 'src') -Force
Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'docs') -Destination $taskBundle -Recurse -Force
Copy-Item -LiteralPath (Join-Path $PSScriptRoot 'docs') -Destination $taskSource -Recurse -Force
$taskZip = Join-Path $taskArtifacts ("EldenSpotter-$taskVersion-windows-x64.zip")
Compress-Archive -Path "$taskBundle/*" -DestinationPath $taskZip -Force
$taskHash = (Get-FileHash -LiteralPath $taskZip -Algorithm SHA256).Hash.ToLowerInvariant()
Set-Content -LiteralPath (Join-Path $taskArtifacts 'SHA256SUMS.txt') -Value "$taskHash  $(Split-Path $taskZip -Leaf)" -Encoding ASCII
Write-Output $taskZip
