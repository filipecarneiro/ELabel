# Open E-Label

The Open E-Label project aims to provide a standardized and open-source solution for electronic labels on table wine bottles selled within the European Union. This initiative seeks to enhance transparency, efficiency, and information accessibility in the wine industry.

## Features

- **Electronic Labeling**: Replacing traditional paper labels with electronic labels, promoting sustainability and reducing waste.
- **Multilingual Support**: Ensuring labels can be displayed in multiple languages to accommodate diverse consumers within the EU.
- **Regulatory Compliance**: Adhering to EU regulations regarding wine labeling, providing a platform that streamlines compliance for wineries. [EU 2021/2117](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02021R2117-20211206) amending Regulations:
	- [EU 1308/2013](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02013R1308-20231208&qid=1701283989850) establishing a common organisation of the markets in agricultural products
	- [EU 1151/2012](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02012R1151-20220608&qid=1701284230571) on quality schemes for agricultural products and foodstuffs
	- [EU 251/2014](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02014R0251-20231208&qid=1701284265305) on the definition, description, presentation, labelling and the protection of geographical indications of aromatised wine products
	- [EU 228/2013](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02013R0228-20211207&qid=1701284298071) laying down specific measures for agriculture in the outermost regions of the Union

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
