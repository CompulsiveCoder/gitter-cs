#!/bin/bash
BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')

git commit -am "Updated version [skip ci]"
git push origin $BRANCH