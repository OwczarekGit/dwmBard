#!/bin/bash

./rebuild.sh
rm -rf ~/.local/share/dwmBard/
mv dwmBard.tar ~/.local/share/
curDir="$(pwd)"
cd ~/.local/share/
tar -xzvf dwmBard.tar --one-top-level
cd $curDir
echo "Installed dwmBard!"
