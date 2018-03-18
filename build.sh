echo "Starting build for gitter project"
echo "Dir: $PWD"

DIR=$PWD

msbuild src/gitter.sln /p:Configuration=Release
