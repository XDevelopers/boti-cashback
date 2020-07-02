#!/bin/bash

MSG=$(cat << "EOF"

--------------------------------------------------------

--------------------------------------------------------

        Welcome to Boticário Cashback Publish process

--------------------------------------------------------

EOF
)
echo "$MSG"

# Get Path from this shell is executed
ROOT_DIR=$(pwd)

MSG=$(cat << "EOF"

--------------------------------------------------------
        Boticário Cashback | CASH - Building the App
--------------------------------------------------------
EOF
)
echo "$MSG"
echo
# Start build
echo "Start build"
yarn build
echo "Set up the Environments"
yarn generate:prd
chmod 777 dist
cd dist/
echo correparti-admin-staging.surge.sh > CNAME
echo BASE_API_URL=https://correparti-api-staging.now.sh/api > .env
echo BASE_API_URL=https://correparti-api-staging.now.sh/api > .env.production
yarn generate:stg
echo
MSG=$(cat << EOF


--------------------------------------------------------
        Boticário Cashback | CASH - Publishing the App
--------------------------------------------------------
EOF
)
echo "$MSG"
surge $(pwd)
MSG=$(cat << EOF


--------------------------------------------------------
        Boticário Cashback | CASH - Deployment was done!
--------------------------------------------------------
EOF
)
cd ..
rm -rf ./dist/