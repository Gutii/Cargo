#!/bin/bash
PROD_SERVER=185.105.224.83
SERVICE_PATH="/var/production/ideal/cargo/"
echo "CI_COMMIT_SHA=$CI_COMMIT_SHA | GITLAB_USER_LOGIN=$GITLAB_USER_LOGIN"
ARTIFACT="/var/share/out/cargo/publish.$CI_COMMIT_SHA.zip"
if [ -f "$ARTIFACT" ]; then
  echo "'$ARTIFACT' exists."
else 
  echo "Couldn't find  '$ARTIFACT'"
  echo "Exit."
  exit 1
fi

pphrase=$(</root/ci/prod/pphrase.txt)
pprod=`cat /root/ci/prod/pprod.txt | openssl enc -aes-256-cbc -md sha512 -a -d -pbkdf2 -iter 100000 -salt -pass pass:"$pphrase"`

echo "copy artifact '$ARTIFACT' to prod server"
sshpass -p "$pprod" scp -o StrictHostKeyChecking=no "$ARTIFACT" root@"$PROD_SERVER":"$SERVICE_PATH"distrib/publish.zip

echo "exec remote command on prod-services"
sshpass -p "$pprod" ssh -o StrictHostKeyChecking=no -T root@"$PROD_SERVER" "cd $SERVICE_PATH; exec \$SHELL -l" <<'EOL'
	pwd
	./rebuild.sh
EOL

echo "Done."