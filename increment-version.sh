#!/bin/sh

echo "Incrementing version"

CURRENT_VERSION=$(cat buildnumber.txt)

echo "Current: $CURRENT_VERSION"

#CURRENT_VERSION=='expr $CURRENT_VERSION + 1'
CURRENT_VERSION=$(($CURRENT_VERSION + 1))
#echo $CURRENT_VERSION | awk 'BEGIN { FS=":" } { $3++;  if ($3 > 99) { $3=0; $2++; if ($2 > 99) { $2=0; $1++ } } } { printf "%02d:%02d:%02d\n", $1, $2, $3 }'
#echo $CURRENT_VERSION | awk -F. -v OFS=. 'NF==1{print ++$NF}; NF>1{if(length($NF+1)>length($NF))$(NF-1)++; $NF=sprintf("%0*d", length($NF), ($NF+1)%(1000^length($NF))); print}' > version.txt

#NEW_VERSION=$(cat version.txt)

echo "New version: $CURRENT_VERSION"

echo $CURRENT_VERSION > buildnumber.txt