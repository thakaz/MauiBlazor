Get-ChildItem -Path . -Recurse -File | Where-Object {$_.extension -in @(".txt", ".cs", ".html", ".css", ".js", ".razor", ".cshtml")} | ForEach-Object {
    $content = Get-Content -Path $_.FullName -Encoding UTF8
    if ($content -like "\uFEFF*") {
        $content = $content.TrimStart("\uFEFF")
        Set-Content -Path $_.FullName -Value $content -Encoding UTF8 -NoNewline
        Write-Host "BOM removed from $($_.FullName)"
    }
}