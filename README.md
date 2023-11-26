# Open E-Label

The Open E-Label project aims to provide a standardized and open-source solution for electronic labels on table wine bottles selled within the European Union. This initiative seeks to enhance transparency, efficiency, and information accessibility in the wine industry.

## Features

- **Electronic Labeling**: Replacing traditional paper labels with electronic labels, promoting sustainability and reducing waste.
- **Multilingual Support**: Ensuring labels can be displayed in multiple languages to accommodate diverse consumers within the EU.
- **Regulatory Compliance**: Adhering to EU regulations regarding wine labeling, providing a platform that streamlines compliance for wineries.

## How to Use

1. Scan the QR code on the wine bottle with a QR code scanner.
2. The E-label for the wine will be displayed on your device.
3. You can switch between languages using the language selection option on the E-label.

## Build & Push Docker image

To build the Open E-Label project using Docker, follow these steps:

1. Build the Docker image:

```shell
docker build -t fcarneiro/elabel:latest -f Dockerfile .
```

2. Push the image to a Docker repository:

```shell
docker push fcarneiro/elabel:latest
```

## Deployment

The web app can be deployed using the published Docker image. Here are the steps to run it with Docker compose:

1 Copy `docker-compose.yml` to server. Change server details as needed:

```shell
scp docker-compose.yml devops@build-101:~/elabel/
```

2 Start containers on server:

```shell
ssh devops@build-101

cd elabel/
docker-compose pull
docker-compose up -d
```

3 View Logs:

```shell
docker-compose logs -f
```

## Contributing

Contributions are welcome! Please read our contributing guidelines before getting started.

## License

Open E-Label is open-source under the MIT license. See the [LICENSE file](LICENSE.txt) for more information.
