#!/bin/bash

./rebuild.sh
rm -rf ~/.local/share/dwmBard/
mv dwmBard.tar ~/.local/share/
cd ~/.local/share/
tar -xzvf dwmBard.tar --one-top-level
echo "Installed dwmBard!"
