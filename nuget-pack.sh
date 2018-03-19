mkdir -p pkg
mkdir -p pkg/archive

mv -f pkg/*.nupkg pkg/archive

VERSION=$(cat version.txt)

BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')

if [ "$BRANCH" = "dev" ]
then
    VERSION="$VERSION-dev"
fi

mono lib/nuget.exe pack Package.nuspec -version $VERSION -OutputDirectory pkg/
