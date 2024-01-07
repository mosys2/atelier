# init-mongo.ps1

# اجرای Docker Compose
docker-compose up -d

# انتظار برای شروع MongoDB
Start-Sleep -Seconds 5

# دریافت نام کانتینر mongo1
$containerNameMongo1 = "endpointapi-mongo1-1"

# اجرای دستورات MongoDB با استفاده از mongosh
docker exec -it ${containerNameMongo1} mongosh --eval "rs.initiate({
  _id: 'myReplicaSet',
  version: 1,
  members: [
    { _id: 0, host: 'endpointapi-mongo1-1:27017' },
    { _id: 1, host: 'endpointapi-mongo2-1:27017' },
    { _id: 2, host: 'endpointapi-mongo3-1:27017' }
  ]
})"
