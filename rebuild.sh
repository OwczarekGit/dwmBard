#!/bin/bash

rm -rf dwmbard/bin/ dwmbard/obj/ dwmBard.tar
#dotnet restore .

# Build it.
dotnet publish -c Release -r linux-x64 -p:PublishSingleFile=true --self-contained=false

cd dwmbard/bin/Release/net5.0/linux-x64/publish/
pwd

tar -czvf dwmBard.tar ./*
mv dwmBard.tar ../../../../../../
cd ../../../../../../

clear
echo "Build complete!"
