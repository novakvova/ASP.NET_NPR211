# npr211-asp

Create docker hub repository - publish
```
docker build -t npr211-asp-api . 
docker run -it --rm -p 5184:80 --name npr211-asp_container npr211-asp-api
docker run -d --restart=always --name npr211-asp_container -p 5184:80 npr211-asp-api
docker run -d --restart=always -v d:/volumes/npr211-asp/uploading:/app/uploading --name npr211-asp_container -p 5184:80 npr211-asp-api
docker run -d --restart=always -v /volumes/npr211-asp/uploading:/app/uploading --name npr211-asp_container -p 5184:80 npr211-asp-api
docker ps -a
docker stop npr211-asp_container
docker rm npr211-asp_container

docker images --all
docker rmi npr211-asp-api

docker login
docker tag npr211-asp-api:latest novakvova/npr211-asp-api:latest
docker push novakvova/npr211-asp-api:latest

docker pull novakvova/npr211-asp-api:latest
docker ps -a
docker run -d --restart=always --name npr211-asp_container -p 5184:80 novakvova/npr211-asp-api

docker run -d --restart=always -v /volumes/npr211-asp/uploading:/app/uploading --name npr211-asp_container -p 5184:80 novakvova/npr211-asp-api


docker pull novakvova/npr211-asp-api:latest
docker images --all
docker ps -a
docker stop npr211-asp_container
docker rm npr211-asp_container
docker run -d --restart=always --name npr211-asp_container -p 5184:80 novakvova/npr211-asp-api
```

```nginx options /etc/nginx/sites-available/default
server {
    server_name   api-npr211-asp.itstep.click *.api-npr211-asp.itstep.click;
    location / {
       proxy_pass         http://localhost:5184;
       proxy_http_version 1.1;
       proxy_set_header   Upgrade $http_upgrade;
       proxy_set_header   Connection keep-alive;
       proxy_set_header   Host $host;
       proxy_cache_bypass $http_upgrade;
       proxy_set_header   X-Forwarded-For $proxy_add_x_forwarded_for;
       proxy_set_header   X-Forwarded-Proto $scheme;
    }
}

server {
		server_name   qubix.itstep.click *.qubix.itstep.click;
		root /var/dist;
		index index.html;

		location / {
			try_files $uri /index.html;
			#try_files $uri $uri/ =404;
		}
}

server {
		server_name   admin-qubix.itstep.click *.admin-qubix.itstep.click;
		root /var/admin-qubix.itstep.click;
		index index.html;

		location / {
			try_files $uri /index.html;
			#try_files $uri $uri/ =404;
		}
}

sudo systemctl restart nginx
certbot
```

/var/api-qubix.itstep.click/