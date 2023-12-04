<div align="center">
<img src="wwwroot/img/icon.svg" width="100" height="100">
</div>

![GitHub License](https://img.shields.io/github/license/filipecarneiro/ELabel)
![Static Badge](https://img.shields.io/badge/Docker-Image-brightgreen?link=https%3A%2F%2Fhub.docker.com%2Fr%2Ffcarneiro%2Felabel)

# Open E-Label

The Open E-Label project aims to provide a standardized and open-source solution for electronic labels on table wine bottles selled within the European Union. This initiative seeks to enhance transparency, efficiency, and information accessibility in the wine industry.

## Features

- **Electronic Labeling**: Replacing traditional paper labels with electronic labels, promoting sustainability and reducing waste.
- **Multilingual Support**: Ensuring labels can be displayed in multiple languages to accommodate diverse consumers within the EU.
- **Regulatory Compliance**: Adhering to EU regulations regarding wine labeling, providing a platform that streamlines compliance for wineries.
	- [EU 2021/2117](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02021R2117-20211206) amending Regulations:
		- [EU 1308/2013](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02013R1308-20231208&qid=1701283989850) establishing a common organisation of the markets in agricultural products
		- [EU 1151/2012](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02012R1151-20220608&qid=1701284230571) on quality schemes for agricultural products and foodstuffs
		- [EU 251/2014](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02014R0251-20231208&qid=1701284265305) on the definition, description, presentation, labelling and the protection of geographical indications of aromatised wine products
		- [EU 228/2013](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02013R0228-20211207&qid=1701284298071) laying down specific measures for agriculture in the outermost regions of the Union
	- [EU 1169/2011](https://eur-lex.europa.eu/legal-content/EN/TXT/?uri=CELEX%3A02011R1169-20180101&qid=1701362311604) provision of food information to consumers
	- [EU 2019/934](https://eur-lex.europa.eu/search.html?scope=EURLEX&text=2019%2F934&lang=en&type=quick&qid=1701362513497) regards wine-growing areas where the alcoholic strength may be increased...

## How to Use

1. Scan the QR code on the wine bottle with a QR code scanner.
2. The E-label for the wine will be displayed on your device.
3. You can switch between languages using the language selection option on the E-label.

## Build & Run locally

To build the Open E-Label project using Visual Studio, follow these steps:

1. Clone this repository to a local folder
1. Open `ELabel.sln` solution with Visual Studio
1. Build and start the project

## Deployment

The web app can be deployed using the [published Docker image](https://hub.docker.com/r/fcarneiro/elabel). Here are the steps to run it with Docker compose:

1 Copy `docker-compose.yml` to your server. Change server details as needed:

```shell
scp docker-compose.yml user@server:~/elabel/
```

2 Start containers on server:

```shell
ssh user@server

cd elabel/
docker-compose pull
docker-compose up -d
```

3 View Logs:

```shell
docker-compose logs -f
```

## Contributing

Contributions are welcome! Please read our [contributing guidelines](CONTRIBUTING.md) before getting started.

## License

Open E-Label is open-source under the MIT license. See the [LICENSE file](LICENSE.txt) for more information.
