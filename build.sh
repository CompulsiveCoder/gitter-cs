echo "Starting build for gitter project"
echo "Dir: $PWD"

DIR=$PWD

xbuild src/gitter.sln /p:Configuration=Release
