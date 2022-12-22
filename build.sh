#!/bin/bash
WorkDir=$(pwd)
echo "WorkDir='$WorkDir'"
echo "CI_COMMIT_SHA='$CI_COMMIT_SHA' | CI_COMMIT_SHORT_SHA='$CI_COMMIT_SHORT_SHA' | CI_COMMIT_TIMESTAMP='$CI_COMMIT_TIMESTAMP' | CI_COMMIT_BRANCH='$CI_COMMIT_BRANCH' | CI_COMMIT_TAG='$CI_COMMIT_TAG'"
OUTDIR="/var/share/out/cargo"
if [ -d "$OUTDIR" ]; then
	if [ -f "$OUTDIR/publish.$CI_COMMIT_SHA.zip" ]; then
		rm "$OUTDIR/publish.$CI_COMMIT_SHA.zip"
	fi	
else
  mkdir "$OUTDIR/"
fi

pphrase=$(</root/ci/nuget/pphrase.txt)
pnuget=`cat /root/ci/nuget/ppasw.txt | openssl enc -aes-256-cbc -md sha512 -a -d -pbkdf2 -iter 100000 -salt -pass pass:"$pphrase"`

dotnet dotnet nuget add source "https://gitlab.com/api/v4/projects/35045095/packages/nuget/index.json" -u gitlab+deploy-token-925568 -p "$pnuget" --store-password-in-clear-text
dotnet publish ./CargoService/Cargo.ServiceHost/Cargo.ServiceHost.csproj --configuration Release -r linux-x64 --framework net6.0 --self-contained false

#build info
sed -i -e "s~__build_hash__~$CI_COMMIT_SHA~g" ./VersionName.json
sed -i -e "s~__ver_build__~$CI_COMMIT_SHORT_SHA~g" ./VersionName.json
sed -i -e "s~__date_build__~$CI_COMMIT_TIMESTAMP~g" ./VersionName.json
sed -i -e "s~__commit_branch__~$CI_COMMIT_BRANCH~g" ./VersionName.json
if [[ -z "${CI_COMMIT_TAG}" ]]; then
  sed -i -e "s~__commit_tag__~$CI_COMMIT_TAG~g" ./VersionName.json
else 
  sed -i -e "s~__commit_tag__~no tag~g" ./VersionName.json
fi
#build

dotnet /root/resrep/ideal.common.resreplacer.dll "$WorkDir/CargoService/Cargo.ServiceHost/bin/Release/net6.0/linux-x64/publish/ideal.common.version.dll" "$WorkDir/CargoService/Cargo.ServiceHost/bin/Release/net6.0/linux-x64/publish/ideal.common.version.new.dll" IDeal.Common.Version.VersionName.resources "$WorkDir/VersionName.json"
if [ -f ./CargoService/Cargo.ServiceHost/bin/Release/net6.0/linux-x64/publish/ideal.common.version.new.dll ]; then
  rm ./CargoService/Cargo.ServiceHost/bin/Release/net6.0/linux-x64/publish/ideal.common.version.dll
  mv ./CargoService/Cargo.ServiceHost/bin/Release/net6.0/linux-x64/publish/ideal.common.version.new.dll ./CargoService/Cargo.ServiceHost/bin/Release/net6.0/linux-x64/publish/ideal.common.version.dll
else 
  echo "Couldn't find  resreplacer output file"
  echo "Exit."
  exit 1
fi

cd ./CargoService/Cargo.ServiceHost/bin/Release/net6.0/linux-x64/ && zip -r "$OUTDIR/publish.$CI_COMMIT_SHA.zip" ./publish/* && cd -
