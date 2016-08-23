echo "Testing datamanager project from github"
echo "  Current directory:"
echo "  $PWD"

BRANCH=$1

if [ -z "$BRANCH" ]; then
    BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')
fi

if [ -z "$BRANCH" ]; then
    BRANCH="master"
fi

echo "  Branch: $BRANCH"

DIR=$PWD

# If the .tmp/gitter-cs directory exists then remove it
if [ -d ".tmp/gitter-cs" ]; then
    rm .tmp/gitter-cs -rf
fi

git clone https://github.com/CompulsiveCoder/gitter-cs.git .tmp/gitter-cs --branch $BRANCH
cd .tmp/gitter-cs && \
sh init-build-test.sh && \
cd $DIR && \
rm .tmp/gitter-cs -rf
