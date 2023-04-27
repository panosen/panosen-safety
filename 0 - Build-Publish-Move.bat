@echo off

dotnet restore

dotnet build --no-restore -c Release

move /Y Panosen.Safety\bin\Release\Panosen.Safety.*.nupkg D:\LocalSavoryNuget\

pause