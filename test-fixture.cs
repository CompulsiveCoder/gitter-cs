FIXTURE=$1

mono lib/NUnit.Runners.2.6.4/tools/nunit-console.exe --run=$1 bin/Release/*.dll
