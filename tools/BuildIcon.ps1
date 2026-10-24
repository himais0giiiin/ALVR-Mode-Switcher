param(
    [string]$ProjectRoot = (Split-Path -Parent $PSScriptRoot)
)

$ErrorActionPreference = 'Stop'

$svgPath = Join-Path $ProjectRoot 'Assets\app-icon.svg'
$pngPath = Join-Path $ProjectRoot 'Assets\app-icon-source.png'
$nextPngPath = Join-Path $ProjectRoot 'Assets\app-icon-source.next.png'
$icoPath = Join-Path $ProjectRoot 'Assets\app.ico'
$generatorProject = Join-Path $ProjectRoot 'tools\IconGenerator\IconGenerator.csproj'

$chromePaths = @(
    'C:\Program Files\Google\Chrome\Application\chrome.exe',
    'C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe',
    'C:\Program Files\Microsoft\Edge\Application\msedge.exe'
)
$browserPath = $chromePaths | Where-Object { Test-Path -LiteralPath $_ } | Select-Object -First 1

if (-not $browserPath) {
    throw 'Chrome or Edge is required to render the SVG.'
}
if (-not (Test-Path -LiteralPath $svgPath)) {
    throw "SVG not found: $svgPath"
}

Remove-Item -LiteralPath $nextPngPath -ErrorAction SilentlyContinue
$svgUri = ([Uri]$svgPath).AbsoluteUri

$browserArguments = @(
    '--headless=new',
    '--disable-gpu',
    '--hide-scrollbars',
    '--force-device-scale-factor=1',
    '--window-size=1024,1024',
    '--default-background-color=00000000',
    "--screenshot=$nextPngPath",
    $svgUri
)
Start-Process -FilePath $browserPath -ArgumentList $browserArguments -Wait | Out-Null

for ($attempt = 0; $attempt -lt 50 -and -not (Test-Path -LiteralPath $nextPngPath); $attempt++) {
    Start-Sleep -Milliseconds 100
}

if (-not (Test-Path -LiteralPath $nextPngPath)) {
    throw 'SVG rendering failed.'
}

Move-Item -LiteralPath $nextPngPath -Destination $pngPath -Force

& 'C:\Program Files\dotnet\dotnet.exe' run `
    --project $generatorProject `
    --configuration Release `
    -- $pngPath $icoPath

if ($LASTEXITCODE -ne 0) {
    throw 'ICO generation failed.'
}

Write-Output "Generated: $pngPath"
Write-Output "Generated: $icoPath"
