@echo off
ilasm ShimSources.il /dll
ilverify ShimSources.dll -r "C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.5\*.dll" --ignore-error UnmanagedPointer