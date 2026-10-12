$ErrorActionPreference = 'Stop'
$taskCompiler = Join-Path $env:WINDIR 'Microsoft.NET\Framework64\v4.0.30319\csc.exe'
Push-Location (Join-Path $PSScriptRoot "src")
try {
  & $taskCompiler /nologo /target:winexe /platform:x64 /optimize+ /out:EldenSpotter.exe /reference:System.Windows.Forms.dll /reference:System.Drawing.dll /reference:System.Web.Extensions.dll EnemyReader.cs NpcDamage.cs StatusSusceptibility.cs Overlay.cs NativeHud.cs Tests.cs DamageTests.cs StatusTests.cs AssemblyInfo.cs
  if ($LASTEXITCODE -ne 0) { throw 'Compilation failed.' }
} finally { Pop-Location }
