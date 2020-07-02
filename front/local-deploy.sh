#!/bin/bash
yarn run generate:stg
cp now.json dist
now -e NODE_ENV="staging" --token=t0sZu75UVI0Hpq2Ym4PgIC6c
now alias correparti-site-staging.now.sh --token=t0sZu75UVI0Hpq2Ym4PgIC6c
#cd ..