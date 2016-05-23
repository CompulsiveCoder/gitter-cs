echo "Preparing for project"
echo "Dir: $PWD"

sudo apt-get update
sudo apt-get install -y git wget mono-complete

mozroots --import --sync
