echo "Getting libraries"
echo "Dir: $PWD"

cert-sync --quiet /etc/ssl/certs/ca-certificates.crt

NUGET_FILE="nuget.exe"

if [ ! -f "$NUGET_FILE" ];
then
    wget http://nuget.org/nuget.exe
fi

mono nuget.exe update -self

mono nuget.exe install nunit -version 2.6.4
mono nuget.exe install nunit.runners -version 2.6.4
