#!/bin/sh

echo "Incrementing version"

CURRENT_VERSION=$(cat version.txt)

echo "Current: $CURRENT_VERSION"
echo $CURRENT_VERSION | awk -F. -v OFS=. 'NF==1{print ++$NF}; NF>1{if(length($NF+1)>length($NF))$(NF-1)++; $NF=sprintf("%0*d", length($NF), ($NF+1)%(10^length($NF))); print}' > version.txt

NEW_VERSION=$(cat version.txt)

echo "New version: $NEW_VERSION"