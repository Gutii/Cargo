#!/bin/bash
DEV_SERVER=185.105.226.107
SERVICE_PATH="/var/development/ideal/cargo/"
echo "CI_COMMIT_SHA=$CI_COMMIT_SHA | GITLAB_USER_LOGIN=$GITLAB_USER_LOGIN"
ARTIFACT="/var/share/out/cargo/publish.$CI_COMMIT_SHA.zip"
if [ -f "$ARTIFACT" ]; then
  echo "'$ARTIFACT' exists."
else 
  echo "Couldn't find  '$ARTIFACT'"
  echo "Exit."
  exit 1
fi

pphrase=$(</root/ci/dev/pphrase.txt)
pdev=`cat /root/ci/dev/pdevel.txt | openssl enc -aes-256-cbc -md sha512 -a -d -pbkdf2 -iter 100000 -salt -pass pass:"$pphrase"`

#copy artifact to dev server
sshpass -p "$pdev" scp -o StrictHostKeyChecking=no "$ARTIFACT" root@"$DEV_SERVER":"$SERVICE_PATH"distrib/publish.zip

#exec remote command on dev-services

sshpass -p "$pdev" ssh -o StrictHostKeyChecking=no -T root@"$DEV_SERVER" "cd $SERVICE_PATH; exec \$SHELL -l" <<'EOL'
	pwd
	./rebuild.sh
EOL

echo "Done."