sh increment-version.sh

mono lib/nuget.exe push pkg/gitter*nupkg -Source https://www.nuget.org/api/v2/package
