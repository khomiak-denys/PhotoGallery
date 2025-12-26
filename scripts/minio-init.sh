#!/usr/bin/env sh
set -e

MINIO_ALIAS=${MINIO_ALIAS:-local}
MINIO_ENDPOINT=${MINIO_ENDPOINT:-http://minio:9000}
MINIO_ACCESS_KEY=${MINIO_ACCESS_KEY:-minioadmin}
MINIO_SECRET_KEY=${MINIO_SECRET_KEY:-minioadmin}
MINIO_BUCKET=${MINIO_BUCKET:-photogallery}

mc alias set "$MINIO_ALIAS" "$MINIO_ENDPOINT" "$MINIO_ACCESS_KEY" "$MINIO_SECRET_KEY"
mc mb -p "${MINIO_ALIAS}/${MINIO_BUCKET}" || true
mc cors set /init/minio-cors.json "${MINIO_ALIAS}/${MINIO_BUCKET}"
