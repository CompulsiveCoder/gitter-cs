#!/bin/bash
BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')

if [ "$BRANCH" = "master" ]
then
  echo "Packing and pushing nuget package"

  sh nuget-pack.sh && \
  sh nuget-push.sh
fi